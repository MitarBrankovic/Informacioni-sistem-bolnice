using System;
using System.Collections.Generic;
using Model;

namespace Repository
{
    public class VestiRepository
    {
        private string path;

        VestiRepository()
        {
            path = @"..\..\..\data\vesti.json";
        }

        public void UpisivanjeUFajl(List<Vest> vesti)
        {
            // TODO: implement
        }

        public List<Vest> CitanjeIzFajla()
        {
            // TODO: implement
            return null;
        }

        public Vest PregledVesti(String sifra)
        {
            // TODO: implement
            return null;
        }

        public List<Vest> PregledSvihVesti()
        {
            // TODO: implement
            return null;
        }


    }
}