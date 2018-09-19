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

		public static void LogMessage(string message, string path)
		{
			StreamWriter writer = null;

			try
			{
				writer = new StreamWriter(path, true);
				writer.WriteLine(DateTime.Now.ToLongDateString() + "\t" + DateTime.Now.ToLongTimeString() + "\t" + message);
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
