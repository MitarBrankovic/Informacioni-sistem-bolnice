/***********************************************************************
 * Module:  Anamneza.cs
 * Author:  Nesa
 * Purpose: Definition of the Class Model.Anamneza
 ***********************************************************************/

using System;

namespace Model
{
    public class Anamneza
    {
        public String Opis { get; set; }

        public Anamneza(String opis)
        {
            Opis = opis;
        }
    }
}