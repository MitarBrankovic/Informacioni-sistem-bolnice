using System;

namespace Model
{
   public class TerminPremestanjaOpreme : Entity
   {
        public TerminPremestanjaOpreme(Oprema oprema, Sala sala, Sala staraSala, DateTime datumPremestaja)
        {
            this.Oprema = oprema;
            this.Sala = sala;
            this.StaraSala = staraSala;
            this.DatumPremestaja = datumPremestaja;
        }

        public TerminPremestanjaOpreme() { }


        public Oprema Oprema { get; set; }

      public Sala Sala { get; set; }
      public Sala StaraSala { get; set; }
   
      public DateTime DatumPremestaja { get; set; }
   
   }
}