namespace Model
{
    public class Drzava
    {
        public string Ime { get; set; }

        public Drzava() { }

        public Drzava(string ime)
        {
            Ime = ime;
        }

        public override string ToString()
        {
            return Ime;
        }

    }
}