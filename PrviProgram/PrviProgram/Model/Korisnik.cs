namespace Model
{
    public class Korisnik
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public Osoba osoba { get; set; }

    }
}