using Model;
using PrviProgram.Repository;
using System.Collections.Generic;

namespace Service.LekarService
{
    public class PreglediService
    {

        private static PreglediService instance = null;
        public static PreglediService getInstance()
        {
            if (instance == null)
            {
                instance = new PreglediService();
            }
            return instance;
        }
        public Termin PregledPregleda(string sifraTermina)
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            foreach (Termin s in termini)
            {
                if (s.SifraTermina.Equals(sifraTermina))
                {
                    return s;
                }
            }
            return null;
        }

        public List<Termin> PregledSvihPregleda()
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            return termini;
        }

        public List<Termin> PregledSvihPregledaLekara(Lekar lekar)
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = new List<Termin>();

            foreach(Termin t in datoteka.CitanjeIzFajla())
            {
                if(t.lekar.Jmbg == lekar.Jmbg)
                {
                    termini.Add(t);
                }
            }

            return termini;
        }

        public void BrisanjePregleda(string sifraTermina)
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            foreach (Termin s in termini)
            {
                if (s.SifraTermina.Equals(sifraTermina))
                {
                    termini.Remove(s);
                    datoteka.UpisivanjeUFajl(termini);
                    break;
                }
            }

        }

        public void IzmenaPregleda(Termin termin)
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            foreach (Termin s in termini)
            {
                if (s.SifraTermina.Equals(termin.SifraTermina))
                {
                    termini.Remove(s);
                    termini.Add(termin);
                    datoteka.UpisivanjeUFajl(termini);
                    break;
                }
            }

        }

        public void DodavanjePregleda(Termin termin)
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            foreach (Termin s in termini)
            {
                if (s.SifraTermina.Equals(termin.SifraTermina))
                {

                }
            }
            termini.Add(termin);
            datoteka.UpisivanjeUFajl(termini);


        }


        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            List<Termin> list = new List<Termin>();

            foreach (Termin tt in termini)
            {
                if (tt.sala.Sifra.Equals(staraSala.Sifra))
                {
                    termini.Remove(tt);
                    tt.sala = novaSala;
                    termini.Add(tt);
                    datoteka.UpisivanjeUFajl(termini);
                    return true;
                }
            }
            return false;

        }

    }
}