using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SGAB.SGAB_Database
{
    /// <summary>
    /// Anger vilka metoder som behövs för att kunna kommunicera med Access. 
    /// </summary>
    public interface IAccessCommunicator
    {
        /// <summary>
        /// Hämtar all data ifrån Acess. 
        /// </summary>
        /// <returns></returns>
        DataTable GetAllFromAccess();
    }
}
