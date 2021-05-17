/***********************************************************************
 * Module:  TerminRenoviranjaSale.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Model.TerminRenoviranjaSale
 ***********************************************************************/

using System;

namespace Model
{
    public enum TipRenoviranja{spajanje,razdvajanje}
    public class TerminRenoviranjaSale
    {
        public TerminRenoviranjaSale(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja, Sala sala1, Sala sala2)
        {
            this.sala = sala;
            this.PocetakRenoviranja = pocetakRenoviranja;
            this.KrajRenoviranja = krajRenoviranja;
            this.sala1 = sala1;
            this.sala2 = sala2;
        }
        public TerminRenoviranjaSale() { }

        public DateTime PocetakRenoviranja { get; set; }
        public DateTime KrajRenoviranja { get; set; }

        public Sala sala { get; set; }

        public Sala sala1 { get; set; }
        public Sala sala2 { get; set; }

        public TipRenoviranja TipRenoviranja { get; set; }

    }
}