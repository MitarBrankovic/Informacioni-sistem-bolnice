using Model;
using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Service
{
    class BolnickoLecenjeService
    {
        private BolnickoLecenjeRepository bolnickoLecenjeRepository = new BolnickoLecenjeRepository();

        public void DodavanjeBolnickogLecenja(BolnickoLecenje bolnickoLecenje)
        {
            List<BolnickoLecenje> bolnickoLecenjeList = bolnickoLecenjeRepository.PregledSvihBolnickihLecenja();
            bolnickoLecenjeList.Add(bolnickoLecenje);
            bolnickoLecenjeRepository.UpisivanjeUFajl(bolnickoLecenjeList);
        }

        public void IzmenaBolnickogLecenja(BolnickoLecenje izmenjenoBolnickoLecenje)
        {
            List<BolnickoLecenje> bolnickoLecenjeList = bolnickoLecenjeRepository.PregledSvihBolnickihLecenja();
            foreach(BolnickoLecenje bolnickoLecenje in bolnickoLecenjeList)
            {
                if(bolnickoLecenje.Sifra == izmenjenoBolnickoLecenje.Sifra)
                {
                    bolnickoLecenjeList.Remove(bolnickoLecenje);
                    bolnickoLecenjeList.Add(izmenjenoBolnickoLecenje);
                    bolnickoLecenjeRepository.UpisivanjeUFajl(bolnickoLecenjeList);
                    break;
                }
            }
        }

        public BolnickoLecenje ProveraDaLiJePacijentTrenutnoNaBolnickomLecenju(Pacijent pacijent)
        {
            foreach (BolnickoLecenje bolnickoLecenje in bolnickoLecenjeRepository.PregledSvihBolnickihLecenjaPacijenta(pacijent.Jmbg))
            {
                if (bolnickoLecenje.DatumPocetka < DateTime.Now && bolnickoLecenje.DatumZavrsetka > DateTime.Now)
                {
                    return bolnickoLecenje;
                }
            }
            return null;
        }

        public bool ProveraDaLiPacijentImaZakazanoBolnickoLecenje(Pacijent pacijent)
        {
            foreach (BolnickoLecenje bolnickoLecenje in bolnickoLecenjeRepository.PregledSvihBolnickihLecenjaPacijenta(pacijent.Jmbg))
            {
                if (bolnickoLecenje.DatumPocetka > DateTime.Now)
                {
                    return true;
                }
                else if (bolnickoLecenje.DatumPocetka < DateTime.Now && bolnickoLecenje.DatumZavrsetka > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
