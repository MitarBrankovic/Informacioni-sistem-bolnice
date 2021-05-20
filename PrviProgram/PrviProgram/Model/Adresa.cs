namespace Model
{
    public class Adresa
    {
        public string Ulica { get; set; }
        public int Broj { get; set; }
        public int Sprat { get; set; }
        public int Stan { get; set; }
        public Grad grad { get; set; }

        public Adresa() { }

        public Adresa(string ulica, int broj, int sprat, int stan, Grad grad)
        {
            Ulica = ulica;
            Broj = broj;
            Sprat = sprat;
            Stan = stan;
            this.grad = grad;
        }                           

    }
}