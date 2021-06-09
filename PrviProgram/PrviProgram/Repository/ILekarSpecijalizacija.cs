using Model;
using System.Collections.Generic;

namespace Repository
{
    public interface ILekarSpecijalizacija
    {
        List<Lekar> PregledLekaraOdredjeneSpecijalizacije(Specijalizacija specijalizacija);
        bool ProveriSpecijalizacijuLekara(Lekar zaLekara, Specijalizacija specijalizacija);
    }
}