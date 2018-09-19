using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta.HelpClasses
{
    public class GPGSA : NmeaString, IParsePattern
    {
        new protected string _NmeaString = "$GPGSA";

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

        public GPGSA()
        {
        }

        /// <summary>
        /// Skapar en ny GPGSA-sträng som parsas med TatukGIS. 
        /// </summary>
        /// <param name="lineToParse"></param>
        public GPGSA(string lineToParse)
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
            return new GPGSA(lineToParse);
        }
    }
}
