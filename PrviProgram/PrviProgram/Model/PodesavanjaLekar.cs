using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Model
{
    public class PodesavanjaLekar
    {
        public Lekar Lekar { get; set; }
        public bool IskljucioToolTips { get; set; }
        public bool PogledaoWizard { get; set; }

        public PodesavanjaLekar(Lekar lekar)
        {
            Lekar = lekar;
            IskljucioToolTips = false;
            PogledaoWizard = false;
        }
    }
}
