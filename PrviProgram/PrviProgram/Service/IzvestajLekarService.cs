using Model;
using PrviProgram.Repository;
using Repository;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Service
{
    public class IzvestajLekarService : IzvestajAbstractService
    {

        private TerminiRepository terminiRepository = new TerminiRepository();
        private PacijentRepository pacijentRepository = new PacijentRepository();

        public override void IzgenerisiIzvestaj(string sifra)
        {
            Termin termin = terminiRepository.PregledTermina(sifra);
            IzvrseniPregled izvrseniPregled = pacijentRepository.PregledIzvrsenogPregleda(sifra);

            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
                string textPDF = "Lekar: " + termin.lekar +
                    "\nPacijent: " + termin.pacijent + "\n\n\n" +
                    "----------------------------------------------------------------" +
                    "\nAnamneza: \n" + izvrseniPregled.anamneza.Opis + "\n\n" +
                    "Recept: \n" + izvrseniPregled.recept.Lekovi +
                    "\nKonzumirati " + izvrseniPregled.recept.VremenskiPeriodUzimanjaLeka + " dana"
                    + "\n" + izvrseniPregled.recept.OpisLeka;
                graphics.DrawString(textPDF, font, PdfBrushes.Black, new PointF(0, 0));
                doc.Save(@"..\..\..\Izvestaji\IzvestajAnamnezaRecept.pdf");
                doc.Close(true);
            }
        }
    }
}
