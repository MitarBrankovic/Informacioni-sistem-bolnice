using System;

namespace Model
{
   public class TerminPremestanjaOpreme
   {
        public TerminPremestanjaOpreme(Oprema oprema, Sala sala, Sala staraSala, DateTime datumPremestaja)
        {
            this.oprema = oprema;
            this.sala = sala;
            this.staraSala = staraSala;
            this.datumPremestaja = datumPremestaja;
        }

        public TerminPremestanjaOpreme() { }

        ~TerminPremestanjaOpreme()
        {
            // TODO: implement
        }


        public Oprema oprema { get; set; }

      public Sala sala { get; set; }
      public Sala staraSala { get; set; }
   
      public DateTime datumPremestaja { get; set; }
   
   }
}