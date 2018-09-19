/********************************************************
 * Measure.cs anv�nds f�r att m�ta i kartan och 
 * f�r att ber�kna en linjes b�ring
 * 
 * LSAM, SWECO Position, v�ren 2006
 ********************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

//using Mellanskog.OP.OPDAL;
//using Mellanskog.OP.Common;

namespace SGAB.SGAB_Karta
{
    /// <summary>
    /// Innhe�ller metoder f�r att m�ta i kartan samt ber�kna b�ring
    /// </summary>
    class Measure
    {
        //Referens till formul�rets GIS/kart-komponent
        TGIS_ViewerWnd _tgisKarta;       

        //Shape f�r linjen
        TGIS_ShapeArc _shpArc;

        //Shape f�r polygonen
        TGIS_ShapePolygon _shpPolygon;

        public Measure(TatukGIS.NDK.WinForms.TGIS_ViewerWnd tgisKarta)
        {
            _tgisKarta = tgisKarta;
        }

        /// <summary>
        /// F�rbereder ett m�tningslager genom att ange utseende 
        /// och l�gga till en linjegeometri
        /// </summary>
        public void PrepareMeasureLine()
        {
            //Lager f�r linjen
            TGIS_LayerVector layerArc = new TGIS_LayerVector();

            try
            {
                layerArc.Params.Line.Color = Color.Blue;
                layerArc.Params.Line.Width = 15;
                layerArc.Params.Line.Style = TGIS_PenStyle.gisPsSolid;
                layerArc.HideFromLegend = true;
                layerArc.Name = "MeasureLine";

                if (layerArc == null) return;

                _tgisKarta.Add((TGIS_LayerAbstract)layerArc);
                
                _shpArc = (TGIS_ShapeArc)layerArc.CreateShape(TGIS_ShapeType.gisShapeTypeArc);

                if (_shpArc == null) return;

                _shpArc.AddPart();

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
                                   
        }

        /// <summary>
        /// T�mmer den tidigare geometrin och f�rbereder f�r att
        /// l�gga till en ny
        /// </summary>
        public void NewMeasureLine()
        {
            try
            {
                _shpArc.Reset();                
                _shpArc.AddPart();                             

                _tgisKarta.Update();
            }
            catch (Exception ex)
            {
              ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// l�gger till en punkt till linjen
        /// </summary>
        /// <param name="X">muspekarens sk�rmkoordinat i '�stlig'-riktning</param>
        /// <param name="Y">muspekarens sk�rmkoordinat i 'nordlig'-riktning</param>
        public void DrawMeasureLine(int X, int Y)
        {
            try
            {
                //anger punkten i kartkoordinater
                TGIS_Point ptg = _tgisKarta.ScreenToMap(TatukGIS.NDK.TGIS_Utils.Point(X, Y));

                //l�gger till punkten till linjen
                _shpArc.AddPoint(ptg);

                _shpArc.Invalidate(false);
                //uppdaterar kartan
                //_tgisKarta.Update();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            
        }

        /// <summary>
        /// ber�knar linjel�ngden
        /// </summary>
        /// <returns>linjel�ngden</returns>
        public double CalculateLine()
        {
            try
            {
                return _shpArc.Length();
            }
           catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            
                return -1;
            }
        }

        /// <summary>
        /// F�rbereder ett m�tningslager genom att ange utseende 
        /// och l�gga till en polygongeometri
        /// </summary>
        public void PrepareMeasureAreal()
        {
            //Lager f�r polygonen
            TGIS_LayerVector layerPolygon = new TGIS_LayerVector();

            try
            {
                layerPolygon.Params.Area.Color = Color.Blue;
                layerPolygon.Params.Area.Pattern = TGIS_BrushStyle.gisBsCross;
                layerPolygon.HideFromLegend = true;
                layerPolygon.Name = "MeasurePolygon";

                if (layerPolygon == null) return;

                _tgisKarta.Add((TGIS_LayerAbstract)layerPolygon);

                _shpPolygon = (TGIS_ShapePolygon)layerPolygon.CreateShape(TGIS_ShapeType.gisShapeTypePolygon);

                if (_shpPolygon == null) return;

                _shpPolygon.AddPart();

            }
            catch (Exception ex)
            {
               ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// T�mmer den tidigare geometrin och f�rbereder f�r att
        /// l�gga till en ny
        /// </summary>
        public void NewMeasureAreal()
        {
            try
            {
                _shpPolygon.Reset();
                _shpPolygon.AddPart();
                _tgisKarta.Update();
            }
            catch (Exception ex)
            {
               ExceptionHandler.HandleException(ex);
            }
     
        }

        /// <summary>
        /// l�gger till en punkt till polygonen
        /// </summary>
        /// <param name="X">kartans sk�rmkoordinat i '�stlig'-riktning</param>
        /// <param name="Y">kartans sk�rmkoordinat i 'nordlig'-riktning</param>
        public void DrawMeasureAreal(int X, int Y)
        {
            try
            {
                //anger punkten i kartkoordinater
                TGIS_Point ptg = _tgisKarta.ScreenToMap(TatukGIS.NDK.TGIS_Utils.Point(X, Y));

                //l�gger till punkten till polygonen
                _shpPolygon.AddPoint(ptg);

                //uppdaterar kartan
                _shpPolygon.Invalidate(false);
                //_tgisKarta.Update();
            }
            catch (Exception ex)
            {
               ExceptionHandler.HandleException(ex);
            }
            
        }

        /// <summary>
        /// ber�knar polygonarean
        /// </summary>
        /// <returns>arealen i Ha med tv� decimaler</returns>
        public string CalculateArea()
        {
            try
            {
                double arealMeter2 = _shpPolygon.Area();

                string arealHa = String.Format("{0:F2}", arealMeter2 / 10000);

                return arealHa;
            }
            catch (Exception ex)
            {
              ExceptionHandler.HandleException(ex);
            
                return "Fel vid ber�kningen";
            }
        }

        /// <summary>
        /// R�knar ut b�ringen mellan f�rsta och sista 
        /// punkten p� linjen
        /// </summary>
        /// <returns>b�ringen utan decimaler</returns>
        public string CalculateBaring()
        {
            try
            {
                double dBearingTatuk = _shpArc.GetAngle(false) * (180 / Math.PI);

                return string.Format("{0:F0}", dBearingTatuk);
            }
            catch (Exception ex)
            {
               ExceptionHandler.HandleException(ex);
            
                return "Fel vid ber�kningen";
            }
        }

        
    }


}



