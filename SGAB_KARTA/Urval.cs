/********************************************************
 * Urval.cs används för att hantera händelser
 * knutna till att selektering görs
 * 
 * SelectSingle
 * SelectMultiple
 * SelectInfo
 * SetSelected
 * PrepareSelectPolygon
 * NewSelectPolygon
 * DrawSelectPolygon
 * Radera
 * KopieraInGeometri
 * Avselektera
 * 
 * LSAM, SWECO Position, våren 2006
 ********************************************************/

using System;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
//using Mellanskog.OP.OPDAL;
//using Mellanskog.OP.Common;
using System.Data;
using System.Data.SqlClient;

namespace SGAB.SGAB_Karta
{
    /// <summary>
    /// används för att hantera händelser
    /// knutna till att urval görs
    /// </summary>
    class Urval
    {
        // Referens till formulärets GIS/kart-komponent
        TGIS_ViewerWnd _tgisKarta;

        // Referens till formulärets legend komponent
        public TGIS_LayerAbstract _activeLayer;

        //array med de urvalda geometrierna
        public ArrayList _shapes = new ArrayList();

        //Shape för selekteringspolygonen
        TGIS_ShapePolygon _shpSelectPolygon;

        Configuration _configuration = Configuration.GetConfiguration();

        //private string KOLUMNNAMN_OBJGUID = "MI_PRINX";   
        public Urval(TGIS_ViewerWnd tgisKarta)
        {
            _tgisKarta = tgisKarta;
         
        }

