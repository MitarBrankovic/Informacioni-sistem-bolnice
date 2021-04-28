namespace Model
{
   public class Oprema
   {

        public Oprema(string naziv, TipOpreme tip, int kolicina)
        {
            this.Naziv = naziv;
            this.Tip = tip;
            this.Kolicina = kolicina;
        }

       
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

        public override string ToString()
        {
            return Naziv;
        }

    }
}