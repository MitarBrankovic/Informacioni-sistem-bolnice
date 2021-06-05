using Model;
using Repository;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
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
    public partial class RadnoVremeIzvestaj : Window
    {
        private ObservableCollection<RadnoVremeLekara> radnaVremena;
        private ObservableCollection<RadnoVremeLekara> pomocnaLista;
        private LekarRepository lekarRepository = new LekarRepository();

        public RadnoVremeIzvestaj(ObservableCollection<RadnoVremeLekara> radnaVremena)
        {
            InitializeComponent();
            this.radnaVremena = radnaVremena;
            pomocnaLista = new ObservableCollection<RadnoVremeLekara>(radnaVremena);
            InicijalizacijaCombo();
        }

        private void InicijalizacijaCombo()
        {
            ComboLekar.ItemsSource = lekarRepository.PregledSvihLekara();
        }

        private void Konvertuj_Click(object sender, RoutedEventArgs e)
        {
            Lekar lekar = (Lekar)ComboLekar.SelectedItem;

            foreach (RadnoVremeLekara r in radnaVremena)
            {
                if (r.Lekar.ToString() != lekar.ToString())
                {
                    pomocnaLista.Remove(r);
                }
            }

            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
                string textPDF = "Radno vreme"+ " " + "dr." + " " + lekar.Ime + " " + lekar.Prezime;
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
                table.Rows.Add(new string[] { "Lekar", "Pocetni datum" ,"Krajnji datum" ,"Pocetno vreme" ,"Krajnje vreme"});
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
            MessageBox.Show("Uspesno kreiran izvestaj radnog vremena lekara, mozete ga pogledati u tekucem direktorijumu");
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
    }
}
