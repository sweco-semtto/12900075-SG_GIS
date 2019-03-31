/********************************************************
 * Karta.cs är huvudformulär i kartdelen.
 * 
 * 
 * LSAM, SWECO Position, våren 2006 & våren 2008
 ********************************************************/

using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace SGAB.SGAB_Karta
{
    public delegate void StatusStartplatsChangedFromMapEventHandler(object sender, StatusStartplatsChangedFromMapEventArgs e);

    public delegate void UnselectAllStartplatserEventHandler(object sender, EventArgs e);

    public delegate void SeletedStartplatserEventHandler(object sender, SelectedStartplatserEventArgs e);

    public delegate void ShowFormAdminEventHandler(object sender, EventArgs e);

    public partial class Karta : UserControl
    {
        private IconManager _IconManager;
        IList<string> _Selected;

        // Definierar en instans av de olika 'hjälpklasserna'
        private Kartverktyg _kartverktyg;
        private Urval _urval; 
        private Measure _measure;
        private GPSHandler _gpsHandler;
        private TGIS_GpsNmea _gps;

        private System.Windows.Forms.Label infoText;
        private System.Windows.Forms.Timer timer1;

        //?? Vad används denna till (används lås i ChangeGPSPosition)
        private bool _timer1Lock = false;
                
        //formulär som öppnas
        public frmInfo _frmInfo;
    
        //skapar en instans av Arguments som håller koll på olika knapptryckningar mm
        Arguments _arguments = Arguments.GetArguments();
        public Configuration Configuration
        {
            get;
            protected set;
        }                
        
        private TGIS_ControlPrintPreviewSimple GIS_ControlPrintPreviewSimple;
       
        // Offset       
        private const int WIDTH_DIV_SCALE = 2;
        private const int OFFSET_WIDTH_SCALE = 20;
        private const int OFFSET_HEIGHT_SCALE = 20;
        private const int OFFSET_HEIGHT_PANEL = 89; //57;
        private const int OFFSET_SPLITTER_LEGEND = 160;

        private int _offsetSplitterLegend = 160;

        /// <summary>
        /// Hämtar eller sätter GPS-spåraren. 
        /// </summary>
        public GPSTracking.ITrackerFromTatukGIS GpsTracker
        {
            get;
            set;
        }

        internal IconManager IconManager
        {
            get
            {
                return _IconManager;
            }
        }

        public SynchronizeTimer SynchronizeTimer
        {
            get;
            protected set;
        }

        protected Timer _ErrorTimer = new Timer();

        public event StatusStartplatsChangedFromMapEventHandler StatusStartplatsChangedFromMap;

        public event UnselectAllStartplatserEventHandler UnselectAllStartplatser;

        public event SeletedStartplatserEventHandler SeletedStartplatser;

        public event ShowFormAdminEventHandler ShowFormAdmin;

        internal TGIS_LayerVector startplatsLayer;

        protected FormNotes _FormNotes;

        protected bool _LoggedInAsAdmin;

        public bool LoggedInAsAdmin
        {
            get
            {
                return _LoggedInAsAdmin;
            }
            set
            {
                _LoggedInAsAdmin = value;
                btnAdmin.Visible = value;
                btnMapError.Visible = value;
            }
        }

        public string Username
        {
            get;
            set;
        }

        public int EntrepreneurId
        {
            get;
            set;
        }

        public Karta()
        {
            InitializeComponent();

            // Skapar det nya GPS objektet m.fl. 
            _gps = tgiS_GpsNmea1;
            _IconManager = new IconManager();
            _IconManager.InitIconManager();
            _Selected = new List<string>();

            this.Configuration = SGAB_Karta.Configuration.GetConfiguration();

            // Skapar meddelandefönstret
            _FormNotes = new FormNotes();
            _FormNotes.Hide();

            // Skapar synkroniseringen
            SynchronizeTimer = new SynchronizeTimer(Configuration.SynchronizationTime, this.LoggedInAsAdmin);
        }

        private void Karta_Load(object sender, EventArgs e)
        {
            try
            {
                // Skapar en instans av de olika 'hjälpklasserna'               
                _kartverktyg = new Kartverktyg(tgisKarta, toolStrip, this);
                _urval = new Urval(tgisKarta);                                         
                _measure = new Measure(tgisKarta);
                
                // Sätter storlek och splitdistans på panelen
                this.splitPanel.Size = new System.Drawing.Size(this.Width, this.Height - OFFSET_HEIGHT_PANEL);
                this.splitPanel.SplitterDistance = splitPanel.Width - OFFSET_SPLITTER_LEGEND;

                // Sätter utseende, längd och placering på skalstocken
                tgisScale.BorderStyle = BorderStyle.None;

                _gpsHandler = new GPSHandler(tgisKarta, _gps);
                _gpsHandler.GpsTracker = this.GpsTracker;
				_gpsHandler.GpsTracker.MapOrGPSError += new SGAB.GPSTracking.UserRegisterMapOrGPSErrorHandler(GpsTracker_MapOrGPSError);

				_ErrorTimer = new Timer();
                _ErrorTimer.Interval = 1000;
                _ErrorTimer.Enabled = true;
                _ErrorTimer.Tick += new System.EventHandler(OnErrorTimerEvent);

                // Laddar kartdata
                _kartverktyg.LaddaKartData();
                startplatsLayer = _kartverktyg.LaddaStartplatser();
                startplatsLayer.PaintShape += PaintShape;
                startplatsLayer.CachedPaint = true;

                StatusStartplatsChangedFromMap += new StatusStartplatsChangedFromMapEventHandler(Karta_StatusStartplatsChangedFromMap);

                StartGPS();

                // Förbereder för att kunna mäta och selektera
                _measure.PrepareMeasureLine();
                _measure.PrepareMeasureAreal();
                _urval.PrepareSelectPolygon();             
                
                // CS hårdkodas till SWEREF99 TM för att scalebar ska fungera
                foreach (var item in tgisKarta.Items)
                {
                    TGIS_LayerAbstract lyr = (TGIS_LayerAbstract)item;
                    lyr.SetCSByEPSG(3006);
                    lyr.IncrementalPaint = true;
                }


                // Startar en synkroniseringstimer
                SynchronizeTimer.Start();
            }
            catch (MapException mex)
            {
                infoText.Text = "Problem att ladda kartan: " + mex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage("MapException + " + mex.Message, Configuration.LogFilePath);
                }

            }
            catch (DataException dex)
            {
                infoText.Text = "Problem med databasen: " + dex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage("DataException + " + dex.Message, Configuration.LogFilePath);
                }
            }
            catch (GPSException gex)
            {
                infoText.Text = "Problem med GPSen: " + gex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage("GPSException_Karta_Load + " + gex.Message, Configuration.LogFilePath);
                }
            }
            catch (FormatException fex)
            {
                infoText.Text = "Kontrollera '.' och ',' i config-filen: " + fex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage("FormatException + " + fex.Message, Configuration.LogFilePath);
                }
            }
			catch (InvalidOperationException ioex)
			{

				infoText.Text = "Finns inga startplatser för årets säsong än. ";
				infoText.Visible = true;

				if (Configuration.LogExceptions)
				{
					Log.LogMessage("Finns inga startplatser för årets säsong än: InvalidOperationException + " + ioex.Message, Configuration.LogFilePath);
				}
			}
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
           
        }

        private void GpsTracker_MapOrGPSError(object sender, EventArgs e)
        {
            //lblMapOrGPSError.Text = "Fel registerat";
            lblMapOrGPSError.Visible = true;
            //lblMapOrGPSError.Refresh();
            _ErrorTimer.Start();
        }

        private void OnErrorTimerEvent(object sender, EventArgs e)
        {
            _ErrorTimer.Stop();
            lblMapOrGPSError.Visible = false;
            //lblMapOrGPSError.Text = "";
        }

        public bool Select(string id)
        {
            if (_Selected.Contains(id))
            {
                return false;
            }
            else
            {
                _Selected.Add(id);

                return true;
            }
        }

        /// <summary>
        /// Tar emot ett event om att startplatser har markerats i FormAdmin. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FormAdmin_SelectedStartplatser(object sender, SelectedStartplatserEventArgs e)
        {
            foreach (string StartplatsID in e.StartplatsIDs)
            {
                Select(StartplatsID);
            }

            tgisKarta.Update();
        }

        /// <summary>
        /// TYar emot ett event om att alla startplatser har avmarkerats i admingränssnittet. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FormAdmin_DeselectStartplatser(object sender, DeselectAllStartplatserFromAdminEventArgs e)
        {
            UnselectAll();
            tgisKarta.Update();
        }

        public void UnselectAll()
        {
            _Selected.Clear();
        }

        private void PaintShape(object _sender, TGIS_PaintShapeEventArgs _e)
        {
            TGIS_Shape shape = _e.Shape;
            shape.MakeEditable();
            string companyName = shape.GetField(ConfigurationManager.AppSettings["NamnFöretagsKolumn"]).ToString();
            string strStatus = shape.GetField(ConfigurationManager.AppSettings["NamnStatusKolumn"]).ToString();
            if (strStatus == "")
            {
                strStatus = "0";
            }
            int status = int.Parse(strStatus);
            shape.Params.Marker.Symbol = null;
            shape.Params.Marker.Symbol = _IconManager.GetIcon(companyName,
                                                              status,
                                                              _Selected.Contains(shape.GetField("Mi_prinx").ToString()));
            shape.Params.Marker.ShowLegend = false;
            shape.Params.Marker.Size = -30;
            shape.Draw();
        }

        public void FormAdmin_StatusStartplatsChanged(object sender, StatusStartplatsChangedFromAdminEventArgs e)
        {
            List<TGIS_Shape> selectedStartplatser = new List<TGIS_Shape>();
            var listOfStartplatser = startplatsLayer.Items;

            foreach (string startplatsID in e.StartplatsIDs)
            {
                foreach (object startplats in listOfStartplatser)
                {
                    TGIS_Shape shape = (startplats as TGIS_Shape);
                    if (startplatsID.Equals(shape.GetField("Mi_prinx")))
                    {
                        selectedStartplatser.Add(shape);
                        break;
                    }
                }
            }

            foreach (TGIS_Shape selectedStartplats in selectedStartplatser)
            {
                TGIS_Shape shp = selectedStartplats.MakeEditable();
                shp.SetField(ConfigurationManager.AppSettings["NamnStatusKolumn"].ToString(), e.Status);

                shp.IsSelected = false;
                shp.Invalidate(true);
            }
        }

        public void FormAdmin_SynchronizationFinished(object sender, SynchronizationFinishedEventArgs e)
        {
            _kartverktyg.ReFillStartplatser((this.startplatsLayer as StartplatsLayer), e.Foretag, e.Startplatser);

            // Ritar om alla symboler
            foreach (TGIS_Shape shape in startplatsLayer.Items)
            {
                // Sätter ett samma värde som finns för att tvinga grafiken att ritas om. 
                TGIS_Shape shp = shape.MakeEditable();
                string status = shp.GetField(ConfigurationManager.AppSettings["NamnStatusKolumn"]).ToString();
                shp.SetField(ConfigurationManager.AppSettings["NamnStatusKolumn"], status);

                shp.IsSelected = false;
                shp.Invalidate(true);
            }
		}

        private void btnUnselect_Click(object sender, EventArgs e)
        {
            UnselectAll();
            tgisKarta.Update();

            if (UnselectAllStartplatser != null)
                UnselectAllStartplatser(this, new EventArgs());
        }

        private void tgisKarta_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                //vid klick i kartan sker olika saker beroende på vilket verktyg som är valt
                //vid vänsterklick:
                if (e.Button == MouseButtons.Left)
                {
                    switch (_arguments.ButtonMode)
                    {
                        //längdmätning
                        case ButtonMode.MeasureLengthMode:
                            {
                                _measure.DrawMeasureLine(e.X, e.Y);

                                break;
                            }
                        //areamätning
                        case ButtonMode.MeasureAreaMode:
                            {
                                _measure.DrawMeasureAreal(e.X, e.Y);
                                break;
                            }
                        //info
                        case ButtonMode.InfoMode:
                            {
                                //hämtar ut en lista på de geometrier som fångas
                                ArrayList arrayList = _urval.SelectInfo(e.X, e.Y);

                                //om man vill kunna välja geometrier med en polygon och 
                                //och visa info för dessa görs det lämpligtvis genom att 
                                //först göra ett urval med mnuSelectPolygon och sedan 
                                //här hämta ArrayListen _urval._shapes. /LSAM

                                if (arrayList != null)
                                {
                                    /* added by me */
                                    bool newSelect = false;
                                    //visar info-formulär med attribut för varje geometri
                                    foreach (TGIS_Shape shp in arrayList)
                                    {
                                        if (shp.Layer.Name == Configuration.NamnBestallningsLager ||
                                            shp.Layer.Name == StartplatsLayer.StartplatsLayerName)
                                        {
                                            _frmInfo = new frmInfo();
                                            _frmInfo.mainForm = this;
                                            _frmInfo.Show();
                                            _frmInfo.VisaInfo(shp);
                                            if (Select(shp.GetField("Mi_prinx").ToString()))
                                            {
                                                newSelect = true;

                                                // Skickar event om att en nya startplats har markerats. 
                                                if (SeletedStartplatser != null)
                                                    SeletedStartplatser(this, new SelectedStartplatserEventArgs(_Selected));
                                            }
                                        }
                                    }
                                    if (newSelect)
                                    {
                                        tgisKarta.Update();
                                    }
                                }
                                break;
                            }

                        //selektering: polygon
                        case ButtonMode.SelectMultipleMode:
                            {
                                _urval.DrawSelectPolygon(e.X, e.Y);

                                break;
                            }
                        //selektering: polygon, för koppling av startplatser till entreprenörer
                        case ButtonMode.SelectMultipleToConnectMode:
                            {
                                _urval.DrawSelectPolygon(e.X, e.Y);

                                break;
                            }
                        //länk-öppning
                        case ButtonMode.HyperLinkMode:
                            {
                                //hämtar ut en lista på de geometrier som fångas
                                ArrayList arrayList = _urval.SelectInfo(e.X, e.Y);

                                if (arrayList != null)
                                {
                                    //öppnar länk för varje geometri
                                    foreach (TGIS_Shape shp in arrayList)
                                    {
                                        _kartverktyg.OpenExcel(shp);
                                    }
                                }

                                break;
                            }

                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    TGIS_LayerAbstract layerBestallning = startplatsLayer;
                    _urval._activeLayer = layerBestallning;
                    _urval.SelectSingle(e.X, e.Y);
                    if (_urval._shapes.Count > 0)
                    {
                        TGIS_Shape shape = (TGIS_Shape)_urval._shapes[0];

                        ContextMenu menue = new ContextMenu();
                        menue.MenuItems.Add(new MenuItem("Ej påbörjad", Menu_Click));
                        menue.MenuItems.Add(new MenuItem("Gödsel utkörd", Menu_Click));
                        menue.MenuItems.Add(new MenuItem("Färdiggödslat", Menu_Click));
                        menue.MenuItems.Add(new MenuItem("Säckar hämtade", Menu_Click));
                        menue.Show(this, new System.Drawing.Point(e.X, e.Y));
                    }
                }
            }
            catch (Exception ex)
            {              
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }
        
        void Menu_Click(object sender, EventArgs e)
        {
            MenuItem menueItem = (MenuItem)sender;
            TGIS_Shape shp = (TGIS_Shape)_urval._shapes[0];
            string namnStatusKolumn = ConfigurationManager.AppSettings["NamnStatusKolumn"];
            shp = shp.MakeEditable();
            shp.SetField(ConfigurationManager.AppSettings["NamnStatusKolumn"], menueItem.Index);
            if (shp.Layer.Name.Equals(ConfigurationManager.AppSettings["NamnBestallningsLager"]))
            {
                TGIS_LayerSHP shapeLayer = (TGIS_LayerSHP)tgisKarta.Get(ConfigurationManager.AppSettings["NamnBestallningsLager"]);
                shapeLayer.SaveData();
            }
            else
            {
                // Uppdatera grafiken igen. 
                shp.IsSelected = false;
                shp.Invalidate(true);

                _FormNotes.SetMenueItem(
                    menueItem.Text, 
                    shp.GetField("Ej påbörjad").ToString(),
                    shp.GetField("Gödsel utkörd").ToString(),
                    shp.GetField("Färdiggödslat").ToString(),
                    shp.GetField("Säckar hämtade").ToString());
                _FormNotes.ShowDialog();
                                
                // Skickar ett event om att en uppdatering har skett i kartan och detta skall sparas i databasen. 
                if (StatusStartplatsChangedFromMap != null)
                    StatusStartplatsChangedFromMap(this, new StatusStartplatsChangedFromMapEventArgs(shp, menueItem.Index, this.EntrepreneurId, _FormNotes.GetLatestText(), _FormNotes.Status));
            }
        }

        protected void Karta_StatusStartplatsChangedFromMap(object sender, StatusStartplatsChangedFromMapEventArgs e)
        {
            this._kartverktyg.Startplatser.UpdateStartplatsStatusFromMap(e.OrderId, e.IdAccess, e.StartplatsStatus, e.StartplatsId, this.EntrepreneurId.ToString(), e.Note, e.FieldStatus);
        }

        private void tgisKarta_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
          
            try
            {
                if (tgisKarta.IsEmpty)
					return;

				//visa mapHints
				TGIS_Point ptg = tgisKarta.ScreenToMap(new System.Drawing.Point(e.X, e.Y));

                //visa aktuell koordinat position
                lblX.Text = String.Format("{0:F0}", ptg.Y);
                lblY.Text = String.Format("{0:F0}", ptg.X);
                         
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
           
        }

        private void tgisKarta_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                switch (_arguments.ButtonMode)
                {
                    //längdmätning
                    case ButtonMode.MeasureLengthMode:
                        {
                            double lengthMeter = Convert.ToInt32(_measure.CalculateLine());
                            MessageBox.Show("Total längd: " + Math.Round((lengthMeter/1000), 1) + " km", "Mätresultat", MessageBoxButtons.OK);
                            _measure.NewMeasureLine();
                            break;
                        }
                    //areamätning
                    case ButtonMode.MeasureAreaMode:
                        {
                            string length = _measure.CalculateArea();
                            MessageBox.Show("Total areal: " + length + " Ha", "Mätresultat", MessageBoxButtons.OK);
                            _measure.NewMeasureAreal();
                            break;
                        }

                    //urval: polygon
                    case ButtonMode.SelectMultipleMode:
                        {
                            KeyValuePair<List<TGIS_Shape>, double> ans = _urval.SelectMultiple();
                            double canSum = ans.Value;
                            int antalValda = _urval._shapes.Count;

                            if (antalValda == 0)
                            {
                                MessageBox.Show("Inga startplatser har valts. Kontrollera att det är lagret med startplatser som är valt i legenden.", "Ingen träff", MessageBoxButtons.OK);
                            }
                            else
                            {
                                MessageBox.Show("Totalt är " + antalValda + " startplatser valda. De innehåller tillsammans " + canSum + " ton.", "Summa gödsel", MessageBoxButtons.OK);
                            }                           
                            _urval.NewSelectPolygon();
                            _urval.Avselektera();

                            // Markerar startplatserna i FormAdmin
                            foreach (TGIS_Shape shp in ans.Key)
                            {
                                string id = shp.GetField("Mi_prinx").ToString();
                                this.Select(id);
                            }

                            // Skickar event om att en nya startplats har markerats. 
                            if (SeletedStartplatser != null)
                                SeletedStartplatser(this, new SelectedStartplatserEventArgs(_Selected));

                            break;
                        }

                    //urval: polygon, öppnar dialog för koppling av startplatser
                    //case ButtonMode.SelectMultipleToConnectMode:
                    //    {
                    //        double canSum = _urval.SelectMultiple();
                    //        int antalValda = _urval._shapes.Count;

                    //        if (antalValda == 0)
                    //        {
                    //            MessageBox.Show("Inga startplatser har valts. Kontrollera att det är lagret med startplatser som är valt i legenden.", "Ingen träff", MessageBoxButtons.OK);
                    //        }
                    //        else
                    //        {
                    //            FormAdmin formAdmin = new FormAdmin();                                
                    //            MessageBox.Show("Koppling av startplatser.");   
                    //        }
                    //        _urval.NewSelectPolygon();
                    //        _urval.Avselektera();
                    //        break;
                    //    }              
                }
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        private void tgisKarta_AfterPaint(object sender, PaintEventArgs e)
        {
            try
            {
                //om skalan har ändrats
                if (txtSkala.Text != tgisKarta.ScaleAsText.Substring(2))
                {
                    //beräknar ny storlek på hektarrutan
                    TGIS_Extent ext = new TGIS_Extent();
                    ext.XMax = 100;
                    ext.XMin = 0;
                    ext.YMax = 100;
                    ext.YMin = 0;

                    Rectangle rect = tgisKarta.MapToScreenRect(ext);

                }

                //skriver ut skalan, tar bort '1:'
                txtSkala.Text = tgisKarta.ScaleAsText.Substring(2);

            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        private void tgisKarta_Resize(object sender, EventArgs e)
        {
            try
            {              

                //justerar storlek och läge på skalstocken
                tgisScale.Width = tgisKarta.Width / WIDTH_DIV_SCALE;
                tgisScale.Location = new System.Drawing.Point(tgisKarta.Location.X + tgisKarta.Width - tgisScale.Width - OFFSET_WIDTH_SCALE, tgisKarta.Location.Y + tgisKarta.Height - OFFSET_HEIGHT_SCALE);

                //justerar storlek och läge på bla skal- och koordinatrutorna
                groupBoxLabels.Size = new Size(tgisKarta.Width, 48);
                groupBoxLabels.Location = new System.Drawing.Point(0, this.Height - 43);

                //justerar storlek på verktygsraden
                toolStrip.Size = new System.Drawing.Size(tgisKarta.Width, 48);

                tgisScale.Refresh();
  
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

             
//Det som sker vid klick i verktygsraden
#region KnappMenyVal
       
        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            // visar hela kartan        
            _kartverktyg.ZoomaFullt();
            tgisScale.Refresh();

        }

        private void btnZoomBox_Click(object sender, EventArgs e)
        {
             // sätt zoomläge
            _kartverktyg.ZoomaBox();

        }

        private void btnPan_Click(object sender, EventArgs e)
        {
            //sätter panoreringsläge
            _kartverktyg.Panorera();
        }

        private void btnSelectMultiple_Click(object sender, EventArgs e)
        {
            //selekterar
            _kartverktyg.Selektera(ButtonMode.SelectMultipleMode);

            //_kartverktyg.Selektera(ButtonMode.SelectMultipleToConnectMode);           
            
        }
      
        private void mnuLangd_Click(object sender, EventArgs e)
        {
            _kartverktyg.Measure(ButtonMode.MeasureLengthMode);
        }

        private void mnuAreal_Click(object sender, EventArgs e)
        {
            _kartverktyg.Measure(ButtonMode.MeasureAreaMode);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            _kartverktyg.Info();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _kartverktyg.AddLokaltData();
            _kartverktyg.SaveProjectFile();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ExportMapToImage();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintMap();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveLayer();
        }

        private void btnLegend_Click(object sender, EventArgs e)
        {
            ShowLegend();
        }

        private void btnScaleBar_Click(object sender, EventArgs e)
        {
            ShowSkalstock();
        }

        private void btnLayerExtent_Click(object sender, EventArgs e)
        {
            ZoomToLayer();
        }

        private void btnHyperLink_Click(object sender, EventArgs e)
        {
            _kartverktyg.HyperLink();
        }

        private void btnGPScenter_Click(object sender, EventArgs e)
        {
            _kartverktyg.ChangePanCenter();
        }

        void tgisKarta_LayerAdd(object _sender, TGIS_LayerEventArgs _e)
        {
            TGIS_LayerVector shapeLayer = (TGIS_LayerVector)tgisKarta.Get("Bestallning_Tatuk");
            TGIS_LayerVector gpsLayer = (TGIS_LayerVector)tgisKarta.Get("GPSlager");

            if (shapeLayer != null && gpsLayer != null)
            {
                int i = shapeLayer.ZOrder;
                int j = gpsLayer.ZOrder;
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (ShowFormAdmin != null)
                ShowFormAdmin(this, new EventArgs());
        }
        
      
#endregion

//De publika metoder som anropas från huvudapplikationen
#region Publika metoder

        /// <summary>
        /// Sätter kartkomponentens storlek
        /// </summary>
        /// <param name="width">önskad bredd</param>
        /// <param name="height">önskad höjd</param>
        public void SetComponentSize(int width, int height)
        {
            try
            {
                _offsetSplitterLegend = splitPanel.Width - splitPanel.SplitterDistance;
                this.Width = width;
                this.Height = height;
                this.splitPanel.Size = new System.Drawing.Size(this.Width, this.Height - OFFSET_HEIGHT_PANEL);

                if ((splitPanel.Width - _offsetSplitterLegend) > 0)
                {
                    this.splitPanel.SplitterDistance = splitPanel.Width - _offsetSplitterLegend;
                }
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// visar/döljer legenden
        /// </summary>
        /// <param name="isVisible">anger om legenden ska vara synlig eller inte</param>
        public void ShowLegend()
        {
            try
            {
                if (tgisLegend.Visible)
                {
                    tgisLegend.Visible = false;
                    splitPanel.Panel2Collapsed = true;
                                       
                }
                else
                {
                    tgisLegend.Visible = true;
                    splitPanel.Panel2Collapsed = false;
                }
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// visar/döljer skalstocken
        /// </summary>
        /// <param name="isVisible">anger om skalstocken ska vara synlig eller inte</param>
        public void ShowSkalstock()
        {
            try
            {
                if (tgisScale.Visible)
                {
                    tgisScale.Visible = false;
                }
                else
                {
                    tgisScale.Visible = true;
                }
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }
             

#endregion

     
               
        /// <summary>
        /// kontrollerar vad som trycks när man är i textrutan för skala
        /// om det är 'Enter' uppdateras kartskalan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSkala_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    tgisKarta.ScaleAsText = "1:" + txtSkala.Text;
                }
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }
       
        /// <summary>
        /// utlöses när man ändrar aktivt lager i legenden
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void ChangeActiveLayer(object _sender, TGIS_LayerEventArgs _e)
        {
            try
            {
                _urval._activeLayer = _e.Layer;
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        //startar GPSen
        public void StartGPS()
        {
            try
            {
                _gpsHandler.CreateGPSLayerRT90();

                _gps.Com = Configuration.GPSPort;
                _gps.BaudRate = Configuration.GPSBaudRate;
                _gps.Active = true;

                timer1.Interval = Configuration.GPSIntervall;
                timer1.Start();               
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Stannar GPSen
        /// </summary>
        public void StopGPS()
        {
            try
            {
                timer1.Enabled = false;
                _gps.Active = false;
            }
            catch (Exception ex)
            {
             
                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            try
            {
                if (_timer1Lock == false)
                {
                    try
                    {
                        _timer1Lock = true;
					
						//Uppdaterar GPS-positionen                
						_gpsHandler.ChangeGPSPosition(infoText, _kartverktyg._pan);
                    }

                    finally
                    {
                        _timer1Lock = false;
                    }
                }
            }
            catch (GPSException gex)
            {
                infoText.Text = gex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage("GPSException_timer1_Tick + " + gex.Message, Configuration.LogFilePath);
                }
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage("Exception_timer1_Tick + " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }
            }

        }

        /// <summary>
        /// Tar bort ett lager från kartan och legenden
        /// </summary>
        public void RemoveLayer()
        {
            try
            {
                FormCloseLayer closeLayerForm = new FormCloseLayer(_kartverktyg.Layers, tgisKarta);
                closeLayerForm.ShowDialog();
                _kartverktyg.SaveProjectFile();

                // Justerar vilka knappar som ska vara intryckta
                _kartverktyg.ChangeTool("btnRemove");

                tgisLegend.Refresh();
                tgisKarta.Update();
            }
           catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }
                              
                ExceptionHandler.HandleException(ex);
                MessageBox.Show("Något gick fel", "Fel vid borttagandet av fil.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        /// <summary>
        /// Zoomar till aktuellt lager
        /// </summary>
        public void ZoomToLayer()
        {
            try
            {
                TGIS_LayerAbstract layer = tgisLegend.GIS_Layer;

                if (layer == null)
                {
                    MessageBox.Show("Inget lager är valt. Markera ett lager.", "Inget lager är valt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                tgisKarta.VisibleExtent = layer.Extent;

                // Justerar vilka knappar som ska vara intryckta
                _kartverktyg.ChangeTool("btnLayerExtent");

            }
           catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
                MessageBox.Show("Något gick fel", "Inget aktivt lager.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// Sparar ner aktuellt kartutsnitt till en bildfil
        /// </summary>
        public void ExportMapToImage()
        {
            try
            {
                if (dlgSaveImage.ShowDialog() == DialogResult.OK)
                {
                    tgisKarta.ExportToImage(dlgSaveImage.FileName, tgisKarta.VisibleExtent);
                }
                
                //justerar vilka knappar som ska vara intryckta
                _kartverktyg.ChangeTool("btnsave");
            }
            catch (Exception ex)
            {
                infoText.Text = ex.Message;
                infoText.Visible = true;

                if (Configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message + " " + ex.StackTrace, Configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }


        /// <summary>
        /// Skriver ut aktuellt kartutsnitt
        /// </summary>
        public void PrintMap()
        {

            TGIS_TemplatePrint tmp;

            tmp = new TGIS_TemplatePrint(tgisKarta);
            try
            {
                //tmp.Path = TGIS_Utils.GisSamplesDataDir() + "printtemplate.tpl";
                tmp.TemplatePath = Configuration.WorkPath + "printtemplate.tpl";
                //tmp.set_GIS_Legend(1, GIS_ControlLegend);
                tmp.set_GIS_Scale(1, tgisScale);
                tmp.set_GIS_ViewerExtent(1, tgisKarta.VisibleExtent);
                //tmp.set_Text( 1, "Title" ) ;
                //tmp.set_Text( 2, "Copyright" ) ;
                GIS_ControlPrintPreviewSimple.Preview();
            }
            finally
            {
                tmp = null;

            }

           _kartverktyg.ChangeTool("btnPrint");
        }


        void tgisLegend_LayerActiveChange(object _sender, TGIS_LayerEventArgs _e)
        {
            _kartverktyg.SaveProjectFile();
        }

        void tgisLegend_LayerParamsChange(object _sender, TGIS_LayerEventArgs _e)
        {
            _kartverktyg.SaveProjectFile();
        }

        void tgisLegend_OrderChange(object sender, System.EventArgs e)
        {
            _kartverktyg.SaveProjectFile();
        }

        private void btnUpdateStatusInfo_Click(object sender, EventArgs e)
        {
            FormUpdateStatusInfo formUpdateStatusInfo = new FormUpdateStatusInfo();
            formUpdateStatusInfo.ShowDialog();         
        }

        private void btnMapError_Click(object sender, EventArgs e)
        {
            GpsTracker.UserRegisterMapOrGPSError();
        }
    }
}
