/********************************************************
 * GPSHandler.cs hanterar allt rörande kommunikation och 
 * hantering av GPS.
 * 
 * LSAM, SWECO Position, December 2005 
 * Modifierad av LSAM Februari 2008
 ********************************************************/
using System;
using System.Runtime.InteropServices;
using System.Globalization;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace SGAB.SGAB_Karta
{
    /// <summary>
    /// Summary description for GPSReader.
    /// </summary>
    [ComVisible(false)]
    public class GPSHandler
    {
        //Referens till formulärets GIS/kart-komponent
        TGIS_ViewerWnd _tgisKarta;

        TGIS_GpsNmea _tgisGPS;
        private TGIS_Shape _shpGps;

        Configuration _configuration = Configuration.GetConfiguration();

        //TGIS_ProjectionAbstract _obj_prj = null;

        string _srInIni;
        string _srOutIni;
		Tatuk_CoordinateTransform _coordTrans; //CoordinateTransform _coordTrans;

		TGIS_LayerVector _llGps = new TGIS_LayerVector();


        TGIS_Point _gpsPoint = new TGIS_Point();

        TGIS_SymbolAbstract _symbolN;
        TGIS_SymbolAbstract _symbolNO;
        TGIS_SymbolAbstract _symbolO;
        TGIS_SymbolAbstract _symbolSO;
        TGIS_SymbolAbstract _symbolS;
        TGIS_SymbolAbstract _symbolSV;
        TGIS_SymbolAbstract _symbolV;
        TGIS_SymbolAbstract _symbolNV;
        TGIS_SymbolAbstract _symbolDefault;

        ///// <summary>
        ///// GPS-handler ifrån Sharp GPS. 
        ///// </summary>
        //private SharpGis.SharpGps.GPSHandler _GPS_Sharp;

        protected GPSTracking.ITrackerFromTatukGIS _GpsTracker;

        /// <summary>
        /// Anger hur stor avstånd GPS-markören måste ha flyttas sig under för att dess
        /// position och pil skall uppdaterats. 
        /// </summary>
        protected double GPSDeviationInMetersMin;

        /// <summary>
        /// Anger hur stor avstånd GPS-markören inte får ha flyttas sig mer för att dess
        /// position och pil skall fortfarande uppdaterats. 
        /// </summary>
        protected double GPSDeviationInMetersMax;

        /// <summary>
        /// Hämtar eller sätter GPS-spåraren.
        /// </summary>
        public GPSTracking.ITrackerFromTatukGIS GpsTracker
        {
            get
            {
                return _GpsTracker;
            }
            set
            {
                _GpsTracker = value;

                // Sätter en lyssnare på rådata (NMEA) ifrån ShapgGPS 
                // MTTO: 2012-02-17. Kommenterar bort rad nedan, har inte SharpGPS-längre
                //_GPS_Sharp.NewGPSFix += new SharpGis.SharpGps.GPSHandler.NewGPSFixHandler(_GpsTracker.GPSRawGPRMCEventHandler);
            }
        }

        //public GPSHandler(TGIS_ViewerWnd GIS, TGIS_GpsNmea GPS, SharpGis.SharpGps.GPSHandler GPS_Sharp)
        public GPSHandler(TGIS_ViewerWnd GIS, TGIS_GpsNmea GPS)
        {
            _tgisKarta = GIS;
            _tgisGPS = GPS;

			// Hämtar hur stor avvikelse i meter som gäller när pilen skall ändras i kartan. 
			GPSDeviationInMetersMin = Configuration.GetConfiguration().GPSDeviationInMetersMin;
            GPSDeviationInMetersMax = Configuration.GetConfiguration().GPSDeviationInMetersMax;
        }

        public void CreateGPSLayerRT90()
        {
            //skapar ett lager med en punkt som kommer att användas som aktuell GPS-punkt
            try
            {

                //TGIS_ProjectionList prj_list = new TGIS_ProjectionList();

                //Transverse Mercator
                //_obj_prj = TGIS_Utils.ProjectionList.FindEx("TMR"); 

                ////projektionsfil för RT90, skapad av MASM
                //_obj_prj.SetUp(0.27587170754584828, 0, 1500064.274, -667.711, 0, 0, 0, 1.00000561024, 0, 0, 0, 0, 0, 0, 0);
                _srInIni = _configuration.srInIni;
                _srOutIni = _configuration.srOutIni;
				_coordTrans = new Tatuk_CoordinateTransform(); //new CoordinateTransform(_srInIni, _srOutIni);

				//lägger till lagret i kartan
				_llGps.Name = "GPSlager";
                _llGps.CachedPaint = false;

                _tgisKarta.Add((TGIS_LayerAbstract)_llGps);

                _llGps.Params.Marker.ShowLegend = true;

                TGIS_SymbolList symbolList = new TGIS_SymbolList();

                _symbolN = symbolList.Prepare(_configuration.SymbolGpsN);
                _symbolNO = symbolList.Prepare(_configuration.SymbolGpsNO);
                _symbolO = symbolList.Prepare(_configuration.SymbolGpsO);
                _symbolSO = symbolList.Prepare(_configuration.SymbolGpsSO);
                _symbolS = symbolList.Prepare(_configuration.SymbolGpsS);
                _symbolSV = symbolList.Prepare(_configuration.SymbolGpsSV);
                _symbolV = symbolList.Prepare(_configuration.SymbolGpsV);
                _symbolNV = symbolList.Prepare(_configuration.SymbolGpsNV);
                _symbolDefault = symbolList.Prepare(_configuration.SymbolGpsDefault);

                //_symbolN = symbolList.Prepare(_configuration.SymbolGpsN + "?TRUE");
                //_symbolNO = symbolList.Prepare(_configuration.SymbolGpsNO + "?TRUE");
                //_symbolO = symbolList.Prepare(_configuration.SymbolGpsO + "?TRUE");
                //_symbolSO = symbolList.Prepare(_configuration.SymbolGpsSO + "?TRUE");
                //_symbolS = symbolList.Prepare(_configuration.SymbolGpsS + "?TRUE");
                //_symbolSV = symbolList.Prepare(_configuration.SymbolGpsSV + "?TRUE");
                //_symbolV = symbolList.Prepare(_configuration.SymbolGpsV + "?TRUE");
                //_symbolNV = symbolList.Prepare(_configuration.SymbolGpsNV + "?TRUE");
                //_symbolDefault = symbolList.Prepare(_configuration.SymbolGpsDefault + "?TRUE");

                //skapar en punktshape i lagret
                //_shpGps = ((TGIS_LayerVector)_tgisKarta.Get("GPSlager")).CreateShape(TGIS_ShapeType.gisShapeTypePoint, TGIS_DimensionType.gisDimensionType2D);
                _shpGps = ((TGIS_LayerVector)_tgisKarta.Get("GPSlager")).CreateShape(TGIS_ShapeType.gisShapeTypePoint);
                //_shpGps.Lock(TGIS_Lock.gisLockExtent);
                _shpGps.AddPart();

                //lägger till en punkt med koordinat 0,0
                //_shpGps.AddPoint(new TGIS_Point(0,0));
                _shpGps.AddPoint(TGIS_Utils.GisPoint(0, 0));
                //_shpGps.Unlock();

                //LSAM: kommenterade bort 5 rader
                //TGIS_LayerAbstract layer = _tgisKarta.Get("GPSlager");
                //int zorder = layer.ZOrder;
                //layer.Move(-zorder);

                //layer.CachedPaint = false;
                //layer.IncrementalPaint = true;

                _tgisGPS.Nmea += new TGIS_GpsNmeaEvent(_tgisGPS_Nmea);
                _tgisGPS.NmeaMessage += new TGIS_GpsNmeaMessageEvent(_tgisGPS_NmeaMessage);

            }
            catch (Exception ex)
            {
                throw new GPSException(ex.InnerException, ex.Message + "i " + "CreateGPSLayerRT90");
            }
        }

        /// <summary>
        /// Skickar gps-rådata till spårningsprogrammet. 
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="e"></param>
        protected void _tgisGPS_NmeaMessage(object _sender, TGIS_GpsNmeaMessageEventArgs e)
        {
            this._GpsTracker.TGIS_GpsNmea_Signal(e);
        }

        /// <summary>
        /// Skickar den tolkade gps-signalen till spårningsprogrammet. 
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="e"></param>
        protected void _tgisGPS_Nmea(object _sender, TGIS_GpsNmeaEventArgs e)
        {
            this._GpsTracker.TGIS_GpsNmea_Signal(e);
        }

        public void ChangeGPSPosition(System.Windows.Forms.Label infoText, bool pan)
        {
            
            //Niklas TEST
            //Vad är inställningarna för lagren?
#if DEBUG
            foreach (var item in _tgisKarta.Items)
            {
                TGIS_LayerAbstract lyr = (TGIS_LayerAbstract)item;
                System.Diagnostics.Debug.Print(String.Format("Layer: {0}  CachedPaint:{1} IncrementalPaint:{2} ZOrder:{3} ZOrderEx:{4} UseConfig:{5} CS.EPSG:{6}", 
                    lyr.Name, lyr.CachedPaint.ToString(), lyr.IncrementalPaint.ToString(), lyr.ZOrder.ToString(), lyr.ZOrderEx.ToString(), lyr.UseConfig.ToString(), lyr.CS.FriendlyName));
            }
#endif
            
            //Uppdaterar aktuell GPS-position
            try
            {
                //TGIS_Coordinate coords = new TGIS_Coordinate();	
                TGIS_ParamsSectionVector paramGps;

                // Skickar information till gps-spårningen. 
                // MTTO: 2012-12-17. Kommenterar bort för SharpGPS har vi inte längre. 
                //this.GpsTracker.GPSEventHandler(this, _GPS_Sharp);

                //om satellitmottagningen är dålig fås ett varningsmeddelande
                //if (false_GPS_Sharp.HasGPSFix == false _tgisGPS.Satellites < 3)
                if (_tgisGPS.Satellites < 3)
                {
                    infoText.Text = "Varning! Ingen eller dålig GPS-mottagning. Antal satelliter är " + _tgisGPS.Satellites + " st. ";
                    _GpsTracker.ComputerRegisterMapOrGPSMessage("Varning! Ingen eller dålig GPS-mottagning. Antal satelliter är " + _tgisGPS.Satellites + " st. ", 
                        _gpsPoint.X, _gpsPoint.Y);

                    //_gpsPoint "nollställs" för att man inte ska luras att tro att man är kvar på föregående plats
                    _gpsPoint = new TGIS_Point();
                    //_gpsPoint = null;				

                }
                else
                {
                    infoText.Text = "";

					//om mottagningen tidigare varit dålig skapas _gpsPoint på nytt här                    
					//if (_gpsPoint. == null)
					//{
					//    _gpsPoint = new TGIS_Point();
					//}

					// MTTO: 2012-02-17. Kommenterat bort sharp-gps och lagt till tatuk. Tatuk skickar ut
					// positionen i radianer så jag konverterar tillbaka till grader med * (180 / Math.PI)
					//läser av koordinater från GPSen
					//coords.X = _GPS_Sharp.GPRMC.Position.Longitude;
					//coords.Y = _GPS_Sharp.GPRMC.Position.Latitude;
					//SGAB_Karta.Point coords = new SGAB_Karta.Point();
					TGIS_Point coords = new TGIS_Point();
                    coords.X = _tgisGPS.Longitude * (180 / Math.PI); 
                    coords.Y = _tgisGPS.Latitude * (180 / Math.PI);

                    //transformerar koordinater
                    //coords = _obj_prj.Projected((TGIS_Coordinate)coords);

                    coords = _coordTrans.Transform(coords);

                    try
                    {
                        double dX = _gpsPoint.X - coords.X; //positiv om flytt söder, negativ om flytt norr
                        double dY = _gpsPoint.Y - coords.Y; //positiv om flytt väster, negativ om flytt öster
                         
                        double euclideanDistance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

                        // ger _gpsPoint de nya koordinaterna om.m. de är rimliga.
                        if (GPSDeviationInMetersMin < euclideanDistance /*&& euclideanDistance < GPSDeviationInMetersMax*/)
                        {
                            _gpsPoint.X = coords.X;
                            _gpsPoint.Y = coords.Y;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new GPSException(ex.InnerException, ex.Message + "i " + "Del1 av ChangeGPSPosition");
                    }

                    if (_llGps != null)
                    {
                        //kopplar parametrar till lagret
                        paramGps = (TGIS_ParamsSectionVector)_llGps.Params;

                        //sätter symbol							
                        paramGps.Marker.Size = _configuration.SymbolSizeGps;

                        //Lina kommenterade bort 2010-05-05 efter att vi identifierat att
                        //denna rad påverkar GPS-pilens angivelse negativt
                        // Martin kommenterade bort denna rad, 2010-11-09, för att undvika -90 graders 
                        // felangivelse för riktningsiflen. 
                        // paramGps.Marker.SymbolRotate = TGIS_Utils.ParamFloat("1", 1);

                        paramGps.Marker.Symbol = GetGPSSymbol();

                    }

                    // Omdatera om gps-punkten inte ligger på punktens centroid. 
                    if (_gpsPoint.X == _shpGps.Centroid().X || _gpsPoint.Y == _shpGps.Centroid().Y)
                    {

                    }
                    else
                    {
                        //Uppdaterar positionen i kartlagret, om.m. ingen koordinat är lika med noll
                        if (_gpsPoint.X != 0 && _gpsPoint.Y != 0)
                            _shpGps.SetPosition(_gpsPoint, null, 0);

                        //om kartan ska centreras på GPSen
                        if (pan == false)
                        {                            
                            TGIS_Point ptg = new TGIS_Point();
                            ptg.X = _shpGps.Centroid().X;
                            ptg.Y = _shpGps.Centroid().Y;

                            //Centrerar kartan, om.m. ingen koordinat är lika med noll
                            //if (ptg.X != 0 && ptg.Y != 0)
                            if (_gpsPoint.X >= 0 && _gpsPoint.Y >= 0)
                            {
                                _tgisKarta.CenterViewport(ptg);
                            }
                            else
                            {
                                infoText.Text = "Varning! Orimliga koordinater ifrån GPS, svag mottagning. ";
                                _GpsTracker.ComputerRegisterMapOrGPSMessage("Varning! Orimliga koordinater ifrån GPS, svag mottagning. ", _gpsPoint.X, _gpsPoint.Y);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new GPSException(ex.InnerException, ex.Message + "i " + "ChangeGPSPosition");
            }
        }


        private TGIS_SymbolAbstract GetGPSSymbol()
        {
            //kontrollerar vilken symbol som ska väljas beroende på i vilken riktning man färdas
            try
            {
                //TGIS_SymbolList symbolList = new TGIS_SymbolList();
                TGIS_Point oldPoint = new TGIS_Point();
                TGIS_SymbolAbstract symbolAbstract; // = new TGIS_SymbolAbstract();

                double vinkelRadians = 0;
                double vinkelDegrees = 0;
                double dX = 0;
                double dY = 0;
                string symbol = null;

                if (_shpGps != null)
                {
                    //hämtar föregående punkt
                    oldPoint.X = _shpGps.Centroid().X;
                    oldPoint.Y = _shpGps.Centroid().Y;

                    //räknar ut skillnaden mellan den föregående och den nya punkten i x- och y-led.
                    //Kastar om X och Y igen eftersom koden för att hämta symbol är skriven enligt RT90, NIAL 2014-04-17
                    dY = oldPoint.X - _gpsPoint.X; //positiv om flytt söder, negativ om flytt norr      //(inkommande long, X, EW)
                    dX = oldPoint.Y - _gpsPoint.Y; //positiv om flytt väster, negativ om flytt öster    //(inkommande lat, Y, NS)


                    // Om vi för första gången anropar dennna metod skall standardsymbolen returneras. 
                    if (oldPoint.X == 0 && oldPoint.Y == 0)
                        return _symbolDefault;

                    // Räknar ut det euklidska avståndet. 
                    double euclideanDistance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

                    //if (Math.Abs(dX) > GPSDeviationInMeters || Math.Abs(dY) > GPSDeviationInMeters)
                    if (GPSDeviationInMetersMin < euclideanDistance && euclideanDistance < GPSDeviationInMetersMax)
                    {
                        //beräknar vinkeln mellan de två punkterna och tar reda på vilken symbol som hör till denna vinkel
                        if (dX > 0)//söder
                        {
                            if (dY > 0)//väster
                            {
                                vinkelRadians = Math.Atan2(dX, dY);
                                vinkelDegrees = vinkelRadians * (180 / Math.PI);

                                if (vinkelDegrees <= 22.5)
                                {
                                    symbol = "V";
                                }
                                if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
                                {
                                    symbol = "SV";
                                }
                                if (vinkelDegrees >= 67.5)
                                {
                                    symbol = "S";
                                }
                            }
                            else if (dY < 0)//öster
                            {
                                dY = Math.Abs(dY);
                                vinkelRadians = Math.Atan2(dX, dY);
                                vinkelDegrees = vinkelRadians * (180 / Math.PI);

                                if (vinkelDegrees <= 22.5)
                                {
                                    symbol = "O";
                                }
                                if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
                                {
                                    symbol = "SO";
                                }
                                if (vinkelDegrees >= 67.5)
                                {
                                    symbol = "S";
                                }
                            }
                            else if (dY == 0)
                            {
                                symbol = "S";
                            }
                        }
                        else if (dX < 0)//norr
                        {
                            if (dY > 0)//väster
                            {
                                dX = Math.Abs(dX);
                                vinkelRadians = Math.Atan2(dX, dY);
                                vinkelDegrees = vinkelRadians * (180 / Math.PI);

                                if (vinkelDegrees <= 22.5)
                                {
                                    symbol = "V";
                                }
                                if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
                                {
                                    symbol = "NV";
                                }
                                if (vinkelDegrees >= 67.5)
                                {
                                    symbol = "N";
                                }
                            }
                            else if (dY < 0)//öster
                            {
                                dX = Math.Abs(dX);
                                dY = Math.Abs(dY);
                                vinkelRadians = Math.Atan2(dX, dY);
                                vinkelDegrees = vinkelRadians * (180 / Math.PI);

                                if (vinkelDegrees <= 22.5)
                                {
                                    symbol = "O";
                                }
                                if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
                                {
                                    symbol = "NO";
                                }
                                if (vinkelDegrees >= 67.5)
                                {
                                    symbol = "N";
                                }
                            }
                            else if (dY == 0)
                            {
                                symbol = "N";
                            }
                        }
                        else if (dX == 0)
                        {
                            if (dY == 0)
                            {
                                symbol = "stillastående";
                            }
                            else if (dY > 0)
                            {
                                symbol = "V";
                            }
                            else if (dY < 0)
                            {
                                symbol = "O";
                            }
                        }

                        GpsTracker.ChangeArrow(oldPoint.X, oldPoint.Y, _gpsPoint.X, _gpsPoint.Y, dX, dY, euclideanDistance, symbol);
                    }

                    //hämtar rätt symbol
                    switch (symbol)
                    {
                        case "N":
                            {
                                symbolAbstract = _symbolN;
                                break;
                            }
                        case "NO":
                            {
                                symbolAbstract = _symbolNO;
                                break;
                            }
                        case "O":
                            {
                                symbolAbstract = _symbolO;
                                break;
                            }
                        case "SO":
                            {
                                symbolAbstract = _symbolSO;
                                break;
                            }
                        case "S":
                            {
                                symbolAbstract = _symbolS;
                                break;
                            }
                        case "SV":
                            {
                                symbolAbstract = _symbolSV;
                                break;
                            }
                        case "V":
                            {
                                symbolAbstract = _symbolV;
                                break;
                            }
                        case "NV":
                            {
                                symbolAbstract = _symbolNV;
                                break;
                            }
                        default:
                            {
                                symbolAbstract = _symbolDefault;
                                break;
                            }
                    }

                    if (symbolAbstract.Name != null)
                    {
                        //returnerar symbolen
                        return symbolAbstract;
                    }
                    else
                    {
                        _gpsPoint = new TGIS_Point();
                        //_gpsPoint = null;
                        throw new GPSException("Ogiltig sökväg till GPS-symbolen");

                    }
                }
                _gpsPoint = new TGIS_Point();
                //_gpsPoint = null;
                throw new GPSException("_shpGPS är null " + _gpsPoint.X + _gpsPoint.Y);

            }
            catch (Exception ex)
            {
                throw new GPSException(ex.InnerException, ex.Message + "i " + "GetGPSSymbol" + "p " + _llGps.Name + _tgisKarta.Get("GPSlager").IsOpened + "sX " + _shpGps.Centroid().X + "sY " + _shpGps.Centroid().Y + "px " + _gpsPoint.X + "py " + _gpsPoint.Y);
            }
        }


        //private TGIS_SymbolAbstract GetGPSSymbol()
        //{
        //    //kontrollerar vilken symbol som ska väljas beroende på i vilken riktning man färdas
        //    try
        //    {
        //        //TGIS_SymbolList symbolList = new TGIS_SymbolList();
        //        TGIS_Point oldPoint = new TGIS_Point();
        //        TGIS_SymbolAbstract symbolAbstract; // = new TGIS_SymbolAbstract();

        //        double vinkelRadians = 0;
        //        double vinkelDegrees = 0;
        //        double dX = 0;
        //        double dY = 0;
        //        string symbol = null;

        //        if (_shpGps != null)
        //        {
        //            //hämtar föregående punkt
        //            oldPoint.X = _shpGps.Centroid().X;
        //            oldPoint.Y = _shpGps.Centroid().Y;

        //            //räknar ut skillnaden mellan den föregående och den nya punkten i x- och y-led.
        //            dX = oldPoint.X - _gpsPoint.X; //positiv om flytt söder, negativ om flytt norr
        //            dY = oldPoint.Y - _gpsPoint.Y; //positiv om flytt väster, negativ om flytt öster

        //            // Om vi för första gången anropar dennna metod skall standardsymbolen returneras. 
        //            if (oldPoint.X == 0 && oldPoint.Y == 0)
        //                return _symbolDefault;

        //            // Räknar ut det euklidska avståndet. 
        //            double euclideanDistance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

        //            //if (Math.Abs(dX) > GPSDeviationInMeters || Math.Abs(dY) > GPSDeviationInMeters)
        //            if (GPSDeviationInMetersMin < euclideanDistance && euclideanDistance < GPSDeviationInMetersMax)
        //            {
        //                //beräknar vinkeln mellan de två punkterna och tar reda på vilken symbol som hör till denna vinkel
        //                if (dX > 0)//söder
        //                {
        //                    if (dY > 0)//väster
        //                    {
        //                        vinkelRadians = Math.Atan2(dX, dY);
        //                        vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                        if (vinkelDegrees <= 22.5)
        //                        {
        //                            symbol = "V";
        //                        }
        //                        if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                        {
        //                            symbol = "SV";
        //                        }
        //                        if (vinkelDegrees >= 67.5)
        //                        {
        //                            symbol = "S";
        //                        }
        //                    }
        //                    else if (dY < 0)//öster
        //                    {
        //                        dY = Math.Abs(dY);
        //                        vinkelRadians = Math.Atan2(dX, dY);
        //                        vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                        if (vinkelDegrees <= 22.5)
        //                        {
        //                            symbol = "O";
        //                        }
        //                        if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                        {
        //                            symbol = "SO";
        //                        }
        //                        if (vinkelDegrees >= 67.5)
        //                        {
        //                            symbol = "S";
        //                        }
        //                    }
        //                    else if (dY == 0)
        //                    {
        //                        symbol = "S";
        //                    }
        //                }
        //                else if (dX < 0)//norr
        //                {
        //                    if (dY > 0)//väster
        //                    {
        //                        dX = Math.Abs(dX);
        //                        vinkelRadians = Math.Atan2(dX, dY);
        //                        vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                        if (vinkelDegrees <= 22.5)
        //                        {
        //                            symbol = "V";
        //                        }
        //                        if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                        {
        //                            symbol = "NV";
        //                        }
        //                        if (vinkelDegrees >= 67.5)
        //                        {
        //                            symbol = "N";
        //                        }
        //                    }
        //                    else if (dY < 0)//öster
        //                    {
        //                        dX = Math.Abs(dX);
        //                        dY = Math.Abs(dY);
        //                        vinkelRadians = Math.Atan2(dX, dY);
        //                        vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                        if (vinkelDegrees <= 22.5)
        //                        {
        //                            symbol = "O";
        //                        }
        //                        if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                        {
        //                            symbol = "NO";
        //                        }
        //                        if (vinkelDegrees >= 67.5)
        //                        {
        //                            symbol = "N";
        //                        }
        //                    }
        //                    else if (dY == 0)
        //                    {
        //                        symbol = "N";
        //                    }
        //                }
        //                else if (dX == 0)
        //                {
        //                    if (dY == 0)
        //                    {
        //                        symbol = "stillastående";
        //                    }
        //                    else if (dY > 0)
        //                    {
        //                        symbol = "V";
        //                    }
        //                    else if (dY < 0)
        //                    {
        //                        symbol = "O";
        //                    }
        //                }

        //                GpsTracker.ChangeArrow(oldPoint.X, oldPoint.Y, _gpsPoint.X, _gpsPoint.Y, dX, dY, euclideanDistance, symbol);
        //            }

        //            //hämtar rätt symbol
        //            switch (symbol)
        //            {
        //                case "N":
        //                    {
        //                        symbolAbstract = _symbolN;
        //                        break;
        //                    }
        //                case "NO":
        //                    {
        //                        symbolAbstract = _symbolNO;
        //                        break;
        //                    }
        //                case "O":
        //                    {
        //                        symbolAbstract = _symbolO;
        //                        break;
        //                    }
        //                case "SO":
        //                    {
        //                        symbolAbstract = _symbolSO;
        //                        break;
        //                    }
        //                case "S":
        //                    {
        //                        symbolAbstract = _symbolS;
        //                        break;
        //                    }
        //                case "SV":
        //                    {
        //                        symbolAbstract = _symbolSV;
        //                        break;
        //                    }
        //                case "V":
        //                    {
        //                        symbolAbstract = _symbolV;
        //                        break;
        //                    }
        //                case "NV":
        //                    {
        //                        symbolAbstract = _symbolNV;
        //                        break;
        //                    }
        //                default:
        //                    {
        //                        symbolAbstract = _symbolDefault;
        //                        break;
        //                    }
        //            }

        //            if (symbolAbstract.Name != null)
        //            {
        //                //returnerar symbolen
        //                return symbolAbstract;
        //            }
        //            else
        //            {
        //                _gpsPoint = new TGIS_Point();
        //                //_gpsPoint = null;
        //                throw new GPSException("Ogiltig sökväg till GPS-symbolen");

        //            }
        //        }
        //        _gpsPoint = new TGIS_Point();
        //        //_gpsPoint = null;
        //        throw new GPSException("_shpGPS är null " + _gpsPoint.X + _gpsPoint.Y);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new GPSException(ex.InnerException, ex.Message + "i " + "GetGPSSymbol" + "p " + _llGps.Name + _tgisKarta.Get("GPSlager").IsOpened + "sX " + _shpGps.Centroid().X + "sY " + _shpGps.Centroid().Y + "px " + _gpsPoint.X + "py " + _gpsPoint.Y);
        //    }
        //}



        //private string GetDirection()
        //{
        //    string symbol = null;

        //    //kontrollerar vilken symbol som ska väljas beroende på i vilken riktning man färdas
        //    try
        //    {
        //        //TGIS_SymbolList symbolList = new TGIS_SymbolList();
        //        TGIS_Point oldPoint = new TGIS_Point();

        //        double vinkelRadians = 0;
        //        double vinkelDegrees = 0;
        //        double dX = 0;
        //        double dY = 0;


        //        if (_shpGps != null)
        //        {
        //            //hämtar föregående punkt
        //            oldPoint.X = _shpGps.Centroid().X;
        //            oldPoint.Y = _shpGps.Centroid().Y;

        //            //räknar ut skillnaden mellan den föregående och den nya punkten i x- och y-led.
        //            dX = oldPoint.X - _gpsPoint.X; //positiv om flytt söder, negativ om flytt norr
        //            dY = oldPoint.Y - _gpsPoint.Y; //positiv om flytt väster, negativ om flytt öster

        //            // Om vi för första gången anropar dennna metod skall standardsymbolen returneras. 
        //            if (oldPoint.X == 0 && oldPoint.Y == 0)
        //                return "stillastående";

        //            // Räknar ut det euklidska avståndet. 
        //            double euclideanDistance = Math.Sqrt(Math.Pow(dX, 2) + Math.Pow(dY, 2));

        //            //if (Math.Abs(dX) > GPSDeviationInMeters || Math.Abs(dY) > GPSDeviationInMeters)
        //            if (GPSDeviationInMetersMin < euclideanDistance && euclideanDistance < GPSDeviationInMetersMax)
        //            {
        //                //beräknar vinkeln mellan de två punkterna och tar reda på vilken symbol som hör till denna vinkel
        //                if (dX > 0)//söder
        //                {
        //                    //if (dY > 0)//väster
        //                    //{
        //                    //    vinkelRadians = Math.Atan2(dX, dY);
        //                    //    vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                    //    if (vinkelDegrees <= 22.5)
        //                    //    {
        //                    //        symbol = "V";
        //                    //    }
        //                    //    if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                    //    {
        //                    //        symbol = "SV";
        //                    //    }
        //                    //    if (vinkelDegrees >= 67.5)
        //                    //    {
        //                    //        symbol = "S";
        //                    //    }
        //                    //}
        //                    //else if (dY < 0)//öster
        //                    //{
        //                    //    dY = Math.Abs(dY);
        //                    //    vinkelRadians = Math.Atan2(dX, dY);
        //                    //    vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                    //    if (vinkelDegrees <= 22.5)
        //                    //    {
        //                    //        symbol = "O";
        //                    //    }
        //                    //    if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                    //    {
        //                    //        symbol = "SO";
        //                    //    }
        //                    //    if (vinkelDegrees >= 67.5)
        //                    //    {
        //                    //        symbol = "S";
        //                    //    }
        //                    //}
        //                    //else if (dY == 0)
        //                    //{
        //                    //    symbol = "S";
        //                    //}
        //                }
        //                else if (dX < 0)//norr
        //                {
        //                    //if (dY > 0)//väster
        //                    //{
        //                    //    dX = Math.Abs(dX);
        //                    //    vinkelRadians = Math.Atan2(dX, dY);
        //                    //    vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                    //    if (vinkelDegrees <= 22.5)
        //                    //    {
        //                    //        symbol = "V";
        //                    //    }
        //                    //    if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                    //    {
        //                    //        symbol = "NV";
        //                    //    }
        //                    //    if (vinkelDegrees >= 67.5)
        //                    //    {
        //                    //        symbol = "N";
        //                    //    }
        //                    //}
        //                    //else if (dY < 0)//öster
        //                    //{
        //                    //    dX = Math.Abs(dX);
        //                    //    dY = Math.Abs(dY);
        //                    //    vinkelRadians = Math.Atan2(dX, dY);
        //                    //    vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                    //    if (vinkelDegrees <= 22.5)
        //                    //    {
        //                    //        symbol = "O";
        //                    //    }
        //                    //    if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                    //    {
        //                    //        symbol = "NO";
        //                    //    }
        //                    //    if (vinkelDegrees >= 67.5)
        //                    //    {
        //                    //        symbol = "N";
        //                    //    }
        //                    //}
        //                    //else if (dY == 0)
        //                    //{
        //                    //    symbol = "N";
        //                    //}
        //                }
        //                else if (dX == 0)
        //                {
        //                    if (dY == 0)
        //                    {
        //                        symbol = "stillastående";
        //                    }
        //                    else if (dY > 0)
        //                    {
        //                        symbol = "V";
        //                    }
        //                    else if (dY < 0)
        //                    {
        //                        symbol = "O";
        //                    }
        //                }

        //                GpsTracker.ChangeArrow(oldPoint.X, oldPoint.Y, _gpsPoint.X, _gpsPoint.Y, dX, dY, euclideanDistance, symbol);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new GPSException(ex.InnerException, ex.Message + "i " + "GetGPSSymbol" + "p " + _llGps.Name + _tgisKarta.Get("GPSlager").IsOpened + "sX " + _shpGps.Centroid().X + "sY " + _shpGps.Centroid().Y + "px " + _gpsPoint.X + "py " + _gpsPoint.Y);
        //    }

        //    return symbol;
        //}


        //ska tas bort när demo inte är aktuellt längre
        //******************************************************************
        //public string DemoGPSposition(int i, bool pan)
        //{
        //    try
        //    {
        //        double[] y = new double[17] {6405139,6405039,6404939,6404839,6404838,6404838,6404839,6404939,6405039,
        //                                            6405139,6405239,6405339,6405340,6405340,6405339,6405239,6405139};

        //        double[] x = new double[17] {1271453,1271453,1271454,1271554,1271654,1271754,1271854,1271954,1271955,
        //                                            1271955,1271954,1271854,1271754,1271654,1271554,1271454,1271453};

        //        double vinkelRadians = 0;
        //        double vinkelDegrees = 0;
        //        double dX = 0;
        //        double dY = 0;

        //        string symbol = null;
        //        TGIS_Point oldPoint = new TGIS_Point();

        //        TGIS_SymbolList symbolList = new TGIS_SymbolList();
        //        TGIS_ParamsSectionVector paramGps;

        //        //kopplar parametrar till lagret
        //        paramGps = (TGIS_ParamsSectionVector)_llGps.Params;

        //        //sätter symbol
        //        oldPoint = _shpGps.GetFirstPoint();

        //        dX = oldPoint.Y - y[i]; //positiv om flytt söder, negativ om flytt norr
        //        dY = oldPoint.X - x[i]; //positiv om flytt väster, negativ om flytt öster

        //        if (dX > 0)//söder
        //        {
        //            if (dY > 0)//väster
        //            {
        //                vinkelRadians = Math.Atan2(dX, dY);
        //                vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                if (vinkelDegrees <= 22.5)
        //                {
        //                    symbol = "V";
        //                }
        //                if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                {
        //                    symbol = "SV";
        //                }
        //                if (vinkelDegrees >= 67.5)
        //                {
        //                    symbol = "S";
        //                }
        //            }
        //            else if (dY < 0)//öster
        //            {
        //                dY = Math.Abs(dY);
        //                vinkelRadians = Math.Atan2(dX, dY);
        //                vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                if (vinkelDegrees <= 22.5)
        //                {
        //                    symbol = "O";
        //                }
        //                if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                {
        //                    symbol = "SO";
        //                }
        //                if (vinkelDegrees >= 67.5)
        //                {
        //                    symbol = "S";
        //                }
        //            }
        //            else if (dY == 0)
        //            {
        //                symbol = "S";
        //            }
        //        }
        //        else if (dX < 0)//norr
        //        {
        //            if (dY > 0)//väster
        //            {
        //                dX = Math.Abs(dX);
        //                vinkelRadians = Math.Atan2(dX, dY);
        //                vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                if (vinkelDegrees <= 22.5)
        //                {
        //                    symbol = "V";
        //                }
        //                if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                {
        //                    symbol = "NV";
        //                }
        //                if (vinkelDegrees >= 67.5)
        //                {
        //                    symbol = "N";
        //                }
        //            }
        //            else if (dY < 0)//öster
        //            {
        //                dX = Math.Abs(dX);
        //                dY = Math.Abs(dY);
        //                vinkelRadians = Math.Atan2(dX, dY);
        //                vinkelDegrees = vinkelRadians * (180 / Math.PI);

        //                if (vinkelDegrees <= 22.5)
        //                {
        //                    symbol = "O";
        //                }
        //                if (vinkelDegrees > 22.5 && vinkelDegrees < 67.5)
        //                {
        //                    symbol = "NO";
        //                }
        //                if (vinkelDegrees >= 67.5)
        //                {
        //                    symbol = "N";
        //                }
        //            }
        //            else if (dY == 0)
        //            {
        //                symbol = "N";
        //            }
        //        }
        //        else if (dX == 0)
        //        {
        //            if (dY == 0)
        //            {
        //                symbol = "ej flytt";
        //            }
        //            else if (dY > 0)
        //            {
        //                symbol = "V";
        //            }
        //            else if (dY < 0)
        //            {
        //                symbol = "O";
        //            }
        //        }

        //        switch (symbol)
        //        {
        //            case "N":
        //                {
        //                    paramGps.Marker.Symbol = symbolList.Prepare(_configuration.SymbolGpsN + "?TRUE");
        //                    break;
        //                }
        //            case "NO":
        //                {
        //                    paramGps.Marker.Symbol = symbolList.Prepare(_configuration.SymbolGpsNO + "?TRUE");
        //                    break;
        //                }
        //            case "O":
        //                {
        //                    paramGps.Marker.Symbol = symbolList.Prepare(_configuration.SymbolGpsO + "?TRUE");
        //                    break;
        //                }
        //            case "SO":
        //                {
        //                    paramGps.Marker.Symbol = symbolList.Prepare(_configuration.SymbolGpsSO + "?TRUE");
        //                    break;
        //                }
        //            case "S":
        //                {
        //                    paramGps.Marker.Symbol = symbolList.Prepare(_configuration.SymbolGpsS + "?TRUE");

        //                    if (symbolList.Prepare(_configuration.SymbolGpsS) != null)
        //                    {
        //                    }
        //                    else
        //                    {
        //                        _gpsPoint = new TGIS_Point();
        //                        //_gpsPoint = null;
        //                        throw new GPSException("Ogiltig sökväg till GPS-symbolen");

        //                    }

        //                    break;
        //                }
        //            case "SV":
        //                {
        //                    paramGps.Marker.Symbol = symbolList.Prepare(_configuration.SymbolGpsSV + "?TRUE");
        //                    break;
        //                }
        //            case "V":
        //                {
        //                    paramGps.Marker.Symbol = symbolList.Prepare(_configuration.SymbolGpsV + "?TRUE");
        //                    break;
        //                }
        //            case "NV":
        //                {
        //                    paramGps.Marker.Symbol = symbolList.Prepare(_configuration.SymbolGpsNV + "?TRUE");
        //                    break;
        //                }
        //        }
        //        paramGps.Marker.Size = _configuration.SymbolSizeGps;

        //        //om mottagningen tidigare varit dålig skapas _gpsPoint på nytt här
        //        //_gpsPoint = new TGIS_Point();
        //        //if (_gpsPoint == null)
        //        //{
        //        //    _gpsPoint = new TGIS_Point();
        //        //}

        //        _gpsPoint.X = x[i];
        //        _gpsPoint.Y = y[i];

        //        _shpGps.SetPosition(_gpsPoint, null, 0);



        //        if (pan == false)
        //        {
        //            TGIS_Point ptg = new TGIS_Point();
        //            ptg.X = _shpGps.Centroid().X;
        //            ptg.Y = _shpGps.Centroid().Y;
        //            _tgisKarta.CenterViewport(ptg);

        //        }
        //        return symbol;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new GPSException(ex.Message);
        //    }
        //}
    }
}
