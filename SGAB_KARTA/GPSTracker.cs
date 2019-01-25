using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace SGAB.GPSTracking
{
    public delegate void UserRegisterMapOrGPSErrorHandler(object sender, EventArgs e);

    public class GPSTracker : ITrackerBase, ITrackerChangePositionInMap, ITrackerFromTatukGIS
    {
        /// <summary>
        /// Event som registerar när användaren tycker på knappen för ett kart- eller gpsfel och felet registeras. 
        /// </summary>
        public event UserRegisterMapOrGPSErrorHandler MapOrGPSError;

        /// <summary>
        /// Data som sparas ifrån GPS:en. 
        /// </summary>
        public StringBuilder TextInTextFile
        {
            get;
            protected set;
        }

        /// <summary>
        /// Hämtar eller sätter antalet tecken innan skrivnig till fil sker. 
        /// </summary>
        public int LimitCharactersUntillWriteToFile
        {
            get;
            set;
        }

        protected string _PathToDirectoryContainingLogFiles;

        /// <summary>
        /// Hämtar eller sätter sökvägen till mappen där alla loggfilerna skall ligga. Kan ge exception om mappen som behövs inte får skapas. 
        /// </summary>
        public string PathToDirectoryContainingLogFiles
        {
            get
            {
                return _PathToDirectoryContainingLogFiles;
            }
            set
            {
                try
                {
                    _PathToDirectoryContainingLogFiles = value;

                    if (!Directory.Exists(PathToDirectoryContainingLogFiles))
                        Directory.CreateDirectory(PathToDirectoryContainingLogFiles);
                }
                catch (Exception ex)
                {
                    TrackingAvailable = false;
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Hämtar eller sätter om GPS-spårning är möjlig. Anledningen till att GPS-spårning inte kan fungera är att skirvrättigheter till logg-mappen saknas. 
        /// </summary>
        public bool TrackingAvailable
        {
            get;
            protected set;
        }

        /// <summary>
        /// Hämtar eller sätter om spårning av GPS:en skall ske. 
        /// </summary>
        public bool TrackGPS
        {
            get;
            set;
        }

        /// <summary>
        /// Hämtar eller sätter att ett fel har uppstått. 
        /// </summary>
        public bool Error
        {
            get;
            set;
        }

        /// <summary>
        /// Hämtar eller sätter att ett fel har uppstått. 
        /// </summary>
        public bool ErrorUser
        {
            get;
            set;
        }

        /// <summary>
        /// Hämtar eller sätter att ett fel har uppstått. 
        /// </summary>
        public bool ErrorComputer
        {
            get;
            set;
        }

        private string newLineInTextFile = "\r\n";

        public GPSTracker()
        {
            TextInTextFile = new StringBuilder();
            LimitCharactersUntillWriteToFile = 75000;
            TrackingAvailable = true;
            TrackGPS = false;
        }

        /// <summary>
        /// Registerar ett kart- eller gpsfel ifrån användaren. Skickar event till lyssnare genom eventet "MapOrGPSError". 
        /// </summary>
        public void UserRegisterMapOrGPSError()
        {
            if (!TrackingAvailable)
                return;

            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append("---> FEL! Upptäckt av användaren");
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append("---\\");
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append(newLineInTextFile);

            Error = true;
            ErrorUser = true;

            if (MapOrGPSError != null)
                MapOrGPSError(this, new EventArgs());
        }

        /// <summary>
        /// Registerar ett kart- eller gpsfel ifrån användaren. Skickar event till lyssnare genom eventet "MapOrGPSError". 
        /// </summary>
        /// <param name="message">Felmeddelandet som skall visas</param>
        /// <param name="x">X-koordinaten. </param>
        /// <param name="y">Y-koordinaten. </param>
        public void ComputerRegisterMapOrGPSMessage(string message, double x, double y)
        {
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append("---> DATOR!");
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append(message);
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append("x: " + x + "   y: " + y);
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append("---\\");
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append(newLineInTextFile);

            Error = true;
            ErrorComputer = true;
        }

        public void StartTracking()
        {
            TrackGPS = true;
        }

        public void StopTracking()
        {
            TrackGPS = false;
            WriteToFile();
        }

        /// <summary>
        /// Responds to sentence events from GPS receiver
        /// </summary>
        public void GPSNmeaEventHandler(object sender, TGIS_GpsNmea gpsNmea)
        {
            if (!TrackGPS)
                return;

            // Lägger till en tidsstämpel
            TextInTextFile.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            TextInTextFile.Append(newLineInTextFile);

            // Lägger till Status
            TextInTextFile.Append("Antal sateliter: ");
            TextInTextFile.Append(gpsNmea.Satellites);
            TextInTextFile.Append(newLineInTextFile);

            // Lägger till Position
            TextInTextFile.Append("Latitud: ");
            TextInTextFile.Append(gpsNmea.Latitude);
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append("Longitud: ");
            TextInTextFile.Append(gpsNmea.Longitude);
            TextInTextFile.Append(newLineInTextFile);

            // Lägger till Hastighet
            TextInTextFile.Append("Hastighet: ");
            TextInTextFile.Append(gpsNmea.Speed);
            TextInTextFile.Append(newLineInTextFile);

            // Lägger till Kurs
            TextInTextFile.Append("Kurs: ");
            TextInTextFile.Append(gpsNmea.Course);
            TextInTextFile.Append(newLineInTextFile);

			// Lägger till Höjd
			TextInTextFile.Append("Höjd: ");
			TextInTextFile.Append(gpsNmea.Altitude);
			TextInTextFile.Append(newLineInTextFile);

			// Lägger till Precision
			TextInTextFile.Append("Precision: ");
            TextInTextFile.Append(gpsNmea.PositionPrec);
            TextInTextFile.Append(newLineInTextFile);

			// Lägger till Tid
			TextInTextFile.Append("Precision: ");
			TextInTextFile.Append(gpsNmea.PositionTime);
			TextInTextFile.Append(newLineInTextFile);

			// Lägger till sluttag. 
			TextInTextFile.Append("--------------------");
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append(newLineInTextFile);

            WriteToFile();
        }

        /// <summary>
        /// Tar emot rådata ifrån SharpGps. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GPSRawGPRMCEventHandler(object sender, TGIS_GpsNmeaEventArgs e)
        {
            if (!TrackGPS)
                return;

            TextInTextFile.Append("******");
            TextInTextFile.Append(newLineInTextFile);

            // Lägger till en tidsstämpel
            TextInTextFile.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            TextInTextFile.Append(newLineInTextFile);

            TextInTextFile.Append(e.Name);
            TextInTextFile.Append(newLineInTextFile);
			TextInTextFile.Append(WriteSentence(e.Items));
			TextInTextFile.Append(newLineInTextFile);
			TextInTextFile.Append("******");
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append(newLineInTextFile);
        }

		/// <summary>
		/// Skriver ut hela meningen från gps:en. 
		/// </summary>
		/// <param name="sentence">Meningen som skall skrivas upp. </param>
		/// <returns></returns>
		protected string WriteSentence(ArrayList sentence)
		{
			StringBuilder ans = new StringBuilder();
			foreach (Object part in sentence)
				ans.Append(((string)part).ToString());

			return ans.ToString();
		}

        /// <summary>
        /// Skriver till fil om längden är för stor. 
        /// </summary>
        public void WriteToFile()
        {
            if (TextInTextFile.Length > LimitCharactersUntillWriteToFile || !TrackGPS)
            {
                string fileName = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ".txt";

                if (ErrorUser)
                    fileName = "FEL " + fileName;

                TextWriter textWriter = new StreamWriter(PathToDirectoryContainingLogFiles + "\\" + fileName);

                textWriter.Write(TextInTextFile.ToString());
                textWriter.Close();

                TextInTextFile.Remove(0, TextInTextFile.Length);
                Error = false;
                ErrorUser = false;
                ErrorComputer = false;
            }
        }

        /// <summary>
        /// Skriver till loggen när en pil ändras. 
        /// </summary>
        /// <param name="oldX">Gamla x-koordianten. </param>
        /// <param name="oldY">Gamla y-koordinaten. </param>
        /// <param name="newX">Nya x-koordinaten. </param>
        /// <param name="newY">Nya y-koordianten. </param>
        /// <param name="dX">Ändringen i x-led. </param>
        /// <param name="dY">Ändringen i y-led. </param>
        /// <param name="dE">Det eukliska avståndet. </param>
        /// <param name="pointOfCompass">Anger vilket väderstreck som pilen visar. </param>
        public void ChangeArrow(double oldX, double oldY, double newX, double newY, double dX, double dY, double dE, string pointOfCompass)
        {
            TextInTextFile.Append("Pil: ");
            TextInTextFile.Append(pointOfCompass);
            TextInTextFile.Append("  |  ");
            TextInTextFile.Append("dE=");
            TextInTextFile.Append(dE);
            TextInTextFile.Append("  ");
            TextInTextFile.Append("dX=");
            TextInTextFile.Append(dX);
            TextInTextFile.Append("  ");
            TextInTextFile.Append("dY=");
            TextInTextFile.Append(dY);
            TextInTextFile.Append("  |  ");
            TextInTextFile.Append("oldX=");
            TextInTextFile.Append(oldX);
            TextInTextFile.Append("  ");
            TextInTextFile.Append("oldY=");
            TextInTextFile.Append(oldY);
            TextInTextFile.Append("  ");
            TextInTextFile.Append("newX=");
            TextInTextFile.Append(newX);
            TextInTextFile.Append("  ");
            TextInTextFile.Append("newY=");
            TextInTextFile.Append(newY);
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append(newLineInTextFile);
        }

        /// <summary>
        /// Sparar ner signalen (rådata) direkt ifrån gps:en till en loggfil. 
        /// </summary>
        /// <param name="RawdataFromGps">Signalen (rådata) direkt ifrån gps:en. </param>
        public void TGIS_GpsNmea_Signal(TGIS_GpsNmeaMessageEventArgs RawdataFromGps)
        {
            if (!TrackGPS)
                return;

            TextInTextFile.Append(RawdataFromGps.NmeaMessage);
            TextInTextFile.Append(newLineInTextFile);
        }


        /// <summary>
        /// Sparar ner signalen (rådata) direkt ifrån gps:en till en loggfil. 
        /// </summary>
        /// <param name="RawdataFromGps">Signalen (rådata) direkt ifrån gps:en. </param>
        public void TGIS_GpsNmea_Signal(TGIS_GpsNmeaEventArgs InterpretedDataFromGps)
        {
            if (!TrackGPS)
                return;

            TextInTextFile.Append(InterpretedDataFromGps.Name);
            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append("Inläst = ");
            TextInTextFile.Append(InterpretedDataFromGps.Parsed);
            TextInTextFile.Append(newLineInTextFile);

            for (int i = 0; i < InterpretedDataFromGps.Items.Count; i++)
            {
                TextInTextFile.Append(InterpretedDataFromGps.Items[i].ToString());
                if (i < InterpretedDataFromGps.Items.Count -1)
                    TextInTextFile.Append(",");
            }

            TextInTextFile.Append(newLineInTextFile);
            TextInTextFile.Append(newLineInTextFile);
        }
    }

    public interface ITrackerBase
    {
        /// <summary>
        /// Metod som användaren kan anropa när hon upptäcker ett fel. 
        /// </summary>
        void UserRegisterMapOrGPSError();

        /// <summary>
        /// Metod som programmerare kan anropa när fel uppkommer. 
        /// </summary>
        /// <param name="message">Felmeddelande som skall visas i loggen. </param>
        /// <param name="x">X-koordinaten. </param>
        /// <param name="y">Y-koordinaten. </param>
        void ComputerRegisterMapOrGPSMessage(string message, double x, double y);

        /// <summary>
        /// Event som registerar när användaren tycker på knappen för ett kart- eller gpsfel och felet registeras. 
        /// </summary>
        event UserRegisterMapOrGPSErrorHandler MapOrGPSError;
    }

    /// <summary>
    /// Ett interface som talar om att pilen i kartan ändras, detta för att positionen har flyttas sig tillräckligt
    /// mycket sedan senaste uppdateringen av positionen. 
    /// </summary>
    public interface ITrackerChangePositionInMap : ITrackerBase
    {
        /// <summary>
        /// Tar emot vilka koordinater det gäller och hur stor ändrigen är. 
        /// </summary>
        /// <param name="oldX">Gamla x-koordianten. </param>
        /// <param name="oldY">Gamla y-koordinaten. </param>
        /// <param name="newX">Nya x-koordinaten. </param>
        /// <param name="newY">Nya y-koordianten. </param>
        /// <param name="dX">Ändringen i x-led. </param>
        /// <param name="dY">Ändringen i y-led. </param>
        /// <param name="dE">Det eukliska avståndet. </param>
        /// <param name="pointOfCompass">Anger vilket väderstreck som pilen visar. </param>
        void ChangeArrow(double oldX, double oldY, double newX, double newY, double dX, double dY, double dE, string pointOfCompass);
    }

    /// <summary>
    /// Interface som talar om vilka metoder gpsspårningen skall ha. 
    /// </summary>
    public interface ITrackerFromTatukGIS : ITrackerChangePositionInMap
    {
        /// <summary>
        /// En handler som tar hand om rådata-signalen ifrån gps:en. 
        /// </summary>
        /// <param name="RawdataFromGps"></param>
        void TGIS_GpsNmea_Signal(TGIS_GpsNmeaMessageEventArgs RawdataFromGps);

        /// <summary>
        /// En handler som tar hand om den tolkade signalen ifrån gps:en. 
        /// </summary>
        /// <param name="InterpretedDataFromGps"></param>
        void TGIS_GpsNmea_Signal(TGIS_GpsNmeaEventArgs InterpretedDataFromGps);
    }
}
