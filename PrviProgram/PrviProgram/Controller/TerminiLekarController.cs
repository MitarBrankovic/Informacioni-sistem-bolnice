using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class TerminiLekarController
    {
        TerminiLekarService terminiLekar = new TerminiLekarService();
        public List<string> ZauzetiTerminiLekaraDatuma(Lekar lekar, DateTime datumTermina)
        {
            return terminiLekar.ZauzetiTerminiLekara(lekar, datumTermina);
        }
        public int[] ProveraZauzetostiLekara(string jmbg, DateTime datum, string[] nizSlobodnihTermina)
        {
            return terminiLekar.ProveraZauzetostiLekara(jmbg, datum, nizSlobodnihTermina);
        }
        public bool ProveraZauzetostiKodLekara(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar)
        {
            return terminiLekar.ProveraZauzetostiKodLekara(pocetakDatum, krajDatum, selektovaniLekar);
        }
        public Lekar LekarKojiJeZaduzenZaTermin(Termin selektovanitermin)
        {
            return terminiLekar.LekarKojiJeZaduzenZaTermin(selektovanitermin);
        }
    }
}
