/***************************************************
 * Configuration.cs
 * 
 * Johan Ander, SWECO Position, 2006 
 ***************************************************/

using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Configuration;

namespace SGAB.SGAB_Karta
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
    //[ComVisible(false)]
	public class Configuration
	{
		private static readonly Configuration _instance = new Configuration();
		
		private const string CONFIG_FILE = "SGAB.exe.config";
		private const string LOG_FILE = "App.log";

		private Hashtable _ht = null;
		
		public Configuration()
		{
			try
			{
				_ht = new Hashtable(100, 0.5f);
				ReadConfigFile();

			}
			catch(Exception ex)
			{
                throw new NullReferenceException("Kan inte läsa configfil: \n\n" + ex);
			}
		}
	
		/// <summary>
		/// Hämtar ett värde från tabellen
		/// </summary>
		/// <param name="key">Nyckel att söka på</param>
		/// <returns>Värdet som string</returns>
		public string GetValue(string key)
		{
			return (string)_ht[key];
		}


		/// <summary>
		/// Lägger till ett objekt i hashtablen
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>
		private void AddValue(string key, string val)
		{
			_ht.Add(key, val);
		}


		private string GetExecutingAssemblyPath()
		{
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				
			if ( !path.EndsWith(@"\") )
			{
				path += @"\";
			}

			return path;
		}


		public bool LogExceptions
		{
			get 
			{ 
				try
				{
					string value = GetValue("LogExceptions");
					if (value == null)
						return false;

					return GetValue("LogExceptions").ToUpper().Equals("TRUE"); 
				}
				catch
				{
					return false; 
				}	
			}
		}

		public bool LogInformation
		{
			get
			{
				try
				{
					string value = GetValue("LogInformation");
					if (value == null)
						return false;

					return GetValue("LogInformation").ToUpper().Equals("TRUE");
				}
				catch
				{
					return false;
				}
			}
		}


		public string LogFilePath
		{
			get { return GetValue("LogFilePath"); }
		}

        private void ReloadConfigFile()
        {
            try
            {
                _ht = new Hashtable(100, 0.5f);
                ReadConfigFile();

            }
            catch (Exception ex)
            {
                throw new NullReferenceException("Kan inte läsa configfil: \n\n" + ex);
            }
        }

		/// <summary>
		/// Läser config-filen och extraherar alla nyckar till en
		/// hashtable
		/// </summary>
		private void ReadConfigFile()
		{
			StreamReader reader = null;

			try
			{
				string path = GetExecutingAssemblyPath() + CONFIG_FILE;

				reader = new StreamReader(path);

				string xml = reader.ReadToEnd();

				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xml);

				//XmlNodeList list = doc.GetElementsByTagName("add");
                XmlNodeList list = doc.SelectNodes("configuration/appSettings/add");

			
				foreach ( XmlNode node in list )
				{
					XmlAttribute key = node.Attributes["key"];
					XmlAttribute value = node.Attributes["value"];
					AddValue(key.Value, value.Value);
				}
			}
			catch (IOException ioe)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if ( reader != null )
				{
					reader.Close();
				}
			}

		}


		/// <summary>
		/// Returnerar instansen av Configuration
		/// </summary>
		/// <returns></returns>
		public static Configuration GetConfiguration()
		{
			return _instance;
		}
        
        /// <summary>
        /// Sökväg till arbetskatalog, innehåller bla 
        /// printtemplate
        /// </summary>		
        public string WorkPath
        {
            get
            {
                return GetValue("WorkPath");
            }
        }

        /// <summary>
        /// Sökväg till projektfilen med kartinställningar
        /// </summary>		
        public string TatukProjectFilePath
        {
            get
            {
                return GetValue("TatukProjectFilePath");
            }
        }

        /// <summary>
        /// Kartutsnitt för uppstart
        /// </summary>		
        public string VisibleExtent
        {
            get
            {
                return GetValue("VisibleExtent");
            }
        }

        /// <summary>
        /// Kartutsnitt för "zooma till fullt"
        /// </summary>		
        public string MaxExtent
        {
            get
            {
                return GetValue("MaxExtent");
            }
        }

        /// <summary>
        /// Sökväg  till katalog med kartdata, 
        /// underlättar vid öppnande av nya lager i kartan
        /// </summary>		
        public string FolderPath
        {
            get
            {
                return GetValue("FolderPath");
            }
        }

        /// <summary>
        /// Sökväg  till mappen med Excel-filer
        /// </summary>		
        public string ExcelFilePath
        {
            get
            {
                return GetValue("ExcelFilePath");
            }
        }

        /// <summary>
        /// Namn på filen som innehåller beställningarna
        /// </summary>		
        public string NamnBestallningsLager
        {
            get
            {
                return GetValue("NamnBestallningsLager");
            }
        }

		/// <summary>
		/// Sökväg till projektfil innehållande info 
		/// om kartdata som ska visas
		/// </summary>		
		public string MapProjectPath
		{	
			get
			{	
				return GetValue("MapProjectPath");		
			}
		}

		/// <summary>
		/// Sökväg till blå symbol
		/// </summary>	
		public string SymbolBlue
		{
			get
			{
				return GetValue("SymbolBlue");
			}
		}

		/// <summary>
		/// symbolstorlek
		/// </summary>	
		public int SymbolSize
		{
			get
			{
				return Convert.ToInt32(GetValue("SymbolSize"));
			}
		}

		/// <summary>
		/// Sökväg till grön symbol
		/// </summary>	
		public string SymbolGreen
		{
			get
			{
				return GetValue("SymbolGreen");
			}
		}

		/// <summary>
		/// Sökväg till gul symbol
		/// </summary>	
		public string SymbolYellow
		{
			get
			{
				return GetValue("SymbolYellow");
			}
		}

		/// <summary>
		/// Sökväg till röd symbol
		/// </summary>	
		public string SymbolRed
		{
			get
			{
				return GetValue("SymbolRed");
			}
		}

		/// <summary>
		/// Sökväg till symbol för att markera GPS-punkter
		/// </summary>	
		public string SymbolGpsN
		{
			get
			{
				return GetValue("SymbolGpsN");
			}
		}
		/// <summary>
		/// Sökväg till symbol för att markera GPS-punkter
		/// </summary>	
		public string SymbolGpsNO
		{
			get
			{
				return GetValue("SymbolGpsNO");
			}
		}
		/// <summary>
		/// Sökväg till symbol för att markera GPS-punkter
		/// </summary>	
		public string SymbolGpsO
		{
			get
			{
				return GetValue("SymbolGpsO");
			}
		}
		/// <summary>
		/// Sökväg till symbol för att markera GPS-punkter
		/// </summary>	
		public string SymbolGpsSO
		{
			get
			{
				return GetValue("SymbolGpsSO");
			}
		}
		/// <summary>
		/// Sökväg till symbol för att markera GPS-punkter
		/// </summary>	
		public string SymbolGpsS
		{
			get
			{
				return GetValue("SymbolGpsS");
			}
		}
		/// <summary>
		/// Sökväg till symbol för att markera GPS-punkter
		/// </summary>	
		public string SymbolGpsSV
		{
			get
			{
				return GetValue("SymbolGpsSV");
			}
		}
		/// <summary>
		/// Sökväg till symbol för att markera GPS-punkter
		/// </summary>	
		public string SymbolGpsV
		{
			get
			{
				return GetValue("SymbolGpsV");
			}
		}
		/// <summary>
		/// Sökväg till symbol för att markera GPS-punkter
		/// </summary>	
		public string SymbolGpsNV
		{
			get
			{
				return GetValue("SymbolGpsNV");
			}
		}
	
		/// <summary>
		/// Sökväg till symbol för att markera GPS-punkter
		/// </summary>	
		public string SymbolGpsDefault
		{
			get
			{
				return GetValue("SymbolGpsDefault");
			}
		}
		
		/// <summary>
		/// symbolstorlek för Gps
		/// </summary>	
		public int SymbolSizeGps
		{
			get
			{
				return Convert.ToInt32(GetValue("SymbolSizeGps"));
			}
		}

		/// <summary>
		/// Namn på Access-tabellen som innehåller den data som ska användas
		/// </summary>	
		public string TableName
		{
			get
			{
				return GetValue("TableName");
			}
		}

		/// <summary>
		/// Namnet på kolumnen i Access-tabellen som ska användas som label
		/// </summary>	
		public string ColumnLabel
		{
			get
			{
				return GetValue("ColumnLabel");
			}
		}

		/// <summary>
		/// Namnet på kolumnen i Access-tabellen som ska användas till X
		/// </summary>	
		public string ColumnX
		{
			get
			{
				return GetValue("ColumnX");
			}
		}

		/// <summary>
		/// Namnet på kolumnen i Access-tabellen som ska användas till Y
		/// </summary>	
		public string ColumnY
		{
			get
			{
				return GetValue("ColumnY");
			}
		}

		/// <summary>
		/// Zoomnivån för detaljkartan
		/// </summary>	
		public double ZoomDetaljkarta
		{
			get
			{
				try
				{
					return Convert.ToDouble(GetValue("ZoomDetaljkarta"));
				}
				catch(Exception ex)
				{
					throw new FormatException(ex.Message);
				}
			}
		}

		/// <summary>
		/// Zoomnivån för detaljkartan
		/// </summary>	
		public double ZoomOversiktskarta
		{
			get
			{
				try
				{
					return Convert.ToDouble(GetValue("ZoomOversiktskarta"));
				}
				catch(Exception ex)
				{
					throw new FormatException(ex.Message);
				}
				
			}
		}

		/// <summary>
		/// Zoomnivån för sverigekarta
		/// </summary>	
		public double ZoomSverigekarta
		{
			get
			{
				try
				{
					return Convert.ToDouble(GetValue("ZoomSverigekarta"));
				}
				catch(Exception ex)
				{
					throw new FormatException(ex.Message);
				}
				
			}
		}

		/// <summary>
		/// Zoomnivån för sverigekarta - översikt
		/// </summary>	
		public double ZoomSverigekartaOversikt
		{
			get
			{
				try
				{
					return Convert.ToDouble(GetValue("ZoomSverigekartaOversikt"));
				}
				catch(Exception ex)
				{
					throw new FormatException(ex.Message);
				}
				
			}
		}

		/// <summary>
		/// Anger vilken kolumn som ska visas i listboxen
		/// </summary>	
		public string ListDisplay
		{
			get
			{
				return GetValue("ListDisplay");
			}
		}

		/// <summary>
		/// Anger vilket värde som ska motsvara det som visas i listboxen
		/// </summary>	
		public string ListValue
		{
			get
			{
				return GetValue("ListValue");
			}
		}

		/// <summary>
		/// Namnet på kolumnen som anger om det är avsändare eller mottagare
		/// </summary>	
		public string Type
		{
			get
			{
				return GetValue("Type");
			}
		}
		
		/// <summary>
		/// Texten som motsvarar Mottagare så som det är angett i kolumnen namngiven i parametern 'Type'
		/// </summary>	
		public string Mottagare
		{
			get
			{
				return GetValue("Mottagare");
			}
		}

		/// <summary>
		/// Texten som motsvarar Avsändare så som det är angett i kolumnen namngiven i parametern 'Type'
		/// </summary>	
		public string Sender
		{
			get
			{
				return GetValue("Sender");
			}
		}

		/// <summary>
		/// GPSens BaudRate
		/// </summary>	
		public int GPSBaudRate
		{
			get
			{
				return Convert.ToInt32(GetValue("GPSBaudRate"));
			}
		}

		/// <summary>
		/// GPSens Com-ports uttag
		/// </summary>	
		public int GPSPort
		{
			get
			{
				return Convert.ToInt32(GetValue("GPSPort"));
			}
		}
		/// <summary>
		/// Intervallet för hur ofta GPSen ska uppdateras
		/// </summary>	
		public int GPSIntervall
		{
			get
			{
				return Convert.ToInt32(GetValue("GPSIntervall"));
			}
		}

        /// <summary>
        /// Anger hur stor avvikelse ifrån en GPS-signal som krävs för att en pil skall visas. Om inget värde finns angivet
        /// kommer standardvärdet 5 meter att användas. 
        /// </summary>
        public double GPSDeviationInMetersMin
        {
            get
            {
                string GPSDeviationInMeters = GetValue("GPSDeviationInMetersMin");
                if (GPSDeviationInMeters == null)
                    return 5.0;

                return Convert.ToDouble(GetValue("GPSDeviationInMetersMin"));
            }
        }

        /// <summary>
        /// Anger hur stor avvikelse ifrån en GPS-signal som krävs för att en pil inte längre skall visas. Om inget värde finns angivet
        /// kommer standardvärdet 100 meter att användas. 
        /// </summary>
        public double GPSDeviationInMetersMax
        {
            get
            {
                string GPSDeviationInMeters = GetValue("GPSDeviationInMetersMax");
                if (GPSDeviationInMeters == null)
                    return 100.0;

                return Convert.ToDouble(GetValue("GPSDeviationInMetersMax"));
            }
        }

		/// <summary>
		/// Namnet på kolumnen som styr symbolernas färgutseende
		/// </summary>	
		public string Framkomlighet
		{
			get
			{
				return GetValue("Framkomlighet");
			}
		}

        /// <summary>
        /// Hämtar in-strängen för transformationen. 
        /// </summary>
        public string srInIni
        {
            get
            {
                return GetValue("srInIni");
            }
        }

        /// <summary>
        /// Hämtar ut-strängen för transformationen. 
        /// </summary>
        public string srOutIni
        {
            get
            {
                return GetValue("srOutIni");
            }
        }
        /// <summary>
        /// Connectionstring till lokal Access-databas.
        /// </summary>
        public string AccessDBConnectionString
        {
            get
            {
                return AccessDBProvider + AccessDBPath + AccessDBSecurity;
            }
        }

        public string AccessDBProvider
        {
            get
            {
                return GetValue("AccessDBProvider");
            }
        }

        public string AccessDBPath
        {
            get
            {
                return GetValue("AccessDBPath");
            }
            set
            {
                UpdateConfigFile("AccessDBPath", value);
            }
        }

        public string AccessDBSecurity
        {
            get
            {
                return GetValue("AccessDBSecurity");
            }
        }

        /// <summary>
        /// Hämtar synkroniseringstiden, om oläsligt returneras värdet 300 sekunder (5 minuter). 
        /// </summary>
        public int SynchronizationTime
        {
            get
            {
                string synchronizationString =  GetValue("SynchronizationTime");

                int synchronizationTime = 300;
                try
                {
                    synchronizationTime = int.Parse(synchronizationString);
                }
                catch (Exception)
                {
                }

                return synchronizationTime;
            }
        }

        public bool SilentErrorMessages
        {
            get
            {
                string silentErrorMessagesString = GetValue("SilentErrorMessages");

                bool silentErrorMessages = true;
                try
                {
                    bool.TryParse(silentErrorMessagesString, out silentErrorMessages);
                }
                catch (Exception)
                {
                }

                return silentErrorMessages;
            }
        }

        /// <summary>
        /// Hämtar eller sätter användarnamnet. 
        /// </summary>
        public string Username
        {
            get
            {
                return GetValue("username");
            }
            set
            {
                UpdateConfigFile("username", value);
            }
        }

        /// <summary>
        /// Hämtar eller sätter lösenordet. 
        /// </summary>
        public string Password
        {
            get
            {
                return GetValue("password");
            }
            set
            {
                UpdateConfigFile("password", value);
            }
        }

        /// <summary>
        /// Hämtar eller sätter om SG-GIS befinner sig i testläge
        /// </summary>
        public string TestMode
        {
            get
            {
                return GetValue("TestMode");
            }
            set
            {
                UpdateConfigFile("TestMode", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInTestMode
        {
            get
            {
                bool testMode = false;

                try
                {
                    testMode = Convert.ToBoolean(GetValue("TestMode"));
                }
                catch (System.FormatException)
                {
                }

                return testMode;
            }
        }


        /// <summary>
        /// Uppdaterar nyckel i konfigurationsfilens sektion "appSettings" med inskickat värde.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void UpdateConfigFile(string key, string value)
        {            
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            ReloadConfigFile();
        }
	}
}
