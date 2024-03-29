using System.Collections.Generic;

namespace Model
{
    public class Sala
    {
        public List<Oprema> oprema;


        public Sala()
        {
            oprema = new List<Oprema>();
        }

        public Sala(TipSale tip, string naziv, int sprat, string sifra)
        {
            this.Naziv = naziv;
            this.Tip = tip;
            this.Sprat = sprat;
            this.Sifra = sifra;
        }

        ~Sala()
        {
            // TODO: implement
        }

        public TipSale Tip { get; set; }
        public string Naziv { get; set; }
        public int Sprat { get; set; }
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

        public override string ToString()
        {
            return Naziv;
        }

        //List<Oprema> opreme = new List<Oprema>();

        /// <pdGenerated>default getter</pdGenerated>
        public List<Oprema> GetOprema()
        {
            if (oprema == null)
                oprema = new List<Oprema>();
            return oprema;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetOprema(List<Oprema> newOprema)
        {
            RemoveAllOprema();
            foreach (Oprema oOprema in newOprema)
                AddOprema(oOprema);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddOprema(Oprema newOprema)
        {
            if (newOprema == null)
                return;
            if (this.oprema == null)
                this.oprema = new List<Oprema>();
            if (!this.oprema.Contains(newOprema))
                this.oprema.Add(newOprema);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveOprema(Oprema oldOprema)
        {
            if (oldOprema == null)
                return;
            if (this.oprema != null)
                if (this.oprema.Contains(oldOprema))
                    this.oprema.Remove(oldOprema);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllOprema()
        {
            if (oprema != null)
                oprema.Clear();
        }

        public string NadjiTip
        {
            get{
                string nadjiTip = "";
                if (this.Tip == TipSale.Kancelarija)
                    nadjiTip = "Kancelarija";
                else if (this.Tip == TipSale.Magacin)
                    nadjiTip = "Magacin";
                else if (this.Tip == TipSale.Operaciona)
                    nadjiTip = "Operaciona";
                else if (this.Tip == TipSale.SalaSaKrevetima)
                    nadjiTip = "Sala sa krevetima";
                else if (this.Tip == TipSale.SalaZaOdmor)
                    nadjiTip = "Sala za odmor";

                return nadjiTip;
            }
            set{ }
        }

    }
}