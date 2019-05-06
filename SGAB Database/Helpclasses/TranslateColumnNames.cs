using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Database
{
    /// <summary>
    /// En hjälpklass för att kunna översätta tabellnamn ifrån MySql till Access. Finns inte å, ä och ö i MySql. 
    /// </summary>
    public class TranslateColumnNames
    {
        protected List<TranslateElement> Columns;

        public TranslateColumnNames()
        {
            Columns = new List<TranslateElement>();
        }

        /// <summary>
        /// Lägger till en översättning mellan MySql och Access. 
        /// </summary>
        /// <param name="ColumnNameInMySql"></param>
        /// <param name="ColumnNameInAccess"></param>
        public void AddTranslation(string ColumnNameInMySql, string ColumnNameInAccess)
        {
            Columns.Add(new TranslateElement(ColumnNameInMySql, ColumnNameInAccess));
        }

        /// <summary>
        /// Gör en översättning av ett kolumnamn ifrån MySql till Access. Om ingen översättning finns retuernas samma värde tillbaka. 
        /// </summary>
        /// <param name="columnNameInMySql">Kolumnnamnet som efterfrågas en översättning på. </param>
        /// <returns>Returnerar svaret om inget svar finns retuneras samma sträng som skickades in. </returns>
        public string TranslateAToB(string columnNameInMySql)
        {
            foreach (TranslateElement element in Columns)
                if (element.ColumnNameInMySql.Equals(columnNameInMySql))
                    return element.ColumnNameInAccess;

            return columnNameInMySql;
        }

        /// <summary>
        /// Gör en översättning av ett kolumnamn ifrån MySql till Access. Om ingen översättning finns retuernas samma värde tillbaka. 
        /// </summary>
        /// <param name="columnNameInMySql">Kolumnnamnet som efterfrågas en översättning på. </param>
        /// <returns>Returnerar svaret om inget svar finns retuneras samma sträng som skickades in. </returns>
        public string TranslateBToA(string columnNameInAccess)
        {
            foreach (TranslateElement element in Columns)
                if (element.ColumnNameInAccess.Equals(columnNameInAccess))
                    return element.ColumnNameInMySql;

            return columnNameInAccess;
        }

        /// <summary>
        /// Innehåller en enda översättning ifrån MySql till Access. 
        /// </summary>
        public class TranslateElement
        {
            /// <summary>
            /// kolumnananet i MySql. 
            /// </summary>
            public string ColumnNameInMySql
            {
                get;
                set;
            }

            /// <summary>
            /// Kolumnanaet i Access. 
            /// </summary>
            public string ColumnNameInAccess
            {
                get;
                set;
            }

            public TranslateElement(string ColumnNameInMySql, string ColumnNameInAccess)
            {
                this.ColumnNameInMySql = ColumnNameInMySql;
                this.ColumnNameInAccess = ColumnNameInAccess;
            }

            public override string ToString()
            {
                return ColumnNameInMySql + "   |   " + ColumnNameInAccess;
            }
        }
    }
}
