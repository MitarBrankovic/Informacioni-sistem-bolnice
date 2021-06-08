using Model;
using PrviProgram.Repository;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using System.Windows.Threading;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class OpremaProzor : UserControl
    {
        private OpremaController opremaController = new OpremaController();
        private SalaRepository salaRepository = new SalaRepository();
        private ObservableCollection<Oprema> opreme;
        private ObservableCollection<Oprema> pomocnaOprema;
        private List<Oprema> ukupnaOprema;
        private List<Sala> sale;
        private string nazivIzabraneSale;
        public DispatcherTimer timer = new DispatcherTimer();
        private ITerminiPremestajaRepository terminiPremestajaRepository = Factory.KreirajTerminPremestajaRep();

        public OpremaProzor()
        {
            InitializeComponent();
            InicijalizacijaTimera();
            sale = salaRepository.CitanjeIzFajla();
            ukupnaOprema = new List<Oprema>();
            InicijalizacijaTabele();
            InicijalizacijaComboBoxaSala();
        }


        private void InicijalizacijaTimera()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            timer.Tick += new EventHandler(IzvrsavanjePremestanjaOpreme);
        }

        public void IzvrsavanjePremestanjaOpreme(object sender, EventArgs e)
        {
            List<TerminPremestanjaOpreme> termini = terminiPremestajaRepository.CitanjeIzFajla();
            foreach (TerminPremestanjaOpreme terminBrojac in termini)
            {
                if (DateTime.Today.Equals(terminBrojac.DatumPremestaja))
                {
                    opremaController.PremestanjeOpreme(terminBrojac.Oprema, terminBrojac.StaraSala, terminBrojac.Sala);
                    //timer.Stop();
                    termini.Remove(terminBrojac);
                    terminiPremestajaRepository.UpisivanjeUFajl(termini);
                    break;
                }
            }
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                String search;
                search = Textbox.Text;
                String[] splited = search.Split(" ");
                if (search == "")
                {
                    ResetovanjeTabele();
                    ProveraTipaOpreme();
                    ProveraKolicineOpreme();
                    ProveraComboSale();
                }
                else if (splited.Length == 1)
                {
                    foreach (Oprema o in pomocnaOprema)
                    {
                        if (!opreme.Contains(o))
                        {
                            opreme.Add(o);
                        }
                        if (!o.Naziv.ToLower().Contains(splited[0].ToLower()))
                        {
                            opreme.Remove(o);
                        }
                    }
                    ProveraTipaOpreme();
                    ProveraKolicineOpreme();
                    ProveraComboSale();
                }
                else if (splited.Length == 2)
                {
                    foreach (Oprema o in pomocnaOprema)
                    {
                        if (!opreme.Contains(o))
                        {
                            opreme.Add(o);
                        }
                        if (!o.Naziv.ToLower().Contains(splited[0].ToLower()) || !o.NazivSale.ToLower().Contains(splited[1].ToLower()))
                        {
                            opreme.Remove(o);
                        }
                    }
                    ProveraTipaOpreme();
                    ProveraKolicineOpreme();
                    ProveraComboSale();
                }
            }
        }

        private void Staticka_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziStatickuOpremu();
        }

        private void Dinamicka_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziDinamickuOpremu();
        }

        private void Obe_Checked(object sender, RoutedEventArgs e)
        {
            ResetovanjeTabele();
            ProveraComboSale();
            ProveraKolicineOpreme();
        }
        private void Do5_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziOpremuKolicineDo5();
        }

        private void Do10_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziOpremuKolicineDo10();
        }

        private void Preko10_Checked(object sender, RoutedEventArgs e)
        {
            PrikaziOpremuKolicinePreko10();
        }
        private void Sve_Checked(object sender, RoutedEventArgs e)
        {
            ResetovanjeTabele();
            ProveraComboSale();
            ProveraTipaOpreme();
        }


        private void ComboSala_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nazivIzabraneSale = ComboSala.SelectedItem.ToString();
            ResetovanjeTabele();
            if (nazivIzabraneSale.Equals("Sve"))
            {
                ResetovanjeTabele();
                ProveraTipaOpreme();
                ProveraKolicineOpreme();
            }
            else
            {
                foreach (Oprema o in ukupnaOprema)
                {
                    if (!o.NazivSale.Equals(nazivIzabraneSale))
                    {
                        opreme.Remove(o);
                    }
                }
                ProveraTipaOpreme();
                ProveraKolicineOpreme();
            }
        }
        public void ProveraComboSale()
        {
            if (!nazivIzabraneSale.Equals("Sve"))
            {
                foreach (Oprema o in ukupnaOprema)
                {
                    if (!o.NazivSale.Equals(nazivIzabraneSale))
                    {
                        opreme.Remove(o);
                    }
                }
                ProveraTipaOpreme();
                ProveraKolicineOpreme();
            }
        }


        private void ResetovanjeTabele()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (!opreme.Contains(o))
                {
                    opreme.Add(o);
                }
            }
        }

        private void PrikaziStatickuOpremu()
        {
            //ResetovanjeTabele();
            foreach (Oprema o in ukupnaOprema)
            {
                if (o.Tip == TipOpreme.Dinamicka)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void PrikaziDinamickuOpremu()
        {
            //ResetovanjeTabele();
            foreach (Oprema o in ukupnaOprema)
            {
                if (o.Tip == TipOpreme.Staticka)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void PrikaziOpremuKolicineDo5()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina > 5)
                {
                    opreme.Remove(o);
                }
            }
        }


        private void PrikaziOpremuKolicineDo10()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina < 6 || o.Kolicina > 10)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void PrikaziOpremuKolicinePreko10()
        {
            foreach (Oprema o in pomocnaOprema)
            {
                if (o.Kolicina < 11)
                {
                    opreme.Remove(o);
                }
            }
        }

        private void ProveraTipaOpreme()
        {
            if (Staticka.IsChecked == true)
            {
                PrikaziStatickuOpremu();
            }

            if (Dinamicka.IsChecked == true)
            {
                PrikaziDinamickuOpremu();
            }
        }

        private void ProveraKolicineOpreme()
        {
            if (Do5.IsChecked == true)
            {
                PrikaziOpremuKolicineDo5();
            }

            if (Do10.IsChecked == true)
            {
                PrikaziOpremuKolicineDo10();
            }

            if (Preko10.IsChecked == true)
            {
                PrikaziOpremuKolicinePreko10();
            }
        }

        private void InicijalizacijaComboBoxaSala()
        {
            Sala sve = new Sala();
            sve.Naziv = "Sve";
            List<Sala> comboSale = new List<Sala>();
            List<Sala> pomocnaLista = new List<Sala>(sale);
            comboSale.Add(sve);
            comboSale.AddRange(pomocnaLista);
            ComboSala.ItemsSource = comboSale;
        }

        private void InicijalizacijaTabele()
        {
            foreach (Sala s in sale)
            {
                ukupnaOprema.AddRange(salaRepository.PregledSvihOpremaPoSali(s));
            }
            opreme = new ObservableCollection<Oprema>(ukupnaOprema);
            pomocnaOprema = new ObservableCollection<Oprema>(ukupnaOprema);
            dataGridOprema.ItemsSource = opreme;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            OpremaDodavanje win = new OpremaDodavanje(opreme);
            win.ShowDialog();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                Oprema selektovanaOprema = (Oprema)dataGridOprema.SelectedItem;
                Sala trenutnaSala = salaRepository.PregledSalePoNazivu(selektovanaOprema.NazivSale);
                OpremaIzmena win = new OpremaIzmena(opreme, selektovanaOprema, trenutnaSala);
                win.ShowDialog();
            }
            else { MessageBox.Show("Morate izabrati opremu!"); }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                Oprema selektovanaOprema = (Oprema)dataGridOprema.SelectedItem;
                Sala trenutnaSala = salaRepository.PregledSalePoNazivu(selektovanaOprema.NazivSale);
                foreach (Oprema opremaBrojac in trenutnaSala.oprema.ToArray())
                {
                    if (opremaBrojac.Naziv.Equals(selektovanaOprema.Naziv))
                    {
                        trenutnaSala.GetOprema().Remove(opremaBrojac);
                    }
                }
                opremaController.BrisanjeOpreme(selektovanaOprema, trenutnaSala);
                opreme.Remove(selektovanaOprema);
            }
            else { MessageBox.Show("Morate izabrati opremu!"); }
        }

        private void Prebaci_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                Oprema selektovanaOprema = (Oprema)dataGridOprema.SelectedItem;
                Sala trenutnaSala = salaRepository.PregledSalePoNazivu(selektovanaOprema.NazivSale);
                if (selektovanaOprema.Tip == TipOpreme.Dinamicka)
                {
                    OpremaDinPremestanje win = new OpremaDinPremestanje(opreme, selektovanaOprema, trenutnaSala);
                    win.ShowDialog();
                }
                else
                {
                    OpremaStatPremestanje win = new OpremaStatPremestanje(selektovanaOprema, trenutnaSala);
                    win.ShowDialog();
                }
            }
            else { MessageBox.Show("Morate izabrati opremu!"); }
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
                    Prebaci.Focus();
                }
                else if (Prebaci.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (Pomoc.IsFocused)
                {
                    Textbox.Focus();
                }
                else if (Textbox.IsFocused)
                {
                    Staticka.Focus();
                }
                else if (Staticka.IsFocused || Dinamicka.IsFocused || Do5.IsFocused || Do10.IsFocused || Preko10.IsFocused || SveTip.IsFocused || SveKolicina.IsFocused)
                {
                    ComboSala.Focus();
                }
                else if (Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (dataGridOprema.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused && !Izmeni.IsFocused && !Izbrisi.IsFocused && !Prebaci.IsFocused && !Pomoc.IsFocused && !Nazad.IsFocused && !ComboSala.IsFocused)
                {
                    Dodaj.Focus();
                }
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (ComboSala.IsFocused)
                {
                    Staticka.Focus();
                }
                else if (Prebaci.IsFocused)
                {
                    Izbrisi.Focus();
                }
                else if (Staticka.IsFocused || Dinamicka.IsFocused || Do5.IsFocused || Do10.IsFocused || Preko10.IsFocused || SveTip.IsFocused || SveKolicina.IsFocused)
                {
                    Textbox.Focus();
                }
                else if (Textbox.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (Pomoc.IsFocused)
                {
                    Prebaci.Focus();
                }
                else if (Prebaci.IsFocused)
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
                else if (dataGridOprema.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused && !Izmeni.IsFocused && !Izbrisi.IsFocused && !Prebaci.IsFocused && !Pomoc.IsFocused && !Nazad.IsFocused && !ComboSala.IsFocused)
                {
                    Dodaj.Focus();
                }
            }
            else if (e.Key == Key.Escape)
            {
                (this.Parent as Grid).Children.Remove(this);
            }
            else if (e.Key == Key.F1)
            {
                PrikaziPomoc();
            }
            else if (e.Key == Key.F2)
            {
                if (dataGridOprema.SelectedIndex != -1)
                {
                    Oprema selektovanaOprema = (Oprema)dataGridOprema.SelectedItem;
                    Sala trenutnaSala = salaRepository.PregledSalePoNazivu(selektovanaOprema.NazivSale);
                    OpremaIzmena win = new OpremaIzmena(opreme, selektovanaOprema, trenutnaSala);
                    win.ShowDialog();
                }
                else { MessageBox.Show("Morate izabrati opremu!"); }
                Dodaj.Focus();
            }
            else if (e.Key == Key.F3)
            {
                if (dataGridOprema.SelectedIndex != -1)
                {
                    Oprema selektovanaOprema = (Oprema)dataGridOprema.SelectedItem;
                    Sala trenutnaSala = salaRepository.PregledSalePoNazivu(selektovanaOprema.NazivSale);
                    if (selektovanaOprema.Tip == TipOpreme.Dinamicka)
                    {
                        OpremaDinPremestanje win = new OpremaDinPremestanje(opreme, selektovanaOprema, trenutnaSala);
                        win.ShowDialog();
                    }
                    else
                    {
                        OpremaStatPremestanje win = new OpremaStatPremestanje(selektovanaOprema, trenutnaSala);
                        win.ShowDialog();
                    }
                }
                else { MessageBox.Show("Morate izabrati opremu!"); }
                Dodaj.Focus();
            }
            else if (Pomoc.IsFocused || Nazad.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (Prebaci.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Prebaci.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            PrikaziPomoc();
        }

        private void PrikaziPomoc()
        {
            MessageBox.Show(
            "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
            "- M: Selektovanje MenuBar-a - FILE.\n" +
            "- ENTER: Potvrda akcije selektovanog dugmeta. \n" +
            "- ESCAPE: Povratak na pocetni prozor. \n" +
            "- DOWN: Selektovanje tabele kada dugmici iznad tabele imaju fokus. \n" +
            "- LCTRL/RCTRL: Vracanje fokusa na dugmice kada je fokus na tabeli.\n" +
            "- F1: Otvaranje Pomoc dijaloga. \n" +
            "- F2: Otvaranje dijaloga izmene selektovane opreme. \n" +
            "- F3: Otvaranje dijaloga premestanja selektovane opreme. \n" +
            "- DEL: Brisanje selektovane opreme. \n\n" +

            "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                , "HELP");
        }

        private void dataGridOprema_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (dataGridOprema.SelectedIndex != -1)
                {
                    Oprema selektovanaOprema = (Oprema)dataGridOprema.SelectedItem;
                    Sala trenutnaSala = salaRepository.PregledSalePoNazivu(selektovanaOprema.NazivSale);
                    foreach (Oprema opremaBrojac in trenutnaSala.oprema.ToArray())
                    {
                        if (opremaBrojac.Naziv.Equals(selektovanaOprema.Naziv))
                        {
                            trenutnaSala.GetOprema().Remove(opremaBrojac);
                        }
                    }
                    opremaController.BrisanjeOpreme(selektovanaOprema, trenutnaSala);
                    opreme.Remove(selektovanaOprema);
                }
                else { MessageBox.Show("Morate izabrati opremu!"); }
                Dodaj.Focus();
            }
        }
    }
}
