/***********************************************************************
 * Module:  UpravljanjeSalama.cs
 * Author:  Saska
 * Purpose: Definition of the Class Logika.LogikaLekar.UpravljanjeSalama
 ***********************************************************************/

using Model;
using PrviProgram.Repository;
using System.Collections.Generic;

namespace Service.UpravnikService
{
    public class SaleService
    {
        private static SaleService instance = null;
        public static SaleService getInstance()
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

        public Sala PregledSale(string sifraSale)
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> pacijenti = datoteka.CitanjeIzFajla();
            foreach (Sala s in pacijenti)
            {
                if (s.Sifra.Equals(sifraSale))
                {
                    return s;
                }
            }
            return null;
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

        public List<Sala> PregledSvihSala()
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            return sale;
        }
    }
}