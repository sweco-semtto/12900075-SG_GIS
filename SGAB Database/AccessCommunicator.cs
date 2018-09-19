using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SGAB.SGAB_Database
{
    public static class AccessCommunicator
    {
        /// <summary>
        /// Hämtar eller sätter anslutningssträngen till en accessdatabas. 
        /// </summary>
        public static string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Hämtar alla poster ifrån Acessdatabasen för en viss tabell. 
        /// </summary>
        /// <param name="tableName">Tabellens namn. </param>
        /// <returns></returns>
        public static DataTable GetAllFromAccess(string tableName)
        {
            return GetAllFromAccess(ConnectionString, tableName);
        }

        /// <summary>
        /// Hämtar alla poster ifrån Acessdatabasen för en viss tabell. 
        /// </summary>'
        /// <param name="connectionString">Om man inte vill använda den globala anslutningssträngen. </param>
        /// <param name="tableName">Tabellens namn. </param>
        /// <returns></returns>
        public static DataTable GetAllFromAccess(string connectionString, string tableName)
        {            
            var conn = new OleDbConnection(connectionString);
            DataTable dt = new DataTable();
            string query = "Select * from " + tableName;

            try
            {
                using (conn)
                {
                    conn.Open();

                    using (var da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = new OleDbCommand(query, conn);
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kan ej hämta data ifrån beställningsdatabasen. Följande fel fås:\n\n" + ex, 
                    "Ej kontakt med beställningsdatabasen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                conn.Close();
            }

            return null;
        }       
    }
}

