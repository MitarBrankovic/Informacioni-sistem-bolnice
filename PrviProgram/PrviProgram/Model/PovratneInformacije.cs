using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PovratneInformacije
    {
        public bool ImaProblem { get; set; }
        public int Ocena { get; set; }
        public String Primedba { get; set; }

        public Osoba Osoba { get; set; }

        public PovratneInformacije() { }

        public PovratneInformacije(bool imaProblem, int ocena, string primedba, Osoba osoba)
        {
            this.ImaProblem = imaProblem;
            this.Ocena = ocena;
            this.Primedba = primedba;
            this.Osoba = osoba;
        }


    }
}
