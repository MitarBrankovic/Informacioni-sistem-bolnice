using Model;
using PrviProgram.Model;
using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Service
{
    class PodesavanjaLekarService
    {
        private PodesavanjaLekarRepository podesavanjaLekarRepository = new PodesavanjaLekarRepository();
        public PodesavanjaLekarService()
        {

        }

        public void IzmenaTooltipPodesavanja(Lekar lekar)
        {
            PodesavanjaLekar podesavanjaLekar = podesavanjaLekarRepository.PregledPodesavanja(lekar.Jmbg);
            podesavanjaLekar.IskljucioToolTips = !(podesavanjaLekar.IskljucioToolTips);
            IzmeniPodesavanjaLekara(podesavanjaLekar);
        }

        public void IzmenaWizardPodesavanja(Lekar lekar)
        {
            PodesavanjaLekar podesavanjaLekar = podesavanjaLekarRepository.PregledPodesavanja(lekar.Jmbg);
            podesavanjaLekar.PogledaoWizard = !(podesavanjaLekar.PogledaoWizard);
            IzmeniPodesavanjaLekara(podesavanjaLekar);
        }

        public void IzmeniPodesavanjaLekara(PodesavanjaLekar podesavanjaLekar)
        {
            List<PodesavanjaLekar> podesavanjaLekarList = podesavanjaLekarRepository.CitanjeIzFajla();
            foreach (PodesavanjaLekar podesavanjaLekarIterator in podesavanjaLekarList)
            {
                if (podesavanjaLekarIterator.Lekar.Jmbg.Equals(podesavanjaLekar.Lekar.Jmbg))
                {
                    podesavanjaLekarList.Remove(podesavanjaLekarIterator);
                    podesavanjaLekarList.Add(podesavanjaLekar);
                    podesavanjaLekarRepository.UpisivanjeUFajl(podesavanjaLekarList);
                    break;
                }
            }
        }
    }
}
