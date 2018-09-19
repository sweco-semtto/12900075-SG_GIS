using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SGAB.SGAB_Database
{
    public class Entreprenorer : MySqlCommunicator, IMySqlCommunicator
    {
        /// <summary>
        /// Id-numret som SG har i listan över Entreprenörer. 
        /// </summary>
        public static string IdSkogensGödsling
        {
            get
            {
                return "0";
            }
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
        /// Skapar en ny kommunikation mellan dator och databas för Entreprenörer. 
        /// </summary>
        public Entreprenorer()
        {
            // Kolumnnamnen för Entreprenörer
            ColumnNamesFromPHP = new List<string>();

            // Kolumnnamn som skall döljas i gränssnittet. 
            ColumnNamesFromPHPToHide = new List<string>();
            ColumnNamesFromPHPToHide.Add("ID");

            // Anger vad xml-taggen/datatabellsnamnet heter. 
            XMLNameForDataTable = "Entreprenor";

            // Anger vad Id-taggen heter i PHP. 
            IdColumnNameInMySql = "ID";

            // Anger en översättning av kolumnnamn från PHP till gridviewrubriker
            TranslatePHPIntoGridHeader = new TranslateColumnNames();
            TranslatePHPIntoGridHeader.AddTranslation("Fraktentreprenor", "Fraktentreprenör");
            TranslatePHPIntoGridHeader.AddTranslation("Spridningsentreprenor", "Spridningsentreprenör");
            TranslatePHPIntoGridHeader.AddTranslation("Anvandarnamn", "Användarnamn");
            TranslatePHPIntoGridHeader.AddTranslation("Losenord", "Lösenord");

            
            // Anger vilka skript som gäller
            Url_To_Script_Delete = "http://www.sg-systemet.com/bestallning/PHP/deleteEntreprenor.php";
            Url_To_Script_Insert = "http://www.sg-systemet.com/bestallning/PHP/insertEntreprenor.php";
            Url_To_Script_Select = "http://www.sg-systemet.com/bestallning/PHP/selectEntreprenor.php";
            Url_To_Script_Update = "http://www.sg-systemet.com/bestallning/PHP/updateEntreprenor.php";

            // Anger skriptnamn som skall synas i ett, ev. felmeddelande om det inte går att hämta startplatser. 
            MessageErrorScriptName = "SelectEntreprenor.php";
        }

        /// <summary>
        /// Uppdaterar gränssnittet med möjliga entreprenörer som finns. Entreprenören SG skall inte vara med. 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataGridView"></param>
        public override void ReFillDataGridView(DataTable data, DataGridView dataGridView)
        {
            if (data == null)
                return;

            dataGridView.Rows.Clear();
            foreach (DataRow row in data.Rows)
            {
                // Skall inte lägga till Entreprenören SG i listan
                if (row["ID"].Equals(Entreprenorer.IdSkogensGödsling))
                    continue;

                int newRowIndex = dataGridView.Rows.Add();
                for (int itemNo = 0; itemNo < row.ItemArray.Length; itemNo++)
                    dataGridView.Rows[newRowIndex].Cells[itemNo].Value = TranslatorMySqlAndAccess.MySql_To_Access(row.ItemArray[itemNo].ToString());
            }            
        }

        /// <summary>
        /// Hämtar en lista över alla möjliga fraktentreprenörer. 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<Entrepreneur> GetPossibleFreightEntrepreneurs(DataTable dataTable)
        {
            List<Entrepreneur> possibleFreightEntrepreneur = new List<Entrepreneur>();

            if (dataTable != null && dataTable.Rows.Count > 0)
                foreach (DataRow row in dataTable.Rows)
                    if (row["Fraktentreprenor"].ToString().Equals("1"))
                        possibleFreightEntrepreneur.Add(new Entrepreneur(row[IdColumnNameInMySql].ToString(), row["Namn"].ToString()));

            return possibleFreightEntrepreneur;
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
        /// Hämtar en lista över alla möjliga spridningsentreprenörer. 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<Entrepreneur> GetPossibleSpreadEntrepreneurs(DataTable dataTable)
        {
            List<Entrepreneur> possibleSpreadEntrepreneur = new List<Entrepreneur>();

            if (dataTable != null && dataTable.Rows.Count > 0)
                foreach (DataRow row in dataTable.Rows)
                    if (row["Spridningsentreprenor"].ToString().Equals("1"))
                        possibleSpreadEntrepreneur.Add(new Entrepreneur(row[IdColumnNameInMySql].ToString(), row["Namn"].ToString()));

            return possibleSpreadEntrepreneur;
        }

        /// <summary>
        /// Hämtar en lista över alla entreprenörer. 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<Entrepreneur> ToList(DataTable dataTable)
        {
            List<Entrepreneur> entrepreneurs = new List<Entrepreneur>();

            if (dataTable != null)
                foreach (DataRow row in dataTable.Rows)
                    entrepreneurs.Add(new Entrepreneur(row[IdColumnNameInMySql].ToString(), row["Namn"].ToString()));          

            return entrepreneurs;
        }

        public List<Entrepreneur> ToListWithSG(DataTable dataTable)
        {
            List<Entrepreneur> entrepreneurs = ToList(dataTable);
            entrepreneurs.Add(new Entrepreneur(Entreprenorer.IdSkogensGödsling, "Skogens Gödsling"));
            return entrepreneurs;
        }
    }
}
