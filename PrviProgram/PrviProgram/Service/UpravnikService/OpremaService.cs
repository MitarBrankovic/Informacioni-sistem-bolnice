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

namespace Service.UpravnikService
{
   public class OpremaService
   {
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
      
      public bool PremestanjeOpreme(Oprema oprema, Sala staraSala, Sala novaSala)
      {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(staraSala.Sifra))
                {
                    foreach (Oprema o in s.oprema)
                    {
                        if (o.Naziv.Equals(oprema.Naziv))
                        {
                            s.oprema.Remove(o);
                            novaSala.oprema.Add(o);
                            datoteka.UpisivanjeUFajl(sale);
                            return true;
                        }
                    }
                }
            }
            return false;
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
   
      public Repository.OpremaRepository opremaRepository;
   
   }
}