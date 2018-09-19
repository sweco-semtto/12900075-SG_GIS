using System;
using System.Collections.Generic;
using System.Text;
using TatukGIS.NDK;

namespace SGAB.SGAB_Karta
{
    public class StatusStartplatsChangedFromMapEventArgs : EventArgs
    {
        public TGIS_Shape StartplatsShape
        {
            get;
            protected set;
        }

        public string StartplatsId
        {
            get;
            protected set;
        }

        public int StartplatsStatus
        {
            get;
            protected set;
        }

        /// <summary>
        /// Hämtar OrderId som behövs för att kunna uppdatera en startplats status i MySql. 
        /// </summary>
        public string OrderId
        {
            get;
            protected set;
        }

        /// <summary>
        /// Hämtar IdAccess som behövs för att kunna uppdatera en startplats status i MySql. 
        /// </summary>
        public string IdAccess
        {
            get;
            protected set;
        }

        /// <summary>
        /// Hämtar användarnamnet ifrån inläsningen. 
        /// </summary>
        public int EntrepreneurId
        {
            get;
            protected set;
        }

        /// <summary>
        /// Hämtar en kommentar som angivits när statusen ändrades. 
        /// </summary>
        public string Note
        {
            get;
            protected set;
        }

        /// <summary>
        /// Anger vilken status som användaren klickade på. 
        /// </summary>
        public int FieldStatus
        {
            get;
            protected set;
        
        }

        /// <summary>
        /// Skapar ett nytt StatusStartplatsChangedFromMapEventArgs. 
        /// </summary>
        /// <param name="startplatsShape"></param>
        /// <param name="startplatsStatus"></param>
        public StatusStartplatsChangedFromMapEventArgs(TGIS_Shape startplatsShape, int startplatsStatus, int entrepreneurId, 
            string note, int fieldStatus)
        {
            this.StartplatsShape = startplatsShape;
            this.EntrepreneurId = entrepreneurId;
            this.Note = note;
            this.FieldStatus = fieldStatus;

            // Tar fram uppgifter direkt här så vi slipper göra det sedan ur TGIS_Shape:en startplatsShape
            StartplatsId = startplatsShape.GetField(StartplatsLayer.StartplatsIdColumnName).ToString();
            OrderId = startplatsShape.GetField("OrderId").ToString();
            IdAccess = startplatsShape.GetField("AccessId").ToString();
            StartplatsStatus = startplatsStatus;
        }
    }
}
