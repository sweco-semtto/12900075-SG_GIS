using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGAB.SGAB_Karta
{
    public partial class FormNotes : Form
    {
        public const int UNKNOWN = -1;
        public const int EJ_PÅBÖRJAD = 0;
        public const int GÖDSEL_UTKÖRT = 1;
        public const int FÄRDIGGÖDSLAT = 2;
        public const int SÄCKAR_HÄMTADE = 3;

        /// <summary>
        /// Hämtar vilken status som kommentaren skrivs in under. 
        /// </summary>
        public int Status
        {
            get;
            protected set;
        }
        
        public FormNotes()
        {
            InitializeComponent();

            Status = UNKNOWN;
        }

        public void SetMenueItem(string text, string note1, string note2, string note3, string note4)
        {
            txbNote1.Enabled = false;
            txbNote2.Enabled = false;
            txbNote3.Enabled = false;
            txbNote4.Enabled = false;

            txbNote1.Text = note1;
            txbNote2.Text = note2;
            txbNote3.Text = note3;
            txbNote4.Text = note4;

            if (text.Equals("Ej påbörjad"))
            {
                Status = EJ_PÅBÖRJAD;
                txbNote1.Enabled = true;  
            }
            else if (text.Equals("Gödsel utkörd"))
            {
                Status = GÖDSEL_UTKÖRT;
                txbNote2.Enabled = true;
            }
            else if (text.Equals("Färdiggödslat"))
            {
                Status = FÄRDIGGÖDSLAT;
                txbNote3.Enabled = true;
            }
            else if (text.Equals("Säckar hämtade"))
            {
                Status = SÄCKAR_HÄMTADE;
                txbNote4.Enabled = true;
            }
            else
                Status = UNKNOWN;

        }

        public string GetLatestText()
        {
            string ans = "";

            if (Status == EJ_PÅBÖRJAD)
                ans = txbNote1.Text;
            else if (Status == GÖDSEL_UTKÖRT)
                ans = txbNote2.Text;
            else if (Status == FÄRDIGGÖDSLAT)
                ans = txbNote3.Text;
            else if (Status == SÄCKAR_HÄMTADE)
                ans = txbNote4.Text;

            return ans;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void txbNote1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave_Click(this, new EventArgs());
        }

        private void txbNote2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave_Click(this, new EventArgs());
        }

        private void txbNote3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave_Click(this, new EventArgs());
        }

        private void txbNote4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave_Click(this, new EventArgs());
        }
    }
}
