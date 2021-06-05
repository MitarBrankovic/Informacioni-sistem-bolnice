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
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for IzdavanjeTerapije.xaml
    /// </summary>
    public partial class IzdavanjeTerapije : Window
    {
        private Pacijent pacijent;
        private TerapijaService terapijaService = new TerapijaService();

        public IzdavanjeTerapije(Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijent = pacijent;
        }



        private void otkaziButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void potvrdiButton_Click(object sender, RoutedEventArgs e)
        {
            Terapija terapija = new Terapija((DateTime)datumPocetakKalendar.SelectedDate, (DateTime)datumKrajKalendara.SelectedDate, textBlockOpis.Text, pacijent);
            terapijaService.DodavanjeTerapije(terapija);
            this.Close();
            
        }
    }
}