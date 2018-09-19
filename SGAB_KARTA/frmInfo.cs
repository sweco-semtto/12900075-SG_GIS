/********************************************************
 * I frmInfo visas attributdata f�r den geometri som 
 * skickas med hit.
 * 
 * LSAM, SWECO Position, v�ren 2006
 ********************************************************/

using System;
using System.Windows.Forms;
using TatukGIS.NDK;
using System.Configuration;
using TatukGIS.NDK.WinForms;

//using Mellanskog.OP.OPDAL;
//using Mellanskog.OP.Common;

namespace SGAB.SGAB_Karta
{
    /// <summary>
    /// Visar attributdata f�r inskickad geometri
    /// </summary>
    public partial class frmInfo : Form
    {
        //Tatuk-kontroll med inbyggd funktion f�r att visa
        //attribut f�r vald geometri
        private TGIS_ControlAttributes controlAttributes;

        /// <summary>
        /// Sparar undan vilken geometri vi visar info f�r s� vi kan avmarkera den i kartan. 
        /// </summary>
        public TGIS_Shape ShowInfoShape
        {
            get;
            protected set;
        }

        public Karta mainForm;

        public frmInfo()
        {
            InitializeComponent();
            System.Drawing.Rectangle bounds = Screen.PrimaryScreen.WorkingArea;
            int width = bounds.Width;
            int height = bounds.Height;
            this.Location = new System.Drawing.Point(width - 50 - 377, height - 20 - 404);
        }

        /// <summary>
        /// Visar information om de valda geometrierna
        /// </summary>
        /// <param name="_shp">den eller de geometrier som valts</param>
        public void VisaInfo(TGIS_Shape _shp)
        {
            this.ShowInfoShape = _shp;

            try
            {
                //text om ingen geometri har hittats
                //(kommer aldrig anropa VisaInfo om det inte finns n�gon geometri)
                if (_shp == null)
                {
                    Text = "Geometri: ingen geometri hittad";
                }
                else
                {
                    Text = String.Format("{0}: {1}", _shp.Layer.Caption, _shp.Uid);

                    //visa alla attribut f�r den utvalda geometrin
                    controlAttributes.ShowShape(_shp);
             
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);      
            }
        }

        /// <summary>
        /// N�r infof�nstret st�ngs skall �ven startplatsen avmarkeras i kartan. 
        /// </summary>
        private void frmInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ShowInfoShape.IsSelected = false;
            this.ShowInfoShape.Invalidate(true); 
        }   
    }
}