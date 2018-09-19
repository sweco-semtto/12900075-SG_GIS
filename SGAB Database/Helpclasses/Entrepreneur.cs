using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Database
{
    public class Entrepreneur
    {
        protected string _Id;
        protected string _Name;

        /// <summary>
        /// Hämtar Entreprenörens idnummer. 
        /// </summary>
        public string Id
        {
            get
            {
                return _Id;
            }
        }

        /// <summary>
        /// Hämtar entreprenörens namn. 
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        /// <summary>
        /// Skapar en ny Entreprenör
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        public Entrepreneur(string Id, string Name)
        {
            _Id = Id;
            _Name = Name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static Entrepreneur FindEntrepreneurById(List<Entrepreneur> entreprenorer, string idToFind)
        {
            Entrepreneur entreprenor = null;

            if (idToFind == "0")
                entreprenor = new Entrepreneur("0", "Ej angiven");
            else
            {
                entreprenor = entreprenorer.Find(
                                delegate(Entrepreneur ent)
                                {
                                    return ent.Id == idToFind;
                                }
                                );
            }

            return entreprenor;
        }

        public static Entrepreneur FindEntrepreneurByName(List<Entrepreneur> entreprenorer, string entrepreneursName)
        {
            Entrepreneur entreprenor = null;

            if (entrepreneursName.Equals("Ej angiven"))
                entreprenor = new Entrepreneur("0", "Ej angiven");
            else
            {
                entreprenor = entreprenorer.Find(
                                delegate(Entrepreneur ent)
                                {
                                    return ent.Name.Equals(entrepreneursName);
                                }
                                );
            }

            return entreprenor;
        }
    }
}
