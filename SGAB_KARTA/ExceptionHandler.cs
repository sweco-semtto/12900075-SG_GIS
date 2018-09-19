using System;
using System.IO;

namespace SGAB.SGAB_Karta
{
	//skapad av sweco/josc 2004-07-xx
	/// <summary>
	/// Hantera alla Exceptions
	/// Har bara en privat konstruktor, gar alltså ej att skapa en instans av objektet
	/// </summary>
	public class ExceptionHandler
	{

		private ExceptionHandler()
		{

		}

		/// <summary>
		/// Huvudmetoden i denna klass. Hantera ett undantag. I nuläget skrivs det till en textfil, sak ändras sedan
		/// </summary>
		/// <param name="ex">Undantaget som ska hanteras.</param>
		public static void HandleException( Exception ex )
		{

            
            string path = System.Configuration.ConfigurationManager.AppSettings["LOFILE_PATH"];
        
			try
			{
				
				using ( StreamWriter writer = new StreamWriter( path, true) )
				{
					writer.WriteLine( "Exception thrown at {0}:", DateTime.Now.ToString() );
					writer.WriteLine( "---------------------------------------" );
					if ( ex.InnerException != null )
					{
						writer.WriteLine( ex.ToString() );
						writer.WriteLine( "InnerException:" );
						writer.WriteLine( ex.InnerException.ToString() );
					}
					else
					{
						writer.WriteLine( ex.ToString() );
					}
					writer.WriteLine();
				}
			}
			catch{}
		}
	}
}
