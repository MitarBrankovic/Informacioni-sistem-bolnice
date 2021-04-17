using Model;
using PrviProgram.Repository;
using System.Collections.Generic;

namespace PrviProgram.Service.LogikaGeneralno
{
    class GradoviService
    {
        public bool DodavanjeGrada(Grad grad)
        {
            GradoviRepository datoteka = new GradoviRepository();
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
            GradoviRepository datoteka = new GradoviRepository();
            List<Grad> gradovi = datoteka.CitanjeIzFajla();
            return gradovi;
        }

    }
}
