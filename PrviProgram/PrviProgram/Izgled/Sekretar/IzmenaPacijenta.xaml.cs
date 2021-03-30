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
        private Logika.LogikaSekretar.UpravljanjePacijentima up;
        private ObservableCollection<Model.Pacijent> pacijenti;
        private Model.Pacijent pacijent;

        public IzmenaPacijenta(ObservableCollection<Model.Pacijent> pacijenti, Model.Pacijent pacijent)
        {
            InitializeComponent();
            up = new Logika.LogikaSekretar.UpravljanjePacijentima();
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
            this.pacijenti.Remove(this.pacijent);
            
            this.pacijent.Korisnik.KorisnickoIme = textBoxKorisnickoIme.Text;
            this.pacijent.Korisnik.Lozinka = textBoxLozinka.Password;

            this.pacijent.Ime = textBoxIme.Text;
            this.pacijent.Prezime = textBoxPrezime.Text;
            this.pacijent.Jmbg = textBoxJMBG.Text;
            this.pacijent.Email = textBoxEmail.Text;
            this.pacijent.Pol = ((bool)radioButtonPolM.IsChecked ? Model.Pol.Muski : Model.Pol.Zenski);
            this.pacijent.AdresaStanovanja = new Model.Adresa();
            this.pacijent.MestoRodjenja = new Model.Grad();

            if (up.IzmenaPacijenta(this.pacijent) == true)
            {
                this.pacijenti.Add(this.pacijent);
            }
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}