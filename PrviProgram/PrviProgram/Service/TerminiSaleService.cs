using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class TerminiSaleService
    {
        private TerminiRenoviranjaRepository terminiRenoviranjaRepository = new TerminiRenoviranjaRepository();
        private SalaRepository salaRepository = new SalaRepository();
        private TerminiRepository terminiRepository = new TerminiRepository();
      
        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {

            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            List<Termin> noviTermini = new List<Termin>();
            foreach (Termin termin in termini)
            {
                noviTermini.Add(termin);
                if (termin.sala.Sifra.Equals(staraSala.Sifra))
                {
                    noviTermini.Remove(termin);
                    termin.sala = novaSala;
                    noviTermini.Add(termin);
                    terminiRepository.UpisivanjeUFajl(noviTermini);

                    return true;
                }
            }
            return false;
        }
        public void BrisanjeSaleIzTermina(Sala sala)
        {
            List<Termin> termini = terminiRepository.PregledSvihTermina();
            foreach (Termin terminIterator in termini)
            {
                if (terminIterator != null)
                {
                    if (terminIterator.sala.Sifra == sala.Sifra)
                    {
                        terminIterator.sala = null;
                        TerminiService.getInstance().IzmenaTermina(terminIterator);
                    }
                }
            }
        }
        public Sala DobavljanjeSale(Termin noviTermin)
        {
            List<Termin> termini = terminiRepository.PregledSvihTermina();
            List<Sala> sale = salaRepository.PregledSvihSala();
            Sala novaSala = new Sala();
            foreach (Sala sala in sale)
            {
                if (!sala.Tip.Equals(TipSale.Magacin) && ProveraSale(sala, termini, noviTermin))
                {
                    novaSala = sala;
                    return novaSala;
                }
            }
            return null;
        }
        public bool ProveraSale(Sala sala, List<Termin> termini, Termin noviTermin)
        {
            if (!ProveraRenoviranjaSale(sala, noviTermin))
            {
                return false;
            }
            if (!ProveraZauzetostTerminaUSali(sala, termini, noviTermin))
            {
                return false;
            }
            return true;
        }
        public bool ProveraZauzetostTerminaUSali(Sala sala, List<Termin> termini, Termin noviTermin)
        {
            foreach (Termin termin in termini)
            {
                if ((termin.Datum.Equals(noviTermin.Datum) && termin.Vreme.Equals(noviTermin.Vreme) && termin.sala.Sifra.Equals(sala.Sifra))
                    || (sala.Tip == TipSale.Magacin || sala.Tip == TipSale.Kancelarija || sala.Tip == TipSale.SalaZaOdmor))
                {
                    return false;
                }
            }
            return true;
        }

        public bool ProveraRenoviranjaSale(Sala sala, Termin noviTermin)
        {
            foreach (TerminRenoviranjaSale terminRenoviranja in terminiRenoviranjaRepository.CitanjeIzFajla())
            {
                if (terminRenoviranja.Sala.Naziv.Equals(sala.Naziv) && IzracunajIntervalRenoviranja(terminRenoviranja).Contains(noviTermin.Datum))
                {
                    return false;
                }
            }
            return true;
        }

        public List<DateTime> IzracunajIntervalRenoviranja(TerminRenoviranjaSale terminRenoviranja)
        {
            List<DateTime> intervalRenoviranja = new List<DateTime>();
            for (DateTime datum = terminRenoviranja.PocetakRenoviranja; datum <= terminRenoviranja.KrajRenoviranja; datum = datum.AddDays(1))
            {
                intervalRenoviranja.Add(datum);
            }

            return intervalRenoviranja;
        }
    }
}
