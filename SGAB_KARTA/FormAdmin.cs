using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using SGAB.SGAB_Database;
using System.IO;

namespace SGAB.SGAB_Karta
{
    public delegate void SelectedStartplatserEventHandler(object sender, SelectedStartplatserEventArgs e);

    public delegate void StatusStartplatsChangedFromAdminEventHandler(object sender, StatusStartplatsChangedFromAdminEventArgs e);

    public delegate void SynchronizationFinishedEventHandler(object sender, SynchronizationFinishedEventArgs e);

    public delegate void DeselectAllStartplatserFromAdminEventHandler(object sender, DeselectAllStartplatserFromAdminEventArgs e);

    /// <summary>
    /// En delegate för att kunna anropa en asynkron metod efter krävande MySql-databasfrågor. 
    /// </summary>
    public delegate void CallbackGetDataMySql(DataTable foretag, DataTable status, DataTable entreprenorer, DataTable startplatser);

    public partial class FormAdmin : Form
    {
        private Stati stati;
        public DataTable StatusFromMySQL
        {
            get;
            protected set;
        }

        private Entreprenorer entreprenorer;
        public DataTable EntreprenorerFromMySQL
        {
            get;
            protected set;
        }

        private Startplats startplatser;
        public DataTable StartplatserFromMySQL
        {
            get;
            protected set;
        }
        private DataTable startplatserFromAccess;

        private Foretag foretag;
        public DataTable ForetagFromMySQL
        {
            get;
            protected set;
        }
        private DataTable foretagFromAccess;

        private FormLoading formLoading = new FormLoading();
        private Configuration _configuration = Configuration.GetConfiguration();
        private string _dbPath = String.Empty;

        /// <summary>
        /// Anger om det finns några synliga startplatser i kartan som skall kommas ihåg. 
        /// </summary>
        protected List<string> VisibleStartplatsIDs
        {
            get;
            set;
        }

        /// <summary>
        /// Anger om det finns några selekterade startplatser i kartan som skall kommas ihåg. 
        /// </summary>
        protected List<string> SelectedStartplatsIDs
        {
            get;
            set;
        }

        /// <summary>
        /// Anger om en sökning har gjorts efter fraktentreprenör. 
        /// </summary>
        protected Entrepreneur SearchFreighter
        {
            get;
            set;
        }

        protected Entrepreneur SearchSpread
        {
            get;
            set;
        }

        protected Status SearchStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Event som talar om att användaren har valt startplatser ifrån admingränssnittet. 
        /// </summary>
        public event SelectedStartplatserEventHandler SelectedStartplatser;

        /// <summary>
        /// Event som talar om att användaren har ändrat status ifrån admingränssnittet. 
        /// </summary>
        public event StatusStartplatsChangedFromAdminEventHandler StatusStartplatsChanged;

        /// <summary>
        /// Event som talar om att synkroniseringen är klar. 
        /// </summary>
        public event SynchronizationFinishedEventHandler SynchronizationFinished;

        /// <summary>
        /// Event som talar om när alla startplatser av avmarkerats i admingränssnittet. 
        /// </summary>
        public event DeselectAllStartplatserFromAdminEventHandler DeselectAllStartplatser;

        /// <summary>
        /// Anger om vi är inloggade som administratör
        /// </summary>
        protected bool _LoggedInAsAdmin;

        /// <summary>
        /// Anger var den temporärar filen med startplatser för offline sparas. 
        /// </summary>
        public static string LoggFolder
        {
            get;
            protected set;
        }

        /// <summary>
        /// Anger vilken ytterligare funktion som skall anropas efter att data har hämtas ifrån MySql. 
        /// </summary>
        protected CallbackGetDataMySql CallbackFunctionMySql;

