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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Model;
using PrviProgram.Repository;
using Repository;
using Service;
using System.Collections.ObjectModel;
using Controller;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class SaleProzor : UserControl
    {
        private UpravnikController upravnikController = new UpravnikController();
        private SalaRepository saleRepository = new SalaRepository();
        private TerminiRenoviranjaRepository terminiRenoviranjaRepository = new TerminiRenoviranjaRepository();
        private ObservableCollection<Sala> sale;
        public DispatcherTimer timer = new DispatcherTimer();

        public SaleProzor()
        {
            InitializeComponent();
            InicijalizacijaTimera();
            sale = new ObservableCollection<Sala>(saleRepository.PregledSvihSala());
            dataGridUpravnik.ItemsSource = sale;
        }

        private void InicijalizacijaTimera()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            timer.Tick += new EventHandler(BrisanjeTerminaRenoviranja);
        }

        public void BrisanjeTerminaRenoviranja(object sender, EventArgs e)
        {
            List<TerminRenoviranjaSale> terminiRenoviranja = terminiRenoviranjaRepository.CitanjeIzFajla();
            foreach (TerminRenoviranjaSale terminBrojac in terminiRenoviranja)
            {
                if (DateTime.Today.Equals(terminBrojac.KrajRenoviranja))
                {
                    if (terminBrojac.TipRenoviranja.Equals(TipRenoviranja.razdvajanje))
                    {
                        //timer.Stop();
                        upravnikController.BrisanjeSale(terminBrojac.Sala);
                        upravnikController.DodavanjeSale(terminBrojac.PrvaSala);
                        upravnikController.DodavanjeSale(terminBrojac.DrugaSala);
                        terminiRenoviranja.Remove(terminBrojac);
                        terminiRenoviranjaRepository.UpisivanjeUFajl(terminiRenoviranja);
                        break;
                    }
                    else
                    {
                        //timer.Stop();
                        upravnikController.BrisanjeSale(terminBrojac.PrvaSala);
                        upravnikController.BrisanjeSale(terminBrojac.DrugaSala);
                        upravnikController.DodavanjeSale(terminBrojac.Sala);
                        terminiRenoviranja.Remove(terminBrojac);
                        terminiRenoviranjaRepository.UpisivanjeUFajl(terminiRenoviranja);
                        break;
                    }
                }
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeSale dodavanje = new DodavanjeSale(sale);
            dodavanje.ShowDialog();
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                upravnikController.BrisanjeSale((Sala)dataGridUpravnik.SelectedItem);
                sale.Remove((Sala)dataGridUpravnik.SelectedItem);
            }
            else { MessageBox.Show("Morate izabrati salu!"); }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                IzmenaSale izmena = new IzmenaSale(sale, (Sala)dataGridUpravnik.SelectedItem);
                izmena.ShowDialog();
            }
            else { MessageBox.Show("Morate izabrati salu!"); }
        }

        private void Oprema_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                SalaOprema salaOprema = new SalaOprema((Sala)dataGridUpravnik.SelectedItem);
                salaOprema.ShowDialog();
            }
            else { MessageBox.Show("Morate izabrati salu!"); }
        }

        private void Renoviranje_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                RenoviranjeSale win = new RenoviranjeSale((Sala)dataGridUpravnik.SelectedItem);
                win.ShowDialog();
            }
            else { MessageBox.Show("Morate izabrati salu!"); }
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void DodajSalu_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeSale win = new DodavanjeSale(sale);
            win.ShowDialog();
        }
        private void Spajanje_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                SaleSpajanje win = new SaleSpajanje((Sala)dataGridUpravnik.SelectedItem);
                win.ShowDialog();
            }
            else { MessageBox.Show("Morate izabrati salu!"); }

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PodesavanjaNaloga_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Podesavanja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HelpMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaleMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpremaMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LekoviMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Dodaj.Focus();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.RightCtrl)
            {
                if (Dodaj.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (Izmeni.IsFocused)
                {
                    Izbrisi.Focus();
                }
                else if (Izbrisi.IsFocused)
                {
                    Oprema.Focus();
                }
                else if (Oprema.IsFocused)
                {
                    Renoviranje.Focus();
                }
                else if (Renoviranje.IsFocused)
                {
                    Spajanje.Focus();
                }
                else if (Spajanje.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (dataGridUpravnik.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused && !Izmeni.IsFocused && !Izbrisi.IsFocused && !Oprema.IsFocused && !Renoviranje.IsFocused && !Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (SaleMenu.IsFocused || LekoviMenu.IsFocused || OpremaMenu.IsFocused || PodesavanjaMenu.IsFocused || PodesavanjaNalogaMenu.IsFocused || HelpMenu.IsFocused)
                    Dodaj.Focus();

            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused)
                {
                    Spajanje.Focus();
                }
                else if (Spajanje.IsFocused)
                {
                    Renoviranje.Focus();
                }
                else if (Renoviranje.IsFocused)
                {
                    Oprema.Focus();
                }
                else if (Oprema.IsFocused)
                {
                    Izbrisi.Focus();
                }
                else if (Izbrisi.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (Izmeni.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (Dodaj.IsFocused)
                {
                    Nazad.Focus();
                }
                else if (dataGridUpravnik.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused && !Izmeni.IsFocused && !Izbrisi.IsFocused && !Oprema.IsFocused && !Renoviranje.IsFocused && !Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (SaleMenu.IsFocused || LekoviMenu.IsFocused || OpremaMenu.IsFocused || PodesavanjaMenu.IsFocused || PodesavanjaNalogaMenu.IsFocused || HelpMenu.IsFocused)
                    Dodaj.Focus();
            }
            else if (e.Key == Key.M)
            {
                File.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                (this.Parent as Grid).Children.Remove(this);
            }
            else if (e.Key == Key.F1)
            {
                MessageBox.Show(
                "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
                "- M: Selektovanje MenuBar-a - FILE.\n" +
                "- ENTER: Potvrda akcije selektovanog dugmeta. \n" +
                "- ESCAPE: Povratak na pocetni prozor. \n" +
                "- DOWN: Selektovanje tabele kada dugmici iznad tabele imaju fokus. \n" +
                "- LCTRL/RCTRL: Vracanje fokusa na dugmice kada je fokus na tabeli.\n" +
                "- F1: Otvaranje Pomoc dijaloga. \n" +
                "- F2: Otvaranje dijaloga izmene selektovane sale. \n" +
                "- F3: Brisanje selektovane sale. \n" +
                "- F4: Otvaranje dijaloga renoviranja selektovane sale. \n\n" +

                "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                , "HELP");
            }
            else if (e.Key == Key.F2)
            {
                if (dataGridUpravnik.SelectedIndex != -1)
                {
                    IzmenaSale izmena = new IzmenaSale(sale, (Sala)dataGridUpravnik.SelectedItem);
                    izmena.ShowDialog();
                }
                else { MessageBox.Show("Morate izabrati salu!"); }
                Dodaj.Focus();
            }
            else if (e.Key == Key.F3)
            {
                if (dataGridUpravnik.SelectedIndex != -1)
                {
                    upravnikController.BrisanjeSale((Sala)dataGridUpravnik.SelectedItem);
                    sale.Remove((Sala)dataGridUpravnik.SelectedItem);
                }
                else { MessageBox.Show("Morate izabrati salu!"); }
                Dodaj.Focus();
            }
            else if (e.Key == Key.F4)
            {
                if (dataGridUpravnik.SelectedIndex != -1)
                {
                    RenoviranjeSale win = new RenoviranjeSale((Sala)dataGridUpravnik.SelectedItem);
                    win.ShowDialog();
                }
                else { MessageBox.Show("Morate izabrati salu!"); }
                Dodaj.Focus();
            }
            else if (Pomoc.IsFocused || Nazad.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Renoviranje.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (Spajanje.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Oprema.IsFocused || Renoviranje.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (e.Key == Key.Space || e.Key == Key.N)
                e.Handled = true;
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "- Use LEFT and RIGHT CTRL to move withind buttons.\n" +
                "- Use CTRL + O to select menu bar - FILE.\n" +
                "- Use ENTER to select the button.\n" +

                "- Use ENTER/SPACE to close this message.\n"

                , "HELP");
        }
    }
}
