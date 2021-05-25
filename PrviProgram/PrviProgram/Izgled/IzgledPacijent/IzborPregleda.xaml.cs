using Controller;
using Model;
using PrviProgram.Service;
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
    /// Interaction logic for IzborPregleda.xaml
    /// </summary>
    public partial class IzborPregleda : Window
    { 

        public ObservableCollection<Termin> termini = new ObservableCollection<Termin>();
        public Pacijent pacijent = new Pacijent();
        public UtilityService utilityService = new UtilityService();
        public PacijentControler pacijentControler = new PacijentControler();
        public IzborPregleda(List<Termin> slobodniTermini, ObservableCollection<Termin> termini,Pacijent pacijent)
        {
            InitializeComponent();

            this.termini = termini;
            this.pacijent = pacijent;
            dataGridIzborPregleda.ItemsSource = slobodniTermini;

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridIzborPregleda.SelectedIndex != -1)
            {
                Termin termin = (Termin)dataGridIzborPregleda.SelectedItem;
                Sala sala = new Sala();
                sala = TerminiService.getInstance().DobavljanjeSale(termin);
                termin.sala = sala;
                termin.SifraTermina = utilityService.GenerisanjeSifre();
                termin.pacijent = pacijent;
                pacijentControler.DodavanjeTermina(termin);
                pacijentControler.PovecavanjeBrojacaPriDodavanjuTermina(pacijent);
                this.termini.Add(termin);
                this.Close();
            }
        }

        private void Otkaži_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