        public FormAdmin(bool LoggedInAsAdmin)
        {
            InitializeComponent();

            _LoggedInAsAdmin = LoggedInAsAdmin;
            Configuration configuration = Configuration.GetConfiguration();
            LoggFolder = configuration.WorkPath;

            if (configuration.IsInTestMode)
            {
                foretag = new Foretag(true);
                startplatser = new Startplats(true);
                stati = new Stati(true);
            }
            else
            {
                foretag = new Foretag();
                startplatser = new Startplats();
                stati = new Stati();
            }
            entreprenorer = new Entreprenorer();
            StatusKodLista.Initialize();

            VisibleStartplatsIDs = new List<string>();
            SelectedStartplatsIDs = new List<string>();
            
            // Läser in connectionstring till Access-databas från config-filen
            AccessCommunicator.ConnectionString = _configuration.AccessDBConnectionString;
            _dbPath = _configuration.AccessDBPath.Replace("Data Source=", "");
            _dbPath = _dbPath.Replace(";", "");

            // Läser in ev. sparade offlinestatusförändringar. 
            SGAB_InternetConnection.InternetConnection.CheckForInternetConnection();
            if (SGAB_InternetConnection.InternetConnection.HasInternetConnection && !LoggedInAsAdmin)
                startplatser.WriteSavedRequests();

            // Ser till att ej kunna starta programmet som administratör och offline
            if (!SGAB_InternetConnection.InternetConnection.HasInternetConnection && LoggedInAsAdmin)
            {
                // Ladda in senaste startplatser ifrån filer. 
            }

			// Sätter lyssnar till när synkroniseringen är klar
            if (LoggedInAsAdmin)
			    SynchronizationFinished += this.FormAdmin_SynchronizationFinished;
		}

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            CallbackGetDataMySql callback = delegate(DataTable dtforetag, DataTable dtstatus, DataTable dtentreprenorer, DataTable dtstartplatser)
            {
                ForetagFromMySQL = dtforetag;
                StatusFromMySQL = dtstatus;
                EntreprenorerFromMySQL = dtentreprenorer;
                StartplatserFromMySQL = dtstartplatser;

                // Fyller i gränssnittet. 
                foretag.CreateDataGridView(ForetagFromMySQL, dgvForetag);
                startplatser.CreateDataGridView(StartplatserFromMySQL, dgvStartplatser);

                if (ForetagFromMySQL == null)
                    return;

                // Väljer tabben med startplatser som standardtabb. 
                tabTest.SelectTab(tabStartplatser);

                if (!File.Exists(_dbPath))
                {
                    MessageBox.Show("Accessdatabasfilen: " + _dbPath + " finns inte, välj en annan fil.", "Accessdatabasfil saknas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Hämtar tabell Företag ifrån lokala Accessdatabasen            
                foretagFromAccess = foretag.GetAllFromAccess();
                startplatserFromAccess = startplatser.GetAllFromAccess();
            };

            // Laddar ner data från domändatabasen i en annan tråd
            // Formadmin visas disablad med en loading ruta ovanpå
            SGAB_InternetConnection.InternetConnection.CheckForInternetConnection();
            if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
                LoadDataFromMySql(true, true, true, true, callback);
        }

        protected virtual void SortStartplatser()
        {
            // Anger hur startplatserna skall sorteras. 
            startplatser.SetColumnHeaderTexts(dgvStartplatser);
            dgvStartplatser.SortCompare += startplatser.DataGridView_SortCompare_Integers;
            if (dgvStartplatser.Columns.Count > 0)
            dgvStartplatser.Sort(dgvStartplatser.Columns[startplatser.StandardColumnToSortAscending], ListSortDirection.Ascending);
        }

        /// <summary>
        /// Laddar data ifrån MySql. 
        /// </summary>
        /// <param name="LoadForetag"></param>
        /// <param name="LoadStatus"></param>
        /// <param name="LoadEntreprenorer"></param>
        /// <param name="LoadStartplatser"></param>
        /// <param name="Callback"></param>
        /// <returns></returns>
        public void LoadDataFromMySql(bool LoadForetagMySql, bool LoadStatusMySql, bool LoadEntreprenorerMySql, bool LoadStartplatserMySql,
            CallbackGetDataMySql Callback)
        {
            formLoading.TopMost = true;
            formLoading.Show();

            List<bool> WorkerArguments = new List<bool>();
            WorkerArguments.Add(LoadForetagMySql);
            WorkerArguments.Add(LoadStatusMySql);
            WorkerArguments.Add(LoadEntreprenorerMySql);
            WorkerArguments.Add(LoadStartplatserMySql);
            CallbackFunctionMySql = Callback;

            try
            {
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync(WorkerArguments);
            }
            catch (InvalidOperationException) 
            { 
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<bool> WorkerArguments = (List<bool>)e.Argument;
            bool LoadForetagMySql       = WorkerArguments[0];
            bool LoadStatusMySql        = WorkerArguments[1];
            bool LoadEntreprenorerMySql = WorkerArguments[2];
            bool LoadStartplatserMySql  = WorkerArguments[3];

            try
            {
                // Hämtar tabeller ifrån MySql
                if (LoadForetagMySql)
                    ForetagFromMySQL = foretag.GetAllFromMySql();
                if (LoadStatusMySql)
                    StatusFromMySQL = stati.GetAllFromMySql();
                if (LoadEntreprenorerMySql)
                    EntreprenorerFromMySQL = entreprenorer.GetAllFromMySql();
                if (LoadStartplatserMySql)
                    StartplatserFromMySQL = startplatser.GetAllFromMySql();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem med att hämta data ifrån MySql.\n\n " + ex);
            }
        }

        // Anropas när backgroundtråden kört klart
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {                       
            this.txtDBPath.Text = _dbPath;            

            // Anropar callbackfunktionen om att du är allt hämtat ifrån MySql. 
            if (CallbackFunctionMySql != null)
            {
                object[] parameters = new object[4];
                parameters[0] = ForetagFromMySQL;
                parameters[1] = StatusFromMySQL;
                parameters[2] = EntreprenorerFromMySQL;
                parameters[3] = StartplatserFromMySQL;
                try
                {
                    this.Invoke(this.CallbackFunctionMySql, parameters);

                    formLoading.Hide();
                    this.Enabled = true;
                }
                // Fångar fel om en synkning redan körs
                catch (Exception ex)
                {
                    MessageBox.Show("Problem med att hämta data ifrån MySql:\n" + ex.ToString(), "Problem med att hämta data", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }  

        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabTest.SelectedIndex == 0)
                {
                    if (!File.Exists(_dbPath))
                    {
                        MessageBox.Show("Accessdatabasfilen: " + _dbPath + " finns inte, välj en annan fil.", "Accessdatabasfil saknas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (tabTest.SelectedIndex == 1)
                {
                    // Tar fram de startplatser som matchar datorns år. 
                    DataTable tmpForetag = ForetagFromMySQL.Clone();
                    tmpForetag.Rows.Clear();
                    int year = DateTime.Now.Year - 1;
                    string date = "#10/1/" + year + "#"; // Tar inte nyår som brytdatum utan 1:a oktober gäller. 
                    DataRow[] rowsThisYear = ForetagFromMySQL.Select("Bestallningsdatum > " + date);
                    List<string> orderIDs = new List<string>();
                    foreach (DataRow row in rowsThisYear)
                        orderIDs.Add(row["OrderID"].ToString());

                    // Uppdaterar företagslistan
                    foretag.ReFillDataGridView(ForetagFromMySQL, dgvForetag, orderIDs);
                    dgvForetag.SortCompare += foretag.DataGridView_SortCompare_Integers;
					if (dgvForetag.Columns.Count > 0)
						dgvForetag.Sort(dgvForetag.Columns[foretag.StandardColumnToSortAscending], ListSortDirection.Ascending);

					// Uppdaterar startplatslistan
					startplatser.ReFillDataGridView(StartplatserFromMySQL, dgvStartplatser, orderIDs);
                    SortStartplatser();
                    ShowVisibleStartplatser();

                    // Fyller comboboxarna med entreprenörsinformation. 
                    FillEntrepreneurComboBoxes(EntreprenorerFromMySQL);
                    FillStatusComboBox();

                    // Översätter entreprenörsid:n och status till klartext och uppdaterar gridview.
                    startplatser.TranslateEntreprenor(dgvStartplatser, entreprenorer.ToList(EntreprenorerFromMySQL));
                    stati.TranslateStatus(dgvStartplatser);

                    // Kollar om någon sökning har gjorts
                    if (SearchFreighter != null && SearchSpread != null && SearchStatus != null &&
                        (!SearchFreighter.ToString().Equals("Ej angiven") ||
                        !SearchSpread.ToString().Equals("Ej angiven") ||
                        !SearchStatus.ToString().Equals("")))
                    {

                    }
                }
                else if (tabTest.SelectedIndex == 2)
                {                    
                    CallbackGetDataMySql callback = delegate(DataTable dtforetag, DataTable dtstatus, DataTable dtentreprenorer, DataTable dtstartplatser)
                    {                        
                        entreprenorer.CreateDataGridView(dtentreprenorer, dgvEntreprenorer);
                        entreprenorer.ReFillDataGridView(dtentreprenorer, dgvEntreprenorer);
                        entreprenorer.SetColumnHeaderTexts(dgvEntreprenorer);
                    };

                    // Hämtar entreprenörer från MySQL. 
                    LoadDataFromMySql(false, false, true, false, callback);
                }
                else if (tabTest.SelectedIndex == 3)
                {
                    // Status-tabellen hämtas i form_load
                    if (dgvStartplatser.SelectedRows.Count == 1)
                    {
                        ShowHistory();
                        startplatser.TranslateLoggEntreprenorer(dgvStatus, entreprenorer.ToListWithSG(EntreprenorerFromMySQL));
                        stati.TranslateStatus_status(dgvStatus);
                    }
                    else
                        MessageBox.Show("Välj en startplats i kartan eller i listan på fliken Koppla startplatser.");                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen kontakta Sweco, felnummer 1: \n\n" + ex.Message);
            }
        }

        #region Tab_Synkronisera

        private void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDBPath.Text.Trim().Equals(String.Empty))
                {
                    MessageBox.Show("Tryck Bläddra och välj en accessdatabasfil för synkronisering.", "Välj Accessdatabas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!File.Exists(txtDBPath.Text.Trim()))
                {
                    MessageBox.Show("Accessdatabasfilen: " + _dbPath + " finns inte, välj en annan fil.", "Accessdatabasfil saknas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SynchronizeWithPHPAndAccess();       
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen kontakta Sweco, felnummer 2: \n\n" + ex.Message);
            }
        }

        public void Karta_SynchronizeTimer_Tick(object sender, EventArgs e)
        {
            SynchronizeWithPHP();
        }

        /// <summary>
        /// Synkroniserar företag och startplatser.
        /// </summary>
        public void SynchronizeWithPHP()
        {
            CallbackGetDataMySql callback = delegate(DataTable dtforetag, DataTable dtstatus, DataTable dtentreprenorer, DataTable dtstartplatser)
            {
                string logPath = SGAB_Karta.Configuration.GetConfiguration().LogFilePath;
                Log.LogDebugMessage("Synkroniserar med PHP", logPath);

                ForetagFromMySQL = dtforetag;
                StartplatserFromMySQL = dtstartplatser;

                // Gör iordning gränssnittet för företag
                // TODO - ta bort grid sen. 
                //foretag.CreateDataGridView(ForetagFromMySQL, dgvForetag);

                // Tar fram de startplatser som matchar datorns år. 
                DataTable tmpForetag = ForetagFromMySQL.Clone();
                tmpForetag.Rows.Clear();
                int year = DateTime.Now.Year - 1;
                string date = "#10/1/" + year + "#"; // Tar inte nyår som brytdatum utan 1:a oktober gäller. 
                DataRow[] rowsThisYear = dtforetag.Select("Bestallningsdatum > " + date);
                List<string> orderIDs = new List<string>();
                foreach (DataRow row in rowsThisYear)
                    orderIDs.Add(row["OrderID"].ToString());

                if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
                {
                    // Synkar in ev. offlinestatusförändringar.
                    this.StartplatserFromMySQL.WriteXml(LoggFolder + "Startplatser.xml", XmlWriteMode.WriteSchema);
                    this.ForetagFromMySQL.WriteXml(LoggFolder + "Foretag.xml", XmlWriteMode.WriteSchema);
                    startplatser.WriteSavedRequests();
                }

                if (_LoggedInAsAdmin)
                {
                    if (dgvForetag.ColumnCount == 0)
                        foretag.CreateDataGridView(ForetagFromMySQL, dgvForetag);

                    // Uppdaterar företagslistan
                    foretag.ReFillDataGridView(ForetagFromMySQL, dgvForetag, orderIDs);
                    dgvForetag.SortCompare += foretag.DataGridView_SortCompare_Integers;
                    dgvForetag.Sort(dgvForetag.Columns[foretag.StandardColumnToSortAscending], ListSortDirection.Ascending);

                    // Uppdaterar startplatslistan
                    if (dgvStartplatser.ColumnCount == 0)
                        startplatser.CreateDataGridView(StartplatserFromMySQL, dgvStartplatser);

                    startplatser.ReFillDataGridView(StartplatserFromMySQL, dgvStartplatser, orderIDs);
                    SortStartplatser();
                    this.ShowVisibleStartplatser();
                    this.SelectStartplatser();

                    // Om vi inte har startat adminförnstret skall kan vi inte visa entreprenörer. 
                    if (EntreprenorerFromMySQL != null)
                    {

                        // Fyller comboboxarna med entreprenörsinformation. 
                        FillEntrepreneurComboBoxes(EntreprenorerFromMySQL);
                        FillStatusComboBox();

                        // Översätter entreprenörsid:n och status till klartext och uppdaterar gridview.
                        startplatser.TranslateEntreprenor(dgvStartplatser, entreprenorer.ToList(EntreprenorerFromMySQL));
                        stati.TranslateStatus(dgvStartplatser);
                    }
                }

                // Skickar ett event om att kartan skall uppdateras, samma kontroll med datum görs i startplatslagret. 
                if (SynchronizationFinished != null)
                    SynchronizationFinished(this, new SynchronizationFinishedEventArgs(ForetagFromMySQL, StartplatserFromMySQL));
            };

            // Sparar undan vilka startplatser som är markerade innan en synkronisering. 
            GetSelectedStartplatserFromAdmin();

            // Synkas endast om vi har en internetuppkoppling. 
            SGAB_InternetConnection.InternetConnection.CheckForInternetConnection();
            if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
                LoadDataFromMySql(true, false, false, true, callback);

        }

        private void GetSelectedStartplatserFromAdmin()
        {
            // Sparar undan vilka startplatser som är markerade innan synkroniseringe utförs. 
            SelectedStartplatsIDs.Clear();
            foreach (DataGridViewRow row in dgvStartplatser.Rows)
                if (row.Selected && row.Visible) // Kan markera osynliga rader, detta vill vi INTE göra. 
                    SelectedStartplatsIDs.Add(row.Cells["ID"].Value.ToString());
        }

        /// <summary>
        /// Synkroniserar företag och startplatser.
        /// </summary>
        public void SynchronizeWithPHPAndAccess()
        {
            CallbackGetDataMySql callback = delegate(DataTable dtforetag, DataTable dtstatus, DataTable dtentreprenorer, DataTable dtstartplatser)
            {
                ForetagFromMySQL = dtforetag;
                StartplatserFromMySQL = dtstartplatser;

                // Hämtar tabell Företag och Startplatser från lokala Accessdatabasen 
                if (File.Exists(_dbPath))
                {
                    foretagFromAccess = foretag.GetAllFromAccess();
                    startplatserFromAccess = startplatser.GetAllFromAccess();
                }
                else
                {
                    MessageBox.Show("Hittar ej beställningsdatabasen, vänligen peka ut en giltligt beställningsdatabas. \nKan ej synkronisera MySql utan beställningsdatabasen. ", "Kan ej synka med beställningsdatabasen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Synkroniserar Företag
                foretag.SynchronizeWithPHP(ForetagFromMySQL, foretagFromAccess);
                ForetagFromMySQL = foretag.GetAllFromMySql();

				// Synkroniserar startplatser. 
				startplatser.ForetagFromAccess = foretagFromAccess;
                startplatser.ForetagFromMySql = ForetagFromMySQL;
                startplatser.SynchronizeWithPHP(StartplatserFromMySQL, startplatserFromAccess);
                StartplatserFromMySQL = startplatser.GetAllFromMySql();

                // Gör iordning gränssnittet för företag
                // TODO - ta bort grid sen. 
                //foretag.CreateDataGridView(ForetagFromMySQL, dgvForetag);

                // Tar fram de startplatser som matchar datorn år. 
                int year = DateTime.Now.Year - 1;
                string date = "#10/1/" + year + "#"; // Tar inte nyår som brytdatum utan 1:a oktober gäller. 
                DataRow[] rowsThisYear = dtforetag.Select("Bestallningsdatum > " + date);
                List<string> orderIDs = new List<string>();
                foreach (DataRow row in rowsThisYear)
                    orderIDs.Add(row["OrderID"].ToString());

                // Uppdaterar företagslistan
                foretag.ReFillDataGridView(ForetagFromMySQL, dgvForetag, orderIDs);
                dgvForetag.SortCompare += foretag.DataGridView_SortCompare_Integers;
                dgvForetag.Sort(dgvForetag.Columns[foretag.StandardColumnToSortAscending], ListSortDirection.Ascending);

                // Uppdaterar startplatslistan
                startplatser.ReFillDataGridView(StartplatserFromMySQL, dgvStartplatser, orderIDs);
                SortStartplatser();
                this.ShowVisibleStartplatser();

                // Fyller comboboxarna med entreprenörsinformation. 
                FillEntrepreneurComboBoxes(EntreprenorerFromMySQL);
                FillStatusComboBox();

				// Uppdaterar tabellerna som vi skall skicka vidare till färdig-sunk-eventet.  
				dtforetag = ForetagFromMySQL;
				dtstartplatser = StartplatserFromMySQL;

				// Skickar ett event om att kartan skall uppdateras, samma kontroll med datum görs i startplatslagret. 
				if (SynchronizationFinished != null)
                    SynchronizationFinished(this, new SynchronizationFinishedEventArgs(dtforetag, dtstartplatser));
            };

            // Hämtar tabell Företag och Startplatser från MySQL   
            LoadDataFromMySql(true, false, false, true, callback);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                // Öppnar dialog för att välja fil samt skriver valet till config-filen.
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.InitialDirectory = @"c:\";
                fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                fdlg.FilterIndex = 2;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    string newDBPath = fdlg.FileName;
                    if (newDBPath.EndsWith(".mdb"))
                    {
                        _configuration.AccessDBPath = "Data Source=" + newDBPath + ";";
                        txtDBPath.Text = newDBPath;
                        _configuration.UpdateConfigFile("AccessDBPath", _configuration.AccessDBPath); 

                        // Anger den nya anslutningsträngen. 
                        AccessCommunicator.ConnectionString = _configuration.AccessDBConnectionString;
                        _dbPath = _configuration.AccessDBPath.Replace("Data Source=", "");
                        _dbPath = _dbPath.Replace(";", "");
                    }
                    else
                        MessageBox.Show("Den valda filen måste vara en Access-databas: .mdb-fil. \nVälj en annan fil.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen kontakta Sweco, felnummer 3: \n\n" + ex.Message);
            }
        }

        #endregion

        #region Tab_Entreprenor

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Vill du spara ändringar?", "Spara ändringar", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dtEntreprenorer = ExportGridViewToDataTable(dgvEntreprenorer);
                    entreprenorer.SynchronizeWithPHP(EntreprenorerFromMySQL, dtEntreprenorer);

                    EntreprenorerFromMySQL = entreprenorer.GetAllFromMySql();
                    entreprenorer.ReFillDataGridView(EntreprenorerFromMySQL, dgvEntreprenorer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen kontakta Sweco, felnummer 4: \n\n" + ex.Message);
            }
            finally
            {
                cbxFreight.Checked = false;
                cbxSpread.Checked = false;
                tbxName.Text = "";
            }
        }

        /// <summary>
        /// Tar hand om klick på raderna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEntreprenorer_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvEntreprenorer == null)
                return;

            // Om en rad är markerad, skall värderna för den synas i fälten i gränssnittet. 
            if (dgvEntreprenorer.SelectedRows.Count == 1)
            {
                if (dgvEntreprenorer.Rows[e.RowIndex].Cells["Namn"].Value != null)
                {
                    this.tbxName.Text = dgvEntreprenorer.Rows[e.RowIndex].Cells["Namn"].Value.ToString();
                    this.tbxName.Refresh();
                }

                if (dgvEntreprenorer.Rows[e.RowIndex].Cells["Fraktentreprenor"].Value != null)
                {
                    if (dgvEntreprenorer.Rows[e.RowIndex].Cells["Fraktentreprenor"].Value.ToString().Equals("1"))
                        cbxFreight.Checked = true;
                    else
                        cbxFreight.Checked = false;
                }

                if (dgvEntreprenorer.Rows[e.RowIndex].Cells["Spridningsentreprenor"].Value != null)
                {
                    if (dgvEntreprenorer.Rows[e.RowIndex].Cells["Spridningsentreprenor"].Value.ToString().Equals("1"))
                        cbxSpread.Checked = true;
                    else
                        cbxSpread.Checked = false;
                }
            }
            else
            {
                this.tbxName.Text = "";
                cbxFreight.Checked = false;
                cbxSpread.Checked = false;
            }
        }

        /// <summary>
        /// Anger vilka fält som skall visas beroende på antalet rader som är markerade. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEntreprenorer_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEntreprenorer.SelectedRows.Count == 0)
            {
                lblName.Enabled = false;
                tbxName.Enabled = false;
                cbxFreight.Enabled = false;
                cbxSpread.Enabled = false;
            }
            else if (dgvEntreprenorer.SelectedRows.Count == 1)
            {
                lblName.Enabled = true;
                tbxName.Enabled = true;
                cbxFreight.Enabled = true;
                cbxSpread.Enabled = true;
            }
            else if (dgvEntreprenorer.SelectedRows.Count > 1)
            {
                lblName.Enabled = false;
                tbxName.Enabled = false;
                cbxFreight.Enabled = true;
                cbxSpread.Enabled = true;
            }
        }

        /// <summary>
        /// Ändrar namnet på en entreprenör i gränssnittet. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (dgvEntreprenorer.SelectedRows.Count == 1)
            {
                dgvEntreprenorer.SelectedRows[0].Cells["Namn"].Value = tbxName.Text;
                dgvEntreprenorer.Refresh();
            }
        }

        /// <summary>
        /// Ändrar om entreprenören är en fraktentreprenör. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxFreight_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selectedRow in dgvEntreprenorer.SelectedRows)
            {
                if (cbxFreight.Checked)
                    selectedRow.Cells["Fraktentreprenor"].Value = "1";
                else
                    selectedRow.Cells["Fraktentreprenor"].Value = "0";
            }

            dgvEntreprenorer.Refresh();
        }

        /// <summary>
        /// Ändrar om entreprenören är en spridningsentreprenör. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxSpread_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selectedRow in dgvEntreprenorer.SelectedRows)
            {
                if (cbxSpread.Checked)
                    selectedRow.Cells["Spridningsentreprenor"].Value = "1";
                else
                    selectedRow.Cells["Spridningsentreprenor"].Value = "0";
            }

            dgvEntreprenorer.Refresh();
        }

        #endregion

        #region Tab_Koppla_Startplatser

        /// <summary>
        /// Laddar listboxarna för frakt- och spridningsentreprenör.
        /// Lägger till värde "Ej angiven" - ID: 0.
        /// </summary>
        /// <param name="entreprenorerFromMySQL"></param>
        private void FillEntrepreneurComboBoxes(DataTable entreprenorerFromMySQL)
        {
            List<Entrepreneur> possibleFreightEntrepreneurs =
                    entreprenorer.GetPossibleFreightEntrepreneurs(entreprenorerFromMySQL);
            List<Entrepreneur> possibleSpreadEntrepreneurs =
                entreprenorer.GetPossibleSpreadEntrepreneurs(entreprenorerFromMySQL);
            Entrepreneur emptyEntrepreneur = new Entrepreneur("0", "Ej angiven");
            Entrepreneur noEntrepreneur = new Entrepreneur("-1", "");
            
            cmbFreight.Items.Clear();
            cmbFreight.Items.Add(noEntrepreneur);
            cmbFreight.Items.Add(emptyEntrepreneur);            
            foreach (Entrepreneur freighter in possibleFreightEntrepreneurs)
            {
                cmbFreight.Items.Add(freighter); 
            }   
            cmbFreight.SelectedItem = noEntrepreneur;

            cmbSpread.Items.Clear(); 
            cmbSpread.Items.Add(noEntrepreneur);
            cmbSpread.Items.Add(emptyEntrepreneur);            
            foreach (Entrepreneur spreader in possibleSpreadEntrepreneurs)
            {
                cmbSpread.Items.Add(spreader);
            }           
            cmbSpread.SelectedItem = noEntrepreneur;
        }

        private void FillStatusComboBox()
        {
            cmbStatus.Items.Clear();
            
            Status noStatus = new Status(-1, "");
            cmbStatus.Items.Add(noStatus);            
            foreach (Status status in StatusKodLista.StatusKoder)
            {
                cmbStatus.Items.Add(status);    
            }
             
            cmbStatus.SelectedItem = noStatus;
        }

        private void dgvStartplatser_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStartplatser.SelectedRows.Count > 0)
                btnBind.Enabled = true;
            else
                btnBind.Enabled = false;

            //Väljer kopplad frakt- och spridningsentreprenör, samt status, i listboxarna
            if (dgvStartplatser.SelectedRows.Count == 1)
            {
                string currentFreighter = dgvStartplatser.SelectedRows[0].Cells["Fraktentreprenors_ID"].Value.ToString();
                string currentSpreader = dgvStartplatser.SelectedRows[0].Cells["Spridningsentreprenors_ID"].Value.ToString();
                string currentStatus = dgvStartplatser.SelectedRows[0].Cells["Status"].Value.ToString();
         
                foreach (Entrepreneur freigther in cmbFreight.Items)
                {
                    if (freigther.Name.Equals(currentFreighter))
                        cmbFreight.SelectedItem = freigther;    
                }

                foreach (Entrepreneur spreader in cmbSpread.Items)
                {
                    if (spreader.Name.Equals(currentSpreader))
                        cmbSpread.SelectedItem = spreader;
                }

                foreach (Status status in cmbStatus.Items)
                {
                    if (status.Id.Equals(currentStatus))
                        cmbStatus.SelectedItem = status;
                }
            }
        }

