using Controller;
using Model;
using PrviProgram.Repository;
using Service;
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

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class SaleSpajanje : Window
    {
        private Sala selektovanaSala;
        private SaleController saleController = new SaleController();
        private SalaRepository salaRepository = new SalaRepository();

        public SaleSpajanje(Sala sala)
        {
            InitializeComponent();
            this.selektovanaSala = sala;
            PrikazPodatakaOpreme();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (PocetakRenoviranja.Text == "" && KrajRenoviranja.Text == "" && TextNaziv.Text == "" && TextSifra.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else
            {
                DateTime pocetak = (DateTime)PocetakRenoviranja.SelectedDate;
                DateTime kraj = (DateTime)KrajRenoviranja.SelectedDate;
                if (DateTime.Compare(pocetak, kraj) > 0)
                {
                    MessageBox.Show("Datumi nisu dobro selektovani!", "Greska");
                }
                else 
                {
                    TerminRenoviranjaSale terminRenoviranjaSale = FormiranjeTerminaRenoviranja();
                    if (terminRenoviranjaSale.PrvaSala.Sprat == terminRenoviranjaSale.DrugaSala.Sprat)
                    {
                        if (!saleController.RenoviranjeSale(terminRenoviranjaSale))
                            MessageBox.Show("Ponovo izaberite datume");
                    }
                    else MessageBox.Show("Sale nisu na istom spratu");
                }
            }
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private TerminRenoviranjaSale FormiranjeTerminaRenoviranja()
        {
            Sala spojenaSala = new Sala(selektovanaSala.Tip, TextNaziv.Text, selektovanaSala.Sprat, TextSifra.Text);
            spojenaSala.oprema = selektovanaSala.oprema;
            foreach (Oprema opremaBrojac in spojenaSala.oprema)
            {
                opremaBrojac.NazivSale = spojenaSala.Naziv;
            }
            TerminRenoviranjaSale terminRenoviranjaSale = new TerminRenoviranjaSale(spojenaSala, (DateTime)(PocetakRenoviranja.SelectedDate),
                (DateTime)(KrajRenoviranja.SelectedDate), selektovanaSala, (Sala)ComboSala.SelectedItem);
            terminRenoviranjaSale.TipRenoviranja = TipRenoviranja.spajanje;
            return terminRenoviranjaSale;
        }

        private void PrikazPodatakaOpreme()
        {
            TrenutnaSala.Text = selektovanaSala.Naziv;
            List<Sala> comboSale = salaRepository.PregledSvihSala();
            foreach (Sala salaBrojac in comboSale.ToArray())
            {
                if (salaBrojac.Sifra.Equals(selektovanaSala.Sifra))
                {
                    comboSale.Remove(salaBrojac);
                    break;
                }
            }
            ComboSala.ItemsSource = comboSale;
        }
    }
}
