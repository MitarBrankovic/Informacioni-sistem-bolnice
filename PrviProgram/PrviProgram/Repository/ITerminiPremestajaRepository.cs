using Model;
using Repository;
using System;

namespace PrviProgram.Repository
{
    public interface ITerminiPremestajaRepository : IGenericFileRepository<TerminPremestanjaOpreme>
    {
        TerminPremestanjaOpreme PregledPrimedbe(DateTime datum);
    }
}