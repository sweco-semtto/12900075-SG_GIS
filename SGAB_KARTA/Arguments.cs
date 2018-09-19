/********************************************************
 * Arguments.cs hanterar/håller koll på dels vilket 
 * läge/mode som är aktivt och dels vilken typ av 
 * ritning det rör sig om när det är ritningsläge.
 * 
 * LSAM, SWECO Position, våren 2006
 ********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta
{
    /// <summary>
    /// Hanterar/håller koll på dels vilket läge/mode 
    /// som är aktivt och dels vilken typ av ritning 
    /// det rör sig om när det är ritningsläge.
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
        /// Hämtar/sätter aktuellt läge
        /// </summary>
        public ButtonMode ButtonMode
		{
            get { return _buttonMode; }
            set { _buttonMode = value; }
		}

        ///// <summary>
        ///// Hämtar/sätter aktuellt ritningsläge
        ///// </summary>
        //public DrawType DrawType
        //{
        //    get { return _drawType; }
        //    set { _drawType = value; }
        //}       

    }
}
