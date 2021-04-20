using Model;
using PrviProgram.Logika.Controllers;
using Repository;
using Service.LekarService;
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
        public IzvrsavanjeAnamneze(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.termin = termin;

            TextboxPacijent.Text = termin.pacijent.Ime + " " + termin.pacijent.Prezime;
            //Kreiranje praznog objekta izvrsenog pregleda
            izvrseniPregled = new IzvrseniPregled();
            izvrseniPregled.Lekar = termin.lekar;
            izvrseniPregled.Datum = termin.Datum;
            izvrseniPregled.TipTermina = termin.TipTermina;
        }

        private void ZavrsiAnamnezu_Click(object sender, RoutedEventArgs e)
        {
            Anamneza anamneza = new Anamneza();
            anamneza.Opis = TextboxAnamneza.Text;
            izvrseniPregled.anamneza = anamneza;

            KartonPacijentaService.getInstance().IzvrsenaAnamneza(izvrseniPregled, pacijentRepository.PregledPacijenta(termin.pacijent.Jmbg));
            //ControllerLekar.getInstance().BrisanjeTermina(termin, (Termin)dataGridLekar.SelectedItem));
            this.Close();
        }

        private void PrepisiTerapiju_Click(object sender, RoutedEventArgs e)
        {
            PrepisiTerapiju prepisi = new PrepisiTerapiju(izvrseniPregled, termin.pacijent);
            prepisi.Show();
        }

        private void PrepisiLek_Click(object sender, RoutedEventArgs e)
        {
            PrepisiLek prepisi = new PrepisiLek(izvrseniPregled, termin.pacijent);
            prepisi.Show();
        }

        private void ZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            ZdravstveniKarton karton = new ZdravstveniKarton(termin.pacijent);
            karton.Show();
        }
    }
}
