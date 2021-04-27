namespace Model
{
   public class Oprema
   {
        public Oprema()
        {
            // TODO: implement
        }

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