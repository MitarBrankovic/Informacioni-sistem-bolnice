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
            int terminSat = int.Parse(terminVreme[0]);
            int terminMinut = int.Parse(terminVreme[1]);
            foreach (Termin t in sviTermini)
            {
                string[] tVreme = t.Vreme.Split(":");
                int tSat = int.Parse(tVreme[0]);
                int tMinut = int.Parse(tVreme[1]);
                if (ProveriSpecijalizacijuLekara(t.lekar) &&
                    tSat >= terminSat && t.Datum.Date.Equals(termin.Datum.Date)
                    && !(terminMinut == 30 && tSat == terminSat && tMinut == 0))
                {
                    terminiLekara.Add(t);
                }
            }
            return terminiLekara;
        }

        private int SortirajTerminePoVremenuPomeranja(Termin x, Termin y)
        {
            Termin a = PronadjiPrviSledeciSlobodanTerminKodLekara(x);
            Termin b = PronadjiPrviSledeciSlobodanTerminKodLekara(y);
            int datumCompare = a.Datum.Date.CompareTo(b.Datum.Date);
            if (datumCompare == 0)
            {
                string[] aVreme = a.Vreme.Split(":");
                string[] bVreme = b.Vreme.Split(":");
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
            List<string> constVreme = new List<string>() { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
            Termin termin = new Termin();
            termin.Datum = zaTermin.Datum;
            termin.Vreme = zaTermin.Vreme;
            termin.lekar = zaTermin.lekar;
            int indexVreme = constVreme.FindIndex(i => i == zaTermin.Vreme);
            while (true)
            {
                foreach (string v in constVreme)
                {
                    int indexV = constVreme.IndexOf(v);
                    if (indexV > indexVreme)
                    {
                        termin.Vreme = v;
                        if (terminiService.ProvaraZauzatostiTermina(termin) == false)
                        {
                            return termin;
                        }
                    }
                }
                termin.Datum = termin.Datum.AddDays(1);
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
            List<Lekar> lekari = lekarRepository.PregledSvihLekara();
            foreach (Termin t in termini)
            {
                if (ProveriSpecijalizacijuLekara(t.lekar) == false)
                {
                    lekari.Remove(lekari.SingleOrDefault(lekar => lekar.Jmbg.Equals(t.lekar.Jmbg)));
                }
                else if (t.Vreme == termin.Vreme && t.Datum.Date.Equals(termin.Datum.Date))
                {
                    lekari.Remove(lekari.SingleOrDefault(lekar => lekar.Jmbg.Equals(t.lekar.Jmbg)));
                }
            }
            return lekari.Count() == 0 ? null : lekari.First();
        }

        private bool ProveriSpecijalizacijuLekara(Lekar zaLekara)
        {
            Lekar lekar = lekarRepository.PregledLekara(zaLekara.Jmbg);
            foreach (Specijalizacija specijalizacija in lekar.GetSpecijalizacija())
            {
                if (specijalizacija.Naziv.Equals(this.specijalizacija.Naziv))
                {
                    return true;
                }
            }
            return false;
        }

        private void OtkaziTerminButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridPacijenta.SelectedItem;
                terminiService.BrisanjeTermina(termin);
                this.termini.Remove(termin);
                this.sviTermini.Remove(termin);
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