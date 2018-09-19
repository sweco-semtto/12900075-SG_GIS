using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta.HelpClasses
{
    public class NotIntresstedParsing : IParsePattern
    {
        /// <summary>
        /// En statisk variabel som anger hur en korrekt inlös Nmea-sträng visas i loggen.  
        /// </summary>
        protected static string _ParsedFromTGIS = string.Empty;

        /// <summary>
        /// Hämtar ut texten som skall parsas in. 
        /// </summary>
        public string ParsePattern
        {
            get
            {
                return NotIntresstedParsing._ParsedFromTGIS;
            }
        }

        /// <summary>
        /// Läser in raden och sparar dess data. 
        /// </summary>
        /// <param name="lineToParse">Raden som skall läsas in</param>
        /// <returns>Returnerar ut sig själv när parningen är klar. </returns>
        public IParsePattern ParseLine(string lineToParse)
        {
            return new NotIntresstedParsing();
        }
    }
}
