using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AnketaService
    {
        private static AnketaService instance = null;
        AnketiranjePacijentaRepository datoteka = new AnketiranjePacijentaRepository();

        public static AnketaService getInstance()
        {
            if (instance == null)
            {
                instance = new AnketaService();
            }
            return instance;
        }
        public bool DaLiJePregledVecAnketiran(Termin termin)
        {
            List<AnketiranjePacijenta> ankete = datoteka.CitanjeIzFajla();
            foreach (AnketiranjePacijenta anketa in ankete)
            {
                if (termin.SifraTermina.Equals(anketa.termin.SifraTermina))
                {
                    return false;
                }
            }
            return true;

        }
    }
}
