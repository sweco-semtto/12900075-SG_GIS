using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta.HelpClasses
{
    public class GPGGA : NmeaString, IParsePattern, ICoordinates
    {
        new protected string _NmeaString = "$GPGGA";

        /// <summary>
        /// Hämtar ut texten som skall parsas in. 
        /// </summary>
        new public string ParsePattern
        {
            get
            {
                return this._NmeaString;
            }
        }

        public GPGGA()
        {
        }

        /// <summary>
        /// Skapar en ny GPRMC-sträng som parsas med TatukGIS. 
        /// </summary>
        /// <param name="lineToParse"></param>
        public GPGGA(string lineToParse)
            : base(lineToParse)
        {
            this._tgisNmea.ParseText(lineToParse);

            int a = 0;
        }

        /// <summary>
        /// Läser in raden och sparar dess data. 
        /// </summary>
        /// <param name="lineToParse">Raden som skall läsas in</param>
        /// <returns>Returnerar ut sig själv när parningen är klar. </returns>
        new public IParsePattern ParseLine(string lineToParse)
        {
            return new GPGGA(lineToParse);
        }
    }
}
