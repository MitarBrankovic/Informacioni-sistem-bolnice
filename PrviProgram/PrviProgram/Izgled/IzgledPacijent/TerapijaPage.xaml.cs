using Model;
using Repository;
using Service;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for TerapijaPage.xaml
    /// </summary>
    public partial class TerapijaPage : Page
    {
        private List<Terapija> terapije = new List<Terapija>();
        private TerapijaRepository terapijaRepository = new TerapijaRepository();
        private DateTime ponedeljak, utorak, sreda, cetvrtak, petak, subota, nedelja;
        private Pacijent pacijent;

        public TerapijaPage(Pacijent pacijent)
        {
            InitializeComponent();
            this.terapije = terapijaRepository.PregledSvihTerapijaKodPacijenta(pacijent);
            this.pacijent = pacijent;
            PronalazenjeDana();
            IspisivanjeTerapije();
            odKogDoKogDatum.Text = ponedeljak.ToShortDateString() + " - " + nedelja.ToShortDateString();

        }

        private void Izveštaj_Click(object sender, RoutedEventArgs e)
        {
            IzvestajAbstractService izvestajAbstractService = new IzvestajPacijentService();
            izvestajAbstractService.IzgenerisiIzvestaj(pacijent.Jmbg);
        }

        public void IspisivanjeTerapije()
        {
           
            foreach(Terapija terapija in terapije)
            {
                if (terapija.PocetakTerapije.Date <= ponedeljak.Date && terapija.KrajTerapije.Date>=ponedeljak.Date) ponedeljakText.Text +="- "+ terapija.OpisTerapije +"\n" ;
                if (terapija.PocetakTerapije <= utorak && terapija.KrajTerapije >= utorak) utorakText.Text += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije <= sreda && terapija.KrajTerapije >= sreda) sredaText.Text += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije <= cetvrtak && terapija.KrajTerapije >= cetvrtak) cetvrtakText.Text += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije <= petak && terapija.KrajTerapije >= petak) petakText.Text += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije <= subota && terapija.KrajTerapije >= subota) subotaText.Text += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije.Date <= nedelja.Date && terapija.KrajTerapije.Date >= nedelja.Date) nedeljaText.Text += "- " + terapija.OpisTerapije + "\n";

            }
        }

        public void PronalazenjeDana()
        {
            if(DateTime.Today.DayOfWeek==DayOfWeek.Monday)
            {
                ponedeljak = DateTime.Today;
                utorak = DateTime.Today.AddDays(1);
                sreda = DateTime.Today.AddDays(2);
                cetvrtak = DateTime.Today.AddDays(3);
                petak = DateTime.Today.AddDays(4);
                subota = DateTime.Today.AddDays(5);
                nedelja = DateTime.Today.AddDays(6);
            }
            else if(DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
            {
                ponedeljak = DateTime.Today.AddDays(-1);
                utorak = DateTime.Today;
                sreda = DateTime.Today.AddDays(1);
                cetvrtak = DateTime.Today.AddDays(2);
                petak = DateTime.Today.AddDays(3);
                subota = DateTime.Today.AddDays(4);
                nedelja = DateTime.Today.AddDays(5);

            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday)
            {
                ponedeljak = DateTime.Today.AddDays(-2);
                utorak = DateTime.Today.AddDays(-1);
                sreda = DateTime.Today;
                cetvrtak = DateTime.Today.AddDays(1);
                petak = DateTime.Today.AddDays(2);
                subota = DateTime.Today.AddDays(3);
                nedelja = DateTime.Today.AddDays(4);

            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Thursday)
            {
                ponedeljak = DateTime.Today.AddDays(-3);
                utorak = DateTime.Today.AddDays(-2);
                sreda = DateTime.Today.AddDays(-1);
                cetvrtak = DateTime.Today;
                petak = DateTime.Today.AddDays(1);
                subota = DateTime.Today.AddDays(2);
                nedelja = DateTime.Today.AddDays(3);

            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
            {
                ponedeljak = DateTime.Today.AddDays(-4);
                utorak = DateTime.Today.AddDays(-3);
                sreda = DateTime.Today.AddDays(-2);
                cetvrtak = DateTime.Today.AddDays(-1);
                petak = DateTime.Today;
                subota = DateTime.Today.AddDays(1);
                nedelja = DateTime.Today.AddDays(2);

            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday)
            {
                ponedeljak = DateTime.Today.AddDays(-5);
                utorak = DateTime.Today.AddDays(-4);
                sreda = DateTime.Today.AddDays(-3);
                cetvrtak = DateTime.Today.AddDays(-2);
                petak = DateTime.Today.AddDays(-1);
                subota = DateTime.Today;
                nedelja = DateTime.Today.AddDays(1);

            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                ponedeljak = DateTime.Today.AddDays(-6);
                utorak = DateTime.Today.AddDays(-5);
                sreda = DateTime.Today.AddDays(-4);
                cetvrtak = DateTime.Today.AddDays(-3);
                petak = DateTime.Today.AddDays(-2);
                subota = DateTime.Today.AddDays(-1);
                nedelja = DateTime.Today;

            }
        }
    }
}
