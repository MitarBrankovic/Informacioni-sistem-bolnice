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
    /// Interaction logic for DodavanjeBeleske.xaml
    /// </summary>
    public partial class DodavanjeBeleske : Window
    {
       public IzvrseniPregled selektovanaAnamneza = new IzvrseniPregled();
        public ObservableCollection<IzvrseniPregled> izvrseniPregledi { get; set; }
        public IzvrseniPregled pregledSaBeleskom = new IzvrseniPregled();
        public Pacijent selektovaniPacijent = new Pacijent();

        public DodavanjeBeleske(IzvrseniPregled selektovanaAnamneza, ObservableCollection<IzvrseniPregled>izvrseniPregledi,Pacijent selektovaniPacijent)
        {
            InitializeComponent();
            this.selektovanaAnamneza = selektovanaAnamneza;
            this.izvrseniPregledi = izvrseniPregledi;
            this.pregledSaBeleskom = selektovanaAnamneza;
            this.selektovaniPacijent = selektovaniPacijent;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            this.izvrseniPregledi.Remove(this.selektovanaAnamneza);
            this.pregledSaBeleskom.Beleska = OpisBeleskeText.Text;
            if (KartonPacijentaService.getInstance().IzmenaIzvrsenogPregleda(pregledSaBeleskom, this.selektovaniPacijent))
            {
                this.izvrseniPregledi.Add(this.pregledSaBeleskom);
            }
            this.Close();

        }

        private void Otkaži_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
