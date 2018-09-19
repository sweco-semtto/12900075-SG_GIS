using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace SGAB.SGAB_Karta
{
    public partial class FormCloseLayer : Form
    {
        private IList<TGIS_LayerAbstract> _Layers;
        private TGIS_ViewerWnd _Map;

        public FormCloseLayer(IList<TGIS_LayerAbstract> layers, TGIS_ViewerWnd map)
        {
            InitializeComponent();
            System.Drawing.Rectangle bounds = Screen.PrimaryScreen.WorkingArea;
            int width = bounds.Width;
            int height = bounds.Height;
            this.Location = new System.Drawing.Point(width - 50 - 377, height - 20 - 404);
            _Layers = layers;
            _Map = map;
            foreach (TGIS_LayerAbstract layer in layers)
            {
                Console.WriteLine(layer.Caption);
                ListBoxLayers.Items.Add(layer.Name);
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            IList<string> selectedLayers = new List<string>();
            foreach (string selectedLayer in ListBoxLayers.SelectedItems)
            {
                selectedLayers.Add(selectedLayer);
            }
            foreach (string selectedLayer in selectedLayers)
            {
                ListBoxLayers.Items.Remove(selectedLayer);
                _Map.Delete(selectedLayer);
            }
            
            //_Map.Update();
            //_Map.Refresh();

            this.Dispose();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}