/***********************************************************************
 * Module:  UpravljanjeSalama.cs
 * Author:  Saska
 * Purpose: Definition of the Class Logika.LogikaLekar.UpravljanjeSalama
 ***********************************************************************/

using Model;
using RadSaDatotekama;
using System.Collections.Generic;

namespace Logika.LogikaSekretar
{
    public class UpravljanjePacijentima
    {
        public bool DodavanjePacijenta(Pacijent pacijent)
        {
            DatotekaPacijent datoteka = new DatotekaPacijent();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(pacijent.Jmbg))
                {
                    return false;
                }
            }
            pacijenti.Add(pacijent);
            datoteka.UpisivanjeUFajl(pacijenti);
            return true;
        }

        public Pacijent PregledPacijenta(string jmbg)
        {
            DatotekaPacijent datoteka = new DatotekaPacijent();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(jmbg))
                {
                    return p;
                }
            }
            return null;
        }

        public bool BrisanjePacijenta(Pacijent pacijent)
        {
            DatotekaPacijent datoteka = new DatotekaPacijent();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(pacijent.Jmbg))
                {
                    pacijenti.Remove(p);
                    datoteka.UpisivanjeUFajl(pacijenti);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaPacijenta(Pacijent pacijent)
        {
            DatotekaPacijent datoteka = new DatotekaPacijent();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(pacijent.Jmbg))
                {
                    pacijenti.Remove(p);
                    pacijenti.Add(pacijent);
                    datoteka.UpisivanjeUFajl(pacijenti);
                    return true;
                }
            }
            return false;
        }

        public List<Pacijent> PregledSvihPacijenata()
        {
            DatotekaPacijent datoteka = new DatotekaPacijent();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            return pacijenti;
        }

    }
}