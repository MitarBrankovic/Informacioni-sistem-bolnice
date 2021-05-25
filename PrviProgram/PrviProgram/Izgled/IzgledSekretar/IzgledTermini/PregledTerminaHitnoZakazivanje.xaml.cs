using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class PregledTerminaHitnoZakazivanje : Window
    {
        private TerminiService terminiService = new TerminiService();
        private UtilityService utilityService = new UtilityService();
        private LekarRepository lekarRepository = new LekarRepository();
        private ObservableCollection<Termin> sviTermini;
        private ObservableCollection<Termin> termini;
        private Termin termin;
        private Specijalizacija specijalizacija;

        public PregledTerminaHitnoZakazivanje(ObservableCollection<Termin> termini, Termin termin, Specijalizacija specijalizacija)
        {
            InitializeComponent();
            this.termin = termin;
            this.sviTermini = termini;
            this.specijalizacija = specijalizacija;
            this.termini = InicijalizujTermine();
            dataGridPacijenta.ItemsSource = this.termini;
        }

        public ObservableCollection<Termin> InicijalizujTermine()
        {
            List<Termin> termini = new List<Termin>(PregledSvihZauzetihTerminaSpecijalizacijeLekara());
            termini.Sort(SortirajTerminePoVremenuPomeranja);
            return new ObservableCollection<Termin>(termini);
        }

        public List<Termin> PregledSvihZauzetihTerminaSpecijalizacijeLekara()
        {
            List<Termin> terminiLekara = new List<Termin>();
            string[] terminVreme = termin.Vreme.Split(":");
            foreach (Termin zauzetiTermini in sviTermini)
            {
                string[] tVreme = zauzetiTermini.Vreme.Split(":");
                if (lekarRepository.ProveriSpecijalizacijuLekara(zauzetiTermini.lekar, specijalizacija)
                    && int.Parse(tVreme[0]) >= int.Parse(terminVreme[0])
                    && zauzetiTermini.Datum.Date.Equals(termin.Datum.Date)
                    && !(int.Parse(terminVreme[1]) == 30 && int.Parse(tVreme[0]) == int.Parse(terminVreme[0]) && int.Parse(tVreme[1]) == 0))
                {
                    terminiLekara.Add(zauzetiTermini);
                }
            }
            return terminiLekara;
        }

        private int SortirajTerminePoVremenuPomeranja(Termin terminX, Termin terminY)
        {
            Termin terminA = PronadjiPrviSledeciSlobodanTerminKodLekara(terminX);
            Termin terminB = PronadjiPrviSledeciSlobodanTerminKodLekara(terminY);
            int datumCompare = terminA.Datum.Date.CompareTo(terminB.Datum.Date);
            if (datumCompare == 0)
            {
                string[] aVreme = terminA.Vreme.Split(":");
                string[] bVreme = terminB.Vreme.Split(":");
                int brojSatiCompare = int.Parse(aVreme[0]).CompareTo(int.Parse(bVreme[0]));
                if (brojSatiCompare == 0)
                {
                    return int.Parse(aVreme[1]).CompareTo(int.Parse(bVreme[1]));
                }
                return brojSatiCompare;
            }
            return datumCompare;
        }

        private Termin PronadjiPrviSledeciSlobodanTerminKodLekara(Termin zaTermin)
        {
            List<string> vremeTermina = utilityService.nizVremena;
            Termin terminDTO = new Termin
            {
                Datum = zaTermin.Datum,
                Vreme = zaTermin.Vreme,
                lekar = zaTermin.lekar
            };
            int indexVreme = vremeTermina.FindIndex(i => i == zaTermin.Vreme);
            while (true)
            {
                foreach (string v in vremeTermina)
                {
                    int indexV = vremeTermina.IndexOf(v);
                    if (indexV > indexVreme)
                    {
                        terminDTO.Vreme = v;
                        if (terminiService.ProveraZauzetostiTermina(terminDTO) == false)
                        {
                            return terminDTO;
                        }
                    }
                }
                terminDTO.Datum = terminDTO.Datum.AddDays(1);
            }
        }

        private void ZakaziTerminButton_Click(object sender, RoutedEventArgs e)
        {
            termin.lekar = PronadjiLekaraSaSlobodnimTerminom(termin);
            if (termin.lekar != null)
            {
                PotvrdaZakazivanjaTermina potvrdaZakazivanjaTermina = new PotvrdaZakazivanjaTermina(sviTermini, termin);
                potvrdaZakazivanjaTermina.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ne postoji slobodan termin.");
            }
        }

        private Lekar PronadjiLekaraSaSlobodnimTerminom(Termin termin)
        {
            List<Lekar> lekari = lekarRepository.PregledLekaraOdredjeneSpecijalizacije(specijalizacija);
            foreach (Termin t in termini)
            {
                if (t.Vreme == termin.Vreme && t.Datum.Date.Equals(termin.Datum.Date))
                {
                    lekari.Remove(lekari.SingleOrDefault(lekar => lekar.Jmbg.Equals(t.lekar.Jmbg)));
                }
            }
            return lekari.Count() == 0 ? null : lekari.First();
        }

        private void OtkaziTerminButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridPacijenta.SelectedItem;
                terminiService.BrisanjeTermina(termin);
                termini.Remove(termin);
                sviTermini.Remove(termin);
            }
        }

        private void PomeriTerminButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridPacijenta.SelectedItem;
                PomeranjeZauzetihTermina pomeranjeZauzetihTermina = new PomeranjeZauzetihTermina(sviTermini, termini, termin);
                pomeranjeZauzetihTermina.Show();
            }
        }

    }
}