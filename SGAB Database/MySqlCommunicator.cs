using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SGAB.SGAB_Database
{
    public delegate void SilentErrorMessageEventHandler(object sender, SilentErrorMessageEventArgs e);

    public abstract class MySqlCommunicator : IMySqlCommunicator
    {
        public event SilentErrorMessageEventHandler SilentErrorMessage;

        /// <summary>
        /// Skriptet som tar bort rader ifrån MySql. 
        /// </summary>
        protected string Url_To_Script_Delete
        {
            get;
            set;
        }

        /// <summary>
        /// Skritpet som lägger till rader till MySql. 
        /// </summary>
        protected string Url_To_Script_Insert
        {
            get;
            set;
        }

        /// <summary>
        /// Skripter som hämtar rader ifrån MySql. 
        /// </summary>
        protected string Url_To_Script_Select
        {
            get;
            set;
        }

        /// <summary>
        /// Skriptet som uppdaterar rader i MySql. 
        /// </summary>
        protected string Url_To_Script_Update
        {
            get;
            set;
        }

        /// <summary>
        /// Det namn som skall visas för användaren om ett skript inte hittas, t.ex. Entreprenorer. 
        /// </summary>
        public string MessageErrorScriptName
        {
            get;
            set;
        }

        /// <summary>
        /// Det namn som select-skriptet i PHP returnerar XML-taggen med, t.ex. Entreprenorer. 
        /// </summary>
        protected string XMLNameForDataTable
        {
            get;
            set;
        }

        /// <summary>
        /// De kolumnnamn som finns i PHP-skripten. 
        /// </summary>
        protected IList<string> ColumnNamesFromPHP
        {
            get;
            set;
        }

        /// <summary>
        /// De kolumnamn som inte skall visas för användare ifrån PHP-skripten. 
        /// </summary>
        protected IList<string> ColumnNamesFromPHPToHide
        {
            get;
            set;
        }

        /// <summary>
        /// Id-kolumnens namn i MySql. 
        /// </summary>
        protected string IdColumnNameInMySql
        {
            get;
            set;
        }

        /// <summary>
        /// Id-kolumnens namn i MySql. 
        /// </summary>
        protected string OrderIdColumnNameInMySql
        {
            get;
            set;
        }

        /// <summary>
        /// Lista med nycklar. Alla värden i denna lista är av typen int och sorteras efter det i DataGridViewn. Använd denna 
        /// variable endast om du har flera nycklar. 
        /// </summary>
        protected IList<string> IdColumns
        {
            get;
            set;
        }

        /// <summary>
        /// Anger vilka kolumner som skall sortera sina värden som om de vore heltal. 
        /// </summary>
        public IList<string> SortColumnValuesAsIntegers
        {
            get;
            protected set;
        }

        /// <summary>
        /// Anger en översättning av kolumnnamn. 
        /// </summary>
        protected TranslateColumnNames TranslationColumnNamesFromMySql
        {
            get;
            set;
        }

        /// <summary>
        /// Hämtar rätt decimalseparerare för MySql, i detta fallet är det en punkt(.) som gäller.
        /// </summary>
        protected IFormatProvider DecimalSepatorForMySql
        {
            get
            {
                CultureInfo cultureInfo = CultureInfo.InstalledUICulture;
                NumberFormatInfo ni = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                ni.NumberDecimalSeparator = ".";

                return ni;
            }
        }

        /// <summary>
        /// Hämtar alla rader från angiven tabell från MySQL-databasen. Returnar null om t.ex. ett proxyfel inträffar.
        /// </summary>
        /// <returns>Returnerar en DataTable med alla rader i. </returns>
        public virtual DataTable GetAllFromMySql()
        {
            DataSet ans = new DataSet();

            try
            {
                // Skickar en request. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url_To_Script_Select);

                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream Answer = response.GetResponseStream();
                //StreamReader _Answer = new StreamReader(Answer);
                //string vystup = _Answer.ReadToEnd();

                // För att skicka med parametrar till php gör något liknade enligt följande
                //string argumentToPHP = "\"date_limit\" : \"2011-9-1\"";
                //string argumentToPHP = "\"2011-9-1\"";

                //byte[] byteArrayOfArguments = Encoding.UTF8.GetBytes(argumentToPHP);
                //request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                //Stream postStream;
                //using (postStream = request.GetRequestStream())
                //{
                //    postStream.Write(byteArrayOfArguments, 0, byteArrayOfArguments.Length);
                //}

                //postStream.Write(byteArrayOfArguments, 0, byteArrayOfArguments.Length);
                //postStream.Close();

                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream Answer = response.GetResponseStream();
                //StreamReader _Answer = new StreamReader(Answer);
                //string vystup = _Answer.ReadToEnd();

                // Tar emot ett response. 
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader responseStream = new StreamReader(response.GetResponseStream());
                ans.ReadXml(responseStream);
            }
            catch (System.Net.WebException wex)
            {
                if (SilentErrorMessage != null)
                {
                    SilentErrorMessage(this, new SilentErrorMessageEventArgs(
                        "Hittar ej PHP-skriptet " + MessageErrorScriptName + " på serven", 
                        wex));
                    return null;
                }
                    
                MessageBox.Show("Hittar ej PHP-skriptet " + MessageErrorScriptName + " på serven. Vänligen kontrollera internetanslutning och försök igen, annars kontakta Skogens Gödsling och uppge följande fel: \n" + wex.ToString(),
                    "PHP-skript saknas. ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Om fel inträffar se till att inte nedan körs, för det finns en datatabell. 
                return null;
            }
            catch (System.Xml.XmlException xmlex)
            {
                if (SilentErrorMessage != null)
                {
                    SilentErrorMessage(this, new SilentErrorMessageEventArgs(
                        "XML-fel inträffar när PHP-skriptet " + MessageErrorScriptName + " körs",
                        xmlex));
                    return null;
                }

                MessageBox.Show("Problem med att läsa ifrån MySql. \n\n" + xmlex.ToString());
                MessageBox.Show("Problem med att läsa ifrån MySql. \n\n" + xmlex.Message);
                MessageBox.Show("Problem med att läsa ifrån MySql. \n\n" + xmlex.InnerException.ToString());
                MessageBox.Show("Problem med att läsa ifrån MySql. \n\n" + xmlex.InnerException.Message);
            }
            catch (Exception ex)
            {
                if (SilentErrorMessage != null)
                {
                    SilentErrorMessage(this, new SilentErrorMessageEventArgs(
                        "Övrigt fel inträffade för när PHP-skriptet " + MessageErrorScriptName + " körs",
                        ex));
                    return null;
                }

                MessageBox.Show("Problem med att läsa ifrån MySql:\n\n" + ex.ToString());
            }

            // Lägger in alla kolumnnamn
            //ColumnNamesFromPHP.Clear();
            //foreach (DataColumn column in ans.Tables[XMLNameForDataTable].Columns)
            //    ColumnNamesFromPHP.Add(column.ColumnName);

            return ans.Tables[XMLNameForDataTable];
        }
        
        /// <summary>
        /// Selekterar från datatable. 
        /// </summary>
        /// <returns>Returnerar en DataTable med alla rader i som uppfyller villkor. </returns>
        public virtual DataTable SelectFromDataTable(DataTable dt, string whereClause)
        {        
            DataRow[] foundRows = dt.Select(whereClause);
            DataTable dtSelected = dt.Clone();
            
            for (int i = 0; i < foundRows.Length; i++)
            {
                DataRow newRow = dtSelected.NewRow();
                newRow.ItemArray = foundRows[i].ItemArray;
                dtSelected.Rows.Add(newRow);
            }

            return dtSelected;    
        }

        /// <summary>
        /// Skapar endast kolumner i DataGridViewn, om några kolumner finns tas de bort och de nya fylls på.  
        /// </summary>
        /// <param name="data">Data ifrån MySQL. </param>
        /// <param name="dataGridView">DataGridViewn som skall modifieras. </param>
        public virtual void CreateDataGridView(DataTable data, DataGridView dataGridView)
        {
            dataGridView.Columns.Clear();

            // Om vi inte har någon data för i år än. 
            if (data == null)
                return;

            // Döljer ID-värdet för användaren, detta automatgenereras av databasen och skall inte ändras. 
            foreach (DataColumn column in data.Columns)
            {
                if (ColumnNamesFromPHPToHide == null)
                    break;

                if (ColumnNamesFromPHPToHide.Contains(column.ColumnName))
                {
                    dataGridView.Columns.Add(column.ColumnName, column.ColumnName);
                    dataGridView.Columns[column.ColumnName].Visible = false;
                }
                else
                    dataGridView.Columns.Add(column.ColumnName, column.ColumnName);
            }
        }

        /// <summary>
        /// Ersätter befintliga rader i DataGridViewn med nytt data. 
        /// </summary>
        /// <param name="data">Data ifrån MySQL. </param>
        /// <param name="dataGridView">DataGridViewn som skall modifieras. </param>
        public virtual void ReFillDataGridView(DataTable data, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            foreach (DataRow row in data.Rows)
            {
                int newRowIndex = dataGridView.Rows.Add();
                for (int itemNo = 0; itemNo < row.ItemArray.Length; itemNo++)
                    dataGridView.Rows[newRowIndex].Cells[itemNo].Value = TranslatorMySqlAndAccess.MySql_To_Access(row.ItemArray[itemNo].ToString());
            }
        }

        /// <summary>
        /// Lyssnare som skall anges för de kolumner vars värden skall sorteras som heltal. 
        /// </summary>
        public virtual void DataGridView_SortCompare_Integers(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (SortColumnValuesAsIntegers == null)
            {
                e.Handled = false;
                return;
            }

            foreach (string columnName in SortColumnValuesAsIntegers)
            {
                // Sorterar om kolumnen efter siffror. 
                if (e.Column.Name.Equals(columnName))
                {
                    if (int.Parse(e.CellValue1.ToString()) > int.Parse(e.CellValue2.ToString()))
                        e.SortResult = 1;
                    else if (int.Parse(e.CellValue1.ToString()) < int.Parse(e.CellValue2.ToString()))
                        e.SortResult = -1;
                    else
                        e.SortResult = 0;
                }
            }

            // Anger att standradsorteringen inte behöver användas. 
            e.Handled = true;
        }

        /// <summary>
        /// Synkroniserar så att MySql-databasen innehåller samma data som inskickad datatabell.
        /// </summary>
        /// <param name="dataFromMySql"></param>
        /// <param name="tableToUpload"></param>
        public virtual void SynchronizeWithPHP(DataTable dataFromMySql, DataTable tableToUpload)
        {
            try
            {
                InsertRows(dataFromMySql, tableToUpload);
            }
            catch (MySqlException mySqlEx)
            {
                string errorMessage = mySqlEx.GetErrors();
                MessageBox.Show("Fel vid synkronisering. " + errorMessage, "Fel vid synkronisering", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                UpdateRows(dataFromMySql, tableToUpload);
            }
            catch (MySqlException mySqlEx)
            {
                string errorMessage = mySqlEx.GetErrors();
                MessageBox.Show("Fel vid synkronisering. " + errorMessage, "Fel vid synkronisering", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                DeleteRows(dataFromMySql, tableToUpload);
            }
            catch (MySqlException mySqlEx)
            {
                string errorMessage = mySqlEx.GetErrors();
                MessageBox.Show("Fel vid synkronisering. " + errorMessage, "Fel vid synkronisering", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

        /// <summary>
        /// Bygger ihop data som skall postas till PHP. 
        /// </summary>
        /// <param name="valueToAffectMySql">Ettv KeyValuePair. </param>
        /// <returns></returns>
        protected virtual string CreatePostDataForPHP(KeyValuePair<string, string> valueToAffectMySql)
        {
            StringBuilder ans = new StringBuilder();

            ans.Append(valueToAffectMySql.Key);
            ans.Append("=");
            ans.Append(valueToAffectMySql.Value);

            return ans.ToString();
        }

        /// <summary>
        /// Bygger ihop data som skall postas till PHP. 
        /// </summary>
        /// <param name="rowToAffectMySql">En lista av KeyValuePair. </param>
        /// <returns></returns>
        protected virtual string CreatePostDataForPHP(List<KeyValuePair<string, string>> rowToAffectMySql)
        {
            StringBuilder ans = new StringBuilder();

            bool secondOrLaterArgument = false;
            foreach (KeyValuePair<string, string> argument in rowToAffectMySql)
            {
                if (secondOrLaterArgument)
                    ans.Append("&");

                ans.Append(argument.Key);
                ans.Append("=");
                ans.Append(argument.Value);
                secondOrLaterArgument = true;
            }

            return ans.ToString();
        }

        /// <summary>
        /// Lägger till rader som fattas till MySql. 
        /// </summary>
        /// <param name="dataFromMySql">Data som finns i MySql-databasen. </param>
        /// <param name="tableToUpload">Data som ska laddas upp, t ex från gränssnittet eller från Access-databasen. </param>
        protected virtual void InsertRows(DataTable dataFromMySql, DataTable dataToUpload)
        {
            // Tar fram värden som skall läggas till i MySql
            List<List<KeyValuePair<string, string>>> valuesToInsert = new List<List<KeyValuePair<string, string>>>();

            List<string> IDsInMySql = new List<string>();
            foreach (DataRow MySqlRow in dataFromMySql.Rows)
                IDsInMySql.Add(MySqlRow[IdColumnNameInMySql].ToString());

            // Jämför ID-värdet för de rader som finns i tabellen som ska laddas upp, med de rader som redan finns i MySql.
            foreach (DataRow row in dataToUpload.Rows)
            {
                // Om vi inte hittar ID-nummret skall raden läggas till
                if (!IDsInMySql.Contains(row[IdColumnNameInMySql].ToString()))
                {
                    List<KeyValuePair<string, string>> rowToInsert = new List<KeyValuePair<string, string>>();

                    // Lägger inte till värdet för ID eftesom det ändå skall automatgenereras i MySql-databasen. 
                    foreach (string columnName in ColumnNamesFromPHP)
                        if (!ColumnNamesFromPHPToHide.Contains(columnName) && row[columnName] != null)
                            rowToInsert.Add(new KeyValuePair<string, string>(columnName, row[columnName].ToString()));

                    // Lägger till raden om den inte är tom, t.ex. som sista raden i gränssnittet är.
                    int blankCounter = 0;
                    foreach (KeyValuePair<string, string> keyValue in rowToInsert)
                        if (keyValue.Value == "" || keyValue.Value == null)
                            blankCounter++;

                    if (blankCounter < rowToInsert.Count)
                        valuesToInsert.Add(rowToInsert);
                }
            }

            SendRequest(valuesToInsert, Url_To_Script_Insert);
        }

        /// <summary>
        /// Uppdaterar rader som ändras i MySql. 
        /// </summary>
        /// <param name="dataFromMySql">Data som finns i MySql-databasen. </param>
        /// <param name="tableToUpload">Data som ska laddas upp, t ex från gränssnittet eller från Access-databasen.</param>
        protected virtual void UpdateRows(DataTable dataFromMySql, DataTable dataToUpload)
        {
            // Tar fram värden som skall uppdateras i MySql. 
            List<List<KeyValuePair<string, string>>> valuesToUpdateStringsInMySql = new List<List<KeyValuePair<string, string>>>();

            // Jämför ID-värdet för de rader som finns i gränssnittet med de rader som finns i MySql. 
            foreach (DataRow MySqlRow in dataFromMySql.Rows)
            {
                foreach (DataRow rowToUpload in dataToUpload.Rows)
                {
                    // När vi har sista raden i gränssnittet som alltid är tom hoppar vi över den. 
                    if (rowToUpload[IdColumnNameInMySql] == null)
                        break;

                    // Om vi hittar ID-nummret undersöks om raden skall uppdaterats. 
                    if (MySqlRow[IdColumnNameInMySql].ToString().Equals(rowToUpload[IdColumnNameInMySql].ToString()))
                    {
                        // Jämför alla kolumner för att hitta skillnader. 
                        foreach (string columnName in ColumnNamesFromPHP)
                        {
                            // Om värdena för en kolumn inte är samma skall den uppdateras. 
                            if (!MySqlRow[columnName].ToString().Equals(rowToUpload[columnName]))
                            {
                                List<KeyValuePair<string, string>> rowToUpdateInMySql = new List<KeyValuePair<string, string>>();

                                // Lägger till id
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>(IdColumnNameInMySql, MySqlRow[IdColumnNameInMySql].ToString()));

                                // Lägger till kolumnamn
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>("ColumnName", columnName));

                                // Lägger till värdet
                                rowToUpdateInMySql.Add(
                                    new KeyValuePair<string, string>("Value", rowToUpload[columnName].ToString()));

                                valuesToUpdateStringsInMySql.Add(rowToUpdateInMySql);
                            }
                        }
                    }
                }
            }

            SendRequest(valuesToUpdateStringsInMySql, Url_To_Script_Update);
        }

        /// <summary>
        /// Tar bort rader i MySql. 
        /// </summary>
        /// <param name="dataFromMySql">Data som finns i MySql-databasen. </param>
        /// <param name="dataToUpload">Data som ska laddas upp, t ex från gränssnittet eller från Access-databasen.</param>
        protected virtual void DeleteRows(DataTable dataFromMySql, DataTable dataToUpload)
        {
            // Tar fram Id som skall tas bort           
            List<List<KeyValuePair<string, string>>> valuesToDelete = new List<List<KeyValuePair<string, string>>>();

            // Hämtar alla Id-nummer ifrån databasen
            List<string> MySqlIds = new List<string>();
            foreach (DataRow MySqlRow in dataFromMySql.Rows)
                MySqlIds.Add(MySqlRow[IdColumnNameInMySql].ToString());

            // Hämtar alla Id-nummer ifrån gränssnittet, inte sista raden som är tom dock. 
            List<string> uploadIds = new List<string>();
            foreach (DataRow rowToUpload in dataToUpload.Rows)
                if (rowToUpload[IdColumnNameInMySql] != null)
                    uploadIds.Add(rowToUpload[IdColumnNameInMySql].ToString());

            // Tar bort alla rader som inte finns med i gränssnittet men som finns med i databasen. 
            foreach (string MySqlId in MySqlIds)
            {
                if (!uploadIds.Contains(MySqlId))
                {
                    List<KeyValuePair<string, string>> rowToDelete = new List<KeyValuePair<string, string>>();
                    rowToDelete.Add(new KeyValuePair<string, string>(IdColumnNameInMySql, MySqlId));
                    valuesToDelete.Add(rowToDelete);
                }
            }

            SendRequest(valuesToDelete, Url_To_Script_Delete);
        }

        /// <summary>
        /// Skriver de inskickade raderna till MySql.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="requestUrlString"></param>
        protected virtual void SendRequest(List<List<KeyValuePair<string, string>>> values, string requestUrlString)
        {
            int errors = 0;
            string result = String.Empty;
            MySqlException ex = new MySqlException();

            foreach (List<KeyValuePair<string, string>> row in values)
            {
                string postData = CreatePostDataForPHP(row);
                PerformRequest(requestUrlString, ref errors, ref result, ex, row, postData);
            }

            if (errors > 0)
                throw ex;
        }

        /// <summary>
        /// Göra själva requesten
        /// </summary>
        /// <param name="requestUrlString"></param>
        /// <param name="errors"></param>
        /// <param name="result"></param>
        /// <param name="ex"></param>
        /// <param name="row"></param>
        /// <param name="postData"></param>
        /// <param name="encoding"></param>
        protected virtual void PerformRequest(string requestUrlString, ref int errors, ref string result, MySqlException ex, List<KeyValuePair<string, string>> row, string postData)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] POST = encoding.GetBytes(postData);
            //UPDATE SG_Startplats Set Fraktentreprenors_ID = 1 WHERE ID = 101  
            // POST:ar till PHP
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrlString);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = POST.Length;

            // Data till PHP som en ström
            Stream StreamPOST = request.GetRequestStream();
            StreamPOST.Write(POST, 0, POST.Length);
            StreamPOST.Close();

			//if (postData == "OrderID=334")
			//{
			//	HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			//	Stream Answer = response.GetResponseStream();
			//	StreamReader _Answer = new StreamReader(Answer);
			//	string vystup = _Answer.ReadToEnd();
			//	MessageBox.Show(vystup);
			//}

			// Får tillbaka ett svar 
			try
			{
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader streamResponse = new StreamReader(response.GetResponseStream());
				DataSet dsTest = new DataSet();
				dsTest.ReadXml(streamResponse);
				DataTable dt = dsTest.Tables["MessageXML"];

				if (dt.Rows.Count > 0)
				{
					if (dt.Columns.Contains("Data")) //INSERT/UPDATE/DELETE
					{
						if (dt.Rows[0]["Data"].ToString().Contains("Failure"))
						{
							errors++;
							result = "Fel vid uppdatering av databasen: " + dt.Rows[0]["Data"].ToString();
							ex.AddError(requestUrlString, row);
						}
						else
							result = "Uppdatering av databasen genomförd: " + dt.Rows[0]["Data"].ToString();
					}
				}
			}
			catch (WebException webex)
			{
				WebResponse errResp = webex.Response;
				using (Stream respStream = errResp.GetResponseStream())
				{
					StreamReader reader = new StreamReader(respStream);
					string text = reader.ReadToEnd();
					MessageBox.Show("Problem med att synka databasen:\n" + text, "Synkningsproblem", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
    }
}
