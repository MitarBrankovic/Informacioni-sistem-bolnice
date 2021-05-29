using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Service
{
    public class RadnoVremeService
    {
        private RadnoVremeLekaraRepository radnoVremeLekaraRepository = new RadnoVremeLekaraRepository();

        public bool DodavanjeLeka(RadnoVremeLekara radnoVreme)
        {
            List<RadnoVremeLekara> radnaVremena = radnoVremeLekaraRepository.CitanjeIzFajla();
            if (radnoVremeLekaraRepository.PregledRadnogVremenaLekara(radnoVreme.Lekar.Ime) != null) return false;
            radnaVremena.Add(radnoVreme);
            radnoVremeLekaraRepository.UpisivanjeUFajl(radnaVremena);
            return true;
        }

        public bool BrisanjeLeka(RadnoVremeLekara radnoVreme)
        {
            List<RadnoVremeLekara> radnaVremena = radnoVremeLekaraRepository.CitanjeIzFajla();
            foreach (RadnoVremeLekara radnoVremeBrojac in radnaVremena)
            {
                if (radnoVremeBrojac.Lekar.Ime.Equals(radnoVreme.Lekar.Ime))
                {
                    radnaVremena.Remove(radnoVremeBrojac);
                    radnoVremeLekaraRepository.UpisivanjeUFajl(radnaVremena);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaLeka(RadnoVremeLekara stariRadnoVreme, RadnoVremeLekara novoRadnoVreme)
        {
            List<RadnoVremeLekara> radnaVremena = radnoVremeLekaraRepository.CitanjeIzFajla();
            foreach (RadnoVremeLekara radnoVremeBrojac in radnaVremena)
            {
                if (radnoVremeBrojac.Lekar.Ime.Equals(stariRadnoVreme.Lekar.Ime))
                {
                    radnaVremena.Remove(radnoVremeBrojac);
                    radnaVremena.Add(novoRadnoVreme);
                    radnoVremeLekaraRepository.UpisivanjeUFajl(radnaVremena);
                    return true;
                }
            }
            return false;
        }

    }
}
