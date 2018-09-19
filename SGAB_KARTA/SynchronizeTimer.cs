using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SGAB.SGAB_Karta
{
    public class SynchronizeTimer
    {
        public Timer Timer
        {
            get;
            protected set;
        }

        public bool AdminSynchronization
        {
            get;
            protected set;
        }

        /// <summary>
        /// Anger intervallet i antalet sekunder mellan väckningarna. 
        /// </summary>
        /// <param name="Intervall"></param>
        public SynchronizeTimer(int Intervall, bool isAdmin)
        {
            this.Timer = new Timer();
            this.Timer.Interval = Intervall * 1000;           

            this.AdminSynchronization = isAdmin;
        }

        public void Start()
        {
            this.Timer.Start();
        }

        public void Stop()
        {
            this.Timer.Stop();
        }
    }
}
