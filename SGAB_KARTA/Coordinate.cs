using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.CoordinateSystems;
using SharpMap.CoordinateSystems.Transformations;
using SharpMap.Converters.WellKnownText;
using TatukGIS.NDK;

namespace SGAB.SGAB_Karta
{
    /// <summary>
    /// Hjälperklass för koordinattransformation 
    /// </summary>
    public class Point
    {
        protected double _X;
        protected double _Y;

        private static System.Globalization.NumberFormatInfo _numInfo;
        static Point()
        {
            _numInfo = new System.Globalization.NumberFormatInfo();
            _numInfo.NumberDecimalSeparator = ".";
        }
        public Point(double x, double y)
        {
            _X = x;
            _Y = y;
        }
        
        public Point (string xstring, string ystring)
        {
            _X = 0d;
            _Y = 0d;
            double x = 0d, y = 0d;
            if ( double.TryParse(xstring, System.Globalization.NumberStyles.Float, _numInfo, out x ))
            {
                _X = x;
            }
            
            if ( double.TryParse(ystring, System.Globalization.NumberStyles.Float, _numInfo, out y ))
            {
                _Y = y;
            }
        }
        public Point() { }

        public double X 
        {
            set
            {
                _X = value;
            }
            get
            {
                return _X;
            }
        }

        public double Y 
        {
            set
            {
                _Y = value;
            }
            get
            {
                return _Y;
            }
        }

        /// <summary>
        /// Skriver över den befintlgia Equals-metoden, ty den fungerade inte i enhetstest. 
        /// </summary>
        /// <param name="obj">Objektet som skall jämföras med denna instansen</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            // If parameter cannot be cast to Point return false.
            Point p = obj as Point;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match. 
            return (X == p.X) && (Y == p.Y);
        }

        /// <summary>
        /// Hämtar en hashkod för objektet, använder sig av basklassen metod. 
        /// </summary>
        /// <returns>Returnerar en hashkod för denna instans. </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

	/// <summary>
	/// Transformerar från WGS84 till RT 90 2,5 gon V. 
	/// </summary>
	public class Tatuk_CoordinateTransform
	{
		/// <summary>
		/// Definitionen för WGS 84. 
		/// </summary>
		protected TGIS_CSCoordinateSystem cs_in_wgs84;

		/// <summary>
		/// Definitionen för RT 90 2,5 gon V. 
		/// </summary>
		protected TGIS_CSCoordinateSystem cs_out_RT90;

		/// <summary>
		/// Skapar en ny koordinattransformation. 
		/// </summary>
		public Tatuk_CoordinateTransform()
		{
			cs_in_wgs84 = TGIS_CSFactory.ByEPSG(4326);
			cs_out_RT90 = TGIS_CSFactory.ByEPSG(3021);
		}

		/// <summary>
		/// Transformerar från WGS84 till RT 90 2,5 gon V. Observera att koordinaterna skall skickas in longitud och sedan latitud. 
		/// </summary>
		/// <param name="coordinate"></param>
		/// <returns></returns>
		public TGIS_Point Transform(TGIS_Point coordinate)
		{
			return cs_out_RT90.FromCS(cs_in_wgs84, coordinate);
		}
	}

	/// <summary>
	/// Klass för att transformera koordinater
	/// </summary>
	public class CoordinateTransform
    {
        private ICoordinateTransformation _coordTrans;
        /// <summary>
        /// Enda Konstruktor 
        /// </summary>
        /// <param name="srInIni">Ini-sträng för källkoordinatsystemet i WKT</param>
        /// <param name="srOutIni">Ini-sträng för destinationskoordinatsystemet i WKT</param>
        public CoordinateTransform(string srInIni, string srOutIni)
        {
            IGeographicCoordinateSystem srIn =
                CoordinateSystemWktReader.Parse(srInIni) as IGeographicCoordinateSystem;
            IProjectedCoordinateSystem srOut =
                CoordinateSystemWktReader.Parse(srOutIni) as IProjectedCoordinateSystem;

            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            _coordTrans = ctfac.CreateFromCoordinateSystems(srIn ,srOut);
        }

        /// <summary>
        /// Transformera en punkt från ett koordinatsystem till ett annat
        /// </summary>
        /// <param name="ptIn">Punkt in</param>
        /// <returns>Punkt ut</returns>
        public Point Transform(Point ptIn)
        {
            double[] d = new double[2];
            d[0] = ptIn.X;
            d[1] = ptIn.Y;
            
            double[] dOut = _coordTrans.MathTransform.Transform(d);

            Point ptOut = new Point(dOut[0], dOut[1]);
            return ptOut;
        }
       
        /// <summary>
        /// Transformera en punkt från ett koordinatsystem till ett annat
        /// </summary>
        /// <param name="ptIn">Punkt in</param>
        /// <returns>Punkt ut</returns>
        public double[] Transform(double[] d)
        {
            double[] dOut = _coordTrans.MathTransform.Transform(d);

            return dOut;
        }
    }
}
