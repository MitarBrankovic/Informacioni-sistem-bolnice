using Model;
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
        private IzvrseniPregled izvrseniPregled;
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private Pacijent pacijent;
        //public IzvrsavanjeAnamneze(ObservableCollection<Termin> termini, Termin termin)
        public IzvrsavanjeAnamneze(IzvrseniPregled izvrseniPregled, Pacijent pacijent)
        {
            InitializeComponent();
            this.izvrseniPregled = izvrseniPregled;
            this.pacijent = pacijent;
            //this.termin = termin;

            TextboxPacijent.Text = pacijent.Ime + " " + pacijent.Prezime;
            if (izvrseniPregled.anamneza != null)
                TextboxAnamneza.Text = izvrseniPregled.anamneza.Opis;
            //Kreiranje praznog objekta izvrsenog pregleda
            /*izvrseniPregled = new IzvrseniPregled();
            izvrseniPregled.Lekar = termin.lekar;
            izvrseniPregled.Datum = termin.Datum;
            izvrseniPregled.TipTermina = termin.TipTermina;*/
        }

        private void ZavrsiAnamnezu_Click(object sender, RoutedEventArgs e)
        {
            Anamneza anamneza = new Anamneza();
            anamneza.Opis = TextboxAnamneza.Text;
            izvrseniPregled.anamneza = anamneza;

            KartonPacijentaService.getInstance().IzvrsenaAnamneza(izvrseniPregled, pacijentRepository.PregledPacijenta(pacijent.Jmbg));
            //ControllerLekar.getInstance().BrisanjeTermina(termin, (Termin)dataGridLekar.SelectedItem));
            this.Close();
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
            ZdravstveniKarton karton = new ZdravstveniKarton(pacijent);
            karton.Show();
        }
    }
}
