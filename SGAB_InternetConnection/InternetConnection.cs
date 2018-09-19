using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB_InternetConnection
{
    public class InternetConnection
    {
        public static bool HasInternetConnection
        {
            get;
            protected set;
        }

        /// <summary>
        /// Kontrollerar om vi har anslutning till internet. 
        /// </summary>
        /// <returns></returns>
        public static void CheckForInternetConnection()
        {
            //HasInternetConnection = false;
            //return;

            try
            {
                System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.google.com");
                HasInternetConnection = true;
            }
            catch
            {
                HasInternetConnection = false; // host not reachable.
            }
        }
    }
}
