/***********************************************************************
 * Module:  Adresa.cs
 * Author:  Darko
 * Purpose: Definition of the Class Adresa
 ***********************************************************************/

namespace Model
{
    public class Adresa
    {
        public string Ulica { get; set; }
        public int Broj { get; set; }
        public int Zgrada { get; set; }
        public int Sprat { get; set; }
        public Grad grad { get; set; }

    }
}