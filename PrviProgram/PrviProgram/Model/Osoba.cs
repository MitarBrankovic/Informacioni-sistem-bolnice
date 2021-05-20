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
        public string KontaktTelefon { get; set; }

        public Osoba() { }

        public Osoba(Osoba o)
        {
            MestoRodjenja = o.MestoRodjenja;
            Korisnik = o.Korisnik;
            AdresaStanovanja = o.AdresaStanovanja;
            Ime = o.Ime;
            Prezime = o.Prezime;
            Email = o.Email;
            Jmbg = o.Jmbg;
            DatumRodjenja = o.DatumRodjenja;
            Pol = o.Pol;
            KontaktTelefon = o.KontaktTelefon;
        }

        public Osoba(Grad mestoRodjenja, Korisnik korisnik, Adresa adresaStanovanja, string ime, string prezime,
                     string email, string jmbg, DateTime datumRodjenja, Pol pol, string kontaktTelefon)
        {
            MestoRodjenja = mestoRodjenja;
            Korisnik = korisnik;
            AdresaStanovanja = adresaStanovanja;
            Ime = ime;
            Prezime = prezime;
            Email = email;
            Jmbg = jmbg;
            DatumRodjenja = datumRodjenja;
            Pol = pol;
            KontaktTelefon = kontaktTelefon;
        }

    }
}