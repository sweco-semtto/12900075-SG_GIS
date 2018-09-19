using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta
{
    public class StatusStartplatsChangedFromAdminEventArgs : EventArgs
    {
        /// <summary>
        /// Hämtar vilka startplatser som är markerade. 
        /// </summary>
        public IList<string> StartplatsIDs
        {
            get;
            protected set;
        }

        /// <summary>
        /// Anger vilken ny status startplatserna skall ha. 
        /// </summary>
        public int Status
        {
            get;
            protected set;
        }

        public StatusStartplatsChangedFromAdminEventArgs(IList<string> StartplatsIDs, int status)
        {
            this.StartplatsIDs = StartplatsIDs;
            this.Status = status;
        }
    }
}
