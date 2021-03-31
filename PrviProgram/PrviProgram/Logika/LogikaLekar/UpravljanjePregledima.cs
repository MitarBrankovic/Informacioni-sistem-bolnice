using Model;
using PrviProgram.RadSaDatotekama;
using RadSaDatotekama;
using System;
using System.Collections.Generic;

namespace Logika.LogikaLekar
{
    public class UpravljanjePregledima
    {

        private static UpravljanjePregledima instance = null;
        public static UpravljanjePregledima getInstance()
        {
            if (instance == null)
            {
                instance = new UpravljanjePregledima();
            }
            return instance;
        }
        public Termin PregledPregleda(string sifraTermina)
        {
            DatotekaTermini datoteka = new DatotekaTermini();
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
            DatotekaTermini datoteka = new DatotekaTermini();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            return termini;
        }

        public void BrisanjePregleda(string sifraTermina)
        {
            DatotekaTermini datoteka = new DatotekaTermini();
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
            DatotekaTermini datoteka = new DatotekaTermini();
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
            DatotekaTermini datoteka = new DatotekaTermini();
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
            DatotekaTermini datoteka = new DatotekaTermini();
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