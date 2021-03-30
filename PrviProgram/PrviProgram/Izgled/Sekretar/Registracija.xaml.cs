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
    public partial class Registracija : Window
    {
        private Logika.LogikaSekretar.UpravljanjePacijentima up;
        private ObservableCollection<Model.Pacijent> pacijenti;

        public Registracija(ObservableCollection<Model.Pacijent> pacijenti)
        {
            InitializeComponent();
            up = new Logika.LogikaSekretar.UpravljanjePacijentima();
            this.pacijenti = pacijenti;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Model.Korisnik korisnik = new Model.Korisnik();
            korisnik.KorisnickoIme = textBoxKorisnickoIme.Text;
            korisnik.Lozinka = textBoxLozinka.Password;

            Model.Pacijent pacijent = new Model.Pacijent();
            pacijent.Ime = textBoxIme.Text;
            pacijent.Prezime = textBoxPrezime.Text;
            pacijent.Jmbg = textBoxJMBG.Text;
            pacijent.Email = textBoxEmail.Text;
            pacijent.Pol = ((bool)radioButtonPolM.IsChecked ? Model.Pol.Muski : Model.Pol.Zenski);
            pacijent.AdresaStanovanja = new Model.Adresa();
            pacijent.MestoRodjenja = new Model.Grad();

            pacijent.termin = new System.Collections.ArrayList();
            pacijent.kartonPacijenta = new Model.KartonPacijenta();
            pacijent.Korisnik = korisnik;

            if (up.DodavanjePacijenta(pacijent) == true)
            {
                this.pacijenti.Add(pacijent);
            }
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}