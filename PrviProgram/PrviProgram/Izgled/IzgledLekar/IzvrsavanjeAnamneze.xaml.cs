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
        private Pacijent pacijent;
        public IzvrsavanjeAnamneze(IzvrseniPregled izvrseniPregled, Pacijent pacijent, ObservableCollection<Termin> termini)
        {
            InitializeComponent();
            this.izvrseniPregled = izvrseniPregled;
            this.pacijent = pacijent;
            this.termini = termini;

            TextboxPacijent.Text = pacijent.Ime + " " + pacijent.Prezime;
            if (izvrseniPregled.anamneza != null)
                TextboxAnamneza.Text = izvrseniPregled.anamneza.Opis;
        }

        private void ZavrsiAnamnezu_Click(object sender, RoutedEventArgs e)
        {
            Anamneza anamneza = new Anamneza();
            anamneza.Opis = TextboxAnamneza.Text;
            izvrseniPregled.anamneza = anamneza;
            AzuriranjePrikazaTermina(termini, izvrseniPregled);
            KartonPacijentaService.getInstance().IzvrsenaAnamneza(izvrseniPregled, pacijentRepository.PregledPacijenta(pacijent.Jmbg));
            this.Close();
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
    }
}
