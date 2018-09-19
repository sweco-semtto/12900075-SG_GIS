/********************************************************
 * Kartverktyg.cs används för att hantera händelser
 * knutna till kartan
 * 
 * Laddar kartdata 
 * Hanterar funktionen bakom vissa av knapparna
 * Hanterar vilken knapp som är intryckt
 * Ritar ut labels med rätt utseende
 * 
 * LSAM, SWECO Position, våren 2006
 ********************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using SGAB.SGAB_Database;


namespace SGAB.SGAB_Karta
{

    [Serializable]
    public struct ShellExecuteInfo
    {
        public int Size;
        public uint Mask;
        public IntPtr hwnd;
        public string Verb;
        public string File;
        public string Parameters;
        public string Directory;
        public uint Show;
        public IntPtr InstApp;
        public IntPtr IDList;
        public string Class;
        public IntPtr hkeyClass;
        public uint HotKey;
        public IntPtr Icon;
        public IntPtr Monitor;
    }

    /// <summary>
    /// Hanterar händelser knutna till kartan
    /// </summary>
    public class Kartverktyg
    {
        // Kod för OpenWithDialog Box
        [DllImport("shell32.dll", SetLastError = true)]
        extern public static bool
            ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);
        public const uint SW_NORMAL = 1;

        Configuration _configuration = Configuration.GetConfiguration();

        //Referens till formulärets GIS/kart-komponent
        TatukGIS.NDK.WinForms.TGIS_ViewerWnd _tgisKarta;

        //Referens till formulärets verktygsfält
        ToolStrip _toolStrip;

        //Referens till formuläret
        Karta _karta;
        
        //skapar en instans av Arguments som håller koll på olika knapptryckningar mm
        Arguments _arguments = Arguments.GetArguments();

        //variabel som håller koll på vilket verktyg som varit aktuellt innan det nu aktuella
        //btnZoomBox är default
        string _lastVerktyg = "btnZoomBox";

        //inställningar för att öppna lokalt data
        int _index = 1;

        public bool _pan = false;

        // håller en lista över öppnade lager vilka är vara möjliga för användaren att stänga
        public IList<TGIS_LayerAbstract> _Layers;

        public IList<TGIS_LayerAbstract> Layers
        {
            get
            {
                return _Layers;
            }
        }

        /// <summary>
        /// Sparar undan de hämtade startplatserna så vi kan ändra status på dem ifrån kartan. 
        /// </summary>
        internal Startplats Startplatser
        {
            get;
            set;
        }

        public Kartverktyg(TatukGIS.NDK.WinForms.TGIS_ViewerWnd tgisKarta, ToolStrip toolStrip, Karta karta)
        {
            _karta = karta;
            _tgisKarta = tgisKarta;
            _toolStrip = toolStrip;
            _Layers = new List<TGIS_LayerAbstract>();
            tgisKarta.LayerAdd += new TGIS_LayerEvent(tgisKarta_LayerAdd);
            tgisKarta.LayerDelete += new TGIS_LayerEvent(tgisKarta_LayerDelete);
        }

        // triggas då ett lager tas bort från kartkontrollen
        void tgisKarta_LayerDelete(object _sender, TGIS_LayerEventArgs _e)
        {
            _Layers.Remove(_e.Layer);
        }
        // triggas då ett nytt lager läggs till kartkontrollen
        void tgisKarta_LayerAdd(object _sender, TGIS_LayerEventArgs _e)
        {
            string layerName = _e.Layer.Name;
            //Lägg bara till de lager som kan öppnas/stängas av användaren
            if (!(layerName == "GPSlager" ||
                layerName == "Startplatser" ||
                layerName == "MeasureLine" ||
                layerName == "MeasurePolygon" ||
                layerName == "Urvalslager" ||
                layerName == ConfigurationManager.AppSettings["NamnBestallningsLager"]))
            {
                _Layers.Add(_e.Layer);
            }
        }

        /// <summary>
        /// Läser in kartdata
        /// </summary>
        public void LaddaKartData()
        {
            try
            {
                
                //Sätter kart-Mode, finns inbyggda Mode i Tatuk 
                _tgisKarta.Mode = TGIS_ViewerMode.gisZoom;

                //justerar vilka knappar som ska vara intryckta
                ChangeTool("btnZoomBox");

                /* added by me */
                string projectFilePath = _configuration.TatukProjectFilePath;
                //projectFilePath = "C:\\TEST\\Kartdata\\SGAB.TTKGP";

                if (System.IO.File.Exists(projectFilePath))
                {
                    try
                    {      
                        //öppnar projektfilen (TTKGP-fil)
                        _tgisKarta.Open(projectFilePath, true);
                        _tgisKarta.SetCSByEPSG(3006); //CS hårdkodas till SWEREF99 TM
                    }
                    catch (Exception e)
                    {
                        string error = e.Message;

                        //ingenting sker om ett lager i projektfilen inte kan läsas
                    }

                    //använder det extent som är angivet i config-filen
                    string[] extent = _configuration.VisibleExtent.Split(',');

                    TGIS_Extent visibleExtent = new TGIS_Extent();

                    visibleExtent.XMin = Convert.ToDouble(extent[0]);
                    visibleExtent.XMax = Convert.ToDouble(extent[1]);
                    visibleExtent.YMin = Convert.ToDouble(extent[2]);
                    visibleExtent.YMax = Convert.ToDouble(extent[3]);                   
               
                    //sätter ny synlig utbredning för kartan
                    _tgisKarta.VisibleExtent = visibleExtent;
                                       
                }
                else
                {
                    throw new Exception("Ogiltig sökväg till kartprojektfilen");
                }
                               
            }
            catch (Exception ex)
            {
              
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }

            //TGIS_LayerSHP layerBestallning = (TGIS_LayerSHP)_tgisKarta.Get(_configuration.NamnBestallningsLager);
            //layerBestallning.CachedPaint = true;
        }

        /// <summary>
        /// Laddar startplatser ifrån MySql. 
        /// </summary>
        /// <returns></returns>
        public TGIS_LayerVector LaddaStartplatser()
        {
            // Tar bort det gamla beställningslagret ifrån kartan.
            TGIS_LayerAbstract removeOldOrderLayer;
            foreach (TGIS_LayerAbstract layer in _tgisKarta.Items)
                if (layer.Name.Equals(_configuration.NamnBestallningsLager))
                {
                    removeOldOrderLayer = layer;
                    _tgisKarta.Items.Remove(removeOldOrderLayer);
                    break;
                }            

            // Skapar ett nytt lager med alla startplatser i. 
            StartplatsLayer startplatsLayer = new StartplatsLayer(_tgisKarta.CS, _karta.IconManager, _configuration.WorkPath);
            _tgisKarta.Add(startplatsLayer);

            Foretag foretag = new Foretag();
            Startplatser = new Startplats();

            // Kollar om vi är i testläge, då skall data hämtas från testdatabasen istället. 
            if (Configuration.GetConfiguration().IsInTestMode)
            {
                foretag = new Foretag(true);
                Startplatser = new Startplats(true);
            }

            if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
            {
                // Hämtar data ifrån MySql. 
                DataTable foretagMySqlTable = foretag.GetAllFromMySql();
                DataTable startplatsMySqlTable = Startplatser.GetAllFromMySql();

                // Kontrollerar så att vi inte har fått t.ex. ett proxyfel, d.v.s. ingen datatabell är null. 
                if (foretagMySqlTable != null && startplatsMySqlTable != null)
                    return ReFillStartplatser(startplatsLayer, foretagMySqlTable, startplatsMySqlTable);
            }
            
            // Läser ifrån xml.
            DataTable foretagXML = new DataTable();
            foretagXML.ReadXml(StartplatsLayer.LoggFolder + "Foretag.xml");
            DataTable startplatserXML = new DataTable();
            startplatserXML.ReadXml(StartplatsLayer.LoggFolder + "Startplatser.xml");

            // Kontrollerar om data i xml är tillräcklig färsk, om inte returnerar ett tom
            // Startplatslager. 
            if (!foretag.CheckIfXMLIsUpToDate(foretagXML))
                return startplatsLayer;

            return ReFillStartplatser(startplatsLayer, foretagXML, startplatserXML);

            //if (!_karta.LoggedInAsAdmin)
            //{
            //    // TODO Göra något
            //}

            //return null;
        }

        public TGIS_LayerVector ReFillStartplatser(StartplatsLayer startplatsLayer, DataTable foretagMySqlTable, 
            DataTable startplatsMySqlTable)
        {
            if (!_karta.LoggedInAsAdmin)
            {
                // Skapar alla symbolerna för startplatserna, men endast de som är kopplade till entreprenören. 
                startplatsLayer.AddStartplatserFromDataTables(foretagMySqlTable, startplatsMySqlTable, _karta.EntrepreneurId);
            }
            else
            {
                // Skapar alla symbolerna för startplatserna. 
                startplatsLayer.AddStartplatserFromDataTables(foretagMySqlTable, startplatsMySqlTable);
            }

            return startplatsLayer;
        }

        // en lista över valda startplatser i kartan
        IList<string> selected = new List<string>();

        /// <summary>
        /// Lägger till en startplats till listan över valda startplatser
        /// </summary>
        public bool Add(string id)
        {
            if (selected.Contains(id))
            {
                return false;
            }
            else
            {
                selected.Add(id);
                return true;
            }
        }

        /// <summary>
        /// Tar bort en startplats till listan över valda startplatser
        /// </summary>
        public void Remove()
        {
            selected.Clear();
        }

        /// <summary>
        /// Zoomar ut så hela kartan syns
        /// </summary>
        public void ZoomaFullt()
        {
            try
            {
                //justerar vilka knappar som ska vara intryckta
                ChangeTool("btnFullExtent");

                //använder det extent som är angivet i config-filen
                string[] extent = _configuration.MaxExtent.Split(',');

                TGIS_Extent maxExtent = new TGIS_Extent();

                maxExtent.XMin = Convert.ToDouble(extent[0]);
                maxExtent.XMax = Convert.ToDouble(extent[1]);
                maxExtent.YMin = Convert.ToDouble(extent[2]);
                maxExtent.YMax = Convert.ToDouble(extent[3]);

                //sätter ny synlig utbredning för kartan
                _tgisKarta.VisibleExtent = maxExtent;
            }
            catch (Exception ex)
            {

                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }

        }


        /// <summary>
        /// Zoomar in eller ut beroende på åt vilket håll man drar rektangeln
        /// </summary>
        public void ZoomaBox()
        {
            try
            {
                //sätter zoomläge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisZoom;

                //sätter zoomläge för att veta vad som ska ske vid klick i karta
                _arguments.ButtonMode = ButtonMode.ZoomBoxMode;

                //justerar vilka knappar som ska vara intryckta
                ChangeTool("btnZoomBox");
            }
            catch (Exception ex)
            {
               
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Panorerar
        /// </summary>
        public void Panorera()
        {
            try
            {
                //sätter panoreringsläge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisDrag;

                //sätter panoreringsläge för att veta vad som ska ske vid klick i karta
                _arguments.ButtonMode = ButtonMode.PanMode;

                //justerar vilka knappar som ska vara intryckta
                ChangeTool("btnPan");
            }
            catch (Exception ex)
            {
               
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Anger att det är selekteringsläge
        /// </summary>
        /// <param name="buttonMode">typ av selektering</param>
        public void Selektera(ButtonMode buttonMode)
        {
            try
            {
                //sätter selekteringsläge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisSelect;

                //sätter typ av selekteringsläge för att veta vad 
                //som ska ske vid klick i karta
                _arguments.ButtonMode = buttonMode;

                //hämtar aktuellt lager och flyttar det överst i lagerlistan
                //detta görs för att CashedPaint = false ska slå igenom.
                //Genom att sätta CashedPaint = false så kan man senare
                //sätta 'shp'.Invalidate(false); för att bara uppdatera denna shp

                TGIS_LayerAbstract layer = _tgisKarta.Get("Urvalslager");

                int zorder = layer.ZOrder;
                layer.Move(-zorder);
                layer.CachedPaint = false;   

                //justerar vilka knappar som ska vara intryckta
                ChangeTool("btnSelectMultiple");

            }
            catch (Exception ex)
            {
             
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }


        /// <summary>
        /// Anger att det är infoläge
        /// </summary>
        public void Info()
        {
            try
            {
                //sätter selekteringsläge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisSelect;

                //sätter infoläge för att veta vad som ska 
                //ske vid klick i karta
                _arguments.ButtonMode = ButtonMode.InfoMode;

                //justerar vilka knappar som ska vara intryckta
                ChangeTool("btnInfo");

            }
            catch (Exception ex)
            {
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Anger att det är hyperlink-läge
        /// </summary>
        public void HyperLink()
        {
            try
            {
                //sätter selekteringsläge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisSelect;

                //sätter infoläge för att veta vad som ska 
                //ske vid klick i karta
                _arguments.ButtonMode = ButtonMode.HyperLinkMode;

                //justerar vilka knappar som ska vara intryckta
                ChangeTool("btnHyperLink");

            }
            catch (Exception ex)
            {
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// Anger att det är mätnings- eller bäringsläge
        /// </summary>
        /// <param name="buttonMode">typ av mätning eller bäring</param>
        public void Measure(ButtonMode buttonMode)
        {
            try
            {
                //sätter editeringsläge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisEdit;

                //sätter typ av mätningsläge för att veta vad 
                //som ska ske vid klick i karta
                _arguments.ButtonMode = buttonMode;

                //hämtar aktuellt lager och flyttar det överst i lagerlistan
                //detta görs för att CashedPaint = false ska slå igenom.
                //Genom att sätta CashedPaint = false så kan man senare
                //sätta 'shp'.Invalidate(false); för att bara uppdatera denna shp
                TGIS_LayerAbstract layer = null;
               
                if (buttonMode == ButtonMode.MeasureLengthMode)
                {
                    layer = _tgisKarta.Get("MeasureLine");
                }
                else if (buttonMode == ButtonMode.MeasureAreaMode)
                {
                    layer = _tgisKarta.Get("MeasurePolygon");
                }

                int zorder = layer.ZOrder;
                layer.Move(-zorder);
                layer.CachedPaint = false;   
                
                //justerar vilka knappar som ska vara intryckta
                ChangeTool("btnMeasure");

            }
            catch (Exception ex)
            {
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }

        }


        /// <summary>
        /// justerar så att rätt knappar är intryckta i verktygsraden
        /// </summary>
        /// <param name="verktyg">string med namnet på knappen i verktygsraden som ska tryckas ner</param>
        public void ChangeTool(string verktyg)
        {
            ToolStripButton tsButton;
            ToolStripDropDownButton tsDDButton;
            try
            {
                foreach (ToolStripItem tsItem in _toolStrip.Items)
                {

                    try
                    {
                        if ((tsItem as ToolStripButton) != null)
                            ((ToolStripButton)tsItem).Checked = false;

                    }
                    catch
                    {   //inget problem om det är fel typ av knapp, testar nästa typ
                        try
                        {
                            ((ToolStripDropDownButton)tsItem).BackgroundImage = null;
                        }
                        catch
                        {
                            //inget problem om det är fel typ av knapp
                        }
                    }
                }

                switch (verktyg)
                {
                    case "btnFullExtent":
                        try
                        {
                            tsButton = (ToolStripButton)_toolStrip.Items[_lastVerktyg];
                            tsButton.Checked = true;
                        }
                        catch
                        {
                            tsDDButton = (ToolStripDropDownButton)_toolStrip.Items[_lastVerktyg];
                            tsDDButton.BackgroundImage = ((System.Drawing.Image)(SGAB.SGAB_Karta.Properties.Resources.ResourceManager.GetObject("Vald")));
                        }
                        verktyg = _lastVerktyg;
                        break;

                    case "btnLayerExtent":
                        try
                        {
                            tsButton = (ToolStripButton)_toolStrip.Items[_lastVerktyg];
                            tsButton.Checked = true;
                        }
                        catch
                        {
                            tsDDButton = (ToolStripDropDownButton)_toolStrip.Items[_lastVerktyg];
                            tsDDButton.BackgroundImage = ((System.Drawing.Image)(SGAB.SGAB_Karta.Properties.Resources.ResourceManager.GetObject("Vald")));
                        }
                        verktyg = _lastVerktyg;
                        break;
                    
                    case "btnZoomBox":
                        tsButton = (ToolStripButton)_toolStrip.Items["btnZoomBox"];
                        tsButton.Checked = true;
                        break;

                    case "btnPan":
                        tsButton = (ToolStripButton)_toolStrip.Items["btnPan"];
                        tsButton.Checked = true;
                        break;

                    case "btnMeasure":
                        try
                        {
                            tsDDButton = (ToolStripDropDownButton)_toolStrip.Items["btnMeasure"];
                            tsDDButton.BackgroundImage = ((System.Drawing.Image)(SGAB.SGAB_Karta.Properties.Resources.ResourceManager.GetObject("Vald")));
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    case "btnSelectMultiple":
                        tsButton = (ToolStripButton)_toolStrip.Items["btnSelectMultiple"];
                        tsButton.Checked = true;
                        break;
                      
                    case "btnInfo":
                        tsButton = (ToolStripButton)_toolStrip.Items["btnInfo"];
                        tsButton.Checked = true;
                        break;

                    case "btnHyperLink":
                        tsButton = (ToolStripButton)_toolStrip.Items["btnHyperLink"];
                        tsButton.Checked = true;
                        break;

                    case "btnSave":
                        try
                        {
                            tsButton = (ToolStripButton)_toolStrip.Items[_lastVerktyg];
                            tsButton.Checked = true;
                        }
                        catch
                        {
                            tsDDButton = (ToolStripDropDownButton)_toolStrip.Items[_lastVerktyg];
                            tsDDButton.BackgroundImage = ((System.Drawing.Image)(SGAB.SGAB_Karta.Properties.Resources.ResourceManager.GetObject("Vald")));
                        }
                        verktyg = _lastVerktyg;
                        break;

                    case "btnPrint":
                        try
                        {
                            tsButton = (ToolStripButton)_toolStrip.Items[_lastVerktyg];
                            tsButton.Checked = true;
                        }
                        catch
                        {
                            tsDDButton = (ToolStripDropDownButton)_toolStrip.Items[_lastVerktyg];
                            tsDDButton.BackgroundImage = ((System.Drawing.Image)(SGAB.SGAB_Karta.Properties.Resources.ResourceManager.GetObject("Vald")));
                        }
                        verktyg = _lastVerktyg;
                        break;

                    case "btnAdd":
                        try
                        {
                            tsButton = (ToolStripButton)_toolStrip.Items[_lastVerktyg];
                            tsButton.Checked = true;
                        }
                        catch
                        {
                            tsDDButton = (ToolStripDropDownButton)_toolStrip.Items[_lastVerktyg];
                            tsDDButton.BackgroundImage = ((System.Drawing.Image)(SGAB.SGAB_Karta.Properties.Resources.ResourceManager.GetObject("Vald")));
                        }
                        verktyg = _lastVerktyg;
                        break;

                    case "btnRemove":
                        try
                        {
                            tsButton = (ToolStripButton)_toolStrip.Items[_lastVerktyg];
                            tsButton.Checked = true;
                        }
                        catch
                        {
                            tsDDButton = (ToolStripDropDownButton)_toolStrip.Items[_lastVerktyg];
                            tsDDButton.BackgroundImage = ((System.Drawing.Image)(SGAB.SGAB_Karta.Properties.Resources.ResourceManager.GetObject("Vald")));
                        }
                        verktyg = _lastVerktyg;
                        break;
                   
                }

                _lastVerktyg = verktyg;


            }
            catch (Exception ex)
            {

                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// skiftar mellan panoreringsläge och GPS-centrering
        /// </summary>
        public void ChangePanCenter()
        {

            if (_pan == false)
            {
                _tgisKarta.Mode = TGIS_ViewerMode.gisDrag;
                _pan = true;

            }
            else
            {
                _tgisKarta.Mode = TGIS_ViewerMode.gisSelect;
                _pan = false;
               
            }

            //justerar vilka knappar som ska vara intryckta
            ChangeTool("btnPan");

        }

        public void OpenExcel(TGIS_Shape shp)
        {
            try
            {
                //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                //excelApp.Visible = true;
                string company = shp.GetField(ConfigurationManager.AppSettings["NamnFöretagsKolumn"]).ToString();
                string region = shp.GetField(ConfigurationManager.AppSettings["NamnRegionKolumn"]).ToString();
                string distrikt = shp.GetField(ConfigurationManager.AppSettings["NamnDistriktKolumn"]).ToString();
                string ordernr = shp.GetField(ConfigurationManager.AppSettings["NamnOrderNrKolumn"]).ToString();
                string workbookPath = _configuration.ExcelFilePath + company + "_" +
                    region + "_" + distrikt + "_" + ordernr + ".xls";

                //startar en ny process
                try
                {
                    System.Diagnostics.Process.Start(workbookPath);
                }
                catch
                {
                    ShellExecuteInfo sei = new ShellExecuteInfo();
                    sei.Size = Marshal.SizeOf(sei);
                    sei.Verb = "openas";
                    sei.File = workbookPath;
                    sei.Show = SW_NORMAL;
                    if (!ShellExecuteEx(ref sei))
                        throw new System.ComponentModel.Win32Exception();
                }

                //Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                //    0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                //    true, false, 0, true, false, false);


            }
            catch (Exception ex)
            {

                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        public void SaveProjectFile()
        {
            try
            {
                TGIS_LayerSHP layerBestallning = (TGIS_LayerSHP)_tgisKarta.Get(ConfigurationManager.AppSettings["NamnBestallningsLager"]);
                if (layerBestallning != null)
                {
                    int order = layerBestallning.ZOrder;
                    layerBestallning.Move(-(order - 1));
                    order = layerBestallning.ZOrder;
                }

                string projectFilePath = _configuration.TatukProjectFilePath;

                _tgisKarta.SaveProjectAs(projectFilePath, true);
            
            }
            catch (Exception ex)
            {

                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            }
        }

        public void AddLokaltData()
        {
            string title = "";

            string shpFilter = "Vanligaste kart-filerna (*.tab, *.shp, *.tif, *.ecw, *.bmp, *.jpg)|*.tab;*.shp;*.tif;*.ecw;*.bmp;*.jpg|TAB-filer (*.tab)|*.tab|Shape-filer (*.shp)|*.shp|Bild-filer (*.tif, *.sid, *.jpg, *.bmp, *.ecw)|*.tif;*.sid;*.jpg;*.bmp;*.ecw|PixelStore (*.ttkps)|*.ttkps|Alla filer (*.*)|*.*";
            string[] filePaths = ShowFileDialog(shpFilter, title);
        
            if (filePaths != null)
            {
                foreach (string file in filePaths)
                {
                    OpenKartFil(file);
                }
            }

            //justerar vilka knappar som ska vara intryckta
            ChangeTool("btnAdd");

            if (filePaths != null)
            {
                AddLokaltData();

            }
        }

        private void OpenKartFil(string filepath)
        {
            //_FrmOPMain._Karta.OpenShapeFile(filepath);
            string fileType = filepath.Substring(filepath.LastIndexOf('.') + 1, 3).ToUpper();

            if (fileType == "SHP")
            {
                OpenShapeFile(filepath);
            }
            else if (fileType == "TAB")
            {
                OpenTabFile(filepath);
            }
            else if (fileType == "TIF")
            {
                OpenTIFFfile(filepath);
            }
            else if (fileType == "SID")
            {
                OpenSIDfile(filepath);
            }
            else if (fileType == "BMP")
            {
                OpenBMPfile(filepath);
            }
            else if (fileType == "JPG")
            {
                OpenJPGfile(filepath);
            }
            else if (fileType == "ECW")
            {
                OpenECWfile(filepath);
            }
            else if (fileType == "TTK")
            {
                OpenPixelStorefile(filepath);
            }
            else
            {
                MessageBox.Show("Vald filtyp stöds inte av programmet och kan inte öppnas.", "Kan inte öppna", MessageBoxButtons.OK);

            }

            //Sätt CachedPaint och IncremetalPaint för det nya lagret
            string fileName = Path.GetFileNameWithoutExtension(filepath);
            TGIS_LayerAbstract newLayer = _tgisKarta.Get(fileName);
            
            if (newLayer != null)
            {
                newLayer.CachedPaint = true;
                newLayer.IncrementalPaint = true;
            }

            //hämtar GPS-lagret och flyttar det överst i lagerlistan
            //detta görs för att CachedPaint = false ska slå igenom.
            //Genom att sätta CachedPaint = false så kan man senare
            //sätta 'shp'.Invalidate(false); för att bara uppdatera denna shp
            // - Screen caching turned on 
            // - Caching for the layer(s) holding the floating (moving) points turned off 
            // - The layer(s) containing the moving points must be the top most layers. 
            

            TGIS_LayerAbstract layer = _tgisKarta.Get("GPSlager");
            if (layer != null)
            {
                int zorder = layer.ZOrder;
                layer.Move(-zorder);
            }

            /* added by me */
            TGIS_LayerSHP layerBestallning = (TGIS_LayerSHP)_tgisKarta.Get(ConfigurationManager.AppSettings["NamnBestallningsLager"]);
            if (layerBestallning != null)
            {
                int order = layerBestallning.ZOrder;
                layerBestallning.Move(-(order - 1));
                order = layerBestallning.ZOrder;
            }

            TGIS_LayerAbstract layerStartplatser = (TGIS_LayerAbstract)_tgisKarta.Get("Startplatser");
            if (layerStartplatser != null)
            {
                int order = layerStartplatser.ZOrder;
                layerStartplatser.Move(-(order - 1));
                order = layerStartplatser.ZOrder;
            }

            //LSAM: kommenterade bort 1 rad
            //layer.CachedPaint = false;

        }

       
        /// <summary>
        /// Visar en Fil dialog där man kan peka ut filer
        /// </summary>
        /// <param name=param name="dialogFilter">Sträng som anger vilka filer som ska visas.</param> 
        /// <returns>Sökvägen till den utpekade filen</returns>
        private string[] ShowFileDialog(string dialogFilter, string title)
        {
            try
            {
                OpenFileDialog fDiag = new OpenFileDialog();
                fDiag.Title = title;
                fDiag.Multiselect = true;
                //fDiag.InitialDirectory = folderPath;
                //fDiag.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*" ;
                fDiag.Filter = dialogFilter;
                fDiag.FilterIndex = _index;
                fDiag.RestoreDirectory = false;

                if (fDiag.ShowDialog() == DialogResult.OK)
                {
                    _index = fDiag.FilterIndex;
                    return fDiag.FileNames;
                }
                else
                {

                    return null;

                }
            }
           catch (Exception ex)
            {
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            
                return null;
            }


        }


        /// <summary>
        /// Öppnar en shapefil i kartan
        /// </summary>
        /// <param name="path">hela sökvägen till filen inklusive filnamn</param>
        public void OpenShapeFile(string path)
        {
            try
            {
                TGIS_LayerSHP ll = new TGIS_LayerSHP();

                ll.Path = path;

                ll.Params.Area.ShowLegend = true;
                ll.Params.Line.ShowLegend = true;
                ll.Params.Marker.ShowLegend = true;
                ll.IgnoreShapeParams = true;

                _tgisKarta.Add(ll);



            }
            catch (Exception ex)
            {
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            
                MessageBox.Show("Kontrollera att filen du försöker öppna är en shape-fil.", "Fel vid öppnandet av shapefil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        bool exceptionCaught;
        public void OpenTabFile(string path)
        {
            //exceptionCaught = false;
            TGIS_LayerTAB ll = new TGIS_LayerTAB();
            try
            {
                ll.Path = path;
                ll.Params.Visible = true;
                ll.Params.Area.ShowLegend = true;
                ll.Params.Line.ShowLegend = true;
                ll.Params.Marker.ShowLegend = true;
                ll.IgnoreShapeParams = true;
                _tgisKarta.Add(ll);
            }
            catch (Exception ex)
            {
               // exceptionCaught = true;
                _Layers.Remove(ll);
                if (ex.GetType().ToString() == "TatukGIS.NDK.EGIS_Exception")
                {
                    //chansar på att felet beror på att det är en tab-fil som hör till en tiff-fil
                    //Testar därför att öppna en tif-fil med samma namn
                    OpenTIFFfile(path);
                    //OpenJPGfile(path);
                }
                else
                {
                    if (_configuration.LogExceptions)
                    {
                        Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                    }
                    ExceptionHandler.HandleException(ex);
                    MessageBox.Show("Kontrollera att filen du försöker öppna är en tab-fil.", "Fel vid öppnandet av tabfil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Öppnar en tabfil i kartan
        /// </summary>
        /// <param name="path">hela sökvägen till filen inklusive filnamn</param>
        /*public void OpenTabFile(string path)
        {
            try
            {
                TGIS_LayerTAB ll = new TGIS_LayerTAB();

                ll.Path = path;

                ll.Params.Visible = true;
                ll.Params.Area.ShowLegend = true;
                ll.Params.Line.ShowLegend = true;
                ll.Params.Marker.ShowLegend = true;
                ll.IgnoreShapeParams = true;

                _tgisKarta.Add(ll);


            }
           catch (Exception ex)
            {
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            
                MessageBox.Show("Kontrollera att filen du försöker öppna är en tab-fil.", "Fel vid öppnandet av tabfil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }*/

        public void OpenTIFFfile(string path)
        {
            try
            {
                //om användaren har markerat en tab-fil som inte går att öppna är
                //sannolikheten ganska stor att det är en tab-fil som är kopplad till
                //en tiff-fil. Kontrollera därför om det finns en tiff-fil med samma 
                //namn öppen redan. Om inte, öppna tiff-filen istället. 
                if (path.Substring(path.LastIndexOf('.') + 1, 3).ToUpper() == "TAB")
                {
                    try
                    {
                        String layerName = path.Substring(path.LastIndexOf('\\') + 1, path.Length - 5 - path.LastIndexOf('\\'));
                        TGIS_LayerAbstract llTemp = (TGIS_LayerAbstract)_tgisKarta.Get(layerName);
                        if (llTemp == null)
                        {
                            OpenTIFFfile(path.Substring(0, path.Length - 4) + ".tif");
                        }
                        else
                        {
                            //ingenting görs, lagret är redan tillagt
                        }
                    }
                    catch (Exception ex)
                    {
                        if (_configuration.LogExceptions)
                        {
                            Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                        }
                        ExceptionHandler.HandleException(ex);
                        MessageBox.Show("Kontrollera att filen du försöker öppna är en tab-fil.", "Fel vid öppnandet av tab-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //kontrollerar om filen finns, det är inte självklart eftersom vi i koden
                    //ovan gör en chansning att det finns en tiff-fil med ett visst namn på
                    //en viss plats.
                    if (File.Exists(path))
                    {
                        //Kontrollerar om lagret redan är tillagt
                        String layerName = path.Substring(path.LastIndexOf('\\') + 1, path.Length - 5 - path.LastIndexOf('\\'));
                        TGIS_LayerAbstract llTemp = (TGIS_LayerAbstract)_tgisKarta.Get(layerName);
                        if (llTemp == null)
                        {
                            TGIS_LayerTIFF ll = new TGIS_LayerTIFF();
                            ll.Path = path;
                            _tgisKarta.Add(ll);
                        }
                        else
                        {
                            //ingenting görs, lagret är redan tillagt
                        }
                    }
                    else
                    {
                        //ingenting görs, filen finns inte
                    }
                }
            }
            catch (Exception ex)
            {
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }
                ExceptionHandler.HandleException(ex);
                MessageBox.Show("Kontrollera att filen du försöker öppna är en tif(f)-fil.", "Fel vid öppnandet av tiffil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Öppnar en Tiff-fil i kartan
        /// </summary>
        /// <param name="path">hela sökvägen till filen inklusive filnamn</param>
        /*public void OpenTIFFfile(string path)
        {
            try
            {
                TGIS_LayerTIFF ll = new TGIS_LayerTIFF();

                ll.Path = path;

                _tgisKarta.Add(ll);


            }
            catch (Exception ex)
            {
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            
                MessageBox.Show("Kontrollera att filen du försöker öppna är en tif(f)-fil.", "Fel vid öppnandet av tiffil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }*/

        /// <summary>
        /// Öppnar en MrSID-fil i kartan
        /// </summary>
        /// <param name="path">hela sökvägen till filen inklusive filnamn</param>
        public void OpenSIDfile(string path)
        {
            try
            {
                TGIS_LayerMrSID ll = new TGIS_LayerMrSID();

                ll.Path = path;

                _tgisKarta.Add(ll);


            }
           catch (Exception ex)
            {

                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            
                MessageBox.Show("Kontrollera att filen du försöker öppna är en MrSID-fil.", "Fel vid öppnandet av MrSID-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// Öppnar en JPG-fil i kartan
        /// </summary>
        /// <param name="path">hela sökvägen till filen inklusive filnamn</param>
        public void OpenJPGfile(string path)
        {
            try
            {
                // Om vi inte har en jpg-fil testar vi om det finns en med "samma" namn undantaget fil-ändelsen.  
                if (path.Substring(path.LastIndexOf('.') + 1, 3).ToUpper() != "JPG")
                    path = path.Substring(0, path.Length - 4) + ".JPG";
                
                TGIS_LayerJPG ll = new TGIS_LayerJPG();

                ll.Path = path;

                _tgisKarta.Add(ll);


            }
            catch (Exception ex)
            {
                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);

                MessageBox.Show("Kontrollera att filen du försöker öppna är en JPG-fil.", "Fel vid öppnandet av JPG-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// Öppnar en ECW-fil i kartan
        /// </summary>
        /// <param name="path">hela sökvägen till filen inklusive filnamn</param>
        public void OpenECWfile(string path)
        {
            try
            {
                TGIS_LayerECW ll = new TGIS_LayerECW();

                ll.Path = path;

                _tgisKarta.Add(ll);

            }
            catch (Exception ex)
            {

                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            
                MessageBox.Show("Kontrollera att filen du försöker öppna är en ECW-fil. " + ex.Message, "Fel vid öppnandet av ECW-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// Öppnar en PixelStore-fil i kartan
        /// </summary>
        /// <param name="path">hela sökvägen till filen inklusive filnamn</param>
        public void OpenPixelStorefile(string path)
        {
            try
            {
                TGIS_LayerPixelStoreAdo2 ll = new TGIS_LayerPixelStoreAdo2();
                ll.Path = path;
                //LSAM: kommenterade bort 2 rader
                //ll.IncrementalPaint = true;
                //ll.CachedPaint = true;
                _tgisKarta.Add(ll);       


            }
            catch (Exception ex)
            {

                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            
                MessageBox.Show("Kontrollera att filen du försöker öppna är en PixelStore(.TTKPS)-fil.", "Fel vid öppnandet av PixelStore-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        /// <summary>
        /// Öppnar en BMP-fil i kartan
        /// </summary>
        /// <param name="path">hela sökvägen till filen inklusive filnamn</param>
        public void OpenBMPfile(string path)
        {
            try
            {
                TGIS_LayerBMP ll = new TGIS_LayerBMP();

                ll.Path = path;

                _tgisKarta.Add(ll);


            }
           catch (Exception ex)
            {

                if (_configuration.LogExceptions)
                {
                    Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                }

                ExceptionHandler.HandleException(ex);
            
                MessageBox.Show("Kontrollera att filen du försöker öppna är en BMP-fil.", "Fel vid öppnandet av BMP-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}