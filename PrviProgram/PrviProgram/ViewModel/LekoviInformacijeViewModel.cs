using Model;
using PrviProgram.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrviProgram.ViewModel
{
    public class LekoviInformacijeViewModel : INotifyPropertyChanged
    {
        public Lek Lek { get; set; }
        public Window Window { get; set; }

        public LekoviInformacijeViewModel(Lek lek, Window window) 
        {
            Lek = lek;
            Window = window;
            //PrikazPodataka();
            odustaniKomanda = new RelayCommand(OdustaniExecuteMethod, canExecuteMethod, false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool canExecuteMethod(object parametar)
        {
            return true;
        }


        private ICommand odustaniKomanda;
        public ICommand OdustaniDugme
        {
            get { return odustaniKomanda; }
            set { odustaniKomanda = value; }
        }

        private void OdustaniExecuteMethod(object parametar)
        {
            Window.Close();
        }
    }
}
