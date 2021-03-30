using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Logika.LogikaPacijent;
using System.Collections.ObjectModel;

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for DodavanjeTermina.xaml
    /// </summary>
    public partial class DodavanjeTermina : Window
    {

        public DodavanjeTermina(ObservableCollection<Termin> termini, Pacijent p)
        {
            this.pacijent = p;
            this.term = termini;
            InitializeComponent();
        }
        private ObservableCollection<Termin> term;
        private Pacijent pacijent;

        bool datum = false;
        bool vreme = false;
        bool ime = false;
        bool prezime = false;
        bool tipPregleda = false;

        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 0;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }
        private void datum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datum1 = sender as DatePicker;

            if (datum1 == null)
            {
                datum = false;
            }
            else
            {
                datum = true;
            }

        }

        private void vreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vreme1 = sender as ComboBox;
            if (vreme1 == null)
            {
                vreme = false;
            }
            else
            {
                vreme = true;
            }

        }



        private void TipTerminaText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tip = sender as ComboBox;
            if (tip == null)
            {
                tipPregleda = false;
            }
            else
            {
                tipPregleda = true;
            }

        }
        private void ImeLekara_TextChanged(object sender, TextChangedEventArgs e)
        {
            var imeL = sender as TextBox;
            if (imeL == null)
            {
                ime = false;
            }
            else
            {
                ime = true;
            }

        }

        private void PrezimeLekara_TextChanged(object sender, TextChangedEventArgs e)
        {
            var prezimeL = sender as TextBox;
            if (prezimeL == null)
            {
                prezime = false;
            }
            else
            {
                prezime = true;
            }
        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin t = new Termin();

            if (ime == true && prezime == true && datum == true && vreme == true && tipPregleda == true)
            {
                t.Datum = (DateTime)(DatumText.SelectedDate);
                Model.Lekar l = new Model.Lekar();
                Random rnd = new Random();
                t.Vreme = vremeText.Text;
                l.Ime = ImeLekara.Text;
                l.Prezime = PrezimeLekara.Text;
                Sala s = new Sala();
                s.Naziv = GenerateName(1);
                s.Sprat = rnd.Next(100, 300);
                t.sala = s;

                t.lekar = l;
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var Random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[Random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                t.SifraTermina = finalString;

                String tip = TipTerminaText.Text;
                if (tip.Equals("Pregled"))
                {
                    t.TipTermina = TipTermina.Pregled;
                }
                if (tip.Equals("Kontrola"))
                {
                    t.TipTermina = TipTermina.Kontrola;
                }

                UpravljanjeTerminima.getInstance().DodavanjeTermina(t, pacijent);
                term.Add(t);
                this.Close();
            }
            else
            {
                MessageBox.Show("Niste popunili sva polja!!!",
               "Error");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}