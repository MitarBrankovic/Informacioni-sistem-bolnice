using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class SaleService
    {
        private static SaleService instance = null;
        public static SaleService GetInstance()
        {
            if (instance == null)
            {
                instance = new SaleService();
            }
            return instance;
        }

        public bool DodavanjeSale(Sala sala)
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sala.Sifra))
                {
                    return false;
                }
            }
            sale.Add(sala);
            datoteka.UpisivanjeUFajl(sale);

            return true;
        }

        public bool BrisanjeSale(Sala sala)
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sala.Sifra))
                {
                    sale.Remove(s);
                    datoteka.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(staraSala.Sifra))
                {
                    sale.Remove(s);
                    sale.Add(novaSala);
                    datoteka.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }

        public bool RenoviranjeSale(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja)
        {
            TerminiRenoviranjaRepository datoteka = new TerminiRenoviranjaRepository();
            List<TerminRenoviranjaSale> terminiRenoviranja = datoteka.CitanjeIzFajla();

            TerminRenoviranjaSale termin = new TerminRenoviranjaSale();

            if (ProveraTerminaRenoviranja(sala, pocetakRenoviranja, krajRenoviranja) == true)
            {
                termin.sala = sala;
                termin.PocetakRenoviranja = pocetakRenoviranja;
                termin.KrajRenoviranja = krajRenoviranja;
            }
            else { return false; }


            foreach (TerminRenoviranjaSale t in terminiRenoviranja) {
                if (t.sala.Sifra.Equals(sala.Sifra))
                {
                    return false;
                }
            }
            terminiRenoviranja.Add(termin);
            datoteka.UpisivanjeUFajl(terminiRenoviranja);
            return true;

        }

        public bool ProveraTerminaRenoviranja(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja) {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();


            var intervalRenoviranja = new List<DateTime>();

            for (var dt = pocetakRenoviranja; dt <= krajRenoviranja; dt = dt.AddDays(1))
            {
                intervalRenoviranja.Add(dt);
            }

            foreach (Termin t in termini)
            {
                if (t.sala.Sifra.Equals(sala.Sifra))
                {
                    if (intervalRenoviranja.Contains(t.Datum))
                    {
                        return false;
                    }
                }

            }
            return true;

        }

    }
}