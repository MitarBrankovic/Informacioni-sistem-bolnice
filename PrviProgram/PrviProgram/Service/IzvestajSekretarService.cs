using System;
using System.Data;
using System.Drawing;
using Model;
using PrviProgram.Repository;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;

namespace Service
{
    public class IzvestajSekretarService : IzvestajAbstractService
    {
        TerminiRepository terminiRepository = new TerminiRepository();
        private DateTime odDana, doDana;

        public override void IzgenerisiIzvestaj(string sifra)
        {
            using (PdfDocument doc = new PdfDocument())
            {
                OdredjivanjeDatuma();
                PdfPage page = doc.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
                string textPDF = "Nedeljni izvestaj o zakazanim operacija i pregleda " + odDana.ToShortDateString() + " - " + doDana.ToShortDateString();
                graphics.DrawString(textPDF, font, PdfBrushes.Black, new PointF(70, 0));
                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();
                table.Columns.Add("Sifra");
                table.Columns.Add("Datum");
                table.Columns.Add("Vreme");
                table.Columns.Add("Pacijent");
                table.Columns.Add("Lekar");
                table.Columns.Add("Sala");
                table.Columns.Add("Tip termina");
                foreach (Termin termin in terminiRepository.PregledSvihTerminaUPeriodu(odDana, doDana))
                {
                    if (termin.guestPacijent == null)
                    {
                        table.Rows.Add(new string[] {termin.SifraTermina, termin.Datum.ToString("d"), termin.Vreme, termin.pacijent.ToString(), termin.lekar.ToString(), termin.sala.ToString(), termin.TipTermina.ToString()});
                    } 
                    else
                    {
                        table.Rows.Add(new string[] {termin.SifraTermina, termin.Datum.ToString("d"), termin.Vreme, termin.guestPacijent.ToString(), termin.lekar.ToString(), termin.sala.ToString(), termin.TipTermina.ToString()});
                    }
                }
                pdfLightTable.DataSource = table;
                pdfLightTable.Style.ShowHeader = true;
                pdfLightTable.Draw(page, new PointF(0, 100));
                doc.Save(@"..\..\..\Izvestaji\IzvestajZakazanihTermina.pdf");
                doc.Close(true);
            }
        }

        private void OdredjivanjeDatuma()
        {
            odDana = DateTime.Today;
            doDana = DateTime.Today.AddDays(6);
        }

    }
}
