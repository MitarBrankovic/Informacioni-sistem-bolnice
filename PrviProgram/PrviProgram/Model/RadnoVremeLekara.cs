using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RadnoVremeLekara
    {
        public Lekar Lekar { get; set; }
        public string PocetakRadnogVremena { get; set; }
        public string KrajRadnogVremena { get; set; }
        public DateTime PocetniDatum { get; set; }
        public DateTime KrajnjiDatum { get; set; }

        public RadnoVremeLekara() { }
        public RadnoVremeLekara(Lekar lekar, string pocetakRadnogVremena, string krajRadnogVremena, DateTime pocetniDatum, DateTime krajnjiDatum)
        {
            Lekar = lekar;
            PocetakRadnogVremena = pocetakRadnogVremena;
            KrajRadnogVremena = krajRadnogVremena;
            PocetniDatum = pocetniDatum;
            KrajnjiDatum = krajnjiDatum;
        }

    }
}
