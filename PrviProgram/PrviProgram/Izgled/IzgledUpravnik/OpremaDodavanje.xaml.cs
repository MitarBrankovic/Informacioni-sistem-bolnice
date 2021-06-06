using Controller;
using Model;
using PrviProgram.Repository;
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

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class OpremaDodavanje : Window
    {
        private UtilityService utilityService = new UtilityService();
        private OpremaController opremaController = new OpremaController();
        private ObservableCollection<Oprema> svaOpremaIzTabele;
        private Sala trenutnaSala;
        private Oprema novaOprema;
        private SalaRepository salaRepository = new SalaRepository();
        private ObservableCollection<TipOpreme> tipOpreme = new ObservableCollection<TipOpreme> { TipOpreme.Dinamicka, TipOpreme.Staticka};

        public OpremaDodavanje(ObservableCollection<Oprema> opreme)
        {
            InitializeComponent();
            this.svaOpremaIzTabele = opreme;
            Tip.ItemsSource = tipOpreme;
            ComboSala.ItemsSource = salaRepository.PregledSvihSala();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (Naziv.Text == "" && Kolicina.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Naziv.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Naziv nije dobro unet!", "Greska");
                Naziv.Text.Remove(Naziv.Text.Length - 1);
            }
            else if (!utilityService.IsNumber(Kolicina.Text))
            {
                MessageBox.Show("Kolicina nije dobro unet!", "Greska");
            }
            else
            {
                trenutnaSala = (Sala)ComboSala.SelectedItem;
                novaOprema = new Oprema(Naziv.Text, int.Parse(Kolicina.Text), (TipOpreme)Tip.SelectedItem, trenutnaSala.Naziv);
                DodavanjeNoveOpreme();
                this.Close();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodavanjeNoveOpreme() 
        {
            if (opremaController.DodavanjeOpreme(novaOprema, trenutnaSala))
            {
                this.svaOpremaIzTabele.Add(novaOprema);
                trenutnaSala.oprema.Add(novaOprema);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            else if (e.Key == Key.Space)
            {
                Naziv.Focus();
            }
        }
    }
}
