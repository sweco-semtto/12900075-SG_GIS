using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SGAB.SGAB_Database
{
    public class Startplats : MySqlCommunicator, IMySqlCommunicator, IAccessCommunicator
    {
        protected string tableNameInAccess = "Startplats";
        protected string startplatsNameInMySql = "Startplats";
		protected string startplatsNameInMyAccess = "Startplats";
		protected string startplatsNameInPHP = "Startplats_Startplats";
        protected string startplatsIdInAccess = "ID";
        protected string startplatsIdInPHP = "ID_Access";
        string ColumnNameDateInMySql = "Bestallningsdatum";
        string ColumnNameDateInAccess = "Beställningsdatum";
       
        /// <summary>
        /// Företagen som finns i Access. 
        /// </summary>
        public DataTable ForetagFromAccess
        {
            get;
            set;
        }

        public DataTable ForetagFromMySql
        {
            get;
            set;
        }

        public string StandardColumnToSortAscending
        {
            get
            {
                return "Ordernr";
            }
        }
        /// <summary>
        /// Id-kolumnens namn i Access. 
        /// </summary>
        protected string IdColumnNameInAccess
        {
            get;
            set;
        }

        /// <summary>
        /// Anger vad ordernr-taggen heter i Access. 
        /// </summary>
        protected string OrdernrColumnInAccess
        {
            get;
            set;
        }

		/// <summary>
		/// Anger vad ordernr-taggen heter i Access. 
		/// </summary>
		protected string OrdernrColumnInMySql
		{
			get;
			set;
		}

		/// <summary>
		/// Anger vilka kolumnan som fins i MySql men som inte finns i Access. 
		/// </summary>
		protected List<string> ColumnsNotFoundInAccess
        {
            get;
            set;
        }

        /// <summary>
        /// Översätter kolumnnamnen ifrån PHP till MySql. 
        /// </summary>
        protected TranslateColumnNames TranslatePHPIntoMySql
        {
            get;
            set;
        }

        /// <summary>
        /// Översätter kolumnnamnen från PHP till gridviewrubriker.
        /// </summary>
        protected TranslateColumnNames TranslatePHPIntoGridHeader
        {
            get;
            set;
        }

        /// <summary>
        /// Anger i vilken mapp offlineuppdateringarna ligger. 
        /// </summary>
        public static string LoggFolder
        {
            get;
            set;
        }

        /// <summary>
        /// Skapar en ny kommunikation mellan dator och databas för Startplatser.
        /// </summary>
        public Startplats()
        {
            // Kolumnnamnen för Entreprenörer
            ColumnNamesFromPHP = new List<string>();

            // Kolumnnamn som skall döljas i gränssnittet. 
            ColumnNamesFromPHPToHide = new List<string>();
            ColumnNamesFromPHPToHide.Add("ID");
            ColumnNamesFromPHPToHide.Add("ID_Access");
            ColumnNamesFromPHPToHide.Add("OrderID");
            ColumnNamesFromPHPToHide.Add("Nordligkoordinat_startplats");
            ColumnNamesFromPHPToHide.Add("Ostligkoordinat_startplats");
            ColumnNamesFromPHPToHide.Add("Bestallningsdatum"); // Kommer ifrån en inner join med Foretag. 
			ColumnNamesFromPHPToHide.Add("Borttagen");

			// Kolumnnamn som inte finns i Access utan bara i MySql
			ColumnsNotFoundInAccess = new List<string>();
            ColumnsNotFoundInAccess.Add("ID"); // Finns i Access men betyder inte samma sak
            ColumnsNotFoundInAccess.Add("Kommentar1");
            ColumnsNotFoundInAccess.Add("Kommentar2");
            ColumnsNotFoundInAccess.Add("Kommentar3");
            ColumnsNotFoundInAccess.Add("Kommentar4");
            ColumnsNotFoundInAccess.Add("Fraktentreprenors_ID");
            ColumnsNotFoundInAccess.Add("Spridningsentreprenors_ID");
            ColumnsNotFoundInAccess.Add("Foretagsnamn"); // Är ifrån Företag men kommer in med via en inner join. 
            ColumnsNotFoundInAccess.Add("Bestallningsdatum"); // Är ifrån Företag men kommer in med via en inner join.
            ColumnsNotFoundInAccess.Add("Borttagen");

            SortColumnValuesAsIntegers = new List<string>();
            SortColumnValuesAsIntegers.Add("Ordernr");

            // Anger vad xml-taggen/datatabellsnamnet heter. 
            XMLNameForDataTable = "Startplats";

            // Anger vad Id-taggen heter i PHP. 
            IdColumnNameInMySql = "OrderID";

            // Anger vad Id-taggen heter i Access
            IdColumnNameInAccess = "ID";

            // Anger vad ordernr-taggen heter i Access
            OrdernrColumnInAccess = "Ordernr";

			// Anger vad ordernr-taggen heter i MySql
			OrdernrColumnInMySql = "Ordernr";

			// Anger vilka skript som gäller
			Url_To_Script_Delete = "http://www.sg-systemet.com/bestallning/PHP/deleteStartplats.php";
            Url_To_Script_Insert = "http://www.sg-systemet.com/bestallning/PHP/insertStartplats.php";
            Url_To_Script_Select = "http://www.sg-systemet.com/bestallning/PHP/selectStartplats.php";
            Url_To_Script_Update = "http://www.sg-systemet.com/bestallning/PHP/updateStartplats.php";

            // Anger skriptnamn som skall synas i ett, ev. felmeddelande om det inte går att hämta startplatser. 
            MessageErrorScriptName = "SelectStartplats.php";

            // Anger en översättning av kolumnamnen ifrån MySql till Access
            TranslationColumnNamesFromMySql = new TranslateColumnNames();
            TranslationColumnNamesFromMySql.AddTranslation("Ingaende_Objekt", "Ingående_Objekt");
            TranslationColumnNamesFromMySql.AddTranslation("ID_Access", "ID");

            // Anger en översättning av kolumnamn ifrån PHP till MySql. 
            TranslatePHPIntoMySql = new TranslateColumnNames();
            TranslatePHPIntoMySql.AddTranslation("Startplats_startplats", "Startplats");

            // Anger en översättning av kolumnnamn från PHP till gridviewrubriker
            TranslatePHPIntoGridHeader = new TranslateColumnNames();
            TranslatePHPIntoGridHeader.AddTranslation("Startplats_startplats", "Startplats");
            TranslatePHPIntoGridHeader.AddTranslation("Nordligkoordinat_startplats", "Nordlig koordinat");
            TranslatePHPIntoGridHeader.AddTranslation("Ostligkoordinat_startplats", "Ostlig koordinat");
            TranslatePHPIntoGridHeader.AddTranslation("Areal_ha_startplats", "Areal ha");
            TranslatePHPIntoGridHeader.AddTranslation("Skog_CAN_ton_startplats", "Skog CAN ton");
            TranslatePHPIntoGridHeader.AddTranslation("Ingaende_Objekt", "Ingående objekt");
            TranslatePHPIntoGridHeader.AddTranslation("Fraktentreprenors_ID", "Fraktentreprenör");
            TranslatePHPIntoGridHeader.AddTranslation("Spridningsentreprenors_ID", "Spridningsentreprenör");
        }

        public Startplats(bool testmode) : this()
        {
            if (testmode)
            {
                // Anger vilka skript som gäller
                Url_To_Script_Delete = "http://www.sg-systemet.com/bestallning/PHP/deleteStartplatsTest.php";
                Url_To_Script_Insert = "http://www.sg-systemet.com/bestallning/PHP/insertStartplatsTest.php";
                Url_To_Script_Select = "http://www.sg-systemet.com/bestallning/PHP/selectStartplatsTest.php";
                Url_To_Script_Update = "http://www.sg-systemet.com/bestallning/PHP/updateStartplatsTest.php";
            }
        }

        /// <summary>
        /// Läser upp tabell från Accessdatabasen samt läser in kolumnnamnen i en lista.
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GetAllFromAccess()
        {
            DataTable dt = AccessCommunicator.GetAllFromAccess(tableNameInAccess);
            return dt;
        }

        /// <summary>
        /// Hämtar alla startplatser ifrån MySql
        /// </summary>
        /// <returns></returns>
        public override DataTable GetAllFromMySql()
        {
            DataTable dataFromMySql = base.GetAllFromMySql();

            // Om datatabellen är null, byggs en tom datatabell upp
            if (dataFromMySql == null)
                dataFromMySql = CreateEmptyMySqlDataTable();

            // Lägger in alla kolumnnamn
            if (ColumnNamesFromPHP != null)
            {
                ColumnNamesFromPHP.Clear();
                foreach (DataColumn column in dataFromMySql.Columns)
                    ColumnNamesFromPHP.Add(column.ColumnName);
            }

            // Om ingen data finns i datatabellen, bygger vi upp kolumnerna i den. 
            return dataFromMySql;
        }

        /// <summary>
        /// Skapar en ny tom datatabell med rätt kolumndefinitioner att använda när det inte finns några företag för året. 
        /// </summary>
        /// <returns></returns>
        protected virtual DataTable CreateEmptyMySqlDataTable()
        {
            DataTable emptyMySqlDataTable = new DataTable();
            emptyMySqlDataTable.Columns.Add("ID", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("ID_Access", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("OrderID", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Ordernr", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Startplats_startplats", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Nordligkoordinat_startplats", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Ostligkoordinat_startplats", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Areal_ha_startplats", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Skog_CAN_ton_startplats", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Ingaende_Objekt", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Kommentar1", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Kommentar2", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Kommentar3", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Kommentar4", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Status", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Fraktentreprenors_ID", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Spridningsentreprenors_ID", System.Type.GetType("System.String"));

            return emptyMySqlDataTable;
        }

        /// <summary>
        /// Fyller på enast med startplatser som stämmer överrens med OrderId:na.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataGridView"></param>
        public virtual void ReFillDataGridView(DataTable data, DataGridView dataGridView, List<string> OrderIDs)
        {
            dataGridView.Rows.Clear();
            foreach (DataRow row in data.Rows)
            {
				// Läger bara till order om det är rätt order-id och raden inte är borttagen. 
                if (OrderIDs.Contains(row["OrderID"].ToString()) && row["Borttagen"].Equals("0"))
                {
                    int newRowIndex = dataGridView.Rows.Add();
                    for (int itemNo = 0; itemNo < Math.Min(row.ItemArray.Length, dataGridView.Columns.Count); itemNo++)
                        dataGridView.Rows[newRowIndex].Cells[itemNo].Value =
                            TranslatorMySqlAndAccess.MySql_To_Access(row.ItemArray[itemNo].ToString());
                }
            }
        }
        
        /// <summary>
        /// Överlagrar standardmetoden för att lägga till rader. 
        /// </summary>
        /// <param name="dataFromMySql"></param>
        /// <param name="dataToUpload"></param>
        protected override void InsertRows(DataTable dataFromMySql, DataTable dataFromAccess)
        {
            // Tar fram värden som skall läggas till i MySql
            List<List<KeyValuePair<string, string>>> valuesToInsert = new List<List<KeyValuePair<string, string>>>();

            // Kollar om vi skall lägga till en rad ifrån Access
            foreach (DataRow startplatsRow in dataFromAccess.Rows)
            {
                // Hämtar orderid från MySql.
                string ordernrFromAccess = startplatsRow[OrdernrColumnInAccess].ToString();
                int orderYearFromAccess = GetYearFromOrdernr(ordernrFromAccess);
                string orderIdInMySql = GetOrderIdFromMySql(ForetagFromMySql, ordernrFromAccess, orderYearFromAccess);
                string IdInAccess = startplatsRow[startplatsIdInAccess].ToString();
				string NameStartplats = startplatsRow[startplatsNameInMySql].ToString();

                if (orderIdInMySql.Equals(string.Empty))
                    continue; // Felhantering om att Företag måste synkroniseras först, alt. att företag synkas först innan denna synk. körs. 

				int a = 0;
				if (orderIdInMySql == "798")
					a = 1;

                // Jämför startplatser ifrån Access med MySql.
                int orderYearFromMySql = GetOrderYearFromMySql(ForetagFromMySql, orderIdInMySql);
                bool startplatsDoesNotExixtsInMySql = !StartplatsExistsInMySQL(dataFromMySql, orderIdInMySql, NameStartplats);
                if (orderYearFromAccess == orderYearFromMySql && startplatsDoesNotExixtsInMySql)
                {
                    List<KeyValuePair<string, string>> rowToInsert = new List<KeyValuePair<string, string>>();

                    // Lägger inte till värdet för ID eftesom det ändå skall automatgenereras i MySql-databasen. 
                    foreach (string columnName in ColumnNamesFromPHP)
                    {
                        string columnNameInMySql = TranslationColumnNamesFromMySql.TranslateAToB(columnName);
                        string columnNameInAccess = TranslatePHPIntoMySql.TranslateAToB(columnNameInMySql);

                        // startplatsRow[TranslationColumnNamesFromMySql.TranslateAToB(TranslatePHPIntoMySql.TranslateAToB(columnName))]
                        
                        // Eftersom vi behöver översätta både ifrån PHP pch ifrån MySql till Access krävs två översättningar. 
                        if (!ColumnsNotFoundInAccess.Contains(columnName) &&
                            columnNameInMySql != IdColumnNameInMySql &&
                            startplatsRow[columnNameInAccess] != null)
                        {
                            if (startplatsRow[columnNameInAccess].GetType().FullName.Equals("System.Double"))
                            {
                                double value = double.Parse(startplatsRow[columnNameInAccess].ToString());
                                rowToInsert.Add(new KeyValuePair<string, string>(
                                    TranslatePHPIntoMySql.TranslateAToB(columnName),
                                    value.ToString(base.DecimalSepatorForMySql)));
                            }
                            else
                                rowToInsert.Add(new KeyValuePair<string, string>(
                                    TranslatePHPIntoMySql.TranslateAToB(columnName),
                                    TranslatorMySqlAndAccess.Access_To_MySql(startplatsRow[columnNameInAccess].ToString())));
                        }
                        else if (!ColumnsNotFoundInAccess.Contains(columnName) && columnName.Equals(IdColumnNameInMySql))
                            rowToInsert.Add(new KeyValuePair<string, string>(
                                IdColumnNameInMySql,
                                orderIdInMySql));

                    }

                    // Lägger till raden om den inte är tom, t.ex. som sista raden i gränssnittet är.
                    int blankCounter = 0;
                    foreach (KeyValuePair<string, string> keyValue in rowToInsert)
                        if (keyValue.Value == "" || keyValue.Value == null)
                            blankCounter++;

                    if (blankCounter < rowToInsert.Count)
                        valuesToInsert.Add(rowToInsert);
                }
            }
            
            SendRequest(valuesToInsert, Url_To_Script_Insert);
        }

        /// <summary>  
        /// Tar fram värden som skall uppdateras i MySql och genomför uppdatering via PHP-anrop
        /// (ej synkronisering).
        /// </summary>
        /// <param name="dataFromMySql"></param>
        /// <param name="dataToUpload">T ex data från gridview</param>
        public void UpdateRowsInMySql_Bind(DataTable dataFromMySql, DataTable dataToUpload)
        {                      
            List<List<KeyValuePair<string, string>>> valuesToUpdate = new List<List<KeyValuePair<string, string>>>();

            // Kollar alla rader efter skillnader. 
            foreach (DataRow MySqlRow in dataFromMySql.Rows)
            {
                foreach (DataRow startplatsRow in dataToUpload.Rows)
                {
                    //Nyckel i MySql: OrderID och ID_Access 
                    string orderIdMySql = startplatsRow[IdColumnNameInMySql].ToString();
                    string accessIdInMySql = startplatsRow[startplatsIdInPHP].ToString();
                   
                    // Om vi hittar raden undersöks om raden skall uppdaterats. 
                    if (MySqlRow[IdColumnNameInMySql].ToString().Equals(startplatsRow[IdColumnNameInMySql].ToString()) && 
                        MySqlRow[startplatsIdInPHP].ToString().Equals(startplatsRow[startplatsIdInPHP].ToString()))
                    {                       
                        // Jämför alla kolumner för att hitta skillnader. 
                        foreach (string columnName in ColumnNamesFromPHP)
                        {
                            // Skall ej kolla kolumnerna ID, OrderID och ID_Access i MySQL 
                            if (columnName.Equals(IdColumnNameInMySql) ||
                                columnName.Equals(startplatsIdInPHP) ||
                                columnName.Equals("ID"))
                                continue;

                            // Kollar efter förändringar
                            string valueToUpload = startplatsRow[columnName].ToString();
                            string valueMySql = TranslatorMySqlAndAccess.MySql_To_Access(MySqlRow[columnName].ToString());
                            if (!valueToUpload.Equals(valueMySql))
                            {
                                List<KeyValuePair<string, string>> rowToUpdate = new List<KeyValuePair<string, string>>();

                                // Lägger till OrderId
                                rowToUpdate.Add(
                                    new KeyValuePair<string, string>(IdColumnNameInMySql, MySqlRow[IdColumnNameInMySql].ToString()));

                                // Lägger till ID_Access 
                                rowToUpdate.Add(
                                    new KeyValuePair<string, string>(startplatsIdInPHP, MySqlRow[startplatsIdInPHP].ToString()));

                                // Lägger till kolumnamn
                                rowToUpdate.Add(
                                    new KeyValuePair<string, string>("ColumnName", columnName));

                                // Lägger till värdet
                                rowToUpdate.Add(
                                    new KeyValuePair<string, string>("Value", 
                                        TranslatorMySqlAndAccess.Access_To_MySql(startplatsRow[columnName].ToString())));

                                valuesToUpdate.Add(rowToUpdate);
                            }
                        }           
                    }                        
                }
            }

            SendRequest(valuesToUpdate, Url_To_Script_Update);
        }

        /// <summary>
        /// Uppdaterar en startplats status. 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="AccessId"></param>
        /// <param name="Status"></param>
        public void UpdateStartplatsStatusFromMap(string OrderId, string AccessId, int Status, string startplatsId, string user, string note, int fieldStatus)
        {
            List<List<KeyValuePair<string, string>>> valuesToUpdate = new List<List<KeyValuePair<string, string>>>();

            // skapar statusuppdateringen. 
            List<KeyValuePair<string, string>> rowToUpdate = new List<KeyValuePair<string, string>>();

            // Lägger till OrderId
            rowToUpdate.Add(
                new KeyValuePair<string, string>(IdColumnNameInMySql, OrderId));

            // Lägger till ID_Access 
            rowToUpdate.Add(
                new KeyValuePair<string, string>(startplatsIdInPHP, AccessId));

            // Lägger till kolumnamn
            rowToUpdate.Add(
                new KeyValuePair<string, string>("ColumnName", "Status"));

            // Lägger till värdet
            rowToUpdate.Add(
                new KeyValuePair<string, string>("Value", Status.ToString()));

            // Lägger till statusuppdateringen. 
            valuesToUpdate.Add(rowToUpdate);

            // Skapar kommentaren. 
            rowToUpdate = new List<KeyValuePair<string, string>>();

            // Lägger till OrderId
            rowToUpdate.Add(
                new KeyValuePair<string, string>(IdColumnNameInMySql, OrderId));

            // Lägger till ID_Access 
            rowToUpdate.Add(
                new KeyValuePair<string, string>(startplatsIdInPHP, AccessId));

            // Lägger till kommentaren i rätt kolumn. 
            if (fieldStatus == 0)
                rowToUpdate.Add(
                    new KeyValuePair<string, string>("ColumnName", "Kommentar1"));
            else if (fieldStatus == 1)
                rowToUpdate.Add(
                    new KeyValuePair<string, string>("ColumnName", "Kommentar2"));
            else if (fieldStatus == 2)
                rowToUpdate.Add(
                    new KeyValuePair<string, string>("ColumnName", "Kommentar3"));
            else if (fieldStatus == 3)
                rowToUpdate.Add(
                    new KeyValuePair<string, string>("ColumnName", "Kommentar4"));

            rowToUpdate.Add(
                new KeyValuePair<string, string>("Value", TranslatorMySqlAndAccess.Access_To_MySql(note)));


            valuesToUpdate.Add(rowToUpdate);

            // Skickar iväg uppdateringen. 
            try
            {
                if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
                    SendRequest(valuesToUpdate, Url_To_Script_Update);
                else
                    SaveRequest(valuesToUpdate);

            }
            catch (MySqlException mysqlex)
            {
                MessageBox.Show("Lyckades inte skriva uppdateringen till databasen \n\n" + mysqlex, "Misslyckades updatera status", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
            {
                // Skickar iväg vem som har gjort uppdateringen. 
                string userName = Entreprenorer.IdSkogensGödsling;
                if (user != null)
                    userName = user;
                Stati stati = new Stati();
                stati.InsertRow(startplatsId, DateTime.Now.ToString(), userName, Status.ToString());
            }
        }

        /// <summary>
        /// Sparar en request till en fil istället för till databasen, detta när internätanslutningen är nere. 
        /// Kan bara spara updates. 
        /// </summary>
        /// <param name="values"></param>
        protected virtual void SaveRequest(List<List<KeyValuePair<string, string>>> values)
        {
            try
            {
                // Skapar en fil om ingen finns
                if (!File.Exists(LoggFolder + "Statusuppdateringar.txt"))
                {
                    TextWriter newTextWriter = new StreamWriter(LoggFolder + "Statusuppdateringar.txt");
                    newTextWriter.Close();
                }

                TextReader textReader = new StreamReader(LoggFolder + "Statusuppdateringar.txt");
                StringBuilder updates = new StringBuilder(textReader.ReadToEnd());
                textReader.Close();

                // Sparar statusändringen. 
                TextWriter textWriter = new StreamWriter(LoggFolder + "Statusuppdateringar.txt");
                foreach (List<KeyValuePair<string, string>> row in values)
                {
                    string postData = CreatePostDataForPHP(row);
                    updates.Append(postData);
                    updates.Append(textWriter.NewLine);
                }

                textWriter.Write(updates.ToString());
                textWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan ej spara status offline, vänligen kontakta Skogens Gödsling. Inga uppdateringar kan sparas.\n\n" + ex.ToString(), "Kan ej skriva status offline", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public virtual void WriteSavedRequests()
        {
            try
            {
                if (File.Exists(LoggFolder + "Statusuppdateringar.txt"))
                {
                    TextReader textReader = new StreamReader(LoggFolder + "Statusuppdateringar.txt");
                    string[] splits = { "\r\n" };
                    string[] updates = textReader.ReadToEnd().Split(splits, StringSplitOptions.RemoveEmptyEntries);
                    textReader.Close();

                    int errors = 0;
                    string result = String.Empty;
                    MySqlException ex = new MySqlException();

                    foreach (string updateRow in updates)
                    {
                        this.PerformRequest(this.Url_To_Script_Update, ref errors, ref result, ex,
                            new List<KeyValuePair<string, string>>(), updateRow);
                    }
                }

                // Blankar uppdateringesfilen tills nästa gång. 
                TextWriter newTextWriter = new StreamWriter(LoggFolder + "Statusuppdateringar.txt");
                newTextWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan ej skriva offlinestatus, vänligen kontakta Skogens Gödsling. Inga uppdateringar kan sparas.\n\n" + ex.ToString(), "Kan ej skriva status offline", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Överlagrar standardmetoden för att uppdatera rader. 
        /// </summary>
        /// <param name="dataFromMySql"></param>
        /// <param name="dataToUpload"></param>
        protected override void UpdateRows(DataTable dataFromMySql, DataTable dataFromAccess)
        {
            // Tar fram värden som skall uppdateras i MySql. 
            List<List<KeyValuePair<string, string>>> valuesToUpdateStringsInMySql = new List<List<KeyValuePair<string, string>>>();

            // Kollar alla rader efter skillnader. 
            foreach (DataRow MySqlRow in dataFromMySql.Rows)
            {
                foreach (DataRow startplatsRow in dataFromAccess.Rows)
                {
                    // Hämtar orderid från MySql.
                    string startplatsIDFromMySql = MySqlRow["ID"].ToString();
                    string orderIDFromAccess = startplatsRow[IdColumnNameInAccess].ToString();
					string ordernrFromMySql = MySqlRow["OrderNr"].ToString();
					string ordernrFromAccess = startplatsRow[OrdernrColumnInAccess].ToString();
                    int orderYearFromAccess = GetYearFromOrdernr(ordernrFromAccess);
                    string orderIdInMySql = MySqlRow["OrderID"].ToString();
                    string AccessIdInMySql = MySqlRow[startplatsIdInPHP].ToString();

                    if (orderIdInMySql.Equals(string.Empty))
                        continue; // Felhantering om att Företag måste synkroniseras först, alt. att företag synkas först innan denna synk. körs. 

                    // Jämför startplatser ifrån Access med MySql. 
                    int orderYearFromMySql = GetOrderYearFromMySql(ForetagFromMySql, orderIdInMySql);
                    if (orderYearFromAccess == orderYearFromMySql && 
						MySqlRow[startplatsNameInPHP].ToString().Equals(startplatsRow[startplatsNameInMyAccess].ToString()))
                    {
                        // Jämför alla kolumner för att hitta skillnader. 
                        foreach (string columnNameFromPHP in ColumnNamesFromPHP)
                        {
                            string columnNameInMySqlFromPHP = TranslationColumnNamesFromMySql.TranslateAToB(columnNameFromPHP);
                            string columnNameInAccess = TranslatePHPIntoMySql.TranslateAToB(columnNameInMySqlFromPHP);

							// Skall ej kolla kolumnen ID och status i MySQL och kolumnen ID i Access
							if (columnNameFromPHP.Equals(startplatsIdInAccess) ||
								columnNameFromPHP.Equals(startplatsIdInPHP) ||
								columnNameFromPHP.Equals(IdColumnNameInMySql) ||
								columnNameFromPHP.Equals("Status") ||
								columnNameFromPHP.Equals("Ordernr") ||
								ColumnsNotFoundInAccess.Contains(columnNameFromPHP))
							{
								if (dataFromAccess.Columns.IndexOf(columnNameInAccess) == -1 &&
									MySqlRow[columnNameFromPHP].ToString().Equals("1") &&
									MySqlRow[OrdernrColumnInMySql].ToString().Equals(ordernrFromAccess))
								{
									List<KeyValuePair<string, string>> rowToUpdateInMySql = new List<KeyValuePair<string, string>>();

									// Lägger till Order Id
									rowToUpdateInMySql.Add(
										new KeyValuePair<string, string>(IdColumnNameInMySql, MySqlRow[IdColumnNameInMySql].ToString()));

									// Lägger till Access Id
									rowToUpdateInMySql.Add(
										new KeyValuePair<string, string>(startplatsIdInPHP, MySqlRow[startplatsIdInPHP].ToString()));

									// Lägger till kolumnamn
									rowToUpdateInMySql.Add(
										new KeyValuePair<string, string>("ColumnName", TranslationColumnNamesFromMySql.TranslateBToA(columnNameInAccess)));

									// Lägger till värdet. 
									rowToUpdateInMySql.Add(
										new KeyValuePair<string, string>("Value", "0"));

									valuesToUpdateStringsInMySql.Add(rowToUpdateInMySql);
								}

								continue;
							}

                            // Kollar efter förändringar
                            string valueAccess = startplatsRow[columnNameInAccess].ToString();
                            string valuePHP = MySqlRow[columnNameFromPHP].ToString();

                            // Om vi har ett decimaltal skall vi se till att samma decimalseparerare används i MySql och Access
                            if (startplatsRow[columnNameInAccess].GetType().FullName.Equals("System.Double"))
                            {
                                double value = double.Parse(startplatsRow[columnNameInAccess].ToString());
                                valueAccess = value.ToString(base.DecimalSepatorForMySql);
                            }

                            if (!TranslatorMySqlAndAccess.Access_To_MySql(valueAccess).Equals(valuePHP) &&
								MySqlRow[OrdernrColumnInMySql].ToString().Equals(ordernrFromAccess))
                            {
                                List<KeyValuePair<string, string>> rowToUpdateInMySql = new List<KeyValuePair<string, string>>();

                                // Lägger till Order Id
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>(IdColumnNameInMySql, MySqlRow[IdColumnNameInMySql].ToString()));

                                // Lägger till Access Id
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>(startplatsIdInPHP, MySqlRow[startplatsIdInPHP].ToString()));

                                // Lägger till kolumnamn
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>("ColumnName", TranslationColumnNamesFromMySql.TranslateBToA(columnNameInAccess)));

                                // Lägger till värdet. 
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>("Value", valueAccess));

                                valuesToUpdateStringsInMySql.Add(rowToUpdateInMySql);
                            }
                        }
                    }
                }
            }

            SendRequest(valuesToUpdateStringsInMySql, Url_To_Script_Update);
        }

        /// <summary>
        /// Hämtar vilket beställningsår som orden las in. 
        /// </summary>
        /// <param name="Ordernr"></param>
        /// <returns></returns>
        protected int GetYearFromOrdernr(string Ordernr)
        {
            foreach (DataRow rowAccess in ForetagFromAccess.Rows)
                if (rowAccess[OrdernrColumnInAccess].ToString().Equals(Ordernr))
                    return Convert.ToDateTime(rowAccess[ColumnNameDateInAccess].ToString()).Year;

            return 0;
        }

        /// <summary>
        /// Hämtar vilket OrderId ett företag har i MySql, om inget hittar returneras tomma strängen. 
        /// </summary>
        /// <param name="dataFromMySql"></param>
        /// <param name="ordernr"></param>
        /// <param name="orderYear"></param>
        /// <returns></returns>
        protected string GetOrderIdFromMySql(DataTable dataForetagFromMySql, string ordernr, int orderYear)
        {
            foreach (DataRow rowMySqlForetag in dataForetagFromMySql.Rows)
            {
                int yearMySql = Convert.ToDateTime(rowMySqlForetag[ColumnNameDateInMySql].ToString()).Year;

                if (rowMySqlForetag[OrdernrColumnInAccess].Equals(ordernr) && orderYear == yearMySql)
                    return rowMySqlForetag[IdColumnNameInMySql].ToString();
            }

            return String.Empty;
        }

        /// <summary>
        /// Hämtar vilket beställningsår ett orderid har i MySql. 
        /// </summary>
        /// <param name="dataForetragFromMySql">Foretagstabellen ifrån MySql. </param>
        /// <param name="orderid">OrderId som vi skall kolla mot. </param>
        /// <returns></returns>
        protected int GetOrderYearFromMySql(DataTable dataForetragFromMySql, string orderid)
        {
            int yearMySql = -1;

            foreach (DataRow MySqlRow in dataForetragFromMySql.Rows)
                if (orderid.Equals(MySqlRow[IdColumnNameInMySql].ToString()))
                    return Convert.ToDateTime(MySqlRow[ColumnNameDateInMySql].ToString()).Year;

            return yearMySql;
        }

        /// <summary>
        /// Kontroller om en startplats finns med i MySql, obervera att denna metod inte tar hänsyn till beställningsår. 
        /// </summary>
        /// <param name="dataFromMySql"></param>
        /// <param name="orderid"></param>
        /// <param name="nameStartplats"></param>
        /// <returns></returns>
        protected bool StartplatsExistsInMySQL(DataTable dataFromMySql, string orderid, string nameStartplats)
        {
			// Justerar startplatsnamnet, för i databasen finns det inte å, ä och ö
			string nameStartplatsKorr = KorrigeraTecken(nameStartplats);

			foreach (DataRow startplatsRow in dataFromMySql.Rows)
                if (orderid.Equals(startplatsRow[IdColumnNameInMySql].ToString()) &&
					nameStartplatsKorr.Equals(startplatsRow[startplatsNameInPHP].ToString()))
                    return true;

            return false;
        }

		/// <summary>
		/// Korrigerar tecken som xml och ansii har problem med, som t.ex. å, ä och ö.
		/// </summary>
		/// <param name="teckensträngSomSkallJusteras"></param>
		/// <returns></returns>
		protected string KorrigeraTecken(string teckensträngSomSkallJusteras)
		{
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("å", "%aring");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("Å", "%Aring");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("ä", "%auml");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("Ä", "%Auml");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("ö", "%ouml");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("Ö", "%Ouml");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("ü", "%uuml");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("Ü", "%Uuml");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("û", "%ucirc");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("Û", "%Ucirc");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("é", "%egrave");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("É", "%Egrave");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("&", "%amp");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("<", "%lt");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace(">", "%gt");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("\"", " %quot");
			teckensträngSomSkallJusteras = teckensträngSomSkallJusteras.Replace("'", "%#39");

			return teckensträngSomSkallJusteras;
		}

        /// <summary>
        /// Tar bort rader i MySql. 
        /// </summary>
        /// <param name="dataFromMySql">Data som finns i MySql-databasen. </param>
        /// <param name="dataToUpload">Data som ska laddas upp, t ex från gränssnittet eller från Access-databasen.</param>
        protected override void DeleteRows(DataTable dataFromMySql, DataTable dataFromAccess)
        {
            // Tar fram Id som skall tas bort 
            List<List<KeyValuePair<string, string>>> valuesToDelete = new List<List<KeyValuePair<string, string>>>();

            // Tar fram alla idnummer ifrån Access
            List<string> IdsAccess = new List<string>();
            foreach (DataRow rowFromAccess in dataFromAccess.Rows)
                IdsAccess.Add(rowFromAccess[IdColumnNameInAccess].ToString());

            int year = DateTime.Now.Year - 1;
            string date = "#10/1/" + year + "#"; // Tar inte nyår som brytdatum utan 1:a oktober gäller. 
            foreach (DataRow rowFromMySql in dataFromMySql.Rows)
            {
                if (DateTime.Parse(rowFromMySql[ColumnNameDateInMySql].ToString()) > DateTime.Parse(date) &&
                    !IdsAccess.Contains(rowFromMySql[startplatsIdInPHP].ToString()))
                {
                    // Tar bort startplatsen, är en lista i en lista för att matcha metoden SendRequest
                    List<KeyValuePair<string, string>> rowToDelete = new List<KeyValuePair<string, string>>();
                    rowToDelete.Add(new KeyValuePair<string, string>(IdColumnNameInMySql, rowFromMySql[IdColumnNameInMySql].ToString()));
                    valuesToDelete.Add(rowToDelete);
                }
            }

            SendRequest(valuesToDelete, Url_To_Script_Delete);
        }

        /// <summary>
        /// Lyssnare för sortering.
        /// Sorterar på kolumner OrderID OCH Startplats; OrderID - siffra, Startplats - sträng
        /// </summary>
        public override void DataGridView_SortCompare_Integers(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name.Equals(IdColumnNameInMySql))
            {                
                if (int.Parse(e.CellValue1.ToString()) > int.Parse(e.CellValue2.ToString()))
                    e.SortResult = 1;
                else if (int.Parse(e.CellValue1.ToString()) < int.Parse(e.CellValue2.ToString()))
                    e.SortResult = -1;
                else
                {
                    //Sorterar rader som tillhör ett orderID utifrån startplatsbokstäver.                    
                    DataGridView dg = (DataGridView)sender;
                    string startplats1 = dg.Rows[e.RowIndex1].Cells["Startplats_startplats"].Value.ToString();
                    string startplats2 = dg.Rows[e.RowIndex2].Cells["Startplats_startplats"].Value.ToString(); 
                    if (startplats1.CompareTo(startplats2) > 0) //startplats1 kommer efter startplats2
                        e.SortResult = 1;
                    else if (startplats1.CompareTo(startplats2) < 0) //startplats1 kommer före startplats2
                        e.SortResult = -1;
                    else 
                        e.SortResult = 0;
                }
            }
            else
                base.DataGridView_SortCompare_Integers(sender, e);
          
            // Anger att standardsorteringen inte behöver användas. 
            e.Handled = true;
        }

        /// <summary>
        /// Sätter kolumnnamn i gridview utifrån översättningslista.
        /// </summary>
        /// <param name="dg"></param>
        public void SetColumnHeaderTexts(DataGridView dg)
        { 
            foreach (DataGridViewColumn col in dg.Columns)
            {
                string colHeader = TranslatePHPIntoGridHeader.TranslateAToB(col.HeaderText);
                col.HeaderText = colHeader;                
            }
        }


        /// <summary>
        /// Översätter entreprenörsid:n till klartext och uppdaterar gridview.
        /// </summary>
        /// <param name="dgvStartplatser"></param>
        /// <param name="entreprenorer"></param>
        public void TranslateEntreprenor(DataGridView dgv, List<Entrepreneur> entreprenorer)
        {
            if (dgv == null)
                return;

            foreach (DataGridViewRow row in dgv.Rows)
            {                 
                string id = row.Cells["Fraktentreprenors_ID"].Value.ToString();
                Entrepreneur entrepreneur = Entrepreneur.FindEntrepreneurById(entreprenorer, id);
                row.Cells["Fraktentreprenors_ID"].Value = entrepreneur == null ? String.Empty : entrepreneur.Name;

                id = row.Cells["Spridningsentreprenors_ID"].Value.ToString();
                entrepreneur = Entrepreneur.FindEntrepreneurById(entreprenorer, id);
                row.Cells["Spridningsentreprenors_ID"].Value = entrepreneur == null ? String.Empty : entrepreneur.Name;
            }
        }

        public void TranslateLoggEntreprenorer(DataGridView dgv, List<Entrepreneur> entreprenorer)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                string id = row.Cells["AndradAv"].Value.ToString();
                Entrepreneur entrepreneur = findEntrepreneurWithSGInList(entreprenorer, id);
                row.Cells["AndradAv"].Value = entrepreneur == null ? String.Empty : entrepreneur.Name;
            }
        }

        private Entrepreneur findEntrepreneurWithSGInList(List<Entrepreneur> entreprenorer, string idToFind)
        {
            Entrepreneur entreprenor = null;

            entreprenor = entreprenorer.Find(
                            delegate(Entrepreneur ent)
                            {
                                return ent.Id == idToFind;
                            }
                            );

            return entreprenor;
        }
    }
}
