/***********************************************************************
 * Module:  IzvrseniPregled.cs
 * Author:  Nesa
 * Purpose: Definition of the Class Model.IzvrseniPregled
 ***********************************************************************/

using System;

namespace Model
{
    public class IzvrseniPregled
    {
        public Termin Termin { get; set; }
        public Anamneza anamneza { get; set; }
        public Recept recept { get; set; }
        public Terapija terapija { get; set; }

    }
}