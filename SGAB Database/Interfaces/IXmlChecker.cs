using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SGAB.SGAB_Database
{
    /// <summary>
    /// Anger vilka metoder som behövs för att kunna kommunicera med Access. 
    /// </summary>
    public interface IXmlChecker
    {
        /// <summary>
        /// Kontrollerar om data som finns i xml-filen är tillräckligt färsk eller är från
        /// tidigare år. 
        /// </summary>
        /// <param name="dataFromXML">Data från xml-fil. </param>
        /// <returns>Returnerar sant om data är tillräckligt färsk. </returns>
        bool CheckIfXMLIsUpToDate(DataTable dataFromXML);
    }
}
