using System;
using System.Collections.Generic;
using Model;

namespace Repository
{
    public class AlergeniRepository
    {
        private string path;

        public AlergeniRepository()
        {
            path = @"..\..\..\data\alergeni.json";
        }

        public void UpisivanjeUFajl(List<Alergen> alergeni)
        {
            // TODO: implement
        }

        public List<Alergen> CitanjeIzFajla()
        {
            // TODO: implement
            return null;
        }

        public Alergen PregledAlergena(String sifra)
        {
            // TODO: implement
            return null;
        }

        public List<Alergen> PregledSvihAlergena()
        {
            // TODO: implement
            return null;
        }

    }
}