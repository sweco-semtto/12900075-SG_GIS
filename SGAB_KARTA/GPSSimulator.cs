using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SGAB.SGAB_Karta.HelpClasses;

namespace SGAB.SGAB_Karta
{
    public partial class GPSSimulator : Form
    {
        NmeaStringFactory factory = new NmeaStringFactory();

        public GPSSimulator()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.InitialDirectory = @"C:\SWECO\Projekt\Skogens Gödsling\Loggar\GPS-loggar";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxLoggPath.Text = dialog.FileName;
            }
        }

        private void buttonReadGPSLogg_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBoxLoggPath.Text))
                MessageBox.Show("Den angivna filen finns inte", "Fil saknas", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            using (StreamReader reader = new StreamReader(textBoxLoggPath.Text))
            {
                string lineToParse;
                while ((lineToParse = reader.ReadLine()) != null)
                {
                    // parsar raden
                    bool parsedSuccessful = factory.ParseLine(lineToParse);

                    if (parsedSuccessful)
                    {
                        this.labelNoOfPosts.Text = factory.Log.Count.ToString();
                        this.labelNoOfPosts.Update();
                    }
                }
            }
        }
    }
}
