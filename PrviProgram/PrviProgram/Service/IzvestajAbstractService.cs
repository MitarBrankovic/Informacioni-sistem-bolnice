using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public abstract class IzvestajAbstractService
    {
        public abstract void FormiranjePDF(string sifra);
        public abstract void PosaljiPoruku();

        public void IzgenerisiIzvestaj(string sifra) 
        {
            FormiranjePDF(sifra);
            PosaljiPoruku();
        }

    }
}
