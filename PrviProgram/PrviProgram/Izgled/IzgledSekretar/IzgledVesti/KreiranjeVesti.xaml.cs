using System;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledVesti
{

    public partial class KreiranjeVesti : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private ObservableCollection<Vest> vesti;
        private Sekretar autor;

        public KreiranjeVesti(ObservableCollection<Vest> vesti, Sekretar autor)
        {
            InitializeComponent();
            this.vesti = vesti;
            this.autor = autor;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Vest vest = PreuzmiVestIzForme();
            sekretarController.DodavanjeVesti(vest);
            vesti.Add(vest);
            Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Vest PreuzmiVestIzForme()
        {
            Vest vest = new Vest();
            vest.Autor = autor;
            vest.Datum = DateTime.Now;
            vest.Naslov = textBoxNaslov.Text;
            vest.SifraVesti = IzracunajSifruVesti();
            vest.Tekst = textBoxText.Text;
            return vest;
        }

        private static string IzracunajSifruVesti()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var Random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }
    }
}
