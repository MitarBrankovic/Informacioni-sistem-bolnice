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
        private Pacijent pacijent;
        public ZdravstveniKarton(Pacijent pacijent)
        {
            InitializeComponent();

            IzvrseniPregled izvrseni = new IzvrseniPregled();
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
            KartonPacijentaRepository.getInstance().UpisivanjeUFajl(kartoni);


            //izvrseniPregledi = new ObservableCollection<IzvrseniPregled>(KartonPacijentaRepository.getInstance().PregledSvihIzvrsenihPregleda(pacijent.kartonPacijenta));

            izvrseniPregledi = new ObservableCollection<IzvrseniPregled>(pacijent.kartonPacijenta.izvrseniPregled);
            dataGridKarton.ItemsSource = izvrseniPregledi;
        }

        private void dataGridKarton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
