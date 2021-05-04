using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SGAB.GPSTracking;
using SGAB.SGAB_Database;
using SGAB.SGAB_Karta;

namespace SGAB
{
    public partial class SGAB : Form
    {    
        private Karta _karta;
        private Keys _errorKeyGPSTracker;
        private Keys _DatabaseKey;
        private Keys _VersionInfo;
        private Keys _SimulateGPS;
        protected GPSTracker _gpsTracker;
        protected FormAdmin _formAdmin;

        public static int EntrepreneurId
        {
            get;
            protected set;
        }

        public static bool LoggedInAsAdmin
        {
            get;
            protected set;
        }

        public SGAB()
        {
            string logPath = SGAB_Karta.Configuration.GetConfiguration().LogFilePath;
            Log.LogInformationMessage("Startar och initialiserar SG-GIS", logPath);

            InitializeComponent();

            Log.LogInformationMessage("Initialisering klar", logPath);

            //justerar storleken på applikationen beroende på vad som har angetts i config-filen
            //drar bort lite för att hela ska synas men användaren kan på detta sätt fylla i
            //de "vanliga" skärmstorlekarna i config-filen
            string[] size = ConfigurationManager.AppSettings["AppSize"].Split(',');
            int width = Convert.ToInt32(size[0]);
            int height = Convert.ToInt32(size[1]);
            this.Size = new System.Drawing.Size(width - 20, height - 60);

            //anpassar etiketten för legenden efter storleken på fönstret
            this.lblLegend.Location = new System.Drawing.Point(width - 112, 27);

            //öppnar applikationen centrerat
            this.StartPosition = FormStartPosition.CenterScreen;

            Log.LogInformationMessage("Skapar upp kartan", logPath);
            _karta = new Karta();
            Log.LogInformationMessage("Skapar kartan skapad", logPath);

            this.splitContainer1.Panel2.Controls.Add(_karta);
            _karta.Dock = DockStyle.Right;
            splitContainer1.Panel1Collapsed = true;

            _karta.SetComponentSize(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);

            Log.LogInformationMessage("Kontrollerar internetuppkoppling", logPath);
            SGAB_InternetConnection.InternetConnection.CheckForInternetConnection();
            if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
                Log.LogInformationMessage("Internetuppkoppling finns", logPath);
            else
                Log.LogInformationMessage("Internetuppkoppling saknas", logPath);

            Startplats.LoggFolder = SGAB_Karta.Configuration.GetConfiguration().WorkPath;

            // Loggar in
            Log.LogInformationMessage("Kontrollerar om admin", logPath);
            LoggedInAsAdmin = LogIn.TryToLoginAsAdmin();
            if (LoggedInAsAdmin)
                Log.LogInformationMessage("Inloggad som admin", logPath);               

            if (!LoggedInAsAdmin && SGAB_InternetConnection.InternetConnection.HasInternetConnection)
            {
                EntrepreneurId = LogIn.TryToLoginAsEntrepreneur(SGAB_Karta.Configuration.GetConfiguration().Username, SGAB_Karta.Configuration.GetConfiguration().Password);
                if (EntrepreneurId != LogIn.NOT_LOGGED_IN)
                {
                    _karta.Username = SGAB_Karta.Configuration.GetConfiguration().Username;
                    _karta.EntrepreneurId = EntrepreneurId;
                    Log.LogInformationMessage("Inloggad som entreprenör " + _karta.Username + " med id " + _karta.EntrepreneurId, logPath);

                    // Spara Entreprenörsid

                }
                else
                {
                    MessageBox.Show("Kunde inte logga in med användarnamn " + SGAB_Karta.Configuration.GetConfiguration().Username + ".\nVänligen kontakta Skogens Gödsling omgående för att lösa problemet. ", "Felaktig inloggning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log.LogErrorMessage("Kunde inte logga in med användarnamn " + SGAB_Karta.Configuration.GetConfiguration().Username, logPath);
                    Environment.Exit(0);
                }
            }
            _karta.LoggedInAsAdmin = SGAB.LoggedInAsAdmin;

            if (!LoggedInAsAdmin && !SGAB_InternetConnection.InternetConnection.HasInternetConnection)
            {
                Log.LogInformationMessage("Läser in entreprenör i offline-läge", logPath);
                _karta.EntrepreneurId = ReadEntreprenuersId();
                Log.LogInformationMessage("Inloggad som entreprenör " + _karta.Username + " med id " + _karta.EntrepreneurId, logPath);
            }
            else if (!LoggedInAsAdmin && SGAB_InternetConnection.InternetConnection.HasInternetConnection)
            {
                Log.LogInformationMessage("Skriver tillbaka entreprenör, id " + EntrepreneurId + " från online-läget", logPath);
                WriteEntreprenuersId(EntrepreneurId);
            }
            else if (LoggedInAsAdmin && !SGAB_InternetConnection.InternetConnection.HasInternetConnection)
            {
                // Dölj knappar i FormAdmin. 
            }

            // Skapar GPS-spårning
            Log.LogInformationMessage("Bygger upp GPS-spårningen", logPath);
            _errorKeyGPSTracker = Keys.Home;
            _gpsTracker = new GPSTracker();
            _karta.GpsTracker = _gpsTracker;
            try
            {
                _gpsTracker.PathToDirectoryContainingLogFiles = @"C:\TEMP\SG log";
                Log.LogInformationMessage("GPS:en spåras till mappen " + _gpsTracker.PathToDirectoryContainingLogFiles, logPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att skapa mapp", "Kan inte skapa mappen C:\\TEMP\\SG log. GPS-spårnigen kommer ej att fungera \n\n" + ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Log.LogErrorMessage("Kan inte skapa mappen C:\\TEMP\\SG log för GPS-spårningen", logPath);
                return;
            }
            Log.LogInformationMessage("Startar GPS-spårningen", logPath);
            _gpsTracker.StartTracking();

            // Tillfälligt för databasdelen
            _DatabaseKey = Keys.F12;

            // Anger att versionen kan kontrolleras genom att rycka på F1-tangenten. 
            _VersionInfo = Keys.F1;

            // Anger att knappen för att starta en GPS-simulering är Pause. 
            _SimulateGPS = Keys.Pause;

            // Administratörsdelen 
            _formAdmin = new FormAdmin(LoggedInAsAdmin);
            _formAdmin.Enabled = false;
            IntPtr handle = _formAdmin.Handle; // För att undvika exception med invoke i FormAdmin.
            _formAdmin.SelectedStartplatser += _karta.FormAdmin_SelectedStartplatser;
            _formAdmin.DeselectAllStartplatser += _karta.FormAdmin_DeselectStartplatser;
            _formAdmin.StatusStartplatsChanged += _karta.FormAdmin_StatusStartplatsChanged;
            _formAdmin.SynchronizationFinished += _karta.FormAdmin_SynchronizationFinished;
            _karta.UnselectAllStartplatser += _formAdmin.Karta_UnselectAllStartplatser;
            _karta.SeletedStartplatser += _formAdmin.Karta_SeletedStartplatser;
            _karta.ShowFormAdmin += new ShowFormAdminEventHandler(karta_ShowFormAdmin);
            _karta.SynchronizeTimer.Timer.Tick += _formAdmin.Karta_SynchronizeTimer_Tick;

            // Genomför en synkronisering vid uppstart. 
            _formAdmin.SynchronizeWithPHP();
        }

        private int ReadEntreprenuersId()
        {
            try
            {
                TextReader textReader = new StreamReader(Startplats.LoggFolder + "EntId.txt");
                StringBuilder updates = new StringBuilder(textReader.ReadToEnd());
                textReader.Close();

                return int.Parse(updates.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beställningsfil saknas, internetuppkoppling krävs för att lösa detta problem \n\n" + ex.ToString(), "Beställningsfil saknas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            return -1;
        }

        /// <summary>
        /// Sparar undan vilket entreprenörsid som entreprenörern har i onlineläge. 
        /// </summary>
        /// <param name="id"></param>
        private void WriteEntreprenuersId(int id)
        {
            TextWriter textWriter = new StreamWriter(Startplats.LoggFolder + "EntId.txt");
            textWriter.Write(id.ToString());
            textWriter.Close();
        }

        private void SGAB_FormClosing(object sender, FormClosingEventArgs e)
        {
            _karta.StopGPS();

            string logPath = SGAB_Karta.Configuration.GetConfiguration().LogFilePath;
            Log.LogInformationMessage("Stänger av SG-GIS", logPath);
            Log.LogClosing("------------------------------------------------------------", logPath);
            Log.LogClosing("", logPath);
        }

        private void karta_ShowFormAdmin(object sender, EventArgs e)
        {
            _formAdmin.Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _gpsTracker.StopTracking();
            _karta.StopGPS();
            base.OnClosing(e);
        }

        /// <summary>
        /// Lyssnar efter knapptryckningar. 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == _errorKeyGPSTracker)
            {
                _gpsTracker.UserRegisterMapOrGPSError();
            }

            if (keyData == _DatabaseKey && SGAB.LoggedInAsAdmin)
            {
                _formAdmin.Show();
            }

            if (keyData == _VersionInfo)
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.ProductVersion;

                MessageBox.Show("Du har version " + version + " av SG-GIS-Databas. ", "Versionsinformation",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (keyData == _SimulateGPS)
            {
                GPSSimulator gpsSimulator = new GPSSimulator();
                gpsSimulator.Show();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}