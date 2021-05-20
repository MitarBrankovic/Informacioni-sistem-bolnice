namespace Model
{
    public class Grad
    {
        public string Ime { get; set; }
        public Drzava drzava { get; set; }

        public Grad() { }

        public Grad(string ime, Drzava drzava)
        {
            Ime = ime;
            this.drzava = drzava;
        }

        public override string ToString()
        {
            return Ime;
        }

    }
}