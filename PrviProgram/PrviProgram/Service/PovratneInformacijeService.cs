using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class PovratneInformacijeService
    {
        private PovratneInformacijeRepository povratneInformacijeRepository = new PovratneInformacijeRepository();

        public bool DodavanjePovratneInformacije(PovratneInformacije povratnaInformacija)
        {
            List<PovratneInformacije> povratne = povratneInformacijeRepository.CitanjeIzFajla();
            if (povratneInformacijeRepository.PregledPovratneInformacije(povratnaInformacija.Osoba.Jmbg) != null) return false;
            povratne.Add(povratnaInformacija);
            povratneInformacijeRepository.UpisivanjeUFajl(povratne);
            return true;
        }

    }
}
