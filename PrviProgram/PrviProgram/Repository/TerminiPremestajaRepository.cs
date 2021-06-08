using Model;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace PrviProgram.Repository
{
    public class TerminiPremestajaRepository : GenericFileRepository<TerminPremestanjaOpreme>, ITerminiPremestajaRepository
    {
        private string path;

        public TerminiPremestajaRepository(string path) : base(path) { }

        public TerminPremestanjaOpreme PregledPrimedbe(DateTime datum)
        {
            List<TerminPremestanjaOpreme> termini = CitanjeIzFajla();
            foreach (TerminPremestanjaOpreme i in termini)
            {
                if (i.DatumPremestaja.Equals(datum))
                {
                    return i;
                }
            }
            return null;
        }





    }
}
