using Model;
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

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for IzborPregleda.xaml
    /// </summary>
    public partial class IzborPregleda : Window
    { 

       public ObservableCollection<Termin> termini1 = new ObservableCollection<Termin>();
        public Pacijent pacijent = new Pacijent();
        public IzborPregleda(List<Termin> termini, ObservableCollection<Termin> termini1,Pacijent pacijent)
        {
            InitializeComponent();

            this.termini1 = termini1;
            this.pacijent = pacijent;
            dataGridIzborPregleda.ItemsSource = termini;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridIzborPregleda.SelectedIndex != -1)
            {
                Termin t = (Termin)dataGridIzborPregleda.SelectedItem;
                Sala s = new Sala();
                s = TerminiService.getInstance().dobavljanjeSale(t);
                t.sala = s;
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[3];
                var Random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[Random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                t.SifraTermina = finalString;
                t.pacijent = pacijent;
                TerminiService.getInstance().DodavanjeTermina(t);
                this.termini1.Add(t);
                this.Close();
                // s.Show();
            }
        }
    }
}
