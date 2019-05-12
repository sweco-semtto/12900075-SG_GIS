using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace SGAB.SGAB_Database
{
    public class Stati : MySqlCommunicator, IMySqlCommunicator
    {
        public Stati()
        {
            // Kolumnnamnen för Status
            ColumnNamesFromPHP = new List<string>();

            // Kolumnnamn som skall döljas i gränssnittet. 
            ColumnNamesFromPHPToHide = new List<string>();
            ColumnNamesFromPHPToHide.Add("ID");

            // Anger vad xml-taggen/datatabellsnamnet heter. 
            XMLNameForDataTable = "Status";

            IdColumnNameInMySql = "ID";

            // Anger vilka skript som gäller
            Url_To_Script_Select = "http://www.sg-systemet.com/bestallning/PHP/selectStatus.php";
            Url_To_Script_Insert = "http://www.sg-systemet.com/bestallning/PHP/insertStatus.php";

            // Anger en översättning av kolumnnamn från PHP till gridviewrubriker
            TranslatePHPIntoGridHeader = new TranslateColumnNames();
            TranslatePHPIntoGridHeader.AddTranslation("AndradAv", "Ändrad Av");
            TranslatePHPIntoGridHeader.AddTranslation("Status_status", "Status");
        }

        public Stati(bool testmode) : this()
        {
            new Stati();

            if (testmode)
            {
                // Anger vilka skript som gäller
                Url_To_Script_Select = "http://www.sg-systemet.com/bestallning/PHP/selectStatusTest.php";
                Url_To_Script_Insert = "http://www.sg-systemet.com/bestallning/PHP/insertStatusTest.php";
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

        public override void CreateDataGridView(DataTable data, DataGridView dataGridView)
        {
            dataGridView.Columns.Clear();

            // Döljer ID-värdet för användaren, detta automatgenereras av databasen och skall inte ändras. 
            foreach (DataColumn column in data.Columns)
            {
                if (ColumnNamesFromPHPToHide.Contains(column.ColumnName))
                {
                    dataGridView.Columns.Add(column.ColumnName, TranslatePHPIntoGridHeader.TranslateAToB(column.ColumnName));
                    dataGridView.Columns[column.ColumnName].Visible = false;
                }
                else
                    dataGridView.Columns.Add(column.ColumnName, TranslatePHPIntoGridHeader.TranslateAToB(column.ColumnName));
            }
        }

        /// <summary>
        /// Översätter status till klartext och uppdaterar gridview.
        /// </summary>
        /// <param name="dgvStartplatser"></param>
        /// <param name="entreprenorer"></param>
        public void TranslateStatus(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                try
                {
                    int statusNr = Convert.ToInt32(row.Cells["Status"].Value);
                    Status status = StatusKodLista.FindById(statusNr);
                    row.Cells["Status"].Value = status == null ? String.Empty : status.Id;
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Översätter status till klartext och uppdaterar gridview.
        /// </summary>
        /// <param name="dgvStartplatser"></param>
        /// <param name="entreprenorer"></param>
        public void TranslateStatus_status(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                int statusNr = Convert.ToInt32(row.Cells["Status_status"].Value);
                Status status = StatusKodLista.FindById(statusNr);
                row.Cells["Status_status"].Value = status == null ? String.Empty : status.Id;
            }
        }

        /// <summary>
        /// Lägger till rad i tabellen Status i MySQL-databasen.
        /// </summary>
        /// <param name="startplatsID"></param>
        /// <param name="anvandarID"></param>
        /// <param name="status"></param>
        public void InsertRow(string startplatsID, string anvandarID, string status)
        {
            List<List<KeyValuePair<string, string>>> valuesToInsert = new List<List<KeyValuePair<string, string>>>();

            List<KeyValuePair<string, string>> rowToInsert = new List<KeyValuePair<string, string>>();

            rowToInsert.Add(
                new KeyValuePair<string, string>("StartplatsID", startplatsID));
            rowToInsert.Add(
                new KeyValuePair<string, string>("Datum", DateTime.Now.ToString()));
            rowToInsert.Add(
                new KeyValuePair<string, string>("AndradAv", anvandarID));
            rowToInsert.Add(
                new KeyValuePair<string, string>("Status_status", status));
            
            valuesToInsert.Add(rowToInsert);

            SendRequest(valuesToInsert, Url_To_Script_Insert);
        }
        
        public void InsertRows(DataTable dtStartplatser, string AndradAv)
        {
            // Lägger till en rad i status-tabellen som historik för varje rad i gridviewn med startplatser         
            foreach (DataRow startplatsRow in dtStartplatser.Rows)
            {
                InsertRow(startplatsRow["ID"].ToString(),
                          AndradAv,
                          startplatsRow["Status"].ToString());           
            }     
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
        /// Lägger till en ny rad bland i statustabellen. 
        /// </summary>
        /// <param name="startplatsId"></param>
        /// <param name="date"></param>
        /// <param name="AndradAv"></param>
        /// <param name="status"></param>
        public void InsertRow(string startplatsId, string date, string AndradAv, string status)
        {
            List<List<KeyValuePair<string, string>>> valuesToInsert = new List<List<KeyValuePair<string, string>>>();
            List<KeyValuePair<string, string>> rowToInsert = new List<KeyValuePair<string, string>>();

            rowToInsert.Add(new KeyValuePair<string, string>("StartplatsID", startplatsId));
            rowToInsert.Add(new KeyValuePair<string, string>("Datum", date));
            rowToInsert.Add(new KeyValuePair<string, string>("AndradAv", AndradAv));
            rowToInsert.Add(new KeyValuePair<string, string>("Status_status", status));

            valuesToInsert.Add(rowToInsert);
            SendRequest(valuesToInsert, Url_To_Script_Insert);
        }
    }
}
