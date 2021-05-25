using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AnketaService
    {
        private static AnketaService instance = null;
        public static AnketaService getInstance()
        {
            if (instance == null)
            {
                instance = new AnketaService();
            }
            return instance;
        }
        public List<Termin> CitanjeTermina()
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            return termini;
        }
        public List<Sala> CitanjeSala()
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            return sale;
        }

        public List<Lekar> CitanjeLekara()
        {
            LekarRepository datoteka = new LekarRepository();
            List<Lekar> lekari = datoteka.CitanjeIzFajla();
            return lekari;
        }
        public List<AnketiranjePacijenta> CitanjeAnketa()
        {
            AnketiranjePacijentaRepository datoteka = new AnketiranjePacijentaRepository();
            List<AnketiranjePacijenta> ankete = datoteka.CitanjeIzFajla();
            return ankete;
        }
        public bool DaLiPostojiBarJedanIzvrsenTermin(Pacijent pacijent)
        {
            List<Termin> termini = CitanjeTermina();
            foreach (Termin termin in termini)
            {
                if (termin.pacijent.Jmbg.Equals(pacijent.Jmbg) && termin.Izvrsen && DaLiJePregledVecAnketiran(termin))
                {
                    return true;
                }
            }
            return false;
        }
        public List<Termin> SviTerminiKojiSuIzvrseni(Pacijent pacijent)
        {
            List<Termin> termini = CitanjeTermina();
            List<Termin> izvrseniTermini = new List<Termin>();

            foreach (Termin termin in termini)
            {
                if (termin.pacijent.Jmbg.Equals(pacijent.Jmbg) && termin.Izvrsen && DaLiJePregledVecAnketiran(termin))
                {
                    izvrseniTermini.Add(termin);
                }
            }
            return izvrseniTermini;
        }
        public bool DaLiJePregledVecAnketiran(Termin termin)
        {
            List<AnketiranjePacijenta> ankete = CitanjeAnketa();
            foreach (AnketiranjePacijenta anketa in ankete)
            {
                if (termin.SifraTermina.Equals(anketa.termin.SifraTermina))
                {
                    return false;
                }
            }
            return true;

        }
    }
}
