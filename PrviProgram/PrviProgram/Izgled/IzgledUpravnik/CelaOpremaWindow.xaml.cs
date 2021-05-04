using Model;
using PrviProgram.Repository;
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
    public partial class CelaOpremaWindow : Window
    {
        public SalaRepository saleRepository = new SalaRepository();
        public ObservableCollection<Oprema> opreme;
        public ObservableCollection<Oprema> pomocnaOprema;
        private List<Oprema> ukupnaOprema;
        private List<Sala> sale;

        public CelaOpremaWindow()
        {
            InitializeComponent();
            sale = saleRepository.CitanjeIzFajla();
            ukupnaOprema = new List<Oprema>();
            InicijalizacijaTabele();
            InicijalizacijaComboBoxaSala();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            String search;
            search = Textbox.Text;
            String[] splited = search.Split(" ");
            if (search == "")
            {
                ResetovanjeTabele();
                ProveraTipaOpreme();
                ProveraKolicineOpreme();
            }
            else if (splited.Length == 1) {
                foreach (Oprema o in pomocnaOprema) {
                    if (!opreme.Contains(o))
                    {
                        opreme.Add(o);
                    }
                    if (!o.Naziv.ToLower().Contains(splited[0].ToLower())) {
                        opreme.Remove(o);
                    }
                }
                ProveraTipaOpreme();
                ProveraKolicineOpreme();
            }
            else if (splited.Length == 2) {
                foreach (Oprema o in pomocnaOprema)
                {
                    if (!opreme.Contains(o))
                    {
                        opreme.Add(o);
                    }
                    if (!o.Naziv.ToLower().Contains(splited[0].ToLower()) || !o.NazivSale.ToLower().Contains(splited[1].ToLower()))
                    {
                        opreme.Remove(o);
                    }
                }
                ProveraTipaOpreme();
                ProveraKolicineOpreme();
            }
        }


        private void Staticka_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziStatickuOpremu();
        }

        private void Dinamicka_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziDinamickuOpremu();
        }

        private void Obe_Checked(object sender, RoutedEventArgs e)
        {
            ResetovanjeTabele();
            ProveraKolicineOpreme();
        }
        private void Do5_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziOpremuKolicineDo5();
        }

        private void Do10_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziOpremuKolicineDo10();
        }

        private void Preko10_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziOpremuKolicinePreko10();
        }
        private void Sve_Checked(object sender, RoutedEventArgs e)
        {
            ResetovanjeTabele();
            ProveraTipaOpreme();
        }


        private void ComboSala_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nazivIzabraneSale;
            nazivIzabraneSale = ComboSala.SelectedItem.ToString();
            ResetovanjeTabele();
            if (nazivIzabraneSale.Equals("Sve")) {
                ResetovanjeTabele();
                ProveraTipaOpreme();
                ProveraKolicineOpreme();
            } else{
                foreach (Oprema o in ukupnaOprema)
                {
                    if (!o.NazivSale.Equals(nazivIzabraneSale))
                    {
                        this.opreme.Remove(o);
                    }
                }
                ProveraTipaOpreme();
                ProveraKolicineOpreme();
            }
        }



        private void ResetovanjeTabele()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (!opreme.Contains(o))
                {
                    opreme.Add(o);
                }
            }
        }

        private void PrikaziStatickuOpremu()
        {        
            foreach (Oprema o in ukupnaOprema)
            {
                if (o.Tip == TipOpreme.Dinamicka)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void PrikaziDinamickuOpremu()
        {
            foreach (Oprema o in ukupnaOprema)
            {
                if (o.Tip == TipOpreme.Staticka)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void PrikaziOpremuKolicineDo5() 
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina > 5)
                {
                    opreme.Remove(o);
                }
            }
        }


        private void PrikaziOpremuKolicineDo10()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina < 6 || o.Kolicina > 10)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void PrikaziOpremuKolicinePreko10()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina < 11)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void ProveraTipaOpreme() 
        {
            if (Staticka.IsChecked == true)
            {
                PrikaziStatickuOpremu();
            }

            if (Dinamicka.IsChecked == true)
            {
                PrikaziDinamickuOpremu();
            }
        }

        private void ProveraKolicineOpreme()
        {
            if (Do5.IsChecked == true)
            {
                PrikaziOpremuKolicineDo5();
            }

            if (Do10.IsChecked == true)
            {
                PrikaziOpremuKolicineDo10();
            }

            if (Preko10.IsChecked == true)
            {
                PrikaziOpremuKolicinePreko10();
            }
        }

        private void InicijalizacijaComboBoxaSala() 
        {
            Sala sve = new Sala();
            sve.Naziv = "Sve";
            List<Sala> comboSale = new List<Sala>();
            List<Sala> pomocnaLista = new List<Sala>(sale);
            comboSale.Add(sve);
            comboSale.AddRange(pomocnaLista);
            ComboSala.ItemsSource = comboSale;
        }

        private void InicijalizacijaTabele() 
        {
            foreach (Sala s in sale)
            {
                ukupnaOprema.AddRange(saleRepository.PregledSvihOpremaPoSali(s));
            }
            opreme = new ObservableCollection<Model.Oprema>(ukupnaOprema);
            pomocnaOprema = new ObservableCollection<Model.Oprema>(ukupnaOprema);
            dataGridOprema.ItemsSource = opreme;
        }
    }
}
