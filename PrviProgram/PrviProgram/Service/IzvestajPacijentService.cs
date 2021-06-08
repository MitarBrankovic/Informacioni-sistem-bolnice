using Model;
using Repository;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;

namespace Service
{
    public class IzvestajPacijentService : IzvestajAbstractService
    {
        private List<Terapija> terapije;
        private TerapijaRepository terapijaRepository = new TerapijaRepository();
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private DateTime ponedeljak, utorak, sreda, cetvrtak, petak, subota, nedelja;
        private String ponedeljakText, utorakText, sredaText, cetvrtakText, petakText, subotaText, nedeljaText;

        public override void FormiranjePDF(string sifra)
        {
            Pacijent pacijent = pacijentRepository.PregledPacijenta(sifra);
            PronalazenjeDana();
            IspisivanjeTerapije(sifra);

            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
                string textPDF = "Sedmicna terapija " + ponedeljak.ToShortDateString() + " - " + nedelja.ToShortDateString() + " \n Pacijenta: " + pacijent.Ime + " " + pacijent.Prezime;
                graphics.DrawString(textPDF, font, PdfBrushes.Black, new PointF(180, 0));

                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();
                table.Columns.Add("Ponedeljak");
                table.Columns.Add("Utorak");
                table.Columns.Add("Sreda");
                table.Columns.Add("Cetvrtak");
                table.Columns.Add("Petak");
                table.Columns.Add("Subota");
                table.Columns.Add("Nedelja");
                table.Rows.Add(new string[] { "Ponedeljak", "Utorak", "Sreda", "Cetvrtak", "Petak", "Subota", "Nedelja" });
                table.Rows.Add(new string[] { ponedeljakText, utorakText, sredaText, cetvrtakText, petakText, subotaText, nedeljaText });


                pdfLightTable.DataSource = table;
                pdfLightTable.Draw(page, new PointF(0, 100));
                doc.Save(@"..\..\..\Izvestaji\IzvestajTerapija.pdf");
                doc.Close(true);
            }
        }

        public void IspisivanjeTerapije(string sifra)
        {
            Pacijent pacijent = pacijentRepository.PregledPacijenta(sifra);
            terapije = terapijaRepository.PregledSvihTerapijaKodPacijenta(pacijent);
            foreach (Terapija terapija in terapije)
            {
                if (terapija.PocetakTerapije.Date <= ponedeljak.Date && terapija.KrajTerapije.Date >= ponedeljak.Date) ponedeljakText += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije <= utorak && terapija.KrajTerapije >= utorak) utorakText += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije <= sreda && terapija.KrajTerapije >= sreda) sredaText += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije <= cetvrtak && terapija.KrajTerapije >= cetvrtak) cetvrtakText += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije <= petak && terapija.KrajTerapije >= petak) petakText += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije <= subota && terapija.KrajTerapije >= subota) subotaText += "- " + terapija.OpisTerapije + "\n";
                if (terapija.PocetakTerapije.Date <= nedelja.Date && terapija.KrajTerapije.Date >= nedelja.Date) nedeljaText += "- " + terapija.OpisTerapije + "\n";

            }
        }

        public override void PosaljiPoruku()
        {
            MessageBox.Show("Uspesno kreiran izvestaj terapija na sedmicnom nivou");
        }

        private void PronalazenjeDana()
        {
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                ponedeljak = DateTime.Today;
                utorak = DateTime.Today.AddDays(1);
                sreda = DateTime.Today.AddDays(2);
                cetvrtak = DateTime.Today.AddDays(3);
                petak = DateTime.Today.AddDays(4);
                subota = DateTime.Today.AddDays(5);
                nedelja = DateTime.Today.AddDays(6);
            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
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
