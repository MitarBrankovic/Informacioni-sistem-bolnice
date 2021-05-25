namespace Model
{
    public class Upravnik : Osoba
    {
        public override string ToString()
        {
            return Ime + " " + Prezime;
        }

        public Upravnik(Osoba osoba) : base(osoba)
        { }

        public Upravnik() { }
    }
}