using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SGAB.SGAB_Karta
{
    public class SynchronizationFinishedEventArgs
    {
        public DataTable Foretag
        {
            get;
            protected set;
        }

        public DataTable Startplatser
        {
            get;
            protected set;
        }

        public SynchronizationFinishedEventArgs(DataTable foretagMySqlTable, DataTable startplatsMySqlTable)
        {
            this.Foretag = foretagMySqlTable;
            this.Startplatser = startplatsMySqlTable;
        }
    }
}
