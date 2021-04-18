using Model;
using PrviProgram.Repository;
using Service.LekarService;
using Service.PacijentService;
using Service.UpravnikService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for DodavanjeTermina.xaml
    /// </summary>
    public partial class DodavanjeTermina : Window
    {
        private SaleService uprSal;
        private SalaRepository saleRep;

        public DodavanjeTermina(ObservableCollection<Termin> termini, Pacijent p)
        {
            this.pacijent = p;
            this.term = termini;
            InitializeComponent();

            uprSal = new SaleService();

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
                t.Vreme = vremeText.Text;

                Model.Lekar l = new Model.Lekar();
                l.Ime = ImeLekara.Text;
                l.Prezime = PrezimeLekara.Text;

                t.pacijent = this.pacijent;

                Random rnd = new Random();

                List<Sala> sale = new List<Sala>();
                sale = saleRep.PregledSvihSala();

                Sala tempSala = new Sala();
                if (sale[0] != null)
                {
                    tempSala = sale[0];
                    t.sala = tempSala;
                }

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

                TerminiService.getInstance().DodavanjeTermina(t, pacijent);
                //term.Add(t);

                PreglediService.getInstance().DodavanjePregleda(t);
                this.term.Add(t);

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