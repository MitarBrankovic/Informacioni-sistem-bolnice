using Model;
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

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for PrepisiLek.xaml
    /// </summary>
    public partial class PrepisiLek : Window
    {
        private IzvrseniPregled izvrseniPregled;
        public PrepisiLek(IzvrseniPregled izvrseniPregled)
        {
            InitializeComponent();
            this.izvrseniPregled = izvrseniPregled;
            TextboxPacijent.Text = izvrseniPregled.Termin.pacijent.Ime + " " + izvrseniPregled.Termin.pacijent.Prezime;

        }

        private void ZavrsiIzdavenjeRecepta_Click(object sender, RoutedEventArgs e)
        {
            Recept recept = new Recept();
            recept.Lekovi = TextboxRecept.Text;
            izvrseniPregled.recept = recept;

            this.Close();
        }
    }
}
