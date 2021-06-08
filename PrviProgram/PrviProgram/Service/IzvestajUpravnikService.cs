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
    public class IzvestajUpravnikService : IzvestajAbstractService
    {
        private LekarRepository lekarRepository = new LekarRepository();
        private RadnoVremeLekaraRepository radnoVremeLekaraRepository = new RadnoVremeLekaraRepository();
        private List<RadnoVremeLekara> pomocnaLista = new List<RadnoVremeLekara>();

        public override void FormiranjePDF(string sifra)
        {
            using (PdfDocument doc = new PdfDocument())
            {
                Lekar lekar = lekarRepository.PregledLekara(sifra);
                foreach (RadnoVremeLekara r in radnoVremeLekaraRepository.PregledSvihRadnihVremena()) 
                {
                    if (r.Lekar.Jmbg == lekar.Jmbg)
                    {
                        pomocnaLista.Add(r);
                    }
                }
                PdfPage page = doc.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
                string textPDF = "Radno vreme" + " " + "dr." + " " + lekar.Ime + " " + lekar.Prezime;
                graphics.DrawString(textPDF, font, PdfBrushes.Black, new PointF(180, 0));

                PdfLightTable pdfLightTable = new PdfLightTable();
                DataTable table = new DataTable();
                table.Columns.Add("Lekar");
                table.Columns.Add("Pocetni datum");
                table.Columns.Add("Krajnji datum");
                table.Columns.Add("Pocetno vreme");
                table.Columns.Add("Krajnje vreme");
                /*table.Columns[0].ColumnName = "Lekar";
                table.Columns[1].ColumnName = "Pocetni datum";
                table.Columns[2].ColumnName = "Krajnji datum";
                table.Columns[3].ColumnName = "Pocetno vreme";
                table.Columns[4].ColumnName = "Krajnje vreme";*/
                table.Rows.Add(new string[] { "Lekar", "Pocetni datum", "Krajnji datum", "Pocetno vreme", "Krajnje vreme" });
                foreach (RadnoVremeLekara radnoVreme in pomocnaLista)
                {
                    table.Rows.Add(new string[] { radnoVreme.Lekar.Ime+" "+radnoVreme.Lekar.Prezime, radnoVreme.PocetniDatum.ToString(),
                        radnoVreme.KrajnjiDatum.ToString(), radnoVreme.PocetakRadnogVremena.ToString(), radnoVreme.KrajRadnogVremena.ToString() });
                }

                pdfLightTable.DataSource = table;
                pdfLightTable.Draw(page, new PointF(0, 100));
                doc.Save(@"..\..\..\Izvestaji\IzvestajRadnoVremeLekara.pdf");
                doc.Close(true);
            }
        }

        public override void PosaljiPoruku()
        {
            MessageBox.Show("Uspesno kreiran izvestaj radnog vremena lekara!");
        }
    }
}
