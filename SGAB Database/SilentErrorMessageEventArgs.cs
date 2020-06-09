using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGAB.SGAB_Database
{
    public class SilentErrorMessageEventArgs
    {
        public string ErrorMessage
        {
            get;
            protected set;
        }

        public Exception Exception
        {
            get;
            protected set;
        }

        public SilentErrorMessageEventArgs(string errorMessage, Exception exception)
        {
            this.ErrorMessage = errorMessage;
            this.Exception = exception;
        }
    }
}
