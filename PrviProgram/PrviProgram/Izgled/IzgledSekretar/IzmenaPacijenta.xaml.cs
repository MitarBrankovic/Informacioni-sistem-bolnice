using Logika.LogikaSekretar;
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

namespace PrviProgram.Izgled.Sekretar
{
    /// <summary>
    /// Interaction logic for IzmenaPacijenta.xaml
    /// </summary>
    public partial class IzmenaPacijenta : Window
    {
        private UpravljanjePacijentima up;
        private ObservableCollection<Model.Pacijent> pacijenti;
        private Model.Pacijent pacijent;

        public IzmenaPacijenta(ObservableCollection<Model.Pacijent> pacijenti, Model.Pacijent pacijent)
        {
            InitializeComponent();
            up = new UpravljanjePacijentima();
            this.pacijenti = pacijenti;
            this.pacijent = pacijent;

            textBoxKorisnickoIme.Text = pacijent.Korisnik.KorisnickoIme;
            textBoxLozinka.Password = pacijent.Korisnik.Lozinka;

            textBoxIme.Text = pacijent.Ime;
            textBoxPrezime.Text = pacijent.Prezime;
            textBoxJMBG.Text = pacijent.Jmbg;
            textBoxEmail.Text = pacijent.Email;
            radioButtonPolM.IsChecked = pacijent.Pol.Equals(Model.Pol.Muski) ? true : false;
            radioButtonPolZ.IsChecked = pacijent.Pol.Equals(Model.Pol.Zenski) ? true : false;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Model.Korisnik korisnik = new Model.Korisnik();
            korisnik.KorisnickoIme = textBoxKorisnickoIme.Text;
            korisnik.Lozinka = textBoxLozinka.Password;

            Model.Pacijent noviPacijent = new Model.Pacijent();
            noviPacijent.Ime = textBoxIme.Text;
            noviPacijent.Prezime = textBoxPrezime.Text;
            noviPacijent.Jmbg = textBoxJMBG.Text;
            noviPacijent.Email = textBoxEmail.Text;
            noviPacijent.Pol = ((bool)radioButtonPolM.IsChecked ? Model.Pol.Muski : Model.Pol.Zenski);
            
            noviPacijent.AdresaStanovanja = this.pacijent.AdresaStanovanja;
            noviPacijent.MestoRodjenja = this.pacijent.MestoRodjenja;
            noviPacijent.termin = this.pacijent.termin;
            noviPacijent.kartonPacijenta = this.pacijent.kartonPacijenta;
            noviPacijent.Korisnik = korisnik;

            if (up.IzmenaPacijenta(this.pacijent, noviPacijent) == true)
            {
                this.pacijenti.Remove(this.pacijent);
                this.pacijenti.Add(noviPacijent);
            }
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}