        /// <summary>
        /// Väljer en geometri i aktivt lager genom ett musklick
        /// Sparar geometrin i _shapes och geometrins objGUID i _objGUIDs
        /// </summary>
        /// <param name="x">muspekarens skärmkoordinat i 'östlig'-riktning</param>
        /// <param name="y">muspekarens skärmkoordinat i 'nordlig'-riktning</param>
        public void SelectSingle(int x, int y)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;
            try
            {
                Avselektera();

                if (_tgisKarta.IsEmpty) return;

                ptg = _tgisKarta.ScreenToMap(new System.Drawing.Point(x, y));

                shp = (TatukGIS.NDK.TGIS_Shape)_tgisKarta.Locate(ptg, 5 / _tgisKarta.Zoom); // 5 pixels precision


                if (shp != null)
                {
                    if (shp.Layer.Name == _activeLayer.Name ||
                        shp.Layer.Name == StartplatsLayer.StartplatsLayerName)
                    {
                        shp = (TGIS_Shape)shp.MakeEditable();

                        //lägger till den hittade geometrin i en lista
                        _shapes.Add(shp);

                        shp.IsSelected = true;
                        shp.Invalidate(true);

                        //_shape = shp;
                        //_btnKopiera.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Ingen geometri hittades i det aktiva lagret ", "Ingen vald", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {

                ExceptionHandler.HandleException(ex);
            }
        }

       /// <summary>
       /// Väljer ut de geometrier, i aktivt lager, som 
       /// geografiskt berörs av _shpSelectPolygon
       /// Sparar geometrierna i _shapes
       /// </summary>
       /// <returns>totalt antal ton</returns>
        public KeyValuePair<List<TGIS_Shape>, double> SelectMultiple()
        {
            TGIS_LayerVector llVector = null;
            double canSum = 0;

            List<TGIS_Shape> selectedShapes = new List<TGIS_Shape>();

            try
            {
                Avselektera();

                try
                {
                    //llVector = (TGIS_LayerVector)_tgisKarta.Get(_activeLayer.Name);
                    //llVector = (TGIS_LayerVector)_tgisKarta.Get("Bestallning_Tatuk");
                    llVector = (TGIS_LayerVector)_tgisKarta.Get("Startplatser");
                }
                catch (Exception ex)
                {
                    //inget sker, kan lagret inte konverteras til vektor ska det heller inte gå att selektera i det
                    MessageBox.Show("Selektera flera startplatser, lagerproblem: \n\n" + ex.ToString());
                }

                if (llVector != null)
                {
                    foreach( TGIS_Shape shp in llVector.Loop( TGIS_Utils.GisWholeWorld(), "", null, "", true) )
                    {
                        bool intersect = shp.Relate(_shpSelectPolygon, TGIS_Utils.GIS_RELATE_INTERSECT());

                        if (intersect == true)
                        {
                            selectedShapes.Add(shp);

                            shp.MakeEditable();

                            //lägger till varje hittad geometri i en lista
                            _shapes.Add(shp);

                            canSum += double.Parse((shp.GetField(ConfigurationManager.AppSettings["NamnCanKolumn"])).ToString());

                            shp.IsSelected = true;
                            shp.Invalidate(true);                      
                       }
                    }                   
                }
                return new KeyValuePair<List<TGIS_Shape>,double>(selectedShapes, canSum);
            }
           catch (Exception ex)
            {
                MessageBox.Show("Selektera flera startplatser, annat problem: \n\n" + ex.ToString());

                ExceptionHandler.HandleException(ex);

                return new KeyValuePair<List<TGIS_Shape>, double>(null, -1);
            }
            
        }

        /// <summary>
        /// väljer ut de geometrier som berörs av den ingående punkten samt en 
        /// buffer runt den på 5 pixlar
        /// </summary>
        /// <param name="x">muspekarens skärmkoordinat i 'östlig'-riktning</param>
        /// <param name="y">muspekarens skärmkoordinat i 'nordlig'-riktning</param>
        /// <returns>de berörda geometrierna</returns>
        public ArrayList SelectInfo(int x, int y)
        {
            TGIS_Point ptg;
            //TGIS_Shape shp;
            TGIS_Shape shpTempPolygon = null;
            TGIS_LayerVector ll = null;
            TGIS_LayerVector llTempShape;
            ArrayList shapes = new ArrayList();

            try
            {
                Avselektera();

                ptg = _tgisKarta.ScreenToMap(new System.Drawing.Point(x, y));

                llTempShape = (TGIS_LayerVector)_tgisKarta.Get("MeasurePolygon");

                shpTempPolygon = (TGIS_ShapePolygon)llTempShape.CreateShape(TGIS_ShapeType.gisShapeTypePolygon);

                shpTempPolygon.AddPart();

                ptg.X = ptg.X - (5 / _tgisKarta.Zoom);
                ptg.Y = ptg.Y + (5 / _tgisKarta.Zoom);
                shpTempPolygon.AddPoint(ptg);

                ptg.X = ptg.X + (10 / _tgisKarta.Zoom);
                ptg.Y = ptg.Y + 0;
                shpTempPolygon.AddPoint(ptg);

                ptg.X = ptg.X - 0;
                ptg.Y = ptg.Y - (10 / _tgisKarta.Zoom);
                shpTempPolygon.AddPoint(ptg);

                ptg.X = ptg.X - (10 / _tgisKarta.Zoom);
                ptg.Y = ptg.Y - 0;
                shpTempPolygon.AddPoint(ptg);

                int i;

                for (i = 0; i < _tgisKarta.Items.Count; i++)
                {
                    try
                    {
                        if (_tgisKarta.Items[i] is TGIS_LayerVector)
                            ll = (TGIS_LayerVector)_tgisKarta.Items[i];
                    }
                    catch
                    {
                        //inget sker, kan lagret inte konverteras til vektor ska det heller inte gå att selektera i det 
                    }

                    if (ll != null)
                    {

                        //shp = ll.FindFirst(TGIS_Utils.GisWholeWorld(), "", shpTempPolygon, TGIS_Utils.GIS_RELATE_INTERSECT(), true);
                        foreach (TGIS_Shape shp in ll.Loop(TGIS_Utils.GisWholeWorld(), "", null, "", true))
                        {
                            bool intersect = shp.Relate(shpTempPolygon, TGIS_Utils.GIS_RELATE_INTERSECT());

                            if (intersect == true)
                            {

                                //while (shp != null)
                                //{
                                    //shp = shp.MakeEditable();
                                    shp.MakeEditable();
                                    shapes.Add(shp.CreateCopy());
                                    //shp.IsSelected = !shp.IsSelected;
                                    shp.Invalidate(true);
                                    //shp = ll.FindNext();
                                //}
                            }
                        }
                        shpTempPolygon.Delete();

                    }
                }
                return shapes;
            }
            catch (Exception ex)
            {
               ExceptionHandler.HandleException(ex);
            
                return shapes;
            }
        }


        /// <summary>
        /// Förbereder urvalspolygonen genom att ange utseende 
        /// och lägga till en polygongeometri
        /// </summary>
        public void PrepareSelectPolygon()
        {
            try
            {
                //Lager för urvalspolygonen
                TGIS_LayerVector layerPolygon = new TGIS_LayerVector();

                //sätter parametrar för urvalspolygonen
                //layerPolygon.Params.Area.Color = Color.Transparent;
                layerPolygon.Params.Area.Pattern = TGIS_BrushStyle.gisBsClear;
                layerPolygon.Params.Area.OutlineColor = Color.Yellow;
                layerPolygon.Params.Area.OutlineWidth = -2;
                //layerPolygon.Transparency = 50;
                layerPolygon.HideFromLegend = true;
                layerPolygon.Name = "Urvalslager";

                if (layerPolygon == null) return;

                //lägger till lagret till kartan
                _tgisKarta.Add((TGIS_LayerAbstract)layerPolygon);

                //skapar en polygon i lagret
                _shpSelectPolygon = (TGIS_ShapePolygon)layerPolygon.CreateShape(TGIS_ShapeType.gisShapeTypePolygon);

                if (_shpSelectPolygon == null) return;

                //förbereder för att lägga till punkter i polygonen
                _shpSelectPolygon.AddPart();

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }

        }

        /// <summary>
        /// Tömmer den tidigare geometrin och förbereder för att
        /// lägga till en ny
        /// </summary>
        public void NewSelectPolygon()
        {
            try
            {
                _shpSelectPolygon.Reset();
                _shpSelectPolygon.AddPart();
                _tgisKarta.Update();
            }
            catch (Exception ex)
            {
               ExceptionHandler.HandleException(ex);
            }
        }

        /// <summary>
        /// lägger till en punkt till polygonen
        /// </summary>
        /// <param name="X">kartans skärmkoordinat i 'östlig'-riktning</param>
        /// <param name="Y">kartans skärmkoordinat i 'nordlig'-riktning</param>
        public void DrawSelectPolygon(int X, int Y)
        {
            try
            {
                TatukGIS.NDK.TGIS_Point ptg;

                //add point to polygon
                ptg = _tgisKarta.ScreenToMap(TatukGIS.NDK.TGIS_Utils.Point(X, Y));

                _shpSelectPolygon.AddPoint(ptg);

                _shpSelectPolygon.Invalidate(false);
                //_tgisKarta.Update();
            }
            catch (Exception ex)
            {
              ExceptionHandler.HandleException(ex);
            }

        }

 
        /// <summary>
        /// Aveselekterar alla geometrier
        /// </summary>
        public void Avselektera()
        {
            TGIS_Shape tmpShp;
            TGIS_LayerVector ll = null;

            try
            {
                for (int i = 0; i < _tgisKarta.Items.Count; i++)
                {
                    try
                    {
                        int n = _tgisKarta.Items.Count;
                        if (_tgisKarta.Items[i] is TGIS_LayerVector)
                            ll = (TGIS_LayerVector)_tgisKarta.Items[i];
                    }
                    catch { }


                    if (ll != null)
                    {
                        tmpShp = ll.FindFirst(TGIS_Utils.GisWholeWorld()) as TGIS_Shape;

                        if (tmpShp != null)
                        {
                            tmpShp = tmpShp.MakeEditable();

                            for (int c = 0; c < ll.Items.Count; c++)
                            {
                                // we can do this because selected items are MakeEditable so they exist on Items list
                                tmpShp = (TGIS_Shape)ll.Items[c];

                                // set all shapes as not selected
                                tmpShp.IsSelected = false;
                                tmpShp.Invalidate(true);
                            }
                        }
                    }
                }

                _shapes.Clear();

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }
    }
}
