using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Service
{
    public class SekretarService
    {
        private SekretarRepository sekretarRepository = new SekretarRepository();

        public bool DodavanjeSekretara(Sekretar sekretar)
        {
            List<Sekretar> sekretari = sekretarRepository.CitanjeIzFajla();
            if (sekretarRepository.PregledSekretara(sekretar.Jmbg) != null) return false;
            sekretari.Add(sekretar);
            sekretarRepository.UpisivanjeUFajl(sekretari);
            return true;
        }

        public bool BrisanjeSekretara(Sekretar sekretar)
        {
            List<Sekretar> sekretari = sekretarRepository.CitanjeIzFajla();
            foreach (Sekretar s in sekretari)
            {
                if (s.Jmbg.Equals(sekretar.Jmbg))
                {
                    sekretari.Remove(s);
                    sekretarRepository.UpisivanjeUFajl(sekretari);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaSekretara(Sekretar stariSekretar, Sekretar noviSekretar)
        {
            List<Sekretar> sekretari = sekretarRepository.CitanjeIzFajla();
            foreach (Sekretar s in sekretari)
            {
                if (s.Jmbg.Equals(stariSekretar.Jmbg))
                {
                    sekretari.Remove(s);
                    sekretari.Add(noviSekretar);
                    sekretarRepository.UpisivanjeUFajl(sekretari);
                    return true;
                }
            }
            return false;
        }

    }
}
