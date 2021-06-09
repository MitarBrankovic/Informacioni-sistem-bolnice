using Model;
using PrviProgram.Command;
using PrviProgram.Izgled.IzgledUpravnik;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrviProgram.ViewModel
{
    public class PodesavanjaViewModel : INotifyPropertyChanged
    {
        private bool isToolTipVisible = true;
        private UtilityService utilityService = new UtilityService();
        private Upravnik upravnik;
        private bool pritisnuto = true;
        public UserControl UserControl { get; set; }




        public PodesavanjaViewModel(Upravnik upravnik, UserControl userControl) 
        {
            this.upravnik = upravnik;
            UserControl = userControl;
            command = new RelayCommand(DaExecuteMethod, canExecuteMethod, false);
            neKomanda = new RelayCommand(NeExecuteMethod, canExecuteMethod, false);
            pomocKomanda = new RelayCommand(PomocExecuteMethod, canExecuteMethod, false);
            izmenaKomanda = new RelayCommand(IzmenaExecuteMethod, canExecuteMethod, false);
            nazadKomanda = new RelayCommand(NazadExecuteMethod, canExecuteMethod, false);
        }

        public bool IsToolTipVisible 
        {
            get { return isToolTipVisible; }
            set { isToolTipVisible = value; }      
        }

        public UtilityService UtilityService
        {
            get { return utilityService; }
            set { utilityService = value; }
        }

        public Upravnik Upravnik 
        {
            get { return upravnik; }
            set { upravnik = value; }
        }

        public bool Pritisnuto
        {
            get { return pritisnuto; }
            set { pritisnuto = value; }
        }


        private ICommand command;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand DaDugme
        {
            get { return command; }
            set { command = value; }
        }

        private bool canExecuteMethod(object parametar)
        {
            return true;
        }
        private void DaExecuteMethod(object parametar)
        {
            if (!pritisnuto)
            {
                this.isToolTipVisible = true;
                Style style = new Style(typeof(ToolTip));
                style.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
                style.Seal();
                foreach (Window window in Application.Current.Windows)
                {
                    window.Resources.Remove(typeof(ToolTip));
                    isToolTipVisible = true;
                }
                pritisnuto = true;
            }

        }


        private ICommand neKomanda;
        public ICommand NeDugme
        {
            get { return neKomanda; }
            set { neKomanda = value; }
        }

        private void NeExecuteMethod(object parametar)
        {
            if (pritisnuto)
            {
                MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure you want to disable tooltips?", "Tooltips",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        this.isToolTipVisible = false;

                        Style style = new Style(typeof(ToolTip));
                        style.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
                        style.Seal();

                        foreach (Window window in Application.Current.Windows)
                        {
                            window.Resources.Add(typeof(ToolTip), style);
                            isToolTipVisible = false;
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
                pritisnuto = false;
            }
        }

        private ICommand pomocKomanda;
        public ICommand PomocDugme
        {
            get { return pomocKomanda; }
            set { pomocKomanda = value; }
        }

        private void PomocExecuteMethod(object parametar)
        {
            MessageBox.Show(
            "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
            "- M: Selektovanje MenuBar-a - FILE.\n" +
            "- ENTER: Potvrda akcije selektovanog dugmeta. \n" +
            "- ESCAPE: Povratak na pocetni prozor. \n" +
            "- LCTRL/RCTRL: Vracanje fokusa na dugmice kada je fokus na tabeli.\n" +
            "- F1: Otvaranje Pomoc dijaloga. \n" +

            "- ENTER/SPACE: Zatvaranje ove poruke.\n"
            , "HELP");
        }

        private ICommand izmenaKomanda;
        public ICommand IzmenaDugme
        {
            get { return izmenaKomanda; }
            set { izmenaKomanda = value; }
        }

        private void IzmenaExecuteMethod(object parametar)
        {
            PodesavanjaNalogaUpravnika win = new PodesavanjaNalogaUpravnika(upravnik);
            win.ShowDialog();
        }

        private ICommand nazadKomanda;
        public ICommand NazadDugme
        {
            get { return nazadKomanda; }
            set { nazadKomanda = value; }
        }

        private void NazadExecuteMethod(object parametar)
        {
            (UserControl.Parent as Grid).Children.Remove(UserControl);
        }
    }
}
