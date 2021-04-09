using Model;
using PrviProgram.RadSaDatotekama;
using System.Collections.Generic;

namespace PrviProgram.Logika.LogikaGeneralna
{
    class UpravljanjeDrzavama
    {
        public bool DodavanjeDrzave(Drzava drzava)
        {
            DatotekaDrzave datoteka = new DatotekaDrzave();
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
            DatotekaDrzave datoteka = new DatotekaDrzave();
            List<Drzava> drzave = datoteka.CitanjeIzFajla();
            return drzave;
        }

    }
}
