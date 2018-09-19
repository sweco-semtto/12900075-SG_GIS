/********************************************************
 * I Enumeration.cs definieras vilka olika typer av 
 * l�gen/modes som kan f�rekomma samt vilka olika typer
 * av ritningsl�gen som kan f�rekomma.
 * 
 * LSAM, SWECO Position, v�ren 2006
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
