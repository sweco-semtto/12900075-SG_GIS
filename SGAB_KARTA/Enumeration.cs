/********************************************************
 * I Enumeration.cs definieras vilka olika typer av 
 * lägen/modes som kan förekomma samt vilka olika typer
 * av ritningslägen som kan förekomma.
 * 
 * LSAM, SWECO Position, våren 2006
 ********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SGAB.SGAB_Karta
{
    public enum ButtonMode
    {
        HyperLinkMode, InfoMode, MeasureAreaMode, MeasureLengthMode, PanMode, SelectMultipleMode, ZoomBoxMode, SelectMultipleToConnectMode
    }

    //public enum DrawType
    //{
    //    Hansynsyta, Objektsyta, EgenYta, Basvag, Kraftledning, Vattendrag, Vag, Peklinje, EgenLinje ,Avlagg, Larmpunkt, EgenPunkt, Text      
    //}
}
