﻿using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledVesti
{
    public partial class PregledSvihVesti : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private VestiRepository vestiRepository = new VestiRepository();
        private ObservableCollection<Vest> vesti;
        private Sekretar sekretar;

        public PregledSvihVesti(Sekretar sekretar)
        {
            InitializeComponent();
            this.sekretar = sekretar;
            vesti = new ObservableCollection<Vest>(vestiRepository.PregledSvihVesti());
            dgDataBinding.ItemsSource = vesti;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            KreiranjeVesti kreiranjeVesti = new KreiranjeVesti(vesti, sekretar);
            kreiranjeVesti.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Vest vest = (Vest)dgDataBinding.SelectedItem;
                // call izmena
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Vest vest = (Vest)dgDataBinding.SelectedItem;
                sekretarController.BrisanjeVesti(vest);
                vesti.Remove(vest);
            }
        }

        private void Prikazi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Vest vest = (Vest)dgDataBinding.SelectedItem;
                //call prikaz
            }
        }

    }
}
