using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGAB.SGAB_Database
{
    public partial class FormAdmin : Form
    {
        private Entreprenorer entreprenorer;
        private DataTable entreprenorerFromMySQL;

        private Startplats startplatser;
        private DataTable startplatserFromMySQL;
        private DataTable startplatserFromAccess;

        private Foretag foretag;
        private DataTable foretagFromMySQL;
        private DataTable foretagFromAccess;

        public FormAdmin()
        {
            InitializeComponent();

            AccessCommunicator.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                "Data Source=C:\\Projekt\\6601556006 Skogens godsling\\Arbetsdata\\SG_GIS\\Kartdata\\Karolinas\\SGAB_test.mdb;" +
                //"Data Source=C:\\SG_GIS\\Kartdata\\Karolinas\\SGAB_test.mdb;" +
                "Persist Security Info=False";

            foretag = new Foretag();
            startplatser = new Startplats();
            entreprenorer = new Entreprenorer();

            //Hämtar tabellen Foretag ifrån Mysql            
            foretagFromMySQL = foretag.GetAllFromMySql();

            if (foretagFromMySQL == null)
                return;

            //Hämtar tabell Företag ifrån lokala Accessdatabasen            
            foretagFromAccess = foretag.GetAllFromAccess();
            startplatserFromAccess = startplatser.GetAllFromAccess();
        }

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
                dt.Columns.Add(gridview.Columns[i].HeaderText, gridview.Columns[i].ValueType);
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

        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabAdmin.SelectedIndex == 1)
            {
                //Hämtar tabell från Mysql            
                //foretagFromMySQL = foretag.GetAllFromMySql(); // Flyttat till FormAdmin()
            }
            else if (tabAdmin.SelectedIndex == 2)
            {
                // Synkroniserar startplatser. 
                startplatserFromMySQL = startplatser.GetAllFromMySql();
                startplatser.ForetagFromAccess = foretagFromAccess;
                startplatser.ForetagFromMySql = foretagFromMySQL;
                startplatser.SynchronizeWithPHP(startplatserFromMySQL, startplatserFromAccess);

                // Gör iordning gränssnittet. 
                startplatser.CreateDataGridView(startplatserFromMySQL, dgvStartplatser);
                startplatser.ReFillDataGridView(startplatserFromMySQL, dgvStartplatser);
            }
            else if (tabAdmin.SelectedIndex == 3)
            {
                // Hämtar entreprenörer ifrån MySQL. 
                entreprenorerFromMySQL = entreprenorer.GetAllFromMySql();
                entreprenorer.CreateDataGridView(entreprenorerFromMySQL, dgvEntreprenorer);
                entreprenorer.ReFillDataGridView(entreprenorerFromMySQL, dgvEntreprenorer);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            // Synkroniserar Företag
            foretag.SynchronizeWithPHP(foretagFromMySQL, foretagFromAccess);
            foretagFromMySQL = foretag.GetAllFromMySql();

            // Gör iordning gränssnittet. 
            foretag.CreateDataGridView(foretagFromMySQL, dgForetag);
            foretag.ReFillDataGridView(foretagFromMySQL, dgForetag);
            dgForetag.SortCompare += foretag.DataGridView_SortCompare_Integers;
            dgForetag.Sort(dgForetag.Columns[foretag.StandardColumnToSortAscending], ListSortDirection.Ascending);
        }

        #region Tab_Entreprenor

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vill du spara ändringar?", "Spara ändringar", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                DataTable dtEntreprenorer = ExportGridViewToDataTable(dgvEntreprenorer);
                entreprenorer.SynchronizeWithPHP(entreprenorerFromMySQL, dtEntreprenorer);

                entreprenorerFromMySQL = entreprenorer.GetAllFromMySql();
                entreprenorer.ReFillDataGridView(entreprenorerFromMySQL, dgvEntreprenorer);
            }
        }

        /// <summary>
        /// Tar hand om klick på raderna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEntreprenorer_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Om en rad är markerad, skall värderna för den synas i fälten i gränssnittet. 
            if (dgvEntreprenorer.SelectedRows.Count == 1)
            {
                this.tbxName.Text = dgvEntreprenorer.Rows[e.RowIndex].Cells["Namn"].Value.ToString();
                this.tbxName.Refresh();

                if (dgvEntreprenorer.Rows[e.RowIndex].Cells["Fraktentreprenor"].Value.ToString().Equals("1"))
                    cbxFreight.Checked = true;
                else
                    cbxFreight.Checked = false;

                if (dgvEntreprenorer.Rows[e.RowIndex].Cells["Spridningsentreprenor"].Value.ToString().Equals("1"))
                    cbxSpread.Checked = true;
                else
                    cbxSpread.Checked = false;
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

        private void dgvStartplatser_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStartplatser.SelectedRows.Count == 1)
            {
                string currentFreighter = dgvStartplatser.SelectedRows[0].Cells["Fraktentreprenors_ID"].Value.ToString();
                string currentSpreader = dgvStartplatser.SelectedRows[0].Cells["Spridningsentreprenors_ID"].Value.ToString();

                // Hämtar möjliga entreprenörer
                List<Entrepreneur> possibleFreightEntrepreneurs =
                    entreprenorer.GetPossibleFreightEntrepreneurs(entreprenorerFromMySQL);
                List<Entrepreneur> possibleSpreadEntrepreneurs =
                    entreprenorer.GetPossibleSpreadEntrepreneurs(entreprenorerFromMySQL);

                // Fyller på comboboxarna och tar fram rätt index
                cmbFreight.Items.Clear();
                foreach (Entrepreneur freighter in possibleFreightEntrepreneurs)
                {
                    cmbFreight.Items.Add(freighter);
                    if (freighter.Id.Equals(currentFreighter))
                        cmbFreight.SelectedItem = freighter;

                }

                cmbSpread.Items.Clear();
                foreach (Entrepreneur spreader in possibleSpreadEntrepreneurs)
                {
                    cmbSpread.Items.Add(spreader);
                    if (spreader.Id.Equals(currentSpreader))
                        cmbSpread.SelectedItem = spreader;
                }
            }
        }

        #endregion

        private void btnBind_Click(object sender, EventArgs e)
        {
            List<Startplats> listStartplatser = new List<Startplats>();
            foreach (Startplats startplats in listStartplatser)
            { 
                
            }
        }
    }
}
