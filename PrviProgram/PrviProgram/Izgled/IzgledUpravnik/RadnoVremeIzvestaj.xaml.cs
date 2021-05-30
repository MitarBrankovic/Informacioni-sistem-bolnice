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
    /// <summary>
    /// Interaction logic for RadnoVremeIzvestaj.xaml
    /// </summary>
    public partial class RadnoVremeIzvestaj : Window
    {
        public RadnoVremeIzvestaj()
        {
            InitializeComponent();
        }

        private void Konvertuj_Click(object sender, RoutedEventArgs e)
        {
            /*using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
                String textPDF = getText();
                //Draw the text
                graphics.DrawString(textPDF, font, PdfBrushes.Black, new PointF(0, 0));

                //Save the document
                document.Save("output8.pdf");


            }
            System.Diagnostics.Process.Start(@"output8.pdf");*/
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
