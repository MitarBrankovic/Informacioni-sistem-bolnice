using Model;
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

        public IzvrsavanjeAnamneze(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.termin = termin;

            TextboxPacijent.Text = termin.pacijent.Ime + " " + termin.pacijent.Prezime;
            //Kreiranje praznog objekta izvrsenog pregleda
            izvrseniPregled = new IzvrseniPregled();
            izvrseniPregled.Termin = termin;
        }

        private void ZavrsiAnamnezu_Click(object sender, RoutedEventArgs e)
        {
            Anamneza anamneza = new Anamneza();
            anamneza.Opis = TextboxAnamneza.Text;
            izvrseniPregled.anamneza = anamneza;

            KartonPacijentaService.getInstance().IzvrsenaAnamneza(izvrseniPregled, termin);

            this.Close();
        }

        private void PrepisiTerapiju_Click(object sender, RoutedEventArgs e)
        {
            PrepisiTerapiju prepisi = new PrepisiTerapiju(izvrseniPregled);
            prepisi.Show();
        }

        private void PrepisiLek_Click(object sender, RoutedEventArgs e)
        {
            PrepisiLek prepisi = new PrepisiLek(izvrseniPregled);
            prepisi.Show();
        }

        private void ZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            ZdravstveniKarton karton = new ZdravstveniKarton(termin.pacijent);
            karton.Show();
        }
    }
}
