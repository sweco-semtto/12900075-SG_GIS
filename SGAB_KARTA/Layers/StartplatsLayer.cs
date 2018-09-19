using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using SGAB.SGAB_Database;
using TatukGIS.NDK;

namespace SGAB.SGAB_Karta
{
    public class StartplatsLayer : TGIS_LayerVector
    {
        /// <summary>
        /// Hämtar vilket kolumnanan som startplats-id:et har i geometrin. 
        /// </summary>
        public static string StartplatsIdColumnName
        {
            get
            {
                return "Mi_prinx";
            }
        }

        /// <summary>
        /// Hämtar lagernamnet för startplatserna. 
        /// </summary>
        public static string StartplatsLayerName
        {
            get
            {
                return "Startplatser";
            }
        }

        /// <summary>
        /// Anger var symbolerna kommer ifrån. 
        /// </summary>
        protected IconManager IconManager
        {
            get;
            set;
        }

        /// <summary>
        /// Anger en översättning mellan id-nummer på en startplats till själva geometrin. 
        /// </summary>
        protected List<KeyValuePair<string, TGIS_Shape>> TranslationIdToShape
        {
            get;
            set;
        }

        /// <summary>
        /// Anger var den temporärar filen med startplatser för offline sparas. 
        /// </summary>
        public static string LoggFolder
        {
            get;
            protected set;
        }

