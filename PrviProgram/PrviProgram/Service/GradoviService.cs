using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
    class GradoviService
    {
        private GradoviRepository gradoviRepository = new GradoviRepository();

        public bool DodavanjeGrada(Grad grad)
        {
            List<Grad> gradovi = gradoviRepository.PregledSvihGradova();
            foreach (Grad g in gradovi)
            {
                if (g.Ime.Equals(grad.Ime))
                {
                    return false;
                }
            }
            gradovi.Add(grad);
            gradoviRepository.UpisivanjeUFajl(gradovi);
            return true;
        }
        
    }
}
