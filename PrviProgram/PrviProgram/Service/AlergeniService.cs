using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
    public class AlergeniService
    {
        private AlergeniRepository alergeniRepository = new AlergeniRepository();

        public bool DodavanjeAlergena(Alergen alergen)
        {
            List<Alergen> alergeni = alergeniRepository.PregledSvihAlergena();
            foreach (Alergen a in alergeni)
            {
                if (a.Naziv.Equals(alergen.Naziv))
                {
                    return false;
                }
            }
            alergeni.Add(alergen);
            alergeniRepository.UpisivanjeUFajl(alergeni);
            return true;
        }

        public bool BrisanjeAlergena(Alergen alergen)
        {
            List<Alergen> alergeni = alergeniRepository.PregledSvihAlergena();
            foreach (Alergen a in alergeni)
            {
                if (a.Naziv.Equals(alergen.Naziv))
                {
                    alergeni.Remove(a);
                    alergeniRepository.UpisivanjeUFajl(alergeni);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaAlergena(Alergen stariAlergen, Alergen noviAlergen)
        {
            List<Alergen> alergeni = alergeniRepository.PregledSvihAlergena();
            foreach (Alergen a in alergeni)
            {
                if (a.Naziv.Equals(stariAlergen.Naziv))
                {
                    alergeni.Remove(stariAlergen);
                    alergeni.Add(noviAlergen);
                    alergeniRepository.UpisivanjeUFajl(alergeni);
                    return true;
                }
            }
            return false;
        }

    }
}