using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta.HelpClasses
{
    public class GPGSV : NmeaString, IParsePattern
    {
        new protected string _NmeaString = "$GPGSV";

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

        public GPGSV()
        {
        }

        /// <summary>
        /// Skapar en ny GPRMC-sträng som parsas med TatukGIS. 
        /// </summary>
        /// <param name="lineToParse"></param>
        public GPGSV(string lineToParse)
            : base(lineToParse)
        {
            this._tgisNmea.ParseText(lineToParse);
        }

        /// <summary>
        /// Läser in raden och sparar dess data. 
        /// </summary>
        /// <param name="lineToParse">Raden som skall läsas in</param>
        /// <returns>Returnerar ut sig själv när parningen är klar. </returns>
        new public IParsePattern ParseLine(string lineToParse)
        {
            return new GPGSV(lineToParse);
        }
    }
}
