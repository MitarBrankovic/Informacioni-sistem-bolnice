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

    }
}