using Model;
using System.Collections.Generic;

namespace Repository
{
    public interface ILekarRepository
    {
        List<Lekar> CitanjeIzFajla();
        Lekar PregledLekara(string jmbg);
        void UpisivanjeUFajl(List<Lekar> lekari);
        List<Korisnik> PregledSvihKorisnika();
        List<Lekar> PregledSvihLekara();
        //bool ProveriSpecijalizacijuLekara(Lekar zaLekara, Specijalizacija specijalizacija);
        //List<Lekar> PregledLekaraOdredjeneSpecijalizacije(Specijalizacija specijalizacija);
    }
}