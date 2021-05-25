namespace Model
{
    public class GuestPacijent
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }
        public string KontaktTelefon { get; set; }
        public Pol Pol { get; set; }

        public GuestPacijent() { }

        public GuestPacijent(string ime, string prezime, string jmbg, string kontaktTelefon, Pol pol)
        {
            Ime = ime;
            Prezime = prezime;
            Jmbg = jmbg;
            KontaktTelefon = kontaktTelefon;
            Pol = pol;
        }

        public override string ToString()
        {
            return "Guest " + Ime + " " + Prezime + " ";
        }

    }
}