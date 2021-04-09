using Model;
using RadSaDatotekama;
using System.Collections.Generic;

namespace Logika.LogikaGeneralna
{
    class UpravljanjeGradovima
    {
        public bool DodavanjeGrada(Grad grad)
        {
            DatotekaGradovi datoteka = new DatotekaGradovi();
            List<Grad> gradovi = datoteka.CitanjeIzFajla();
            foreach (Grad g in gradovi)
            {
                if (g.Ime.Equals(grad.Ime))
                {
                    return false;
                }
            }
            gradovi.Add(grad);
            datoteka.UpisivanjeUFajl(gradovi);
            return true;
        }

        public List<Grad> PregledSvihGradova()
        {
            DatotekaGradovi datoteka = new DatotekaGradovi();
            List<Grad> gradovi = datoteka.CitanjeIzFajla();
            return gradovi;
        }

    }
}
