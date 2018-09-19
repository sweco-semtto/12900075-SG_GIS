using System;
using System.Collections.Generic;
using System.Text;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace SGAB.SGAB_Karta.HelpClasses
{
    public class NmeaString : IParsePattern
    {
        /// <summary>
        /// En statisk variabel som anger hur Nmea-strängen ser ut. 
        /// </summary>
        protected static string _NmeaString;

        /// <summary>
        /// Tatukgis-objeket som används för att parsa en rad. 
        /// </summary>
        protected TGIS_GpsNmea _tgisNmea;

        /// <summary>
        /// En statisk metod som hämtar hur Nmea-strängen ser ut. 
        /// </summary>
        public string ParsePattern
        {
            get
            {
                return _NmeaString;
            }
        }

        public NmeaString()
        {
        }

        /// <summary>
        /// Konstruktior som skapar ett TatukGIS-objet att läsa in nmea-strängar med. 
        /// </summary>
        /// <param name="lineToParse"></param>
        public NmeaString(string lineToParse)
        {
            this._tgisNmea = new TGIS_GpsNmea();
        }


        /// <summary>
        /// Denna metod skall inte användas är enbart till för att flagga underklasserna som ärver ifrån denna klass. 
        /// </summary>
        /// <param name="lineToParse">Raden som skall läsas in</param>
        /// <returns>Returnerar alltid null. </returns>
        public IParsePattern ParseLine(string lineToParse)
        {
            return null;
        }
    }
}
