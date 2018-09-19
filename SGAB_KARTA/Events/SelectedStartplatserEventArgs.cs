using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta
{
    /// <summary>
    /// Ett event som talar om när startplatser blir selecterade i kartan. 
    /// </summary>
    public class SelectedStartplatserEventArgs : EventArgs
    {
        /// <summary>
        /// Hämtar vilka startplatser som är markerade. 
        /// </summary>
        public IList<string> StartplatsIDs
        {
            get;
            protected set;
        }

        public SelectedStartplatserEventArgs(IList<string> StartplatsIDs)
        {
            this.StartplatsIDs = StartplatsIDs;
        }
    }
}
