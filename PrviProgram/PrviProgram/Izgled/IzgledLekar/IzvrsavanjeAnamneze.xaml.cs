using Model;
using PrviProgram.Repository;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for IzvrsavanjeAnamneze.xaml
    /// </summary>
    public partial class IzvrsavanjeAnamneze : Window
    {
        private Termin termin;
        private ObservableCollection<Termin> termini;
        private IzvrseniPregled izvrseniPregled;
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private TerminiService terminiService = new TerminiService();
        private Pacijent pacijent;
        public IzvrsavanjeAnamneze(IzvrseniPregled izvrseniPregled, Pacijent pacijent, ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.termin = termin;
            this.izvrseniPregled = izvrseniPregled;
            this.pacijent = pacijent;
            this.termini = termini;

            if (!proveraIzvrsenostiTermina())
            {
                InicijalizacijaIzvrsenogTermina();
            }
            PopuniTextboxPacijenta();
            
        }

        private void PopuniTextboxPacijenta()
        {
            TextboxPacijent.Text = pacijent.Ime + " " + pacijent.Prezime;
            if (izvrseniPregled != null && izvrseniPregled.anamneza != null)
                TextboxAnamneza.Text = izvrseniPregled.anamneza.Opis;
        }
        private void InicijalizacijaIzvrsenogTermina()
        {
            izvrseniPregled = new IzvrseniPregled();
            izvrseniPregled.Lekar = termin.lekar;
            izvrseniPregled.Datum = termin.Datum;
            izvrseniPregled.TipTermina = termin.TipTermina;
            izvrseniPregled.Sifra = termin.SifraTermina;
        }
        private void ZavrsiAnamnezu_Click(object sender, RoutedEventArgs e)
        {
            KreiranjeAnamneze();
            if (!proveraIzvrsenostiTermina())
            {       
                termin.izvrsen = true;
                terminiService.IzmenaTermina(termin);
            }
            AzuriranjePrikazaTermina(termini, izvrseniPregled);
            KartonPacijentaService.getInstance().IzvrsenaAnamneza(izvrseniPregled, pacijentRepository.PregledPacijenta(pacijent.Jmbg));
            this.Close();
        }
        private void KreiranjeAnamneze()
        {
            Anamneza anamneza = new Anamneza();
            anamneza.Opis = TextboxAnamneza.Text;
            izvrseniPregled.anamneza = anamneza;
        }
        private bool proveraIzvrsenostiTermina()
        {
            if(termin.izvrsen == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void AzuriranjePrikazaTermina(ObservableCollection<Termin> termini, IzvrseniPregled izvrseniPregled)
        {
            foreach (Termin t in termini)
            {
                if (t.SifraTermina == izvrseniPregled.Sifra)
                {
                    Termin noviTermin = t;
                    noviTermin.izvrsen = true;
                    termini.Remove(t);
                    termini.Add(noviTermin);
                    break;
                }
            }
        }

        private void PrepisiTerapiju_Click(object sender, RoutedEventArgs e)
        {
            PrepisiTerapiju prepisi = new PrepisiTerapiju(izvrseniPregled, pacijent);
            prepisi.Show();
        }

        private void PrepisiLek_Click(object sender, RoutedEventArgs e)
        {
            PrepisiLek prepisi = new PrepisiLek(izvrseniPregled, pacijent);
            prepisi.Show();
        }

        private void ZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            ZdravstveniKarton karton = new ZdravstveniKarton(pacijent, termini);
            karton.Show();
        }

        private void Uput_Click(object sender, RoutedEventArgs e)
        {
            UputWindow uputWindow = new UputWindow(pacijent);
            uputWindow.Show();
        }
    }
}
