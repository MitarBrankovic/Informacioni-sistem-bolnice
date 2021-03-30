/***********************************************************************
 * Module:  UpravljanjeSalama.cs
 * Author:  Saska
 * Purpose: Definition of the Class Logika.LogikaLekar.UpravljanjeSalama
 ***********************************************************************/

using Model;
using RadSaDatotekama;
using System.Collections.Generic;

namespace Logika.LogikaUpravnik
{
    public class UpravljanjeSalama
    {
        private static UpravljanjeSalama instance = null;
        public static UpravljanjeSalama getInstance()
        {
            if (instance == null)
            {
                instance = new UpravljanjeSalama();
            }
            return instance;
        }

        public bool DodavanjeSale(Sala sala)
        {
            DatotekaSala datoteka = new DatotekaSala();
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
            DatotekaSala datoteka = new DatotekaSala();
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
            DatotekaSala datoteka = new DatotekaSala();
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

        public bool IzmenaSale(Sala sala)
        {
            DatotekaSala datoteka = new DatotekaSala();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sala.Sifra))
                {
                    sale.Remove(s);
                    sale.Add(sala);
                    datoteka.UpisivanjeUFajl(sale);
                    return true;
                }
            }
            return false;
        }

        public List<Sala> PregledSvihSala()
        {
            DatotekaSala datoteka = new DatotekaSala();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            return sale;
        }
    }
}