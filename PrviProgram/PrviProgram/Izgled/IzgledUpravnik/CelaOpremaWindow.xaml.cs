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
        public SalaRepository saleRep;
        public ObservableCollection<Oprema> opreme;
        public ObservableCollection<Oprema> pomocnaOprema;
        List<Oprema> ukupnaOprema;
        List<Sala> sale;

        public CelaOpremaWindow()
        {
            InitializeComponent();

            saleRep = new SalaRepository();
            sale = saleRep.CitanjeIzFajla();
            ukupnaOprema = new List<Oprema>();


            inicijalizacijaTabele();
            inicijalizacijaComboBoxaSala();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            String search;
            search = Textbox.Text;
            String[] splited = search.Split(" ");

            if (search == "")
            {
                resetovanjeTabele();
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
            }
        }


        private void Staticka_Checked(object sender, RoutedEventArgs e)
        {
            prikaziStatickuOpremu();
        }

        private void Dinamicka_Checked(object sender, RoutedEventArgs e)
        {
            prikaziDinamickuOpremu();
        }

        private void Obe_Checked(object sender, RoutedEventArgs e)
        {
            resetovanjeTabele();

            proveraKolicineOpreme();
        }


        private void Do5_Checked(object sender, RoutedEventArgs e)
        {
            prikaziOpremuKolicineDo5();
        }

        private void Do10_Checked(object sender, RoutedEventArgs e)
        {
            prikaziOpremuKolicineDo10();
        }

        private void Preko10_Checked(object sender, RoutedEventArgs e)
        {
            prikaziOpremuKolicinePreko10();
        }

        private void Sve_Checked(object sender, RoutedEventArgs e)
        {
            resetovanjeTabele();

            proveraTipaOpreme();
        }


        private void ComboSala_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nazivIzabraneSale;
            nazivIzabraneSale = ComboSala.SelectedItem.ToString();

            resetovanjeTabele();


            if (nazivIzabraneSale.Equals("Sve")) {
                resetovanjeTabele();
                proveraTipaOpreme();
                proveraKolicineOpreme();
            } else{
                foreach (Oprema o in ukupnaOprema)
                {
                    if (!o.NazivSale.Equals(nazivIzabraneSale))
                    {
                        opreme.Remove(o);
                    }

                }
                proveraTipaOpreme();
                proveraKolicineOpreme();
            }
        }



        private void resetovanjeTabele()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (!opreme.Contains(o))
                {
                    opreme.Add(o);
                }
            }
        }

        private void prikaziStatickuOpremu()
        {        
            foreach (Oprema o in ukupnaOprema)
            {
                if (o.Tip == TipOpreme.Dinamicka)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void prikaziDinamickuOpremu()
        {
            foreach (Oprema o in ukupnaOprema)
            {
                if (o.Tip == TipOpreme.Staticka)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void prikaziOpremuKolicineDo5() 
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina > 5)
                {
                    opreme.Remove(o);
                }
            }
        }


        private void prikaziOpremuKolicineDo10()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina < 6 && o.Kolicina > 10)
                {
                    opreme.Remove(o);
                }
            }
        }


        private void prikaziOpremuKolicinePreko10()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina < 11)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void proveraTipaOpreme() 
        {
            if (Staticka.IsChecked == true)
            {
                prikaziStatickuOpremu();
            }

            if (Dinamicka.IsChecked == true)
            {
                prikaziDinamickuOpremu();
            }
        }

        private void proveraKolicineOpreme()
        {
            if (Do5.IsChecked == true)
            {
                prikaziOpremuKolicineDo5();
            }

            if (Do10.IsChecked == true)
            {
                prikaziOpremuKolicineDo10();
            }

            if (Preko10.IsChecked == true)
            {
                prikaziOpremuKolicinePreko10();
            }
        }



        private void inicijalizacijaComboBoxaSala() 
        {
            Sala sve = new Sala();
            sve.Naziv = "Sve";
            List<Sala> comboSale = new List<Sala>();
            List<Sala> pomocnaLista = new List<Sala>(sale);
            comboSale.Add(sve);
            comboSale.AddRange(pomocnaLista);

            ComboSala.ItemsSource = comboSale;
        }

        private void inicijalizacijaTabele() 
        {
            foreach (Sala s in sale)
            {
                ukupnaOprema.AddRange(saleRep.PregledSvihOpremaPoSali(s));
            }

            opreme = new ObservableCollection<Model.Oprema>(ukupnaOprema);
            pomocnaOprema = new ObservableCollection<Model.Oprema>(ukupnaOprema);
            dataGridOprema.ItemsSource = opreme;

        }
    }
}
