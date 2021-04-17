using Model;
using PrviProgram.Repository;
using System.Collections.Generic;

namespace PrviProgram.Service.LogikaGeneralno
{
    class DrzaveService
    {
        public bool DodavanjeDrzave(Drzava drzava)
        {
            DrzaveRepository datoteka = new DrzaveRepository();
            List<Drzava> drzave = datoteka.CitanjeIzFajla();
            foreach (Drzava d in drzave)
            {
                if (d.Ime.Equals(drzava.Ime))
                {
                    return false;
                }
            }
            drzave.Add(drzava);
            datoteka.UpisivanjeUFajl(drzave);
            return false;
        }

        public List<Drzava> PregledSvihDrzava()
        {
            DrzaveRepository datoteka = new DrzaveRepository();
            List<Drzava> drzave = datoteka.CitanjeIzFajla();
            return drzave;
        }

    }
}
