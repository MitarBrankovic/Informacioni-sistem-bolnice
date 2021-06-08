using Model;
using Repository;

namespace PrviProgram.Repository
{
    public interface IPrimedbeNaLekRepository : IGenericFileRepository<PrimedbaNaLek>
    {
        PrimedbaNaLek PregledPrimedbe(string sifra);
    }
}