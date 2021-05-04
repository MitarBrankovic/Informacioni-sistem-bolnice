using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class OpremaService
    {
        private SalaRepository salaRepository = new SalaRepository();
        private TerminiPremestajaRepository terminiPremestajaRepository = new TerminiPremestajaRepository();
        private static OpremaService instance = null;
        public static OpremaService GetInstance()
        {
            if (instance == null)
            {
                instance = new OpremaService();
            }
            return instance;
        }

        public bool DodavanjeOpreme(Oprema oprema, Sala sala)
        {
            List<Sala> sale = salaRepository.CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sala.Sifra))
                {
                    if (ProveraPostojanjaOpreme(oprema, salaBrojac) == false) 
                    {
                        return false;
                    }
                    salaBrojac.oprema.Add(oprema);
                    break;
                }
            }
            salaRepository.UpisivanjeUFajl(sale);
            return true;
        }

        public bool ProveraPostojanjaOpreme(Oprema oprema, Sala salaBrojac)
        {
            foreach (Oprema opremaBrojac in salaBrojac.oprema)
            {
                if (opremaBrojac.Naziv.Equals(oprema.Naziv))
                {
                    return false;
                }
            }
            return true;
        }

        public bool BrisanjeOpreme(Oprema oprema, Sala sala)
        {
            List<Sala> sale = salaRepository.CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sala.Sifra))
                {
                    if (BrisanjeOpremeUSali(salaBrojac, oprema, sale) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool BrisanjeOpremeUSali(Sala salaBrojac, Oprema oprema, List<Sala> sale)
        {
            foreach (Oprema opremaBrojac in salaBrojac.oprema)
            {
                if (opremaBrojac.Naziv.Equals(oprema.Naziv))
                {
                    salaBrojac.oprema.Remove(opremaBrojac);
                    salaRepository.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaOpreme(Oprema staraOprema, Oprema novaOprema, Sala sala)
        {
            List<Sala> sale = salaRepository.CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sala.Sifra))
                {
                    if (IzmenaOpremeUSali(staraOprema, novaOprema, salaBrojac, sale) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IzmenaOpremeUSali(Oprema staraOprema, Oprema novaOprema, Sala salaBrojac, List<Sala> sale)
        {
            foreach (Oprema opremaBrojac in salaBrojac.oprema)
            {
                if (opremaBrojac.Naziv.Equals(staraOprema.Naziv))
                {
                    salaBrojac.oprema.Remove(opremaBrojac);
                    salaBrojac.oprema.Add(novaOprema);
                    salaRepository.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }

        public bool PremestanjeOpreme(Oprema oprema, Sala staraSala, Sala novaSala)
        {
            List<Sala> sale = salaRepository.CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(staraSala.Sifra))
                {
                    if (PremestanjeOpremeIzStareSale(salaBrojac, oprema, sale) == false)
                    {
                        return false;
                    }
                }
            }
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(novaSala.Sifra))
                {
                    if (PremestanjeOpremeUNovuSalu(salaBrojac, oprema, sale) == true)
                    {
                        return true;
                    }
                    salaBrojac.oprema.Add(oprema);
                    salaRepository.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }

        public bool PremestanjeOpremeIzStareSale(Sala salaBrojac, Oprema oprema, List<Sala> sale)
        {
            foreach (Oprema opremaBrojac in salaBrojac.oprema)
            {
                if (opremaBrojac.Naziv.Equals(oprema.Naziv))
                {
                    salaBrojac.oprema.Remove(opremaBrojac);
                    Oprema ostatakOpremeUStarojSali = new Oprema(opremaBrojac.Naziv, opremaBrojac.Kolicina - oprema.Kolicina, opremaBrojac.Tip, opremaBrojac.NazivSale);
                    if (ostatakOpremeUStarojSali.Kolicina > 0)
                    {
                        salaBrojac.oprema.Add(ostatakOpremeUStarojSali);
                    }
                    else if (ostatakOpremeUStarojSali.Kolicina == 0)
                    {}
                    else
                    {
                        return false;
                    }
                    salaRepository.UpisivanjeUFajl(sale);
                    break;
                }
            }
            return true;
        }

        public bool PremestanjeOpremeUNovuSalu(Sala salaBrojac, Oprema oprema, List<Sala> sale)
        {
            foreach (Oprema opremaBrojac in salaBrojac.oprema)
            {
                if (opremaBrojac.Naziv.Equals(oprema.Naziv))
                {
                    salaBrojac.oprema.Remove(opremaBrojac);
                    Oprema prebacenaOprema = new Oprema(oprema.Naziv, opremaBrojac.Kolicina + oprema.Kolicina, oprema.Tip, opremaBrojac.NazivSale);
                    salaBrojac.oprema.Add(prebacenaOprema);
                    salaRepository.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }

        public bool DodavanjeTermina(Sala novaSala, Sala stara, Oprema oprema, DateTime datumTermina)
        {
            List<TerminPremestanjaOpreme> termini = terminiPremestajaRepository.CitanjeIzFajla();
            TerminPremestanjaOpreme termin = new TerminPremestanjaOpreme(oprema, novaSala, stara, datumTermina);
            termini.Add(termin);
            terminiPremestajaRepository.UpisivanjeUFajl(termini);
            return true;
        }
    }
}