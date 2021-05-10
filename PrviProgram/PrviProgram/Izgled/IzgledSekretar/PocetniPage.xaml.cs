using System.Windows.Controls;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar
{
    public partial class PocetniPage : Page
    {
        public Sekretar Sekretar { get; set; }

        public PocetniPage(Sekretar sekretar)
        {
            InitializeComponent();
            this.Sekretar = sekretar;
            DataContext = this;
        }
    }
}
