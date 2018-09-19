using System;
using System.Collections.Generic;
using System.Text;
using TatukGIS.NDK;
using System.Configuration;
using System.Windows.Forms;

namespace SGAB.SGAB_Karta
{
    public class IconManager
    {
        private TGIS_SymbolList _IconList;
        private Dictionary<string, TGIS_SymbolAbstract> _IconDictionary;

        protected Dictionary<string, TGIS_SymbolPicture> _Icons;

        public IconManager()
        {
            _IconList = new TGIS_SymbolList();
            _IconDictionary = new Dictionary<string, TGIS_SymbolAbstract>();

            _Icons = new Dictionary<string, TGIS_SymbolPicture>();
        }

        public TGIS_SymbolAbstract GetIcon(string organization, int status, bool isSelected)
        {
            string key = "";
            string org = organization.ToLower();
            if (org.Contains("bergvik") ||
                org.Contains("stora enso") ||
                org.Contains("korsnäs"))
            {
                key = "storaEnso" + status.ToString();
            }
            else if (org.Contains("sveaskog"))
            {
                key = "sveaskog" + status.ToString();
            }
            else if (org.Contains("sca"))
            {
                key = "sca" + status.ToString();
            }
            else if (org.Contains("stift") ||
                org.Contains("allmänning") ||
                org.Contains("besparing") ||
                org.Contains("häradskog"))
            {
                key = "stift" + status.ToString();
            }
            else
            {
                key = "all" + status.ToString();
            }
            if (isSelected) { key = key + "_S"; }

            return _IconDictionary[key];
            //return new TGIS_SymbolPicture(ConfigurationManager.AppSettings[key] + "?TRUE"); // Endast för de datorer som berörs av tatukgis-grafikbugg (långsamt)
        }

        public void InitIconManager()
        {

            _IconDictionary.Add("storaEnso0", _IconList.Prepare(ConfigurationManager.AppSettings["StoraEnso0"]));
            _IconDictionary.Add("storaEnso1", _IconList.Prepare(ConfigurationManager.AppSettings["StoraEnso1"]));
            _IconDictionary.Add("storaEnso2", _IconList.Prepare(ConfigurationManager.AppSettings["StoraEnso2"]));
            _IconDictionary.Add("storaEnso3", _IconList.Prepare(ConfigurationManager.AppSettings["StoraEnso3"]));
            _IconDictionary.Add("storaEnso0_S", _IconList.Prepare(ConfigurationManager.AppSettings["StoraEnso0_S"]));
            _IconDictionary.Add("storaEnso1_S", _IconList.Prepare(ConfigurationManager.AppSettings["StoraEnso1_S"]));
            _IconDictionary.Add("storaEnso2_S", _IconList.Prepare(ConfigurationManager.AppSettings["StoraEnso2_S"]));
            _IconDictionary.Add("storaEnso3_S", _IconList.Prepare(ConfigurationManager.AppSettings["StoraEnso3_S"]));

            // custom icons for Sveaskog
            _IconDictionary.Add("sveaskog0", _IconList.Prepare(ConfigurationManager.AppSettings["Sveaskog0"]));
            _IconDictionary.Add("sveaskog1", _IconList.Prepare(ConfigurationManager.AppSettings["Sveaskog1"]));
            _IconDictionary.Add("sveaskog2", _IconList.Prepare(ConfigurationManager.AppSettings["Sveaskog2"]));
            _IconDictionary.Add("sveaskog3", _IconList.Prepare(ConfigurationManager.AppSettings["Sveaskog3"]));
            _IconDictionary.Add("sveaskog0_S", _IconList.Prepare(ConfigurationManager.AppSettings["Sveaskog0_S"]));
            _IconDictionary.Add("sveaskog1_S", _IconList.Prepare(ConfigurationManager.AppSettings["Sveaskog1_S"]));
            _IconDictionary.Add("sveaskog2_S", _IconList.Prepare(ConfigurationManager.AppSettings["Sveaskog2_S"]));
            _IconDictionary.Add("sveaskog3_S", _IconList.Prepare(ConfigurationManager.AppSettings["Sveaskog3_S"]));

            // custom icons for SCA
            _IconDictionary.Add("sca0", _IconList.Prepare(ConfigurationManager.AppSettings["SCA0"]));
            _IconDictionary.Add("sca1", _IconList.Prepare(ConfigurationManager.AppSettings["SCA1"]));
            _IconDictionary.Add("sca2", _IconList.Prepare(ConfigurationManager.AppSettings["SCA2"]));
            _IconDictionary.Add("sca3", _IconList.Prepare(ConfigurationManager.AppSettings["SCA3"]));
            _IconDictionary.Add("sca0_S", _IconList.Prepare(ConfigurationManager.AppSettings["SCA0_S"]));
            _IconDictionary.Add("sca1_S", _IconList.Prepare(ConfigurationManager.AppSettings["SCA1_S"]));
            _IconDictionary.Add("sca2_S", _IconList.Prepare(ConfigurationManager.AppSettings["SCA2_S"]));
            _IconDictionary.Add("sca3_S", _IconList.Prepare(ConfigurationManager.AppSettings["SCA3_S"]));

            // custom icons for Stift
            _IconDictionary.Add("stift0", _IconList.Prepare(ConfigurationManager.AppSettings["Stift0"]));
            _IconDictionary.Add("stift1", _IconList.Prepare(ConfigurationManager.AppSettings["Stift1"]));
            _IconDictionary.Add("stift2", _IconList.Prepare(ConfigurationManager.AppSettings["Stift2"]));
            _IconDictionary.Add("stift3", _IconList.Prepare(ConfigurationManager.AppSettings["Stift3"]));
            _IconDictionary.Add("stift0_S", _IconList.Prepare(ConfigurationManager.AppSettings["Stift0_S"]));
            _IconDictionary.Add("stift1_S", _IconList.Prepare(ConfigurationManager.AppSettings["Stift1_S"]));
            _IconDictionary.Add("stift2_S", _IconList.Prepare(ConfigurationManager.AppSettings["Stift2_S"]));
            _IconDictionary.Add("stift3_S", _IconList.Prepare(ConfigurationManager.AppSettings["Stift3_S"]));

            // custom icons for other
            _IconDictionary.Add("all0", _IconList.Prepare(ConfigurationManager.AppSettings["All0"]));
            _IconDictionary.Add("all1", _IconList.Prepare(ConfigurationManager.AppSettings["All1"]));
            _IconDictionary.Add("all2", _IconList.Prepare(ConfigurationManager.AppSettings["All2"]));
            _IconDictionary.Add("all3", _IconList.Prepare(ConfigurationManager.AppSettings["All3"]));
            _IconDictionary.Add("all0_S", _IconList.Prepare(ConfigurationManager.AppSettings["All0_S"]));
            _IconDictionary.Add("all1_S", _IconList.Prepare(ConfigurationManager.AppSettings["All1_S"]));
            _IconDictionary.Add("all2_S", _IconList.Prepare(ConfigurationManager.AppSettings["All2_S"]));
            _IconDictionary.Add("all3_S", _IconList.Prepare(ConfigurationManager.AppSettings["All3_S"]));

        }
    }
}
