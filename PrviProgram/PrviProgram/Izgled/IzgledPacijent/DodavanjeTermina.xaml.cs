using Model;
using PrviProgram.Repository;
using Repository;
using Service.LekarService;
using Service.PacijentService;
using Service.UpravnikService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for DodavanjeTermina.xaml
    /// </summary>
    public partial class DodavanjeTermina : Window
    {
        private SaleService uprSal;
        private SalaRepository saleRep = new SalaRepository();
        public List<Lekar> lekari = new List<Lekar>();
        public LekarRepository rl = new LekarRepository();
        public Lekar l = new Lekar();
        string[] niz = { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        ObservableCollection<string> niz1 = new ObservableCollection<string> { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        string[] jmbg = new string[10];
        Termin t = new Termin();
        // Lekar l3 = new Lekar();


        public DodavanjeTermina(ObservableCollection<Termin> termini, Pacijent p)
        {
         
            InitializeComponent();

            LekarRepository datoteka = new LekarRepository();
            List<Lekar> lekari = datoteka.CitanjeIzFajla();
            int i = 0;
            foreach (Lekar l in lekari)
            {
               // lekarComboBox.Items.Add(l.ImePrezime);
                jmbg[i] = l.Jmbg;
                i++;
            }
            this.pacijent = p;
            this.term = termini;

          
            vremeText.ItemsSource= niz1;
            this.uprSal = new SaleService();
            lekarComboBox.IsEnabled = false;
            DatumText.IsEnabled = false;
            vremeText.IsEnabled = false;
            TipTerminaText.IsEnabled = false;
            Potvrdi.IsEnabled = false;


        }
        private ObservableCollection<Termin> term;
        private Pacijent pacijent;


        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 0;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }
       
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            

            if(PrioritetComboBox.SelectedIndex == 0)
            {
                this.t.Datum = (DateTime)DatumText.SelectedDate;
                this.t.Vreme = vremeText.Text;
                Model.Lekar l = new Model.Lekar();
                l= (Lekar)lekarComboBox.SelectedItem;
                for(int i=0;i<jmbg.Length;i++)
                {
                    if(lekarComboBox.SelectedIndex==i)
                    {
                        l.Jmbg = jmbg[i];
                        break;
                    }
                }
                List<Sala> sale = new List<Sala>();
                sale = saleRep.PregledSvihSala();

                Sala tempSala = new Sala();
                foreach(Sala s in sale)
                {
                    if (s.Dostupnost.Equals(true) && !s.Naziv.Equals("Magacin"))
                    {
                        tempSala = s;
                        this.t.sala = tempSala;
                        break;
                    }
                }
                this.t.lekar = l;

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var Random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[Random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                this.t.SifraTermina = finalString;
                String tip = TipTerminaText.Text;
                if (tip.Equals("Pregled"))
                {
                    this.t.TipTermina = TipTermina.Pregled;
                }
                if (tip.Equals("Kontrola"))
                {
                    this.t.TipTermina = TipTermina.Kontrola;
                }

                TerminiService.getInstance().DodavanjeTermina(this.t, pacijent);
                this.term.Add(this.t);
            }
           else if (PrioritetComboBox.SelectedIndex == 1)
            {
                this.t.Datum = (DateTime)DatumText.SelectedDate;
                this.t.Vreme = vremeText.Text;
                Model.Lekar l = new Model.Lekar();
                l = (Lekar)lekarComboBox.SelectedItem;
                for (int i = 0; i < jmbg.Length; i++)
                {
                    if (lekarComboBox.SelectedIndex == i)
                    {
                        l.Jmbg = jmbg[i];
                        break;
                    }
                }
                List<Sala> sale = new List<Sala>();
                sale = saleRep.PregledSvihSala();

                Sala tempSala = new Sala();
                foreach (Sala s in sale)
                {
                    if (s.Dostupnost.Equals(true) && !s.Naziv.Equals("Magacin"))
                    {
                        tempSala = s;
                        this.t.sala = tempSala;
                        break;
                    }
                }
                this.t.lekar = l;

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var Random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[Random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                this.t.SifraTermina = finalString;
                String tip = TipTerminaText.Text;
                if (tip.Equals("Pregled"))
                {
                    this.t.TipTermina = TipTermina.Pregled;
                }
                if (tip.Equals("Kontrola"))
                {
                    this.t.TipTermina = TipTermina.Kontrola;
                }

                TerminiService.getInstance().DodavanjeTermina(this.t, pacijent);
                this.term.Add(this.t);
            }
            else
            {

                this.t.Datum = (DateTime)DatumText.SelectedDate;
                this.t.Vreme = vremeText.Text;
                Model.Lekar l = new Model.Lekar();
                l = (Lekar)lekarComboBox.SelectedItem;
                for (int i = 0; i < jmbg.Length; i++)
                {
                    if (lekarComboBox.SelectedIndex == i)
                    {
                        l.Jmbg = jmbg[i];
                        break;
                    }
                }
                List<Sala> sale = new List<Sala>();
                sale = saleRep.PregledSvihSala();

                Sala tempSala = new Sala();
                foreach (Sala s in sale)
                {
                    if (s.Dostupnost.Equals(true) && !s.Naziv.Equals("Magacin"))
                    {
                        tempSala = s;
                        this.t.sala = tempSala;
                        break;
                    }
                }
                this.t.lekar = l;

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var Random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[Random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                this.t.SifraTermina = finalString;
                String tip = TipTerminaText.Text;
                if (tip.Equals("Pregled"))
                {
                    this.t.TipTermina = TipTermina.Pregled;
                }
                if (tip.Equals("Kontrola"))
                {
                    this.t.TipTermina = TipTermina.Kontrola;
                }

                TerminiService.getInstance().DodavanjeTermina(this.t, pacijent);
                this.term.Add(this.t);
            }
            this.Close();


            /*Termin t = new Termin();

             if ( datum == true && vreme == true && tipPregleda == true)
             {
                 t.Datum = (DateTime)(DatumText.SelectedDate);
                 t.Vreme = vremeText.Text;

                 Model.Lekar l = new Model.Lekar();
                 l.ImePrezime = lekarComboBox.SelectedItem.ToString();

                 t.pacijent = this.pacijent;

                 Random rnd = new Random();

                 List<Sala> sale = new List<Sala>();
                 sale = saleRep.PregledSvihSala();

                 Sala tempSala = new Sala();
                 if (sale[0] != null)
                 {
                     tempSala = sale[0];
                     t.sala = tempSala;
                 }

                 t.lekar = l;
                 var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                 var stringChars = new char[8];
                 var Random = new Random();

                 for (int i = 0; i < stringChars.Length; i++)
                 {
                     stringChars[i] = chars[Random.Next(chars.Length)];
                 }

                 var finalString = new String(stringChars);
                 t.SifraTermina = finalString;

                 String tip = TipTerminaText.Text;
                 if (tip.Equals("Pregled"))
                 {
                     t.TipTermina = TipTermina.Pregled;
                 }
                 if (tip.Equals("Kontrola"))
                 {
                     t.TipTermina = TipTermina.Kontrola;
                 }

                 TerminiService.getInstance().DodavanjeTermina(t, pacijent);
                 //term.Add(t);

                // PreglediService.getInstance().DodavanjePregleda(t);
                 this.term.Add(t);

                 this.Close();
             }
             else
             {
                 MessageBox.Show("Niste popunili sva polja!!!",
                "Error");
             }
            */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrioritetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PrioritetComboBox.SelectedIndex==0)
            {
                this.lekari = rl.CitanjeIzFajla();
                
                 lekarComboBox.ItemsSource = lekari;
               
                DatumText.IsEnabled = true;
                lekarComboBox.IsEnabled = true;
                PrioritetComboBox.IsEnabled = false;
            }
            else if(PrioritetComboBox.SelectedIndex==1)
            {
                DatumText.IsEnabled = true;
                vremeText.IsEnabled = true;
                PrioritetComboBox.IsEnabled = false;
            }
            else if(PrioritetComboBox.SelectedIndex==2)
            {
                this.lekari = rl.CitanjeIzFajla();
                lekarComboBox.ItemsSource = lekari;

                DatumText.IsEnabled = true;
                vremeText.IsEnabled = true;
                lekarComboBox.IsEnabled = true;
                PrioritetComboBox.IsEnabled = false;
            }
            else
            {
                DatumText.IsEnabled = false;
                vremeText.IsEnabled = false;
                lekarComboBox.IsEnabled = false;
            }
        }
        public void brisanjeComboBoxova(int[] niz)
        {
            int j = 0;
            int k = 0;
            for (int i = 0; i < niz.Length; i++)
            {
             
                if (niz[i] == 1)
                {
                    j= i+k;
                    niz1.RemoveAt(j);
                    
                    k--;
                    
                }
            }

        }

        private void vreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(vremeText.SelectedItem.ToString().Equals(""))
            {
                if(PrioritetComboBox.SelectedIndex == 1)
                {
                    lekarComboBox.IsEnabled = false;
                }
            }
            else
            {
                if (PrioritetComboBox.SelectedIndex == 1)
                {

                    string str = (string)vremeText.SelectedItem;
                    
                    List<Lekar> lekari2 = TerminiService.getInstance().proveraVremenaKodLekara(str, (DateTime)DatumText.SelectedDate);
                    lekarComboBox.ItemsSource = lekari2;

                    vremeText.IsEnabled = false;
                    lekarComboBox.IsEnabled = true;
                    TipTerminaText.IsEnabled = true;
                }
                else if(PrioritetComboBox.SelectedIndex==2)
                {
                    if(lekarComboBox.SelectedItem==null)
                    {
                        string str = (string)vremeText.SelectedItem;

                        List<Lekar> lekari2 = TerminiService.getInstance().proveraVremenaKodLekara(str, (DateTime)DatumText.SelectedDate);
                        lekarComboBox.ItemsSource = lekari2;

                        vremeText.IsEnabled = false;
                        lekarComboBox.IsEnabled = true;
                        TipTerminaText.IsEnabled = true;
                    }
                }
                
    
                else
                {
                    TipTerminaText.IsEnabled = true;
                    Potvrdi.IsEnabled = true;
                }

            }
        }

        private void lekarComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lekarComboBox.SelectedItem.Equals(""))
            {
                if(PrioritetComboBox.SelectedIndex == 0)
                {
                    vremeText.IsEnabled = false;
                }

            }
            else
            {
                if((PrioritetComboBox.SelectedIndex == 0))
                {
                    int i = lekarComboBox.SelectedIndex;
                    string JMBG = jmbg[i];
                    
                    int[] noviNiz = (int[])TerminiService.getInstance().proveraZauzetostiLekara(JMBG, (DateTime)DatumText.SelectedDate, niz);
                    brisanjeComboBoxova(noviNiz);
                    
                    lekarComboBox.IsEnabled = false;
                    
                    vremeText.IsEnabled = true;
                    TipTerminaText.IsEnabled = true;
                }
                else if(PrioritetComboBox.SelectedIndex==2)
                {
                    if(vremeText.SelectedItem==null)
                    {
                        int i = lekarComboBox.SelectedIndex;
                        string JMBG = jmbg[i];

                        int[] noviNiz = (int[])TerminiService.getInstance().proveraZauzetostiLekara(JMBG, (DateTime)DatumText.SelectedDate, niz);
                        brisanjeComboBoxova(noviNiz);

                        lekarComboBox.IsEnabled = false;

                        vremeText.IsEnabled = true;
                        TipTerminaText.IsEnabled = true;
                    }
                }
                else
                {

                    TipTerminaText.IsEnabled = true;
                    Potvrdi.IsEnabled = true;
                }
            }
        }

        private void TipTerminaText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TipTerminaText.SelectedItem.ToString().Equals(""))
            {
                Potvrdi.IsEnabled = false;
            }
            else
            {
                Potvrdi.IsEnabled = true;
            }
        }
    }
}