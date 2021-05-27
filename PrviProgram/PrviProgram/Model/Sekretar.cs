namespace Model
{
    public class Sekretar : Osoba
    {
        public override string ToString()
        {
            return Ime + " " + Prezime;
        }

        public Sekretar() { }
        public Sekretar(Osoba osoba) : base(osoba)
        { }
    }
}