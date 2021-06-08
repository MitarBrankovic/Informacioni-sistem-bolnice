using Model;
using System.Collections.Generic;

namespace Repository
{
    public interface IGenericFileRepository<T> where T : Entity
    {
        List<T> CitanjeIzFajla();
        List<T> PregledSvih();
        void UpisivanjeUFajl(List<T> lista);
    }
}