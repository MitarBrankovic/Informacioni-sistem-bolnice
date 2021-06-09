using Model;
using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class TerminiLekarService
    {
        private TerminiRepository terminiRepository = new TerminiRepository();
        public int[] nizSlobodnihIZauzetihTermina = new int[24] { 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<string> ZauzetiTerminiLekara(Lekar lekar, DateTime datumTermina)
        {
            List<string> zauzetiTermini = new List<string>();
            List<Termin> termini = terminiRepository.PregledSvihTermina();
            foreach (Termin termin in termini)
            {
                if (termin.lekar.Jmbg.Equals(lekar.Jmbg) && termin.Datum.Date.Equals(datumTermina.Date))
                {
                    zauzetiTermini.Add(termin.Vreme);
                }
            }
            return zauzetiTermini;
        }
        public int[] ProveraZauzetostiLekara(string jmbg, DateTime selektovaniDatum, string[] nizSlobodnihTermina)

        {
            List<Termin> termini = terminiRepository.PregledSvihTermina();
            foreach (Termin termin in termini)
            {
                if (jmbg.Equals(termin.lekar.Jmbg) && selektovaniDatum.Equals(termin.Datum))
                {
                    PopunjavanjeNizaZauzetihTermina(nizSlobodnihTermina, termin);
                }
            }
            return this.nizSlobodnihIZauzetihTermina;
        }
        public void PopunjavanjeNizaZauzetihTermina(string[] nizSlobodnihTermina, Termin termin)
        {
            for (int i = 0; i < nizSlobodnihTermina.Length; i++)
            {
                if (termin.Vreme.Equals(nizSlobodnihTermina[i]))
                {
                    this.nizSlobodnihIZauzetihTermina[i] = 1;
                }
            }
        }
        public bool ProveraZauzetostiKodLekara(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar)
        {
            List<Termin> termini = terminiRepository.PregledSvihTermina();
            foreach (Termin termin in termini)
            {
                if (termin.Datum >= pocetakDatum && termin.Datum <= krajDatum && termin.lekar.Jmbg.Equals(selektovaniLekar.Jmbg))
                {
                    return false;
                }
            }
            return true;
        }
        public Lekar LekarKojiJeZaduzenZaTermin(Termin selektovanitermin)
        {
            Lekar lekar = new Lekar();
            List<Termin> termini = terminiRepository.PregledSvihTermina();
            foreach (Termin termin in termini)
            {
                if (termin.SifraTermina.Equals(selektovanitermin.SifraTermina))
                {
                    lekar = termin.lekar;
                    return lekar;
                }
            }
            return null;
        }
    }
}
