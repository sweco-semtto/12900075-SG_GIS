using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.IO;
using System.Text;

namespace SGAB
{
    public class LogIn
    {
        /// <summary>
        /// Anger att inloggningen misslyckades
        /// </summary>
        public const int NOT_LOGGED_IN = -1;

        protected static string LogInUrl = "http://www.sg-systemet.com/bestallning/PHP/checkLogin.php";

        public static int TryToLoginAsEntrepreneur(string username, string password)
        {
            string postData = "username=" + username + "&password=" + password;
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] POST = encoding.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(LogInUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = POST.Length;

            // Data till PHP som en ström
            Stream StreamPOST = request.GetRequestStream();
            StreamPOST.Write(POST, 0, POST.Length);
            StreamPOST.Close();

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream Answer = response.GetResponseStream();
            //StreamReader _Answer = new StreamReader(Answer);
            //string vystup = _Answer.ReadToEnd();

            // Får tillbaka ett svar 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader streamResponse = new StreamReader(response.GetResponseStream());
            DataSet dsTest = new DataSet();
            dsTest.ReadXml(streamResponse);
            DataTable dt = dsTest.Tables["Data"];

            if (dt == null)
                return NOT_LOGGED_IN;

            return int.Parse(dt.Rows[0]["ID"].ToString());
        }

        /// <summary>
        /// Logga in om inte administratörs dll:en hittas. 
        /// </summary>
        public static bool TryToLoginAsAdmin()
        {
            DllLoader dllLoader = new DllLoader();
            IAdmin admin = dllLoader.LoadAdmin();
            bool ans = admin == null ? false : true;

            return ans;
        }
    }
}
