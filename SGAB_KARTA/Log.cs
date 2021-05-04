using System;
using System.IO;

namespace SGAB.SGAB_Karta
{
	/// <summary>
	/// Summary description for Log.
	/// </summary>
	public class Log
	{
		public Log()
		{		
		}

		/// <summary>
		/// Loggar ett felmeddelande 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="path"></param>
		public static void LogErrorMessage(string message, string path)
		{
			if (!SGAB_Karta.Configuration.GetConfiguration().LogExceptions)
				return;

			LogMessage("ERR: " + message, path);
		}

		public static void LogDebugMessage(string message, string path)
		{
			if (!SGAB_Karta.Configuration.GetConfiguration().LogDebug)
				return;

			LogMessage("DEBUG: " + message, path);
		}

		public static void LogClosing(string message, string path)
		{
			if (!SGAB_Karta.Configuration.GetConfiguration().LogExceptions && !SGAB_Karta.Configuration.GetConfiguration().LogDebug)
				return;

			LogMessage(message, path, false);
		}

		private static void LogMessage(string message, string path, bool writeTimeStamp = true)
		{
			StreamWriter writer = null;

			try
			{
				writer = new StreamWriter(path, true);

				if (!writeTimeStamp)
				{
					writer.WriteLine(message);
					return;
				}

				writer.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "\t" + message);
			}
			catch (Exception)
			{
			}
			finally
			{
                if (writer != null)
				    writer.Close();
			}
		}
	}
}
