using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Database
{
    public class Status
    {
        protected string _Id;
        protected int _StatusNr;

        public string Id
        {
            get  {  return _Id; }
        }

        public int StatusNr
        {
            get{    return _StatusNr;   }
            set { _StatusNr = value;     }
        }
       
        public Status(int StatusNr, string Id)
        {
            _Id = Id;
            _StatusNr = StatusNr;
        }

        public override string ToString()
        {            
            return Id;
        }       
    }

    public class StatusKodLista
    {
        private static List<Status> _statusKoder;

        public static void Initialize()
        {
            _statusKoder = new List<Status>();
            _statusKoder.Add(new Status(0, "Ej påbörjad"));
            _statusKoder.Add(new Status(1, "Gödsel utkörd"));
            _statusKoder.Add(new Status(2, "Färdiggödslat"));
            _statusKoder.Add(new Status(3, "Säckar hämtade"));    
        }

        public static List<Status> StatusKoder
        {
            get
            {
                return _statusKoder;
            }
        }

        public static Status FindById(int idToFind)
        {
            Status kod;

            kod = _statusKoder.Find(
                                delegate(Status status)
                                {
                                    return status.StatusNr == idToFind;
                                }
                                );
            return kod;
        }

        public static Status FindByName(string nameToFind)
        {
            Status kod;

            kod = _statusKoder.Find(
                                delegate(Status status)
                                {
                                    return status.Id.Equals(nameToFind);
                                }
                                );
            return kod;
        }
    }
}
