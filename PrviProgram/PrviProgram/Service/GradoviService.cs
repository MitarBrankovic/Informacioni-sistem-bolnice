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
            if (gradoviRepository.PregledGrada(grad.Ime) != null)
            {
                return false;
            }
            List<Grad> gradovi = gradoviRepository.PregledSvihGradova();
            gradovi.Add(grad);
            gradoviRepository.UpisivanjeUFajl(gradovi);
            return true;
        }
        
    }
}
