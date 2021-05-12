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

        public bool RenoviranjeSale(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja)
        {
            List<TerminRenoviranjaSale> terminiRenoviranja = terminiRenoviranjaRepository.CitanjeIzFajla();
            if (FormiranjeTerminaRenoviranjaNakonProvere(sala, pocetakRenoviranja, krajRenoviranja) == false || ProveraSaleTerminaRenoviranja(sala, terminiRenoviranja) == false) 
            {
                return false;
            }
            terminiRenoviranja.Add(terminRenoviranjaSale);
            terminiRenoviranjaRepository.UpisivanjeUFajl(terminiRenoviranja);
            return true;
        }

        public bool FormiranjeTerminaRenoviranjaNakonProvere(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja)
        {
            if (ProveraDatumaTerminaRenoviranja(sala, pocetakRenoviranja, krajRenoviranja) == true)
            {
                terminRenoviranjaSale.sala = sala;
                terminRenoviranjaSale.PocetakRenoviranja = pocetakRenoviranja;
                terminRenoviranjaSale.KrajRenoviranja = krajRenoviranja;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ProveraDatumaTerminaRenoviranja(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            var intervalRenoviranja = FormiranjeIntervala(pocetakRenoviranja, krajRenoviranja);
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
            var intervalRenoviranja = new List<DateTime>();
            for (var dt = pocetakRenoviranja; dt <= krajRenoviranja; dt = dt.AddDays(1))
            {
                intervalRenoviranja.Add(dt);
            }
            return intervalRenoviranja;
        }
    }
}