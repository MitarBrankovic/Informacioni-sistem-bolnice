using Model;
using System;
using System.Collections.Generic;
using System.Text;
using Service;

namespace Controller
{
    public class AntiTrollController
    {
        public void PovecavanjeBrojacaPriDodavanjuTermina(Pacijent pacijent)
        {
            AntiTrollService.getInstance().PovecavanjeBrojacaPriDodavanjuTermina(pacijent);
        }
        public void PovecavanjeBrojacaPriIzmeniTermina(Pacijent pacijent)
        {
            AntiTrollService.getInstance().PovecavanjeBrojacaPriIzmeniTermina(pacijent);
        }
        public void PovecavanjeBrojacaPriOtkazivanjuTermina(Pacijent pacijent)
        {
            AntiTrollService.getInstance().PovecavanjeBrojacaPriOtkazivanjuTermina(pacijent);
        }
    }
}
