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
                antiTrollPacijenata.Add(new AntiTrollPacijenta(1, 0, 0, pacijent, DateTime.Now, false));
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
                    antiTrollPacijenata.Add(new AntiTrollPacijenta(antiTrollPacijenta.BrojacDodavanihTermina + 1,
                        antiTrollPacijenta.BrojacIzmenenjenihTermina, 
                        antiTrollPacijenta.BrojacOtkazanihTermina, antiTrollPacijenta.pacijent,
                        DateTime.Now, false));
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
                antiTrollPacijenata.Add(new AntiTrollPacijenta(0, 1, 0, pacijent, DateTime.Now, false));
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
                   
                    antiTrollPacijenata.Add(new AntiTrollPacijenta(antiTrollPacijenta.BrojacDodavanihTermina, 
                        antiTrollPacijenta.BrojacIzmenenjenihTermina + 1, 
                        antiTrollPacijenta.BrojacOtkazanihTermina,
                        antiTrollPacijenta.pacijent, DateTime.Now, false));
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
                antiTrollPacijenata.Add(new AntiTrollPacijenta(0, 0, 1, pacijent, DateTime.Now, false));
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
                   
                    antiTrollPacijenata.Add(new AntiTrollPacijenta(antiTrollPacijenta.BrojacDodavanihTermina, 
                        antiTrollPacijenta.BrojacIzmenenjenihTermina, 
                        antiTrollPacijenta.BrojacOtkazanihTermina + 1, 
                        antiTrollPacijenta.pacijent, DateTime.Now, false));
                    datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
                    break;
                }
            }
        }


    }
}
