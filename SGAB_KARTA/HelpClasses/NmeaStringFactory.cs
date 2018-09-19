using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta.HelpClasses
{
    public class NmeaStringFactory
    {
        /// <summary>
        /// Hämtar eller sätter de möjliga Nmea-strängarna som finns i loggen. 
        /// </summary>
        public List<NmeaString> PossibleNmeas
        {
            get;
            set;
        }


        public List<NmeaString> Log
        {
            get;
            set;
        }

        public List<IParsePattern> ParsePattern
        {
            get;
            set;
        }

        protected int _ParsePatternIndex = 0;

        /// <summary>
        /// Skapar en ny fabrik för att läsa in en logg. 
        /// </summary>
        public NmeaStringFactory()
        {
            // Lägger till de fyra möjliga nmea-strängarna som finns i loggen.
            PossibleNmeas = new List<NmeaString>();
            PossibleNmeas.Add(new GPGGA());
            PossibleNmeas.Add(new GPGSA());
            PossibleNmeas.Add(new GPGSV());
            PossibleNmeas.Add(new GPRMC());

            // Anger hur parsningsmönstret ser ut i loggfilen
            ParsePattern = new List<IParsePattern>();
            ParsePattern.Add(new NmeaString());
            ParsePattern.Add(new NotIntresstedParsing());
            ParsePattern.Add(new NotIntresstedParsing());
            ParsePattern.Add(new NotIntresstedParsing());

            // Skapar en ny logg-lista. 
            Log = new List<NmeaString>();
        }


        public bool ParseLine(string lineToParse)
        {
            // Tar fram de första sex tecknen ur raden om det finns några
            string startOfLine = string.Empty;
            if (lineToParse.Length > 6)
                startOfLine = lineToParse.Substring(0, 6);

            // Skall bara läsa in första raden i loggen, resten är till för förtydligande
            if (this._ParsePatternIndex == 0)
            {
                foreach (IParsePattern nmea in this.PossibleNmeas)
                {
                    if (nmea.ParsePattern.Equals(startOfLine))
                    {
                        IParsePattern parsedNmea = nmea.ParseLine(lineToParse);
                        if (parsedNmea is NmeaString)
                        {
                            this.Log.Add((NmeaString)parsedNmea);
                            IncreaseParsePatternIndex();
                            return true;
                        }
                    }
                }
            }

            /**
             * Ökar på index om.m. vi har ett index större än noll. Detta för att efter en Nmea-rå-sträng kommer det alltid
             * minst tre ointressnta rader. Om det i loggen står en pil eller fel ifrån användare eller dator så skall dessa
             * inte brys om eftersom detta är tillfälliga saker som inte alltid är med i standardmönstret i loggen. 
             */
            if (this._ParsePatternIndex > 0)
                IncreaseParsePatternIndex();

            return false;
        }

        /// <summary>
        /// Ökar indexet för parsningen modulo antalet i mönstret. 
        /// </summary>
        protected virtual void IncreaseParsePatternIndex()
        {
            this._ParsePatternIndex = (this._ParsePatternIndex + 1) % ParsePattern.Count;
        }
    }
}
