using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SGAB.SGAB_Database
{
    public class Foretag : MySqlCommunicator, IMySqlCommunicator, IAccessCommunicator, IXmlChecker
    {
        protected string tableNameInAccess = "Företag";
        string ColumnNameDateInMySql = "Bestallningsdatum";
        string ColumnNameDateInAccess = "Beställningsdatum";

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
        /// Anger vilka kolumnan som fins i MySql men som inte finns i Access. 
        /// </summary>
        protected List<string> ColumnsNotFoundInAccess
        {
            get;
            set;
        }
       
        /// <summary>
        /// Skapar en ny kommunikation mellan dator och databas för Foretag. 
        /// </summary>
        public Foretag()
        {
            // Kolumnnamnen för Foretag
            ColumnNamesFromPHP = new List<string>();

            IdColumnNameInMySql = "OrderID";
            IdColumnNameInAccess = "Ordernr";
            
            IdColumns = new List<string>();
            IdColumns.Add(IdColumnNameInAccess);
            IdColumns.Add(ColumnNameDateInMySql);
            
            // Kolumnnamn som skall döljas i gränssnittet. 
            ColumnNamesFromPHPToHide = new List<string>();
            ColumnNamesFromPHPToHide.Add(IdColumnNameInMySql);

            // Anger vad xml-taggen/datatabellsnamnet heter. 
            XMLNameForDataTable = "Foretag";          

            // Anger vilka skript som gäller
            Url_To_Script_Delete = "https://www.sg-systemet.com/bestallning/PHP/DeleteForetag.php";
            Url_To_Script_Insert = "https://www.sg-systemet.com/bestallning/PHP/InsertForetag.php";
            Url_To_Script_Select = "https://www.sg-systemet.com/bestallning/PHP/SelectForetag.php";
            Url_To_Script_Update = "https://www.sg-systemet.com/bestallning/PHP/UpdateForetag.php";

            // Anger skriptnamn som skall synas i ett, ev. felmeddelande om det inte går att hämta företag. 
            MessageErrorScriptName = "SelectForetag.php";

            ColumnsNotFoundInAccess = new List<string>();
            ColumnsNotFoundInAccess.Add("OrderID");
            ColumnsNotFoundInAccess.Add("Borttagen");

            // Anger vilka kolumner som har värden som skall sorteras som om de vore heltal.
            SortColumnValuesAsIntegers = new List<string>();
            SortColumnValuesAsIntegers.Add("Ordernr");

            // Anger en översättning av kolumnamnen ifrån MySql till Access
            TranslationColumnNamesFromMySql = new TranslateColumnNames();
            TranslationColumnNamesFromMySql.AddTranslation("Bestallningsreferens", "Beställningsreferens");
            TranslationColumnNamesFromMySql.AddTranslation("Bestallningsdatum", "Beställningsdatum");
            TranslationColumnNamesFromMySql.AddTranslation("Tidsstampel", "Tidsstämpel");
            TranslationColumnNamesFromMySql.AddTranslation("Foretagsnamn", "Företagsnamn");
            TranslationColumnNamesFromMySql.AddTranslation("Region_Forvaltning", "Region_Förvaltning");
            TranslationColumnNamesFromMySql.AddTranslation("Distrikt_Omrade", "Distrikt_Område");
            TranslationColumnNamesFromMySql.AddTranslation("Lan", "Län");
        }

        public Foretag(bool testmode) : this()
        {
            if (testmode)
            {
                // Anger vilka skript som gäller
                Url_To_Script_Delete = "https://www.sg-systemet.com/bestallning/PHP/DeleteForetagTest.php";
                Url_To_Script_Insert = "https://www.sg-systemet.com/bestallning/PHP/InsertForetagTest.php";
                Url_To_Script_Select = "https://www.sg-systemet.com/bestallning/PHP/SelectForetagTest.php";
                Url_To_Script_Update = "https://www.sg-systemet.com/bestallning/PHP/UpdateForetagTest.php";
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
        /// Hämtar alla företag ifrån MySql. 
        /// </summary>
        /// <returns></returns>
        public override DataTable GetAllFromMySql()
        {
            DataTable dataFromMySql = base.GetAllFromMySql();

            // Om datatabellen är null, byggs en tom datatabell upp
            if (dataFromMySql == null)
               dataFromMySql =  CreateEmptyMySqlDataTable();

            // Lägger in alla kolumnnamn
            if (ColumnNamesFromPHP != null)
            {
                ColumnNamesFromPHP.Clear();
                foreach (DataColumn column in dataFromMySql.Columns)
                    ColumnNamesFromPHP.Add(column.ColumnName);
            }

            return dataFromMySql;
        }

        /// <summary>
        /// Skapar en ny tom datatabell med rätt kolumndefinitioner att använda när det inte finns några företag för året. 
        /// </summary>
        /// <returns></returns>
        protected virtual DataTable CreateEmptyMySqlDataTable()
        {
            DataTable emptyMySqlDataTable = new DataTable();
            emptyMySqlDataTable.Columns.Add("OrderID", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Ordernr", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Bestallningsreferens", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Bestallningsdatum", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Tidsstampel", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Foretagsnamn", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Faktureringsadress", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Postnummer", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Ort", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Region_Forvaltning", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Distrikt_Omrade", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("VAT", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Kontaktperson1", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("TelefonArb1", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("TelefonMobil1", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("TelefonHem1", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Epostadress1", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Kontaktperson2", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("TelefonArb2", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("TelefonMobil2", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("TelefonHem2", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Epostadress2", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("Kommentar", System.Type.GetType("System.String"));
            emptyMySqlDataTable.Columns.Add("OrdernrText", System.Type.GetType("System.String"));

            return emptyMySqlDataTable;
        }

        /// <summary>
        /// Fyller på enadt med startplatser som stämmer överrens med ORderId:na.
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

                    // Börjar på 1 för ID-numret från Access är inte längre ordernumret. Därmed har Access 25 rader och MySql 24 rader (OrderId i MySql hämtas inte). 
                    for (int itemNo = 1; itemNo < Math.Min(row.ItemArray.Length, dataGridView.Columns.Count); itemNo++)
                        dataGridView.Rows[newRowIndex].Cells[itemNo].Value =
                            TranslatorMySqlAndAccess.MySql_To_Access(row.ItemArray[itemNo].ToString());
                }
            }
        }

        /// <summary>
        /// Lägger till rader som fattas till MySql. 
        /// </summary>
        /// <param name="dataFromMySql">Data som finns i MySql-databasen. </param>
        /// <param name="tableToUpload">Data som ska laddas upp, t ex från gränssnittet eller från Access-databasen. </param>
        protected override void InsertRows(DataTable dataFromMySql, DataTable dataFromAccess)
        {
            // Värden som skall läggas till
            List<List<KeyValuePair<string, string>>> valuesToInsert = new List<List<KeyValuePair<string, string>>>();
            
            // tar fram de båda Access-nycklarna som finns för Företag ur MySql. 
            List<string> IDsInMySql = new List<string>();
            List<string> DatesInMySql = new List<string>();
            foreach (DataRow MySqlRow in dataFromMySql.Rows)
            {
                IDsInMySql.Add(MySqlRow[IdColumnNameInAccess].ToString());
                DatesInMySql.Add(MySqlRow[ColumnNameDateInMySql].ToString());
            }
        
            // Jämför nyckeln för de rader som finns i tabellen som ska laddas upp, med de rader som redan finns i MySql.
            foreach (DataRow rowFromAccess in dataFromAccess.Rows)
            {
                bool addToMySql = false;
                List<int> indiciesInAccess = new List<int>();

                int indexRowAccess = -1; // Finns inget index sedan innan, .d.v.s. raden i Access finns inte i MySql
                if (IDsInMySql.Count != 0)
                    indexRowAccess = IDsInMySql.IndexOf(rowFromAccess[IdColumnNameInAccess].ToString());

                // Kollar om det finns mer än ett års index som matchar, måste kontrollera samtliga. 
                if (indexRowAccess >= 0)
                {
                    indiciesInAccess.Add(indexRowAccess);
                    while (IDsInMySql.IndexOf(rowFromAccess[IdColumnNameInAccess].ToString(), indexRowAccess + 1) >= 0)
                    {
                        indexRowAccess = IDsInMySql.IndexOf(rowFromAccess[IdColumnNameInAccess].ToString(), indexRowAccess + 1);
                        indiciesInAccess.Add(indexRowAccess);
                    }
                }

                // Om inte ordernummret finns emd sedan innan lägg till det. 
                if (indexRowAccess == -1) // Hittade inget index sedan innan
                    addToMySql = true;
                // Om ordernummret finns med sedan innan men har ett annat år kopplat till sig skall det läggas med. 
                //else if (indexRowAccess >= 0) // Hittar ett index
                foreach (int indexInAccess in indiciesInAccess)
                {
                    int yearMySql = Convert.ToDateTime(DatesInMySql[indexInAccess].ToString()).Year;
                    int yearAccess = Convert.ToDateTime(rowFromAccess[ColumnNameDateInAccess].ToString()).Year;

                    // Om årets upplaga av ordernummret finns inte med, skall raden kanske läggas till. Måste kolla alla rader först. 
                    if (yearMySql != yearAccess)
                        addToMySql = true;

                    // Om årets upplaga finns med se till att inte lägga till
                    if (yearMySql == yearAccess)
                    {
                        addToMySql = false;
                        break;
                    }
                }
                if (addToMySql)
                {
                    List<KeyValuePair<string, string>> rowToInsert = new List<KeyValuePair<string, string>>();

                    //// Om testdatabasen är tom, läs in den hårdkodade tabelldefinitionen.
                    //if (ColumnNamesFromPHP == null)
                    //{
                    //    // Hämtar tabelldefinitionen, eftersom internetdatabasen är dom. 
                    //    DataTable tmp = CreateEmptyMySqlDataTable();
                       
                    //    // Fyller på vilka kolumner det finns
                    //    ColumnNamesFromPHP = new List<string>();
                    //    foreach (DataColumn column in tmp.Columns)
                    //        ColumnNamesFromPHP.Add(column.ColumnName);
                    //}

                    // Lägger inte till värdet för ID eftersom det ändå skall automatgenereras i MySql-databasen. 
                    for (int i = 0; i < ColumnNamesFromPHP.Count; i++)
                    {
                        if (ColumnNamesFromPHP[i] == IdColumnNameInMySql || ColumnsNotFoundInAccess.Contains(ColumnNamesFromPHP[i]))
                            continue;

                        if (rowFromAccess[i] == null)
                            continue;

                        rowToInsert.Add(
                            new KeyValuePair<string, string>(
                                ColumnNamesFromPHP[i],
                                TranslatorMySqlAndAccess.Access_To_MySql(rowFromAccess[i].ToString()
                        )));
                    }

                    valuesToInsert.Add(rowToInsert);
                }
            }

            SendRequest(valuesToInsert, Url_To_Script_Insert);
        }

        /// <summary>
        /// Uppdaterar rader som ändras i MySql. 
        /// </summary>
        /// <param name="dataFromMySql">Data som finns i MySql-databasen. </param>
        /// <param name="tableToUpload">Data som ska laddas upp, t ex från gränssnittet eller från Access-databasen.</param>
        protected override void UpdateRows(DataTable dataFromMySql, DataTable dataFromAccess)
        {
            // Tar fram värden som skall uppdateras i MySql. 
            List<List<KeyValuePair<string, string>>> valuesToUpdateStringsInMySql = new List<List<KeyValuePair<string, string>>>();

            // Jämför ID-värdet för de rader som finns i gränssnittet med de rader som finns i MySql. 
            foreach (DataRow MySqlRow in dataFromMySql.Rows)
            {
                foreach (DataRow rowFromAccess in dataFromAccess.Rows)
                {
                    // När vi har sista raden i gränssnittet som alltid är tom hoppar vi över den. 
                    if (rowFromAccess[IdColumnNameInAccess] == null)
                        break;

                    // Tar fram åren ur MySql respektive Access för att jämföra dem. 
                    int yearMySql = Convert.ToDateTime(MySqlRow[ColumnNameDateInMySql].ToString()).Year;
                    int yearAccess = Convert.ToDateTime(rowFromAccess[ColumnNameDateInAccess].ToString()).Year;

                    // Om vi hittar ID-nummret och rätt år hittas undersöks om raden skall uppdaterats. 
                    if (TranslatorMySqlAndAccess.MySql_To_Access(MySqlRow[IdColumnNameInAccess].ToString())
                        .Equals(rowFromAccess[IdColumnNameInAccess].ToString()) && 
                        yearMySql == yearAccess)
                    {
                        // Jämför alla kolumner för att hitta skillnader. 
                        foreach (string columnNameFromPHP in ColumnNamesFromPHP)
                        {
                            string columnNameInAccess = TranslationColumnNamesFromMySql.TranslateAToB(columnNameFromPHP);

                            // Idnummrent i MySql finns inte med i Access eftersom Accessdatabasen rensar varje år. 
                            if (columnNameFromPHP.Equals(IdColumnNameInMySql))
                                continue;

                            // Kollar om kolumnnamnet finns med i både MySql och Access, t.ex. kolumnen Borttagen finns bara i MySql
                            if (dataFromAccess.Columns.IndexOf(columnNameInAccess) == -1 && 
								MySqlRow[columnNameFromPHP].ToString().Equals("1"))
							{
								List<KeyValuePair<string, string>> rowToUpdateInMySql = new List<KeyValuePair<string, string>>();

								// Lägger till id
								rowToUpdateInMySql.Add(
									new KeyValuePair<string, string>(IdColumnNameInMySql, MySqlRow[IdColumnNameInMySql].ToString()));

								// Lägger till kolumnamn
								rowToUpdateInMySql.Add(
									new KeyValuePair<string, string>("ColumnName", columnNameFromPHP));

								// Lägger till värdet
								rowToUpdateInMySql.Add(
									new KeyValuePair<string, string>("Value", "0"));

								valuesToUpdateStringsInMySql.Add(rowToUpdateInMySql);
								continue;
							}

                            // Om värdena för en kolumn inte är samma skall den uppdateras. 
                            // Måste kolla om bara ett mellanslag kommer ifrån Access för då tar MySql bort det. 
                            if (!ColumnsNotFoundInAccess.Contains(columnNameFromPHP) &&
								!TranslatorMySqlAndAccess.MySql_To_Access(MySqlRow[columnNameFromPHP].ToString()).
                                Equals(rowFromAccess[columnNameInAccess].ToString()) &&
                                !rowFromAccess[columnNameInAccess].ToString().Trim().Equals("")) 
                            {
                                List<KeyValuePair<string, string>> rowToUpdateInMySql = new List<KeyValuePair<string, string>>();

                                // Lägger till id
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>(IdColumnNameInMySql, MySqlRow[IdColumnNameInMySql].ToString()));

                                // Lägger till kolumnamn
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>("ColumnName", columnNameFromPHP));

                                // Lägger till värdet
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>("Value", TranslatorMySqlAndAccess.Access_To_MySql(rowFromAccess[columnNameInAccess].ToString())));

                                valuesToUpdateStringsInMySql.Add(rowToUpdateInMySql);
                            }
                        }
                    }
                }
            }

            SendRequest(valuesToUpdateStringsInMySql, Url_To_Script_Update);
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
                    !IdsAccess.Contains(rowFromMySql[IdColumnNameInAccess].ToString()))
                {
                    // Tar bort startplatsen, är en lista i en lista för att matcha metoden SendRequest
                    List<KeyValuePair<string, string>> rowToDelete = new List<KeyValuePair<string, string>>();
                    rowToDelete.Add(new KeyValuePair<string, string>(IdColumnNameInMySql, rowFromMySql[IdColumnNameInMySql].ToString()));
                    valuesToDelete.Add(rowToDelete);
                }
            }

            SendRequest(valuesToDelete, Url_To_Script_Delete);

            //// Tar fram Id som skall tas bort           
            //List<List<KeyValuePair<string, string>>> valuesToDelete = new List<List<KeyValuePair<string, string>>>();

            //// Hämtar alla vären ifrån MySql.
            //List<string> MySqlIdsInMySql = new List<string>();
            //List<string> AccessIdsMySql = new List<string>();
            //List<string> DatesMySql = new List<string>();
            //foreach (DataRow MySqlRow in dataFromMySql.Rows)
            //{
            //    MySqlIdsInMySql.Add(MySqlRow[IdColumnNameInMySql].ToString());
            //    AccessIdsMySql.Add(MySqlRow[IdColumnNameInAccess].ToString());
            //    DatesMySql.Add(MySqlRow[ColumnNameDateInMySql].ToString());
            //}

            //// Hämtar alla värden ifrån Access. 
            //List<string> IdsAccess = new List<string>();
            //List<string> DatesAccess = new List<string>();
            //foreach (DataRow AccessRow in dataToUpload.Rows)
            //{
            //    IdsAccess.Add(AccessRow[IdColumnNameInAccess].ToString());
            //    DatesAccess.Add(AccessRow[ColumnNameDateInAccess].ToString());
            //}

            //// Tar bort rader ifrån MySql som inte finns med i Access. 
            //for (int i = 0; i < AccessIdsMySql.Count; i++)
            //{
            //    bool removeFromMySql = false;

            //    int indexInAccess = IdsAccess.IndexOf(AccessIdsMySql[i]);
            //    if (indexInAccess == -1)
            //        removeFromMySql = true;
            //    else if (indexInAccess >= 0)
            //    {
            //        // Tar fram åren ur MySql respektive Access för att jämföra dem. 
            //        int yearMySql = Convert.ToDateTime(DatesMySql[i].ToString()).Year;
            //        int yearAccess = Convert.ToDateTime(DatesAccess[indexInAccess].ToString()).Year;

            //        // Om åren inte stämmer överrens skall raden tas bort. 
            //        if (yearMySql != yearAccess)
            //            removeFromMySql = true;
            //    }

            //    if (removeFromMySql)
            //    {
            //        List<KeyValuePair<string, string>> rowToDelete = new List<KeyValuePair<string, string>>();
            //        rowToDelete.Add(new KeyValuePair<string, string>(IdColumnNameInMySql, MySqlIdsInMySql[i]));
            //        valuesToDelete.Add(rowToDelete);
            //    }
            //}

            //SendRequest(valuesToDelete, Url_To_Script_Delete);
        }

        /// <summary>
        /// Kontrollerar om data som finns i xml-filen är tillräckligt färsk eller är från
        /// tidigare år. 
        /// </summary>
        /// <param name="dataFromXML">Data från xml-fil. </param>
        /// <returns>Returnerar sant om data är tillräckligt färsk. </returns>
        public bool CheckIfXMLIsUpToDate(DataTable dataFromXML)
        {
            int year = DateTime.Now.Year - 1;
            string date = year + "-09-01"; // Tar inte nyår som brytdatum utan 1:a septemper gäller. 
            DateTime dateLowerLimt = DateTime.Parse(date);

            // Kollar om det finns någon rad i xml:en som är för gammal, då är alla rader för gamla. 
            foreach (DataRow foretagRow in dataFromXML.Rows)
                if (DateTime.Parse(foretagRow[this.ColumnNameDateInMySql] as string) < dateLowerLimt)
                    return false;

            return true;
        }

		
    }
}
