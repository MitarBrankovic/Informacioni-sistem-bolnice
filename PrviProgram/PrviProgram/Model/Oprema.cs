namespace Model
{
   public class Oprema
   {
        public Oprema(string Naziv, int Kolicina, TipOpreme Tip, string NazivSale)
        {
            this.Naziv = Naziv;
            this.Kolicina = Kolicina;
            this.Tip = Tip;
            this.NazivSale = NazivSale;
        }

        public Oprema() { }

        ~Oprema()
        {
            // TODO: implement
        }


        public string Naziv { get; set; }
        public int Kolicina { get; set; }
        public TipOpreme Tip { get; set; }

        public string NazivSale { get; set; }

        public override string ToString()
        {
            return Naziv;
        }

    }
}