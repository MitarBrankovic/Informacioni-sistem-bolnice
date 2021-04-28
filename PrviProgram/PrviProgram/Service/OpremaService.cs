/***********************************************************************
 * Module:  OpremaService.cs
 * Author:  Nesa
 * Purpose: Definition of the Class Service.UpravnikService.OpremaService
 ***********************************************************************/

using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class OpremaService
    {
        public SalaRepository salaRepository = new SalaRepository();
        private static OpremaService instance = null;
        public static OpremaService getInstance()
        {
            if (instance == null)
            {
                instance = new OpremaService();
            }
            return instance;
        }



        public bool DodavanjeOpreme(Oprema oprema, Sala sala)
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sala.Sifra))
                {
                    foreach (Oprema o in s.oprema)
                    {
                        if (o.Naziv.Equals(oprema.Naziv))
                        {
                            return false;
                        }
                    }
                    s.oprema.Add(oprema);
                    break;
                }
            }
            datoteka.UpisivanjeUFajl(sale);

            return true;
        }

        public bool BrisanjeOpreme(Oprema oprema, Sala sala)
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sala.Sifra))
                {
                    foreach (Oprema o in s.oprema)
                    {
                        if (o.Naziv.Equals(oprema.Naziv))
                        {
                            s.oprema.Remove(o);
                            datoteka.UpisivanjeUFajl(sale);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IzmenaOpreme(Oprema staraOprema, Oprema novaOprema, Sala sala)
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sala.Sifra))
                {
                    foreach (Oprema o in s.oprema)
                    {
                        if (o.Naziv.Equals(staraOprema.Naziv))
                        {
                            s.oprema.Remove(o);
                            s.oprema.Add(novaOprema);
                            datoteka.UpisivanjeUFajl(sale);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        

        public bool PremestanjeOpremeIzStareSaleUNovuSalu(Oprema oprema, Sala staraSala, Sala novaSala)
        {
            List<Sala> sale = salaRepository.PregledSvihSala();



            return true;
        }
        public Sala PremestanjeOpremeIzStareSale(Sala staraSala, Oprema opremaKojaPremesta)
        {
            foreach (Oprema o in staraSala.oprema)
            {
                if (o.Naziv.Equals(opremaKojaPremesta.Naziv))
                {
                    staraSala.oprema.Remove(o);
                    Oprema op = new Oprema(o.Naziv, o.Tip,izracunavanjeKolicine(o, opremaKojaPremesta));
                    if (UslovPremestanjeOpremeIzStareSale(staraSala, op) == false)
                    {
                        return null;
                    }
                }
            }
            return staraSala;
        }
        public int izracunavanjeKolicine(Oprema staraOprema, Oprema opremaKojaPremesta)
        {
            return staraOprema.Kolicina - opremaKojaPremesta.Kolicina;
        }

        public bool UslovPremestanjeOpremeIzStareSale(Sala staraSala,Oprema novaOprema)
        {
            if (novaOprema.Kolicina < 0)
            {
                return false;
            }
            else if (novaOprema.Kolicina > 0)
            {
                staraSala.oprema.Add(novaOprema);
            }
            return true;
        }

        public bool PremestanjeOpreme(Oprema oprema, Sala staraSala, Sala novaSala)
        {
           
    
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(staraSala.Sifra))
                {
                    foreach (Oprema o in s.oprema)
                    {
                        if (o.Naziv.Equals(oprema.Naziv))
                        {
                            s.oprema.Remove(o);
                            Oprema op = new Oprema();   // ako postoji vec ta oprema, da se izmeni kolicina
                            op.Naziv = o.Naziv;
                            op.Tip = o.Tip;
                            op.Kolicina = o.Kolicina - oprema.Kolicina;
                            if (op.Kolicina > 0)
                            {
                                s.oprema.Add(op);
                            }
                            else if (op.Kolicina == 0)
                            {
                            }
                            else
                            {
                                return false;
                            }

                            datoteka.UpisivanjeUFajl(sale);
                            break;
                        }
                    }
                }
            }
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(novaSala.Sifra))
                {
                    foreach (Oprema o in s.oprema)
                    {
                        if (o.Naziv.Equals(oprema.Naziv))
                        {
                            s.oprema.Remove(o);
                            Oprema op = new Oprema();   // ako postoji vec ta oprema, da se izmeni kolicina
                            op.Naziv = oprema.Naziv;
                            op.Tip = oprema.Tip;
                            op.Kolicina = o.Kolicina + oprema.Kolicina;

                            s.oprema.Add(op);
                            datoteka.UpisivanjeUFajl(sale);
                            return true;
                        }
                    }
                    s.oprema.Add(oprema);
                    datoteka.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }


        public bool dodavanjeTermina(Sala novaSala, Sala stara, Oprema oprema, DateTime datumTermina)
        {

            TerminiPremestajaRepository datoteka = new TerminiPremestajaRepository();
            List<TerminPremestanjaOpreme> termini = datoteka.CitanjeIzFajla();


            TerminPremestanjaOpreme termin = new TerminPremestanjaOpreme();
            termin.oprema = oprema;
            termin.sala = novaSala;
            termin.datumPremestaja = datumTermina;
            termin.staraSala = stara;

            termini.Add(termin);
            datoteka.UpisivanjeUFajl(termini);

            return true;
        }


        public bool KolicinaIsValid()
        {
            // TODO: implement
            return false;
        }

        /*public TipOpreme ProveraTipa()
        {
              return TipOpreme;
              //IZMENI OVO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }*/

        public void ZakazivanjePremestajaOpreme()
        {
            // TODO: implement
        }

        public OpremaRepository opremaRepository;

    }
}