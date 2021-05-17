using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class SaleService
    {
        private SalaRepository salaRepository = new SalaRepository();
        private TerminiRenoviranjaRepository terminiRenoviranjaRepository = new TerminiRenoviranjaRepository();
        private TerminiRepository terminiRepository = new TerminiRepository();
        private TerminRenoviranjaSale terminRenoviranjaSale = new TerminRenoviranjaSale();

        public bool DodavanjeSale(Sala sala)
        {
            List<Sala> sale = salaRepository.CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sala.Sifra))
                {
                    return false;
                }
            }
            sale.Add(sala);
            salaRepository.UpisivanjeUFajl(sale);
            return true;
        }

        public bool BrisanjeSale(Sala sala)
        {
            List<Sala> sale = salaRepository.CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sala.Sifra))
                {
                    sale.Remove(salaBrojac);
                    salaRepository.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {
            List<Sala> sale = salaRepository.CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(staraSala.Sifra))
                {
                    sale.Remove(salaBrojac);
                    sale.Add(novaSala);
                    salaRepository.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }

        public bool RenoviranjeSale(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja, Sala sala1, Sala sala2)
        {

            return false;
        }

        public bool FormiranjeTerminaRenoviranjaNakonProvere(TerminRenoviranjaSale terminRenoviranjaSale)
        {
            if (ProveraDatumaTerminaRenoviranja(terminRenoviranjaSale.sala, terminRenoviranjaSale.PocetakRenoviranja, terminRenoviranjaSale.KrajRenoviranja))
            {
                terminRenoviranjaSale.sala = terminRenoviranjaSale.sala;
                terminRenoviranjaSale.PocetakRenoviranja = terminRenoviranjaSale.PocetakRenoviranja;
                terminRenoviranjaSale.KrajRenoviranja = terminRenoviranjaSale.KrajRenoviranja;
                terminRenoviranjaSale.sala1 = terminRenoviranjaSale.sala1;
                terminRenoviranjaSale.sala2 = terminRenoviranjaSale.sala2;
                return true;
            }
            else return false;
        }

        public bool ProveraDatumaTerminaRenoviranja(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            List<DateTime> intervalRenoviranja = FormiranjeIntervala(pocetakRenoviranja, krajRenoviranja);
            foreach (Termin terminBrojac in termini)
            {
                if (terminBrojac.sala.Sifra.Equals(sala.Sifra))
                {
                    if (intervalRenoviranja.Contains(terminBrojac.Datum))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool ProveraSaleTerminaRenoviranja(Sala sala, List<TerminRenoviranjaSale> terminiRenoviranja)
        {
            foreach (TerminRenoviranjaSale terminBrojac in terminiRenoviranja)
            {
                if (terminBrojac.sala.Sifra.Equals(sala.Sifra))
                {
                    return false;
                }
            }
            return true;
        }
        public List<DateTime> FormiranjeIntervala(DateTime pocetakRenoviranja, DateTime krajRenoviranja)
        {
            List<DateTime> intervalRenoviranja = new List<DateTime>();
            for (DateTime datumBrojac = pocetakRenoviranja; datumBrojac <= krajRenoviranja; datumBrojac = datumBrojac.AddDays(1))
            {
                intervalRenoviranja.Add(datumBrojac);
            }
            return intervalRenoviranja;
        }

        public bool RenoviranjeSale(TerminRenoviranjaSale terminRenoviranjaSale)
        {
            List<TerminRenoviranjaSale> termini = terminiRenoviranjaRepository.CitanjeIzFajla();
            if (!FormiranjeTerminaRenoviranjaNakonProvere(terminRenoviranjaSale) || !ProveraSaleTerminaRenoviranja(terminRenoviranjaSale.sala, termini)
                || !ProveraSaleTerminaRenoviranja(terminRenoviranjaSale.sala1, termini) || !ProveraSaleTerminaRenoviranja(terminRenoviranjaSale.sala2, termini))
            {
                return false;
            }
            termini.Add(terminRenoviranjaSale);
            terminiRenoviranjaRepository.UpisivanjeUFajl(termini);
            return true;
        }
    }
}