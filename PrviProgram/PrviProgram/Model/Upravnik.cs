namespace Model
{
    public class Upravnik : Osoba
    {
        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}