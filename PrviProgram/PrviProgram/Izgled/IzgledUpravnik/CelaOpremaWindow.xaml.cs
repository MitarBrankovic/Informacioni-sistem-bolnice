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
    /// <summary>
    /// Interaction logic for CelaOpremaWindow.xaml
    /// </summary>
    public partial class CelaOpremaWindow : Window
    {
        public SalaRepository saleRep;
        public ObservableCollection<Oprema> opreme;
        public ObservableCollection<Oprema> pomocnaOprema;
        List<Oprema> ukupnaOprema;


        public CelaOpremaWindow()
        {
            InitializeComponent();

            saleRep = new SalaRepository();

            List<Sala> sale = saleRep.CitanjeIzFajla();
            ukupnaOprema = new List<Oprema>();


            foreach (Sala s in sale) {
                ukupnaOprema.AddRange(saleRep.PregledSvihOpremaPoSali(s));
            }

            opreme = new ObservableCollection<Model.Oprema>(ukupnaOprema);
            pomocnaOprema = new ObservableCollection<Model.Oprema>(ukupnaOprema);
            dataGridOprema.ItemsSource = opreme;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            String search;
            search = Textbox.Text;

            if (search == "")
            {
                foreach (Oprema o in pomocnaOprema)
                {
                    if (!opreme.Contains(o))
                    {
                        opreme.Add(o);
                    }
                }
            }
            else {
                foreach (Oprema o in pomocnaOprema) {
                    if (!o.Naziv.ToLower().Contains(search.ToLower())) {
                        opreme.Remove(o);
                    }
                }
            
            }


        }



//###############################TIP##################################################
        private void Staticka_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Oprema o in ukupnaOprema) {
                if (o.Tip == TipOpreme.Dinamicka) {
                    opreme.Remove(o);
                }
            
            }
        }

        private void Dinamicka_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Oprema o in ukupnaOprema)
            {
                if (o.Tip == TipOpreme.Staticka)
                {
                    opreme.Remove(o);
                }

            }
        }

        private void Obe_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Oprema o in pomocnaOprema) {
                if (!opreme.Contains(o)){
                    opreme.Add(o);
                }
            }


            if (Do5.IsChecked == true) {
                foreach (Oprema o in pomocnaOprema)
                {
                    if (o.Kolicina > 5)
                    {
                        opreme.Remove(o);
                    }
                }
            }

            if (Do10.IsChecked == true)
            {
                foreach (Oprema o in pomocnaOprema)
                {
                    if (o.Kolicina < 6 && o.Kolicina > 10)
                    {
                        opreme.Remove(o);
                    }
                }
            }

            if (Preko10.IsChecked == true)
            {
                foreach (Oprema o in pomocnaOprema)
                {
                    if (o.Kolicina < 11)
                    {
                        opreme.Remove(o);
                    }
                }
            }
        }


//#############################KOLICINA##################################################

        private void Do5_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina > 5)
                {
                    opreme.Remove(o);
                }
            }


        }

        private void Do10_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina < 6 && o.Kolicina > 10)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void Preko10_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina < 11)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void Sve_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (!opreme.Contains(o))
                {
                    opreme.Add(o);
                }
            }

            if (Staticka.IsChecked == true) {
                foreach (Oprema o in ukupnaOprema)
                {
                    if (o.Tip == TipOpreme.Dinamicka)
                    {
                        opreme.Remove(o);
                    }

                }
            }

            if (Dinamicka.IsChecked == true)
            {
                foreach (Oprema o in ukupnaOprema)
                {
                    if (o.Tip == TipOpreme.Staticka)
                    {
                        opreme.Remove(o);
                    }

                }
            }
        }
    }
}
