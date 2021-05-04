using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using SGAB.SGAB_Karta;

namespace SGAB
{
    public class DllLoader
    {
        /// <summary>
        /// Hämtar eller sätter var Admin.dll ligger. 
        /// </summary>
        public string PathToAdminDll
        {
            get;
            set;
        }

        public DllLoader()
        {
            this.PathToAdminDll = Application.StartupPath + "\\Admin.dll";
        }

        /// <summary>
        /// Läser in Admin.dll om det finns någon. 
        /// </summary>
        /// <returns></returns>
        public IAdmin LoadAdmin()
        {
            if (!File.Exists(this.PathToAdminDll))
                return null;

            try
            {
                // Laddar in dll:en ifrån mappen. 
                Assembly assembly = Assembly.LoadFrom(PathToAdminDll);
                // Tar fram typen för att kronstuktorer i dll:en. 
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    // Skapar en instans
                    object instance = assembly.CreateInstance(type.FullName);

                    // Lägger till instancen om den är en knapp. 
                    if (instance is IAdmin)
                        return ((IAdmin)instance);
                }
            }
            catch (Exception ex)
            {
                string logPath = SGAB_Karta.Configuration.GetConfiguration().LogFilePath;
                Log.LogErrorMessage("Admininläsning: \n" + ex.ToString(), logPath);
            }

            return null;
        }
    }
}
