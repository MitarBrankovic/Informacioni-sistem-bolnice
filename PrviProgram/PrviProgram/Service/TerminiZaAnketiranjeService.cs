﻿using Model;
using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class TerminiZaAnketiranjeService
    {
        TerminiRepository terminRepository = new TerminiRepository();
        public bool DaLiPostojiBarJedanIzvrsenTermin(Pacijent pacijent)
        {
            List<Termin> termini = terminRepository.CitanjeIzFajla();
            foreach (Termin termin in termini)
            {
                if (termin.pacijent.Jmbg.Equals(pacijent.Jmbg) && termin.Izvrsen && AnketaService.getInstance().DaLiJePregledVecAnketiran(termin))
                {
                    return true;
                }
            }
            return false;
        }
        public List<Termin> SviTerminiKojiSuIzvrseni(Pacijent pacijent)
        {
            List<Termin> termini = CitanjeTermina();
            List<Termin> izvrseniTermini = new List<Termin>();

            foreach (Termin termin in termini)
            {
                if (termin.pacijent.Jmbg.Equals(pacijent.Jmbg) && termin.Izvrsen && AnketaService.getInstance().DaLiJePregledVecAnketiran(termin))
                {
                    izvrseniTermini.Add(termin);
                }
            }
            return izvrseniTermini;
        }
    }
}
