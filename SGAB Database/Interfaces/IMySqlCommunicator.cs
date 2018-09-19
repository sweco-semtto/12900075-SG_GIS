using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SGAB.SGAB_Database
{
    /// <summary>
    /// Anger vilka metoder som skall finnas för att hämta data ifrån alla MySql-tabeller. 
    /// </summary>
    public interface IMySqlCommunicator
    {
        /// <summary>
        /// Hämtar alla Entreprenörer ifrån MySQL-databasen. 
        /// </summary>
        /// <returns>Returnerar en DataTable med alla Entreprenörer i. </returns>
        DataTable GetAllFromMySql();

        /// <summary>
        /// Skapar endast kolumner i DataGridViewn, om några kolumner finns tas de bort och de nya fylls på.  
        /// </summary>
        /// <param name="data">Data ifrån MySQL. </param>
        /// <param name="dataGridView">DataGridViewn som skall modifieras. </param>
        void CreateDataGridView(DataTable data, DataGridView dataGridView);

        /// <summary>
        /// Ersätter befintliga rader i DataGridViewn med nytt data. 
        /// </summary>
        /// <param name="data">Data ifrån MySQL. </param>
        /// <param name="dataGridView">DataGridViewn som skall modifieras. </param>
        void ReFillDataGridView(DataTable data, DataGridView dataGridView);

        /// <summary>
        /// Lyssnare som skall anges för de kolumner vars värden skall sorteras som heltal. 
        /// </summary>
        void DataGridView_SortCompare_Integers(object sender, DataGridViewSortCompareEventArgs e);

        /// <summary>
        /// Synkroniserar så att MySql-databasen innehåller samma data som DataGridViewn. 
        /// </summary>
        /// <param name="dataFromMySql">Data som finns i MySql-databasen. </param>
        /// <param name="dataFromUserInterface">Data som finns i gränssnittet. </param>
        void SynchronizeWithPHP(DataTable dataFromMySql, DataTable dataToUpload);
    }
}