        private void cmbFreight_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearchStartplatser.Enabled = true;
            if (dgvStartplatser.SelectedRows.Count > 0)
                btnBind.Enabled = true;
        }

        private void cmbSpread_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearchStartplatser.Enabled = true;
            if (dgvStartplatser.SelectedRows.Count > 0)
                btnBind.Enabled = true;
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearchStartplatser.Enabled = true;
            if (dgvStartplatser.SelectedRows.Count > 0)
                btnBind.Enabled = true;
        }

		private void btnBind_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            int statusNr = -1;

            try
            {
                if (dgvStartplatser.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Välj startplatser som ska kopplas genom att markera en rad/flera rader i listan.");
                    return;
                }

                Entrepreneur freightEntrepreneur = (Entrepreneur)cmbFreight.SelectedItem;
                Entrepreneur spreadEntrepreneur = (Entrepreneur)cmbSpread.SelectedItem;
                string freightEntrepreneurID = freightEntrepreneur.Id;
                string spreadEntrepreneurID = spreadEntrepreneur.Id;
                Status status = (Status)cmbStatus.SelectedItem;
                statusNr = status.StatusNr;

                if (!(!freightEntrepreneurID.Equals("-1") || !spreadEntrepreneurID.Equals("-1") || statusNr != -1))
                {
                    MessageBox.Show("Välj minst entreprenörer eller status i listboxarna.");
                    this.Cursor = Cursors.Default;
                    return;
                }

                // För knappen Koppla grå
                btnBind.Enabled = false;

                // Uppdaterar gridvärden - entreprenörsid och status, men bara de markerade och synliga raderna. 
                GetSelectedStartplatserFromAdmin();
                for (int i = 0; i < dgvStartplatser.Rows.Count; i++)
                {
                    if (this.SelectedStartplatsIDs.Contains(
                        dgvStartplatser.Rows[i].Cells["ID"].Value.ToString()))
                    {
                        if (!freightEntrepreneurID.Equals("-1"))
                            dgvStartplatser.Rows[i].Cells["Fraktentreprenors_ID"].Value = Convert.ToInt32(freightEntrepreneurID);

                        if (!spreadEntrepreneur.Equals("-1"))
                            dgvStartplatser.Rows[i].Cells["Spridningsentreprenors_ID"].Value = Convert.ToInt32(spreadEntrepreneurID);

                        if (statusNr != -1)
                            dgvStartplatser.Rows[i].Cells["Status"].Value = Convert.ToInt32(statusNr);
                    }
                }

                // Laddar upp valda rader till MySql-databasen
                // Uppdaterar Startplats samt lägger till en rad i Status som historik.
                DataTable dtStartplatser = ExportSelectedRowsInGridViewToDataTable(dgvStartplatser);
                startplatser.UpdateRowsInMySql_Bind(StartplatserFromMySQL, dtStartplatser);
                stati.InsertRows(dtStartplatser, "0");

                // Uppdaterar selekterade raders entreprenörsid:n och status åter till klartext
                foreach (DataGridViewRow row in dgvStartplatser.SelectedRows)
                {
                    row.Cells["Fraktentreprenors_ID"].Value = freightEntrepreneur.Id;
                    row.Cells["Spridningsentreprenors_ID"].Value = spreadEntrepreneur.Id;
                    row.Cells["Status"].Value = StatusKodLista.FindById(statusNr) == null ? row.Cells["Status"].Value : status.Id;
                }
            }
            catch (MySqlException mysqlex)
            {
                string errorMessage = mysqlex.GetErrors();
                MessageBox.Show("Fel vid koppling. " + errorMessage, "Fel vid koppling", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Felnummer 5: \nKontrollera internetuppkoppling och proxyinställningar. \n\n" + ex.Message);
            }
            finally
            {
                btnShowAll.Enabled = true;
                btnSearchStartplatser.Enabled = true;
                btnShowInMap.Enabled = true;

                // Översätter entreprenörsid:n och status till klartext och uppdaterar gridview.
                startplatser.TranslateEntreprenor(dgvStartplatser, entreprenorer.ToList(EntreprenorerFromMySQL));
                stati.TranslateStatus(dgvStartplatser);
            }

            List<string> StartplatsIDs = new List<string>();
            for (int selectedRowIndex = 0; selectedRowIndex < dgvStartplatser.SelectedRows.Count; selectedRowIndex++)
            {
                // Lägger till de markerade startplatserna. 
                string startplatsID = dgvStartplatser.SelectedRows[selectedRowIndex].Cells["ID"].Value.ToString();
                StartplatsIDs.Add(startplatsID);
            }

            if (StatusStartplatsChanged != null)
                StatusStartplatsChanged(this, new StatusStartplatsChangedFromAdminEventArgs(StartplatsIDs, statusNr));

            this.Cursor = Cursors.Default;
        }
		
        private void btnSearchStartplatser_Click(object sender, EventArgs e)
        {
            try
            {
                Entrepreneur freightEntrepreneur = (Entrepreneur)cmbFreight.SelectedItem;
                Entrepreneur spreadEntrepreneur = (Entrepreneur)cmbSpread.SelectedItem;
                string freightEntrepreneurID = freightEntrepreneur.Id;
                string spreadEntrepreneurID = spreadEntrepreneur.Id;                                
                Status status = (Status)cmbStatus.SelectedItem;
                int statusKod = status.StatusNr;

                showConnectedStartplatser(freightEntrepreneurID, spreadEntrepreneurID, statusKod.ToString());

                // Översätter entreprenörsid:n och status till klartext och uppdaterar gridview.
                startplatser.TranslateEntreprenor(dgvStartplatser, entreprenorer.ToList(EntreprenorerFromMySQL));
                stati.TranslateStatus(dgvStartplatser);

                // Sparar undan vad vilken sökning som har gjort för växlande av tabbar
                this.SearchFreighter = freightEntrepreneur;
                this.SearchSpread = spreadEntrepreneur;
                this.SearchStatus = status;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen kontakta Sweco, felnummer 6: \n\n" + ex.Message);
            }
            finally
            {
                btnBind.Enabled = false;
                btnSearchStartplatser.Enabled = false;
                btnShowAll.Enabled = true;                
                btnShowInMap.Enabled = true;
            }
        }
        
        /// <summary>
        /// Visar alla startplatser i gridviewn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                showAllStartplatser();

                // Översätter entreprenörsid:n och status till klartext och uppdaterar gridview.
                startplatser.TranslateEntreprenor(dgvStartplatser, entreprenorer.ToList(EntreprenorerFromMySQL));
                stati.TranslateStatus(dgvStartplatser);

                cmbSpread.SelectedIndex = 0;
                cmbFreight.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen kontakta Sweco, felnummer 7: \n\n" + ex.Message);
            }
            finally
            {
                btnBind.Enabled = false;
                btnShowAll.Enabled = false;
                btnSearchStartplatser.Enabled = true;                
                btnShowInMap.Enabled = true;
            }
        }

        private void btnShowInMap_Click(object sender, EventArgs e)
        {
            try
            {
                //List<string> StartplatsIDs = new List<string>();
                //for (int selectedRowIndex = 0; selectedRowIndex < dgvStartplatser.SelectedRows.Count; selectedRowIndex++)
                //{
                //    // Lägger till de markerade startplatserna. 
                //    string startplatsID = dgvStartplatser.SelectedRows[selectedRowIndex].Cells["ID"].Value.ToString();
                //    StartplatsIDs.Add(startplatsID);
                //}

                GetSelectedStartplatserFromAdmin();

                if (SelectedStartplatser != null)
                    SelectedStartplatser(this, new SelectedStartplatserEventArgs(this.SelectedStartplatsIDs));
                    //SelectedStartplatser(this, new SelectedStartplatserEventArgs(StartplatsIDs));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen kontakta Sweco, felnummer 8: \n\n" + ex.Message);
            }
            finally
            {
                btnBind.Enabled = false;                
                btnShowInMap.Enabled = false;
                btnShowAll.Enabled = true;
                btnSearchStartplatser.Enabled = true;                
            }
        }

        /// <summary>
        /// Hämtar alla startplatser som är kopplade till en viss fraktentreprenör/en viss spridningsentreprenör
        /// /ett visst status.
        /// </summary>
        /// <param name="freightEntrepreneurID"></param>
        /// <param name="spreadEntrepreneurID"></param>
        /// <param name="statusID"></param>
        private void showConnectedStartplatser(string freightEntrepreneurID, string spreadEntrepreneurID, string statusID)
        {
            string whereClause = "";

            if (!freightEntrepreneurID.Equals("-1"))
            {
                whereClause = "Fraktentreprenors_ID = '" + freightEntrepreneurID + "'";
            }

            if (!spreadEntrepreneurID.Equals("-1"))
            {
                if (whereClause.Length == 0)
                    whereClause += "Spridningsentreprenors_ID = '" + spreadEntrepreneurID + "'";
                else
                    whereClause += " AND Spridningsentreprenors_ID = '" + spreadEntrepreneurID + "'";
            }

            if (!statusID.Equals("-1"))
            { 
                if (whereClause.Length == 0)
                    whereClause += "Status = '" + statusID + "'";
                else
                    whereClause += " AND Status = '" + statusID + "'";
            }
            
            StartplatserFromMySQL = startplatser.GetAllFromMySql();
            
            DataTable startplatserSelected = startplatser.SelectFromDataTable(StartplatserFromMySQL, whereClause);
            startplatser.ReFillDataGridView(startplatserSelected, dgvStartplatser);
            SortStartplatser();
            this.ShowVisibleStartplatser();
        }

        /// <summary>
        /// Hämtar alla startplatser från MySql och visar dessa i gridviewn.
        /// </summary>
        private void showAllStartplatser()
        { 
            //Uppdaterar griden
            //StartplatserFromMySQL = startplatser.GetAllFromMySql();

            // Avmarkerar de markerade startplatserna. 
            VisibleStartplatsIDs.Clear();
            ShowVisibleStartplatser();

            if (DeselectAllStartplatser != null)
                DeselectAllStartplatser(this, new DeselectAllStartplatserFromAdminEventArgs());

            // Fyller på med alla startplatser som finns. 
            startplatser.ReFillDataGridView(StartplatserFromMySQL, dgvStartplatser);
            SortStartplatser();
            this.ShowVisibleStartplatser();
        }

        #endregion   

        #region Tab_Historik

        private void ShowHistory()
        {
            // Hämtar status-tabellen från MySQL, kollar vald startplats och gör selektion i tabellen.
            StatusFromMySQL = stati.GetAllFromMySql();
            stati.CreateDataGridView(StatusFromMySQL, dgvStatus);
            string startplatsID = dgvStartplatser.SelectedRows[0].Cells["ID"].Value.ToString();
            DataTable dtStatusLogg = stati.SelectFromDataTable(StatusFromMySQL, "StartplatsID = '" + startplatsID + "'");
            stati.ReFillDataGridView(dtStatusLogg, dgvStatus);
            stati.SetColumnHeaderTexts(dgvStatus);
        }

        #endregion
        /// <summary>
        /// Exporterar en GridViews data till en DataTable. 
        /// </summary>
        /// <param name="gridview"></param>
        /// <returns></returns>
        private DataTable ExportGridViewToDataTable(DataGridView gridview)
        {
            //Skapa tabellstruktur i datatable
            DataTable dt = new DataTable();
            for (int i = 0; i < gridview.ColumnCount; i++)
            {
                dt.Columns.Add(gridview.Columns[i].Name);
            }

            //Kopierar över rader från gridview till datatable
            DataRow dr = null;
            foreach (DataGridViewRow gridRow in gridview.Rows)
            {
                dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dr[i] = gridRow.Cells[i].Value;
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// Exporterar en GridViews selekterade rader till en DataTable. 
        /// </summary>
        /// <param name="gridview"></param>
        /// <returns></returns>
        private DataTable ExportSelectedRowsInGridViewToDataTable(DataGridView gridview)
        {
            //Skapa tabellstruktur i datatable
            DataTable dt = new DataTable();
            for (int i = 0; i < gridview.ColumnCount; i++)
            {
                dt.Columns.Add(gridview.Columns[i].Name);
            }

            // Tar fram alla entreprenörer
            List<Entrepreneur> allEntrepreneurs = new List<Entrepreneur>();
            allEntrepreneurs = this.entreprenorer.ToList(this.EntreprenorerFromMySQL);

            //Kopierar över rader från gridview (selekterade rader) till datatable
            DataRow dr = null;
            foreach (DataGridViewRow gridRow in gridview.Rows)
            {
                // Måste kolla selekterade och synliga rader för man kan selektera osynliga rader. 
                if (gridRow.Selected && gridRow.Visible)
                {
                    dr = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        // Övversätter entgreprenörer och status till siffror igen. 
                        if (dt.Columns[i].ColumnName.Equals("Spridningsentreprenors_ID") ||
                            dt.Columns[i].ColumnName.Equals("Fraktentreprenors_ID"))
                        {
                            // Gör en översättning ifrån namn till idnummer. 
                            Entrepreneur entrepreneur = Entrepreneur.FindEntrepreneurByName(allEntrepreneurs, gridRow.Cells[i].Value.ToString());
                            if (entrepreneur == null) // Om vi inte någon entreprenör har vi redan Id:et ifrån comboboxarna
                                dr[i] = gridRow.Cells[i].Value.ToString();
                            else
                                dr[i] = entrepreneur.Id;
                        }
                        else if (dt.Columns[i].ColumnName.Equals("Status"))
                        {
                            // Gör en översättning ifrån namn till idnummer. 
                            Status status = StatusKodLista.FindByName(gridRow.Cells[i].Value.ToString());
                            if (status == null) // Om vi inte hittade någon status har vi redan Id:et ifrån comboboxen. 
                                dr[i] = gridRow.Cells[i].Value;
                            else
                                dr[i] = status.StatusNr;
                        }
                        else
                            dr[i] = gridRow.Cells[i].Value;
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        /// <summary>
        /// Väljer vilka startplatsser som skall selekteras. 
        /// </summary>
        protected virtual void ShowVisibleStartplatser()
        {
            // Om inga markerade startplatser finns, skall alla visas. 
            if (VisibleStartplatsIDs.Count == 0)
                return;

            // Loopar igenom och kollar vilka startplatser som skall vara synliga. 
            foreach (DataGridViewRow row in dgvStartplatser.Rows)
            {
                if (this.VisibleStartplatsIDs.Contains(row.Cells[0].Value.ToString()))
                    row.Visible = true;
                else
                    row.Visible = false;
            }
        }

        protected virtual void SelectStartplatser()
        {
            //dgvStartplatser.ClearSelection();

            //foreach (string startplatsId in SelectedStartplatsIDs)
            //{
            //    foreach (DataGridViewRow row in dgvStartplatser.Rows)
            //    {
            //        if (row.Cells[0].Value.ToString().Equals(startplatsId))
            //            row.Selected = true;
            //    }
            //}

            dgvStartplatser.ClearSelection();

            // Loopar igenom och kollar vilka startplatser som skall vara synliga. 
            foreach (DataGridViewRow row in dgvStartplatser.Rows)
            {
                if (this.SelectedStartplatsIDs.Contains(row.Cells["OrderID"].Value.ToString()))
                    row.Selected = true;
                else
                    row.Selected = false;
            }
        }

        public void Karta_UnselectAllStartplatser(object sender, EventArgs e)
        {
            VisibleStartplatsIDs.Clear();
            ShowVisibleStartplatser(); // Väljer inga nya startplatser, d.v.s avselekterar alla. 
            btnShowInMap.Enabled = true;
        }

        public void Karta_SeletedStartplatser(object sender, SelectedStartplatserEventArgs e)
        {
            VisibleStartplatsIDs = (List<string>)e.StartplatsIDs;
            ShowVisibleStartplatser();
        }

        private void FormAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        /// <summary>
        /// Tar hand om klick när miljön försätt i testläge. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxTestMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxTestMode.Checked)
            {
                _configuration.TestMode = "true";
                lblTestMode.Text = "Standardläge innebär att nästa gång SG-GIS startas kommer startplatser att hämtas från vanliga miljön istället för skarpa miljön";
            }
            else
            {
                _configuration.TestMode = "false";
                lblTestMode.Text = "Testläge innebär att nästa gång SG-GIS startas kommer startplatser att hämtas från testmiljön istället för skarpa miljön";
            }
        }

        private void tabTest_Click(object sender, EventArgs e)
        {
            if (_configuration.IsInTestMode)
                cbxTestMode.Checked = true;
            else
                cbxTestMode.Checked = false;
        }

        private void btnClearTestData_Click(object sender, EventArgs e)
        {
			Foretag testForetag = new Foretag(true);
			
        }

		public void FormAdmin_SynchronizationFinished(object sender, SynchronizationFinishedEventArgs e)
		{
			List<string> orderIDs = new List<string>();
			foreach (DataRow row in e.Foretag.Rows)
				orderIDs.Add(row["OrderID"].ToString());

			foretag.ReFillDataGridView(ForetagFromMySQL, dgvForetag, orderIDs);
		}
	}
}
