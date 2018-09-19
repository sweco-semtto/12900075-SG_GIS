/********************************************************
 * Kartverktyg.cs anv�nds f�r att hantera h�ndelser
 * knutna till kartan
 * 
 * Laddar kartdata 
 * Hanterar funktionen bakom vissa av knapparna
 * Hanterar vilken knapp som �r intryckt
 * Ritar ut labels med r�tt utseende
 * 
 * LSAM, SWECO Position, v�ren 2006
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
    /// Hanterar h�ndelser knutna till kartan
    /// </summary>
    public class Kartverktyg
    {
        // Kod f�r OpenWithDialog Box
        [DllImport("shell32.dll", SetLastError = true)]
        extern public static bool
            ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);
        public const uint SW_NORMAL = 1;

        Configuration _configuration = Configuration.GetConfiguration();

        //Referens till formul�rets GIS/kart-komponent
        TatukGIS.NDK.WinForms.TGIS_ViewerWnd _tgisKarta;

        //Referens till formul�rets verktygsf�lt
        ToolStrip _toolStrip;

        //Referens till formul�ret
        Karta _karta;
        
        //skapar en instans av Arguments som h�ller koll p� olika knapptryckningar mm
        Arguments _arguments = Arguments.GetArguments();

        //variabel som h�ller koll p� vilket verktyg som varit aktuellt innan det nu aktuella
        //btnZoomBox �r default
        string _lastVerktyg = "btnZoomBox";

        //inst�llningar f�r att �ppna lokalt data
        int _index = 1;

        public bool _pan = false;

        // h�ller en lista �ver �ppnade lager vilka �r vara m�jliga f�r anv�ndaren att st�nga
        public IList<TGIS_LayerAbstract> _Layers;

        public IList<TGIS_LayerAbstract> Layers
        {
            get
            {
                return _Layers;
            }
        }

        /// <summary>
        /// Sparar undan de h�mtade startplatserna s� vi kan �ndra status p� dem ifr�n kartan. 
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

        // triggas d� ett lager tas bort fr�n kartkontrollen
        void tgisKarta_LayerDelete(object _sender, TGIS_LayerEventArgs _e)
        {
            _Layers.Remove(_e.Layer);
        }
        // triggas d� ett nytt lager l�ggs till kartkontrollen
        void tgisKarta_LayerAdd(object _sender, TGIS_LayerEventArgs _e)
        {
            string layerName = _e.Layer.Name;
            //L�gg bara till de lager som kan �ppnas/st�ngas av anv�ndaren
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
        /// L�ser in kartdata
        /// </summary>
        public void LaddaKartData()
        {
            try
            {
                
                //S�tter kart-Mode, finns inbyggda Mode i Tatuk 
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
                        //�ppnar projektfilen (TTKGP-fil)
                        _tgisKarta.Open(projectFilePath, true);
                        _tgisKarta.SetCSByEPSG(3006); //CS h�rdkodas till SWEREF99 TM
                    }
                    catch (Exception e)
                    {
                        string error = e.Message;

                        //ingenting sker om ett lager i projektfilen inte kan l�sas
                    }

                    //anv�nder det extent som �r angivet i config-filen
                    string[] extent = _configuration.VisibleExtent.Split(',');

                    TGIS_Extent visibleExtent = new TGIS_Extent();

                    visibleExtent.XMin = Convert.ToDouble(extent[0]);
                    visibleExtent.XMax = Convert.ToDouble(extent[1]);
                    visibleExtent.YMin = Convert.ToDouble(extent[2]);
                    visibleExtent.YMax = Convert.ToDouble(extent[3]);                   
               
                    //s�tter ny synlig utbredning f�r kartan
                    _tgisKarta.VisibleExtent = visibleExtent;
                                       
                }
                else
                {
                    throw new Exception("Ogiltig s�kv�g till kartprojektfilen");
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
        /// Laddar startplatser ifr�n MySql. 
        /// </summary>
        /// <returns></returns>
        public TGIS_LayerVector LaddaStartplatser()
        {
            // Tar bort det gamla best�llningslagret ifr�n kartan.
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

            // Kollar om vi �r i testl�ge, d� skall data h�mtas fr�n testdatabasen ist�llet. 
            if (Configuration.GetConfiguration().IsInTestMode)
            {
                foretag = new Foretag(true);
                Startplatser = new Startplats(true);
            }

            if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
            {
                // H�mtar data ifr�n MySql. 
                DataTable foretagMySqlTable = foretag.GetAllFromMySql();
                DataTable startplatsMySqlTable = Startplatser.GetAllFromMySql();

                // Kontrollerar s� att vi inte har f�tt t.ex. ett proxyfel, d.v.s. ingen datatabell �r null. 
                if (foretagMySqlTable != null && startplatsMySqlTable != null)
                    return ReFillStartplatser(startplatsLayer, foretagMySqlTable, startplatsMySqlTable);
            }
            
            // L�ser ifr�n xml.
            DataTable foretagXML = new DataTable();
            foretagXML.ReadXml(StartplatsLayer.LoggFolder + "Foretag.xml");
            DataTable startplatserXML = new DataTable();
            startplatserXML.ReadXml(StartplatsLayer.LoggFolder + "Startplatser.xml");

            // Kontrollerar om data i xml �r tillr�cklig f�rsk, om inte returnerar ett tom
            // Startplatslager. 
            if (!foretag.CheckIfXMLIsUpToDate(foretagXML))
                return startplatsLayer;

            return ReFillStartplatser(startplatsLayer, foretagXML, startplatserXML);

            //if (!_karta.LoggedInAsAdmin)
            //{
            //    // TODO G�ra n�got
            //}

            //return null;
        }

        public TGIS_LayerVector ReFillStartplatser(StartplatsLayer startplatsLayer, DataTable foretagMySqlTable, 
            DataTable startplatsMySqlTable)
        {
            if (!_karta.LoggedInAsAdmin)
            {
                // Skapar alla symbolerna f�r startplatserna, men endast de som �r kopplade till entrepren�ren. 
                startplatsLayer.AddStartplatserFromDataTables(foretagMySqlTable, startplatsMySqlTable, _karta.EntrepreneurId);
            }
            else
            {
                // Skapar alla symbolerna f�r startplatserna. 
                startplatsLayer.AddStartplatserFromDataTables(foretagMySqlTable, startplatsMySqlTable);
            }

            return startplatsLayer;
        }

        // en lista �ver valda startplatser i kartan
        IList<string> selected = new List<string>();

        /// <summary>
        /// L�gger till en startplats till listan �ver valda startplatser
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
        /// Tar bort en startplats till listan �ver valda startplatser
        /// </summary>
        public void Remove()
        {
            selected.Clear();
        }

        /// <summary>
        /// Zoomar ut s� hela kartan syns
        /// </summary>
        public void ZoomaFullt()
        {
            try
            {
                //justerar vilka knappar som ska vara intryckta
                ChangeTool("btnFullExtent");

                //anv�nder det extent som �r angivet i config-filen
                string[] extent = _configuration.MaxExtent.Split(',');

                TGIS_Extent maxExtent = new TGIS_Extent();

                maxExtent.XMin = Convert.ToDouble(extent[0]);
                maxExtent.XMax = Convert.ToDouble(extent[1]);
                maxExtent.YMin = Convert.ToDouble(extent[2]);
                maxExtent.YMax = Convert.ToDouble(extent[3]);

                //s�tter ny synlig utbredning f�r kartan
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
        /// Zoomar in eller ut beroende p� �t vilket h�ll man drar rektangeln
        /// </summary>
        public void ZoomaBox()
        {
            try
            {
                //s�tter zooml�ge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisZoom;

                //s�tter zooml�ge f�r att veta vad som ska ske vid klick i karta
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
                //s�tter panoreringsl�ge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisDrag;

                //s�tter panoreringsl�ge f�r att veta vad som ska ske vid klick i karta
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
        /// Anger att det �r selekteringsl�ge
        /// </summary>
        /// <param name="buttonMode">typ av selektering</param>
        public void Selektera(ButtonMode buttonMode)
        {
            try
            {
                //s�tter selekteringsl�ge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisSelect;

                //s�tter typ av selekteringsl�ge f�r att veta vad 
                //som ska ske vid klick i karta
                _arguments.ButtonMode = buttonMode;

                //h�mtar aktuellt lager och flyttar det �verst i lagerlistan
                //detta g�rs f�r att CashedPaint = false ska sl� igenom.
                //Genom att s�tta CashedPaint = false s� kan man senare
                //s�tta 'shp'.Invalidate(false); f�r att bara uppdatera denna shp

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
        /// Anger att det �r infol�ge
        /// </summary>
        public void Info()
        {
            try
            {
                //s�tter selekteringsl�ge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisSelect;

                //s�tter infol�ge f�r att veta vad som ska 
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
        /// Anger att det �r hyperlink-l�ge
        /// </summary>
        public void HyperLink()
        {
            try
            {
                //s�tter selekteringsl�ge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisSelect;

                //s�tter infol�ge f�r att veta vad som ska 
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
        /// Anger att det �r m�tnings- eller b�ringsl�ge
        /// </summary>
        /// <param name="buttonMode">typ av m�tning eller b�ring</param>
        public void Measure(ButtonMode buttonMode)
        {
            try
            {
                //s�tter editeringsl�ge i Tatuks inbyggda
                _tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisEdit;

                //s�tter typ av m�tningsl�ge f�r att veta vad 
                //som ska ske vid klick i karta
                _arguments.ButtonMode = buttonMode;

                //h�mtar aktuellt lager och flyttar det �verst i lagerlistan
                //detta g�rs f�r att CashedPaint = false ska sl� igenom.
                //Genom att s�tta CashedPaint = false s� kan man senare
                //s�tta 'shp'.Invalidate(false); f�r att bara uppdatera denna shp
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
        /// justerar s� att r�tt knappar �r intryckta i verktygsraden
        /// </summary>
        /// <param name="verktyg">string med namnet p� knappen i verktygsraden som ska tryckas ner</param>
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
                    {   //inget problem om det �r fel typ av knapp, testar n�sta typ
                        try
                        {
                            ((ToolStripDropDownButton)tsItem).BackgroundImage = null;
                        }
                        catch
                        {
                            //inget problem om det �r fel typ av knapp
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
        /// skiftar mellan panoreringsl�ge och GPS-centrering
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
                string company = shp.GetField(ConfigurationManager.AppSettings["NamnF�retagsKolumn"]).ToString();
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
                MessageBox.Show("Vald filtyp st�ds inte av programmet och kan inte �ppnas.", "Kan inte �ppna", MessageBoxButtons.OK);

            }

            //S�tt CachedPaint och IncremetalPaint f�r det nya lagret
            string fileName = Path.GetFileNameWithoutExtension(filepath);
            TGIS_LayerAbstract newLayer = _tgisKarta.Get(fileName);
            
            if (newLayer != null)
            {
                newLayer.CachedPaint = true;
                newLayer.IncrementalPaint = true;
            }

            //h�mtar GPS-lagret och flyttar det �verst i lagerlistan
            //detta g�rs f�r att CachedPaint = false ska sl� igenom.
            //Genom att s�tta CachedPaint = false s� kan man senare
            //s�tta 'shp'.Invalidate(false); f�r att bara uppdatera denna shp
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
        /// Visar en Fil dialog d�r man kan peka ut filer
        /// </summary>
        /// <param name=param name="dialogFilter">Str�ng som anger vilka filer som ska visas.</param> 
        /// <returns>S�kv�gen till den utpekade filen</returns>
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
        /// �ppnar en shapefil i kartan
        /// </summary>
        /// <param name="path">hela s�kv�gen till filen inklusive filnamn</param>
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
            
                MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en shape-fil.", "Fel vid �ppnandet av shapefil", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    //chansar p� att felet beror p� att det �r en tab-fil som h�r till en tiff-fil
                    //Testar d�rf�r att �ppna en tif-fil med samma namn
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
                    MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en tab-fil.", "Fel vid �ppnandet av tabfil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// �ppnar en tabfil i kartan
        /// </summary>
        /// <param name="path">hela s�kv�gen till filen inklusive filnamn</param>
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
            
                MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en tab-fil.", "Fel vid �ppnandet av tabfil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }*/

        public void OpenTIFFfile(string path)
        {
            try
            {
                //om anv�ndaren har markerat en tab-fil som inte g�r att �ppna �r
                //sannolikheten ganska stor att det �r en tab-fil som �r kopplad till
                //en tiff-fil. Kontrollera d�rf�r om det finns en tiff-fil med samma 
                //namn �ppen redan. Om inte, �ppna tiff-filen ist�llet. 
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
                            //ingenting g�rs, lagret �r redan tillagt
                        }
                    }
                    catch (Exception ex)
                    {
                        if (_configuration.LogExceptions)
                        {
                            Log.LogMessage(ex.GetType().ToString() + " " + ex.Message, _configuration.LogFilePath);
                        }
                        ExceptionHandler.HandleException(ex);
                        MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en tab-fil.", "Fel vid �ppnandet av tab-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //kontrollerar om filen finns, det �r inte sj�lvklart eftersom vi i koden
                    //ovan g�r en chansning att det finns en tiff-fil med ett visst namn p�
                    //en viss plats.
                    if (File.Exists(path))
                    {
                        //Kontrollerar om lagret redan �r tillagt
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
                            //ingenting g�rs, lagret �r redan tillagt
                        }
                    }
                    else
                    {
                        //ingenting g�rs, filen finns inte
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
                MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en tif(f)-fil.", "Fel vid �ppnandet av tiffil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// �ppnar en Tiff-fil i kartan
        /// </summary>
        /// <param name="path">hela s�kv�gen till filen inklusive filnamn</param>
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
            
                MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en tif(f)-fil.", "Fel vid �ppnandet av tiffil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }*/

        /// <summary>
        /// �ppnar en MrSID-fil i kartan
        /// </summary>
        /// <param name="path">hela s�kv�gen till filen inklusive filnamn</param>
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
            
                MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en MrSID-fil.", "Fel vid �ppnandet av MrSID-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// �ppnar en JPG-fil i kartan
        /// </summary>
        /// <param name="path">hela s�kv�gen till filen inklusive filnamn</param>
        public void OpenJPGfile(string path)
        {
            try
            {
                // Om vi inte har en jpg-fil testar vi om det finns en med "samma" namn undantaget fil-�ndelsen.  
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

                MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en JPG-fil.", "Fel vid �ppnandet av JPG-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// �ppnar en ECW-fil i kartan
        /// </summary>
        /// <param name="path">hela s�kv�gen till filen inklusive filnamn</param>
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
            
                MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en ECW-fil. " + ex.Message, "Fel vid �ppnandet av ECW-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// �ppnar en PixelStore-fil i kartan
        /// </summary>
        /// <param name="path">hela s�kv�gen till filen inklusive filnamn</param>
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
            
                MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en PixelStore(.TTKPS)-fil.", "Fel vid �ppnandet av PixelStore-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        /// <summary>
        /// �ppnar en BMP-fil i kartan
        /// </summary>
        /// <param name="path">hela s�kv�gen till filen inklusive filnamn</param>
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
            
                MessageBox.Show("Kontrollera att filen du f�rs�ker �ppna �r en BMP-fil.", "Fel vid �ppnandet av BMP-fil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}