using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public static class Factory
    {

        public static IPrimedbeNaLekRepository KreirajPrimedeRep()
        {
            return new PrimedbeNaLekRepository(@"..\..\..\data\primedbeNaLek.json");
        }

        public static ITerminiPremestajaRepository KreirajTerminPremestajaRep()
        {
            return new TerminiPremestajaRepository(@"..\..\..\data\terminiPremestaja.json");
        }

    }
}
