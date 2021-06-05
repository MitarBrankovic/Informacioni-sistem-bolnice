using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class TerapijaService
    {
        private TerapijaRepository terapijaRepository = new TerapijaRepository();

        public void DodavanjeTerapije(Terapija terapija)
        {

            List<Terapija> terapije = terapijaRepository.CitanjeIzFajla();
            terapije.Add(terapija);
            terapijaRepository.UpisivanjeUFajl(terapije);
       
        }

    }
}