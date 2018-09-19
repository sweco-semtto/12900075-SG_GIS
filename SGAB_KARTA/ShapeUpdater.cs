using System;
using TatukGIS.NDK;

namespace SGAB.SGAB_Karta
{
    public static class ShapeUpdater
    {
        /// <summary>
        /// Updates contents of column STATUS in one shape file with data from column STATUS from another shape file
        /// </summary>
        /// <param name="pathToFileToUpdate">Path to the shape file that will be updated</param>
        /// <param name="pathToFileWithUpdates">Path to the shape file that contains update data</param>
        /// <returns>A string with update status</returns>
        public static string UpdateShapeFile(string pathToFileToUpdate, string pathToFileWithUpdates)
        {
            string updateResult = "Uppdateringen lyckades";

            try
            {
                TGIS_LayerSHP shapeFileToUpdate = (TGIS_LayerSHP)(TGIS_Utils.GisCreateLayer("ToUpdate", pathToFileToUpdate));
                shapeFileToUpdate.Open();

                TGIS_LayerSHP shapeFileWithUpdates = (TGIS_LayerSHP)(TGIS_Utils.GisCreateLayer("WithUpdate", pathToFileWithUpdates));
                shapeFileWithUpdates.Open();

                foreach (object i in shapeFileWithUpdates.Loop())
                {
                    TGIS_Shape rowWithUpdate = (TGIS_Shape)i;

                    string rowWithUpdate_STARTPLATS = rowWithUpdate.GetField("STARTPLATS").ToString();
                    string rowWithUpdate_ORDERNR = rowWithUpdate.GetField("ORDERNR").ToString();
                    string rowWithUpdate_STATUS = rowWithUpdate.GetField("STATUS").ToString();

                    foreach (object j in shapeFileToUpdate.Loop())
                    {
                        TGIS_Shape rowToUpdate = ((TGIS_Shape)j).MakeEditable();

                        string rowToUpdate_ORDERNR = rowToUpdate.GetField("ORDERNR").ToString();
                        string rowToUpdate_STARTPLATS = rowToUpdate.GetField("STARTPLATS").ToString();

                        if (rowWithUpdate_STARTPLATS == rowToUpdate_STARTPLATS && rowWithUpdate_ORDERNR == rowToUpdate_ORDERNR)
                        {
                            rowToUpdate.SetField("STATUS", rowWithUpdate_STATUS);
                        }
                    }
                }

                shapeFileToUpdate.SaveAll();
            }
            catch (Exception ex)
            {
                updateResult = "Uppdateringen misslyckades. " + ex.Message;
            }

            return updateResult;
        }
    }
}
