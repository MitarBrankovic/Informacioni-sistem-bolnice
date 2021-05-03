namespace Model
{
    public class Sekretar : Osoba
    {
        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}