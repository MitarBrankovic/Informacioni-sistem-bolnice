using Model;
using Repository;
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
    /// Interaction logic for ZdravstveniKarton.xaml
    /// </summary>
    public partial class ZdravstveniKarton : Window
    {

        public ObservableCollection<IzvrseniPregled> izvrseniPregledi;
        public PacijentRepository pacijentRepository = new PacijentRepository();
        private Pacijent pacijent;
        public ZdravstveniKarton(Pacijent pacijent)
        {
            InitializeComponent();

            /*IzvrseniPregled izvrseni = new IzvrseniPregled();
            Anamneza anamneza = new Anamneza();
            anamneza.Opis = "anamneza";

            Terapija terapija = new Terapija();
            terapija.Opis = "terapija";

            Recept recept = new Recept();
            recept.Lekovi = "lekovi";

            izvrseni.anamneza = anamneza;
            izvrseni.terapija = terapija;
            izvrseni.recept = recept;

            pacijent.kartonPacijenta.AddIzvrseniPregled(izvrseni);
            pacijent.kartonPacijenta.Sifra = "sifraKartona";
            List<KartonPacijenta> kartoni = new List<KartonPacijenta>();
            kartoni.Add(pacijent.kartonPacijenta);
            KartonPacijentaRepository.getInstance().UpisivanjeUFajl(kartoni);*/


            //izvrseniPregledi = new ObservableCollection<IzvrseniPregled>(KartonPacijentaRepository.getInstance().PregledSvihIzvrsenihPregleda(pacijent.kartonPacijenta));
            //pacijentRepository.PregledPacijenta(pacijent.Jmbg);
            this.pacijent = pacijent;
            izvrseniPregledi = new ObservableCollection<IzvrseniPregled>(pacijentRepository.PregledPacijenta(pacijent.Jmbg).kartonPacijenta.izvrseniPregled);
            dataGridKarton.ItemsSource = izvrseniPregledi;
        }

        private void dataGridKarton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Azuriraj_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridKarton.SelectedIndex != -1)
            {
                /*Termin termin = (Termin)dataGridLekar.SelectedItem;
                izvrseniPregled = new IzvrseniPregled();
                izvrseniPregled.Lekar = termin.lekar;
                izvrseniPregled.Datum = termin.Datum;
                izvrseniPregled.TipTermina = termin.TipTermina;*/
                IzvrseniPregled izvrseniPregled = (IzvrseniPregled)dataGridKarton.SelectedItem;

                IzvrsavanjeAnamneze anamneza = new IzvrsavanjeAnamneze(izvrseniPregled, pacijent);
                anamneza.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");
            }
        }
    }
}
