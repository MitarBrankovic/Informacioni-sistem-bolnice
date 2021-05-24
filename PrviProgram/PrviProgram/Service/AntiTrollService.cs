using Model;
using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AntiTrollService
    {
        public AntiTrollRepository datotekaAnitTrollMehanizma = new AntiTrollRepository();
        private static AntiTrollService instance = null;
    
        public static AntiTrollService getInstance()
        {
            if (instance == null)
            {
                instance = new AntiTrollService();
            }
            return instance;
        }


        public void PovecavanjeBrojacaPriDodavanjuTermina(Pacijent pacijent)
        {
            List<AntiTrollPacijenta> antiTrollPacijenata = datotekaAnitTrollMehanizma.CitanjeIzFajla();
            if(antiTrollPacijenata.Count==0)
            {
                AntiTrollPacijenta antiTrollPacijenta = new AntiTrollPacijenta(1, 0, 0, pacijent, DateTime.Now, false);
                antiTrollPacijenata.Add(antiTrollPacijenta);
                datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
            }
            else
            {
                PovecavanjeBrojacaDodavanjuTermina(antiTrollPacijenata, pacijent);
            }
           
        }

        public void PovecavanjeBrojacaDodavanjuTermina(List<AntiTrollPacijenta> antiTrollPacijenata,Pacijent pacijent)
        {
            foreach (AntiTrollPacijenta antiTrollPacijenta in antiTrollPacijenata)
            {
                if (antiTrollPacijenta.pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    antiTrollPacijenata.Remove(antiTrollPacijenta);
                    antiTrollPacijenata.Add(new AntiTrollPacijenta(antiTrollPacijenta.brojacDodavanihTermina + 1, antiTrollPacijenta.brojacIzmenenjenihTermina, antiTrollPacijenta.brojacOtkazanihTermina, antiTrollPacijenta.pacijent, DateTime.Now, false));
                    datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
                    break;
                }
            }
        }

        public void PovecavanjeBrojacaPriIzmeniTermina(Pacijent pacijent)
        {
            List<AntiTrollPacijenta> antiTrollPacijenata = datotekaAnitTrollMehanizma.CitanjeIzFajla();
            if (antiTrollPacijenata.Count == 0)
            {
                AntiTrollPacijenta antiTrollPacijenta = new AntiTrollPacijenta(0, 1, 0, pacijent, DateTime.Now, false);
                antiTrollPacijenata.Add(antiTrollPacijenta);
                datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
            }
            else
            {
                PovecavanjeBrojacaIzmeniTermina(antiTrollPacijenata, pacijent);

            }

        }
        public void PovecavanjeBrojacaIzmeniTermina(List<AntiTrollPacijenta> antiTrollPacijenata, Pacijent pacijent)
        {
            foreach (AntiTrollPacijenta antiTrollPacijenta in antiTrollPacijenata)
            {
                if (antiTrollPacijenta.pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    antiTrollPacijenata.Remove(antiTrollPacijenta);
                    AntiTrollPacijenta antiTroll = new AntiTrollPacijenta(antiTrollPacijenta.brojacDodavanihTermina, antiTrollPacijenta.brojacIzmenenjenihTermina+1, antiTrollPacijenta.brojacOtkazanihTermina, antiTrollPacijenta.pacijent, DateTime.Now, false);
                    antiTrollPacijenata.Add(antiTroll);
                    datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
                    break;
                }
            }
        }

        public void PovecavanjeBrojacaPriOtkazivanjuTermina(Pacijent pacijent)
        {
            List<AntiTrollPacijenta> antiTrollPacijenata = datotekaAnitTrollMehanizma.CitanjeIzFajla();
            if (antiTrollPacijenata.Count == 0)
            {
                AntiTrollPacijenta antiTrollPacijenta = new AntiTrollPacijenta(0, 0, 1, pacijent, DateTime.Now, false);
                antiTrollPacijenata.Add(antiTrollPacijenta);
                datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
            }
            else
            {
                PovecavanjeBrojacaOtkazivanjeTermina(antiTrollPacijenata, pacijent);

            }

        }
        public void PovecavanjeBrojacaOtkazivanjeTermina(List<AntiTrollPacijenta> antiTrollPacijenata, Pacijent pacijent)
        {
            foreach (AntiTrollPacijenta antiTrollPacijenta in antiTrollPacijenata)
            {
                if (antiTrollPacijenta.pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    antiTrollPacijenata.Remove(antiTrollPacijenta);
                    AntiTrollPacijenta antiTroll = new AntiTrollPacijenta(antiTrollPacijenta.brojacDodavanihTermina, antiTrollPacijenta.brojacIzmenenjenihTermina, antiTrollPacijenta.brojacOtkazanihTermina +1, antiTrollPacijenta.pacijent, DateTime.Now, false);
                    antiTrollPacijenata.Add(antiTroll);
                    datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
                    break;
                }
            }
        }


    }
}
