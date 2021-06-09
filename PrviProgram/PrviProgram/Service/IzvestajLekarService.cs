using Model;
using PrviProgram.Repository;
using Repository;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;

namespace Service
{
    public class IzvestajLekarService : IzvestajAbstractService
    {

        private TerminiRepository terminiRepository = new TerminiRepository();
        private PacijentRepository pacijentRepository = new PacijentRepository();

        public override void FormiranjePDF(string sifra)
        {
            Termin termin = terminiRepository.PregledTermina(sifra);
            IzvrseniPregled izvrseniPregled = pacijentRepository.PregledIzvrsenogPregleda(sifra);

            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
                PdfFont naslov = new PdfStandardFont(PdfFontFamily.Helvetica, 30);

                string naslovPDF = "IZVESTAJ O ANAMNEZI I RECEPTU" + "\n\n\n";
                graphics.DrawString(naslovPDF, font, PdfBrushes.Black, new PointF(0, 0));

                string textPDF = "\n\n\n\n" + "Lekar: " + termin.lekar +
                    "\nPacijent: " + termin.pacijent +
                    "\nDatum: " + izvrseniPregled.Datum.ToString("d") + "\n\n\n" +
                    "-------------------------------------------------------------------" +
                    "\n\nAnamneza: \n\n" + izvrseniPregled.anamneza.Opis + "\n\n\n" +
                    "Recept: \n\n" + izvrseniPregled.recept.Lekovi +
                    "\nKonzumirati " + izvrseniPregled.recept.VremenskiPeriodUzimanjaLeka + " dana"
                    + "\n" + izvrseniPregled.recept.OpisLeka;
                graphics.DrawString(textPDF, font, PdfBrushes.Black, new PointF(0, 0));
                doc.Save(@"..\..\..\Izvestaji\IzvestajAnamnezaRecept.pdf");
                doc.Close(true);
            }
        }

        public override void PosaljiPoruku()
        {
            MessageBox.Show("Uspesno obavljen izvestaj");
        }
    }
}
