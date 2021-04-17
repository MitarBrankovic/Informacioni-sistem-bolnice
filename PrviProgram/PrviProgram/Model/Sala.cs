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

        public System.Collections.ArrayList oprema;

        /// <pdGenerated>default getter</pdGenerated>
        public System.Collections.ArrayList GetOprema()
        {
            if (oprema == null)
                oprema = new System.Collections.ArrayList();
            return oprema;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetOprema(System.Collections.ArrayList newOprema)
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
                this.oprema = new System.Collections.ArrayList();
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