using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Database
{
    /// <summary>
    /// En statisk klass som översätter mellan UTF-8 och ANSI med hjälp av xml-taggar för t.ex. å, ä och ö m.fl. Använder sig av %-tecknet istället för &-teckenet
    /// i MySql för &-teckenet är separarare mellan de olika argumenten i PHP-skripten. 
    /// </summary>
    public static class TranslatorMySqlAndAccess
    {
        public static bool _UTF_8 = false;
        public static bool _SpecialCharactes = false;

        /// <summary>
        /// Hämtar om MySql är inställd för att klara av UTF-8-tecken, t.ex. å, ä och ö. 
        /// </summary>
        public static bool UTF_8
        {
            get
            {
                return _UTF_8;
            }
        }

        /// <summary>
        /// Hämtar om MySql är inställd för att klara av specialtecken, t.ex. &. 
        /// </summary>
        public static bool SpecialCharactes
        {
            get
            {
                return _SpecialCharactes;
            }
        }

        /// <summary>
        /// Översätter ifrån MySql till Access. 
        /// </summary>
        /// <param name="MySqlText">Text i MySql. </param>
        /// <returns>Text i Access.</returns>
        public static string MySql_To_Access(string MySqlText)
        {
            if (!UTF_8)
            {
                // Ersätter alla &aring med å
                MySqlText = MySqlText.Replace("%aring", "å");

                // Ersätter alla &Aring med Å
                MySqlText = MySqlText.Replace("%Aring", "Å");

                // Ersätter alla &auml med ä
                MySqlText = MySqlText.Replace("%auml", "ä");

                // Ersätter alla &Auml med Ä
                MySqlText = MySqlText.Replace("%Auml", "Ä");

                // Ersätter alla &ouml med ö
                MySqlText = MySqlText.Replace("%ouml", "ö");

                // Ersätter alla &Ouml med Ö
                MySqlText = MySqlText.Replace("%Ouml", "Ö");

                //Ersätter övriga specialtecken. 
                MySqlText = MySqlText.Replace("%uuml", "ü");
                MySqlText = MySqlText.Replace("%Uuml", "Ü");
                MySqlText = MySqlText.Replace("%ucirc", "û");
                MySqlText = MySqlText.Replace("%Ucirc", "Û");
                MySqlText = MySqlText.Replace("%egrave", "é");
                MySqlText = MySqlText.Replace("%Egrave", "É");
                MySqlText = MySqlText.Replace("%eacute", "è");
                MySqlText = MySqlText.Replace("%Eacute", "È");
            }

            if (!SpecialCharactes)
            {
                MySqlText = MySqlText.Replace("%amp", "&");
                MySqlText = MySqlText.Replace("%lt", "<");
                MySqlText = MySqlText.Replace("%gt", ">");
                MySqlText = MySqlText.Replace("%quot", "\"");
                MySqlText = MySqlText.Replace("%#39", "'");
                MySqlText = MySqlText.Replace("%plus", "+");
            }

            return MySqlText;
        }

        /// <summary>
        /// ÖVersätter ifrån Access till MySql. 
        /// </summary>
        /// <param name="AccessText">Text i Access. </param>
        /// <returns>Text i MySql.</returns>
        public static string Access_To_MySql(string AccessText)
        {
            if (!UTF_8)
            {
                // Ersätter alla %aring med å
                AccessText = AccessText.Replace("å", "%aring");

                // Ersätter alla %Aring med Å
                AccessText = AccessText.Replace("Å", "%Aring");

                // Ersätter alla %auml med ä
                AccessText = AccessText.Replace("ä", "%auml");

                // Ersätter alla %Auml med Ä
                AccessText = AccessText.Replace("Ä", "%Auml");

                // Ersätter alla %ouml med ö
                AccessText = AccessText.Replace("ö", "%ouml");

                // Ersätter alla %Ouml med Ö
                AccessText = AccessText.Replace("Ö", "%Ouml");

                //Ersätter övriga specialtecken. 
                AccessText = AccessText.Replace("ü", "%uuml");
                AccessText = AccessText.Replace("Ü", "%Uuml");
                AccessText = AccessText.Replace("û", "%ucirc");
                AccessText = AccessText.Replace("Û", "%Ucirc");
                AccessText = AccessText.Replace("é", "%egrave");
                AccessText = AccessText.Replace("É", "%Egrave");
                AccessText = AccessText.Replace("è", "%eacute");
                AccessText = AccessText.Replace("È", "%Eacute");
            }

            if (!SpecialCharactes)
            {
                AccessText = AccessText.Replace("&", "%amp");
                AccessText = AccessText.Replace("<", "%lt");
                AccessText = AccessText.Replace(">", "%gt");
                AccessText = AccessText.Replace("\"", "%quot");
                AccessText = AccessText.Replace("'", "%#39");
                AccessText = AccessText.Replace("'", "%#39");
                AccessText = AccessText.Replace("+", "%plus");
            }

            return AccessText;
        }
    }
}
