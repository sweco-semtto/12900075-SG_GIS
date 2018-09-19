/********************************************************
 * Arguments.cs hanterar/h�ller koll p� dels vilket 
 * l�ge/mode som �r aktivt och dels vilken typ av 
 * ritning det r�r sig om n�r det �r ritningsl�ge.
 * 
 * LSAM, SWECO Position, v�ren 2006
 ********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta
{
    /// <summary>
    /// Hanterar/h�ller koll p� dels vilket l�ge/mode 
    /// som �r aktivt och dels vilken typ av ritning 
    /// det r�r sig om n�r det �r ritningsl�ge.
    /// </summary>
    public class Arguments
    {
        private static readonly Arguments _instance = new Arguments(); 
        private ButtonMode _buttonMode = ButtonMode.ZoomBoxMode;
        //private DrawType _drawType;

        public Arguments()
		{
		}

        /// <summary>
        /// Returnerar instansen av Arguments
        /// </summary>
        /// <returns></returns>
        public static Arguments GetArguments()
        {
            return _instance;
        }

        /// <summary>
        /// H�mtar/s�tter aktuellt l�ge
        /// </summary>
        public ButtonMode ButtonMode
		{
            get { return _buttonMode; }
            set { _buttonMode = value; }
		}

        ///// <summary>
        ///// H�mtar/s�tter aktuellt ritningsl�ge
        ///// </summary>
        //public DrawType DrawType
        //{
        //    get { return _drawType; }
        //    set { _drawType = value; }
        //}       

    }
}
