using Model;
using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class BeleskaService
    {
        private static BeleskaService instance = null;
        BeleskaRepository beleskaRepository = new BeleskaRepository();
        
   
        public static BeleskaService getInstance()
        {
            if (instance == null)
            {
                instance = new BeleskaService();
            }
            return instance;
        }
        public void DodavanjeBeleske(Beleska beleska)
        {
            List<Beleska> beleske = beleskaRepository.CitanjeIzFajla();
            beleske.Add(beleska);
            beleskaRepository.UpisivanjeUFajl(beleske);
        }
    }
}
