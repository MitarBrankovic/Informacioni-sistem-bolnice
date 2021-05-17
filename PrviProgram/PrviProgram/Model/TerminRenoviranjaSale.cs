using System;

namespace Model
{
    public enum TipRenoviranja{spajanje,razdvajanje}
    public class TerminRenoviranjaSale
    {
        public TerminRenoviranjaSale(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja, Sala prvaSala, Sala drugaSala)
        {
            this.Sala = sala;
            this.PocetakRenoviranja = pocetakRenoviranja;
            this.KrajRenoviranja = krajRenoviranja;
            this.PrvaSala = prvaSala;
            this.DrugaSala = drugaSala;
        }
        public TerminRenoviranjaSale() { }

        public DateTime PocetakRenoviranja { get; set; }
        public DateTime KrajRenoviranja { get; set; }

        public Sala Sala { get; set; }

        public Sala PrvaSala { get; set; }
        public Sala DrugaSala { get; set; }

        public TipRenoviranja TipRenoviranja { get; set; }

    }
}