using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;

namespace SGAB.SGAB_Karta
{
    public partial class FormUpdateStatusInfo : Form
    {
  
        public FormUpdateStatusInfo()
        {
            InitializeComponent();
            System.Drawing.Rectangle bounds = Screen.PrimaryScreen.WorkingArea;
            int width = bounds.Width;
            int height = bounds.Height;
            this.Location = new System.Drawing.Point(width - 50 - 377, height - 20 - 404);
            txtFilePathOldFile.Text = @"C:\SG_GIS\Kartdata\Beställningar\Bestallning_Tatuk.shp";
                     
        }
              

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool updateOK;
            string updateStatus = ShapeUpdater.UpdateShapeFile(txtFilePathNewFile.Text, txtFilePathOldFile.Text);
            if (updateStatus.CompareTo("Uppdateringen lyckades") == 0)
            {
                updateOK = true;
            }
            else
            {
                updateOK = false;
            }

            if (updateOK == true)
            {
                MessageBox.Show("Uppdateringen av den nya filen gick bra.\n\nStäng SG-GIS-programmet och öppna utforskaren.\nKopiera den nya filen Bestallning_Tatuk.shp (som t.ex. finns på USB) till mappen C:\\SG_GIS\\Kartdata\\Beställningar.\n\nDärefter kan du öppna SG-GIS-programmet igen och fortsätta som vanligt.", "Uppdatering OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show(updateStatus + "\nKontrollera att du valt rätt filer och försök sedan igen.", "Misslyckad uppdatering", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSelectOldFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Peka ut den gamla filen som innehåller statusinformation";
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Shape-filer (*.shp)|*.shp|Alla filer (*.*)|*.*";
            fileDialog.InitialDirectory = @"C:\SG_GIS\Kartdata\Beställningar";
            //fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePathOldFile.Text = fileDialog.FileName;
            }
                       
        }

        private void btnSelectNewFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Peka ut den nya filen dit statusinformation ska överföras";
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Shape-filer (*.shp)|*.shp|Alla filer (*.*)|*.*";
            fileDialog.InitialDirectory = @"E:";
            //fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePathNewFile.Text = fileDialog.FileName;
            }
        }
    }
}