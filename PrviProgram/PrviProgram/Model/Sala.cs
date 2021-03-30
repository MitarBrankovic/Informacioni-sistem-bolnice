namespace Model
{
    public class Sala
    {
        public Sala()
        {
            // TODO: implement
        }

        ~Sala()
        {
            // TODO: implement
        }

        public TipSale Tip { get; set; }
        public string Naziv { get; set; }
        public int Sprat { get; set; }
        public bool Dostupnost { get; set; }
        public string Sifra { get; set; }

        public string NazivSprat
        {
            get
            {
                return Naziv + " " + Sprat.ToString();
            }
            set
            {

            }
        }

    }
}