        /// <summary>
        /// Skapar ett nytt lager med startplatser ifrån MySql i sig. 
        /// </summary>
        /// <param name="CS"></param>
        public StartplatsLayer(TGIS_CSCoordinateSystem CS, IconManager IconManager, string loggFolder)
        {
            TranslationIdToShape = new List<KeyValuePair<string, TGIS_Shape>>();

            // Anger namn och koordinatsystem. 
            this.Name = "Startplatser";
            this.CS = CS;

            // Anger hanteraren för symbolerna. 
            this.IconManager = IconManager;
            
            // Sparar var startplatser för offline skall sparas någonstans. 
            LoggFolder = loggFolder;

            // Lägger till fälten som behövs
            this.AddField(ConfigurationManager.AppSettings["NamnFöretagsKolumn"].ToString(), TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField(ConfigurationManager.AppSettings["NamnRegionKolumn"].ToString(), TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField(ConfigurationManager.AppSettings["NamnDistriktKolumn"].ToString(), TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField(ConfigurationManager.AppSettings["NamnCanKolumn"].ToString(), TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField(ConfigurationManager.AppSettings["NamnStatusKolumn"].ToString(), TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Startplats", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Areal", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Kontakt", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Mobil", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Telefon arbete", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Kommentar", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Ej påbörjad", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Gödsel utkörd", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Färdiggödslat", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Säckar hämtade", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Ordernr", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("OrderId", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("AccessId", TGIS_FieldType.gisFieldTypeString, 10, 2);
            this.AddField("Mi_prinx", TGIS_FieldType.gisFieldTypeString, 10, 2);
        }

        public void AddStartplats(TGIS_Point coordinatesPoint, DataRow rowStartplats, DataRow rowForetag)
        {
            TGIS_Shape startplatsPoint = this.CreateShape(TGIS_ShapeType.gisShapeTypePoint);

            startplatsPoint.SetField(ConfigurationManager.AppSettings["NamnFöretagsKolumn"].ToString(), 
                TranslatorMySqlAndAccess.MySql_To_Access(rowForetag["Foretagsnamn"].ToString()));
            startplatsPoint.SetField(ConfigurationManager.AppSettings["NamnRegionKolumn"].ToString(), 
                TranslatorMySqlAndAccess.MySql_To_Access(rowForetag["Region_Forvaltning"].ToString()));
            startplatsPoint.SetField(ConfigurationManager.AppSettings["NamnDistriktKolumn"].ToString(), 
                TranslatorMySqlAndAccess.MySql_To_Access(rowForetag["Distrikt_Omrade"].ToString()));
            startplatsPoint.SetField(ConfigurationManager.AppSettings["NamnCanKolumn"].ToString(), 
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["Skog_CAN_ton_startplats"].ToString()));
            startplatsPoint.SetField(ConfigurationManager.AppSettings["NamnStatusKolumn"].ToString(), 
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["Status"].ToString()));
            startplatsPoint.SetField("Startplats",
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["Startplats_startplats"].ToString()));
            startplatsPoint.SetField("Areal", 
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["Areal_ha_startplats"].ToString()));
            startplatsPoint.SetField("Kontakt", 
                TranslatorMySqlAndAccess.MySql_To_Access(rowForetag["Kontaktperson1"].ToString()));
            startplatsPoint.SetField("Mobil", 
                TranslatorMySqlAndAccess.MySql_To_Access(rowForetag["TelefonMobil1"].ToString()));
            startplatsPoint.SetField("Telefon arbete", 
                TranslatorMySqlAndAccess.MySql_To_Access(rowForetag["TelefonArb1"].ToString()));
            startplatsPoint.SetField("Kommentar", 
                TranslatorMySqlAndAccess.MySql_To_Access(rowForetag["Kommentar"].ToString()));
            startplatsPoint.SetField("Ej påbörjad",
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["Kommentar1"].ToString()));
            startplatsPoint.SetField("Gödsel utkörd",
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["Kommentar2"].ToString()));
            startplatsPoint.SetField("Färdiggödslat",
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["Kommentar3"].ToString()));
            startplatsPoint.SetField("Säckar hämtade",
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["Kommentar4"].ToString()));
            startplatsPoint.SetField("Ordernr", 
                TranslatorMySqlAndAccess.MySql_To_Access(rowForetag["Ordernr"].ToString()));
            startplatsPoint.SetField("OrderId", 
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["OrderID"].ToString()));
            startplatsPoint.SetField("AccessId", 
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["ID_Access"].ToString()));
            startplatsPoint.SetField("Mi_prinx", 
                TranslatorMySqlAndAccess.MySql_To_Access(rowStartplats["ID"].ToString()));

            if (rowStartplats["ID"].ToString().Equals("") || rowForetag["Ordernr"].ToString().Equals("") ||
                rowStartplats["ID_Access"].ToString().Equals(""))
            {
            }

            // Lägger till själva koordinaten
            startplatsPoint.AddPart();
            startplatsPoint.AddPoint(coordinatesPoint);

            // Lägger till startplatsen i en översättningslista kopplat till geometrin. 
            this.TranslationIdToShape.Add(new KeyValuePair<string, TGIS_Shape>(rowStartplats["ID"].ToString(), startplatsPoint));
        }


        public TGIS_LayerVector AddStartplatserFromDataTables(DataTable foretagMySqlTable, DataTable startplatsMySqlTable)
        {

            //this.Items.Clear();
            List<string> _orderIDs = new List<string>();

            foreach (DataRow startplatsRow in startplatsMySqlTable.Rows)
            {
                // Tar fram koordinaterna. 
                double CoordX = double.Parse(startplatsRow["Ostligkoordinat_startplats"].ToString());
                double CoordY = double.Parse(startplatsRow["Nordligkoordinat_startplats"].ToString());
                TGIS_Point coordinatesPoint = new TGIS_Point(CoordX, CoordY);

                // Tar fram företagsraden. 
                string OrderID = startplatsRow["OrderID"].ToString();
                int year = DateTime.Now.Year - 1;
                string date = "#10/1/" + year + "#"; // Tar inte nyår som brytdatum utan 1:a oktober gäller. 

                foreach (DataRow foretagsRow in foretagMySqlTable.Rows)
                {
                    if (foretagsRow["OrderID"].ToString().Equals(OrderID))
                    {
                        // Uppdaterar startplatsen om.m. den är inom beställningsdatumgränsen, har rätt OrderId och är inte borttagen. 
                        if (DateTime.Parse(foretagsRow["Bestallningsdatum"].ToString()) > DateTime.Parse(date) &&
                            startplatsRow["OrderID"].Equals(OrderID) &&
                            startplatsRow["Borttagen"].Equals("0"))
                        {
                            // Tar bort startplatsen. 
                            DeleteStartplats(startplatsRow["ID"].ToString());

                            // Lägger till startplatsen (som nyss togs bort för att uppdatera dess metadata). 
                            this.AddStartplats(coordinatesPoint, startplatsRow, foretagsRow);
                        }
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Tar bort en startplats från grafiken m.h.a. dess id-nummer. 
        /// </summary>
        /// <param name="Id">Agner vilken id-nummer startplatsen har som skall tas bort. </param>
        /// <returns>Returnerar true om startplatsen togs bort, false om dess index inte hittas. </returns>
        public bool DeleteStartplats(string Id)
        {
            const int NO_ID_FOUND = -1;
            int id_found_index = NO_ID_FOUND;
         
            // Söker igenom alla startplatser i grafiken efter ett visst id-nummer och tar bort den. 
            string startplatsId = String.Empty;
            for (int startplatsNumber = 0; startplatsNumber < this.Items.Count; startplatsNumber++)
            {
                if (this.Items[startplatsNumber] is TGIS_ShapePoint)
                {
                    startplatsId = (this.Items[startplatsNumber] as TGIS_ShapePoint).GetField("Mi_prinx").ToString();
                    if (startplatsId.Equals(Id))
                    {
                        id_found_index = startplatsNumber;
                        break;
                    }
                }
            }

            // Om vi har hittat en startplats, ta bort den utanför for-loopen. 
            if (id_found_index != NO_ID_FOUND)
            {
                this.Items.Delete(id_found_index);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Kollar om Startplatsen redan finns i kartan, då uppdateras den bara. Anledningen till detta är att ibland hämtas inte alla startplatser vid en hämtning
        /// och vi vill inte att en startplats skall försvinna från kartan. 
        /// </summary>
        /// <param name="coordinatesPoint">Startplatsens koordinater. </param>
        /// <param name="rowStartplats">Uppgifter om startplatsen. </param>
        /// <param name="rowForetag">Uppgifter om företaget. </param>
        /// <returns>Returnerar om startplatsen fanns och därmed har uppdaterats</returns>
        public bool UpdateStartplatsIfAlreadyExists(TGIS_Point coordinatesPoint, DataRow rowStartplats, DataRow rowForetag)
        {
            //this.Items.

            return false;
        }

        /// <summary>
        /// VARNING! Använda Select på en DataTable i .NET kan ge exception. Använd medtoden nedan istället. 
        /// </summary>
        /// <param name="foretagMySqlTable"></param>
        /// <param name="startplatsMySqlRows"></param>
        /// <returns></returns>
        public TGIS_LayerVector AddStartplatserFromDataTables(DataTable foretagMySqlTable, DataRow[] startplatsMySqlRows)
        {
            this.Items.Clear();

            foreach (DataRow row in startplatsMySqlRows)
            {
                // Tar fram koordinaterna. 
                double CoordX = double.Parse(row["Ostligkoordinat_startplats"].ToString());
                double CoordY = double.Parse(row["Nordligkoordinat_startplats"].ToString());
                TGIS_Point coordinatesPoint = new TGIS_Point(CoordX, CoordY);

                // Tar fram företagsraden. 
                string OrderID = row["OrderID"].ToString();
                DataRow[] selectedRows = foretagMySqlTable.Select("OrderID = " + OrderID);
                DataRow selectedRow = selectedRows.GetValue(0) as DataRow;

                // Lägger till startplatsen. 
                this.AddStartplats(coordinatesPoint, row, selectedRow);
            }

            return this;
        }

        public TGIS_LayerVector AddStartplatserFromDataTables(DataTable foretagMySqlTable, DataTable startplatserMySql, int entrepreneurId)
        {
            this.Items.Clear();

            // Tar en kopia på strukturen för företagen och startplatserna. 
            DataTable myForetag = foretagMySqlTable.Clone();
            DataTable myStartplatser = startplatserMySql.Clone();

            // Lägger till alla företag till xml-filen. 
            foreach (DataRow foretagsRow in foretagMySqlTable.Rows)
                myForetag.ImportRow(foretagsRow);

            // Lägger till alla startplatser till xml-filen. 
            foreach (DataRow startplatsRow in startplatserMySql.Rows)
            {
                if (startplatsRow["Fraktentreprenors_ID"].ToString().Equals(entrepreneurId.ToString()) ||
                    startplatsRow["Spridningsentreprenors_ID"].ToString().Equals(entrepreneurId.ToString()))
                {
                    // Tar fram koordinaterna. 
                    double CoordX = double.Parse(startplatsRow["Ostligkoordinat_startplats"].ToString());
                    double CoordY = double.Parse(startplatsRow["Nordligkoordinat_startplats"].ToString());
                    TGIS_Point coordinatesPoint = new TGIS_Point(CoordX, CoordY);

                    // Tar fram företagsraden. 
                    string orderId = startplatsRow["OrderID"].ToString();
                    foreach (DataRow foretagsRow in foretagMySqlTable.Rows)
                    {
                        if (foretagsRow["OrderID"].ToString().Equals(orderId))
                        {
                            this.AddStartplats(coordinatesPoint, startplatsRow, foretagsRow);

                            if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
                            {
                                // Kopierar in raden i kopian. 
                                myStartplatser.ImportRow(startplatsRow);
                            }
                        }
                    }
                }
            }

            // Sparar kopian i en textfil. 
            if (SGAB_InternetConnection.InternetConnection.HasInternetConnection)
            {
                myStartplatser.WriteXml(LoggFolder + "Startplatser.xml", XmlWriteMode.WriteSchema);
                myForetag.WriteXml(LoggFolder + "Foretag.xml", XmlWriteMode.WriteSchema);
            }

            return this;
        }
    }
}
