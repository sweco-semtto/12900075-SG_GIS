using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta.HelpClasses
{
    public interface IParsePattern
    {
        string ParsePattern
        {
            get;
        }

        IParsePattern ParseLine(string lineToParse);
    }
}
