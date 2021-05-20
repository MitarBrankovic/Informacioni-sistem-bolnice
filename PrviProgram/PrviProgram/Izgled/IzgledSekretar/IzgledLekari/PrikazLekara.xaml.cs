﻿using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledLekari
{
    public partial class PrikazLekara : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private LekarRepository lekarRepository = new LekarRepository();
        public ObservableCollection<Lekar> lekari;

        public PrikazLekara()
        {
            InitializeComponent();
            lekari = new ObservableCollection<Lekar>(lekarRepository.PregledSvihLekara());
            dgDataBinding.ItemsSource = lekari;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if ((Lekar)dgDataBinding.SelectedItem != null)
            {
                sekretarController.BrisanjeLekara((Lekar)dgDataBinding.SelectedItem);
                lekari.Remove((Lekar)dgDataBinding.SelectedItem);
            }
        }
        private void Specijalizacije_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
