using System;

namespace Model
{
    public class Osoba
    {
        public Grad MestoRodjenja { get; set; }
        public Korisnik Korisnik { get; set; }
        public Adresa AdresaStanovanja { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Jmbg { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public Pol Pol { get; set; }

    }
}