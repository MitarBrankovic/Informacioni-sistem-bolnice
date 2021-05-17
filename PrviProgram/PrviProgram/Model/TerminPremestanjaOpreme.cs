using System;

namespace Model
{
   public class TerminPremestanjaOpreme
   {
        public TerminPremestanjaOpreme(Oprema oprema, Sala sala, Sala staraSala, DateTime datumPremestaja)
        {
            this.Oprema = oprema;
            this.Sala = sala;
            this.StaraSala = staraSala;
            this.DatumPremestaja = datumPremestaja;
        }

        public TerminPremestanjaOpreme() { }

        ~TerminPremestanjaOpreme()
        {
            // TODO: implement
        }


        public Oprema Oprema { get; set; }

      public Sala Sala { get; set; }
      public Sala StaraSala { get; set; }
   
      public DateTime DatumPremestaja { get; set; }
   
   }
}