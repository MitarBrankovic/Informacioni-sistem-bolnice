using Model;
using PrviProgram.Service;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class LekoviController
    {
        private LekoviService lekoviService = new LekoviService();
        private PrimedbeNaLekService primedbeNaLekService = new PrimedbeNaLekService();

        public bool DodavanjeLeka(Lek lek)
        {
            if (lekoviService.DodavanjeLeka(lek))
                return true;
            else
                return false;
        }

        public bool BrisanjeLeka(Lek lek)
        {
            if (lekoviService.BrisanjeLeka(lek))
            {
                lekoviService.BrisanjeAlternativnihLekova(lek);
                return true;
            }
            else
                return false;
        }

        public bool IzmenaLeka(Lek stariLek, Lek noviLek)
        {
            if (lekoviService.IzmenaLeka(stariLek, noviLek))
                return true;
            else
                return false;
        }

        public bool BrisanjePrimedbe(PrimedbaNaLek primedba)
        {
            if (primedbeNaLekService.BrisanjePrimedbe(primedba))
                return true;
            else
                return false;
        }
    }
}
