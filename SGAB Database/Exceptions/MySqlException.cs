using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Database
{
    public class MySqlException : Exception
    {

        public List<MySqlExceptionAtom> Exceptions
        {
            get;
            protected set;
        }

        public MySqlException() 
        {
            Exceptions = new List<MySqlExceptionAtom>();
        }

        public void AddError(string ScriptName, List<KeyValuePair<string, string>> Values)
        {
            Exceptions.Add(new MySqlExceptionAtom(ScriptName, Values));
        }

        public string GetErrors()
        {
            string errorMessage = String.Empty;

            foreach (MySqlException.MySqlExceptionAtom mySqlExAtom in Exceptions)
            {
                errorMessage = "Skript: " + mySqlExAtom.ScriptName + "\n";
                foreach (KeyValuePair<string, string> argument in mySqlExAtom.Values)
                {
                    errorMessage = errorMessage + "Kolumn: " + argument.Key + " Värde: " + argument.Value + "\n";
                }                
            }
            return errorMessage;
        }

        public class MySqlExceptionAtom
        {
            public MySqlExceptionAtom(string ScriptName, List<KeyValuePair<string, string>> Values) 
            {
                this.ScriptName = ScriptName;
                this.Values = Values;
            }
            
            public string ScriptName
            {
                get;
                protected set;
            }

            public List<KeyValuePair<string, string>> Values
            {
                get;
                protected set;
            }
        }
    }
}
