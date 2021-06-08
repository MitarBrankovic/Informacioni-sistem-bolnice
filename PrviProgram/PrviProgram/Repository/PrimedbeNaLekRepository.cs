using Model;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrviProgram.Repository
{
    public class PrimedbeNaLekRepository : GenericFileRepository<PrimedbaNaLek>, IPrimedbeNaLekRepository
    {
        private string path = @"..\..\..\data\primedbeNaLek.json";
        public PrimedbeNaLekRepository(string path) : base(path) { }

        public PrimedbaNaLek PregledPrimedbe(string sifra)
        {
            List<PrimedbaNaLek> primedbe = CitanjeIzFajla();
            foreach (PrimedbaNaLek i in primedbe)
            {
                if (i.Sifra.Equals(sifra))
                {
                    return i;
                }
            }
            return null;
        }
    }
}
