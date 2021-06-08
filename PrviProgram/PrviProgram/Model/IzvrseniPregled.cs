/***********************************************************************
 * Module:  IzvrseniPregled.cs
 * Author:  Nesa
 * Purpose: Definition of the Class Model.IzvrseniPregled
 ***********************************************************************/

using System;
using System.ComponentModel;

namespace Model
{
    public class IzvrseniPregled : INotifyPropertyChanged
    {
        //public Termin Termin { get; set; }
        public String Sifra { get; set; }
        public DateTime Datum { get; set; }
        public TipTermina TipTermina { get; set; }

        public Lekar Lekar { get; set; }
        public Anamneza anamneza { get; set; }
        public Recept recept { get; set; }
        public string Beleska { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if (ph != null)
                ph(this, new PropertyChangedEventArgs(p));
        }
    }
}