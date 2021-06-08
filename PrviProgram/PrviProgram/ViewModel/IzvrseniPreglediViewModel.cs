using Model;
using PrviProgram.Command;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace PrviProgram.ViewModel
{
    public class IzvrseniPreglediViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IzvrseniPregled _izvrseniPregled;

        private IzvrseniPregled pregledSaBeleskom;
        
        private Pacijent selektovaniPacijent;
        private  String tekstBeleska;
     

        public IzvrseniPreglediViewModel(IzvrseniPregled selektovanaAnamneza, ObservableCollection<IzvrseniPregled> izvrseniPregledi, Pacijent selektovaniPacijent)
        {
            this._izvrseniPregled = selektovanaAnamneza;
            this.izvrseniPregleds = izvrseniPregledi;
            this.pregledSaBeleskom = selektovanaAnamneza;
            this.selektovaniPacijent = selektovaniPacijent;

        }
        public String TeksBeleska
        {
            get
            {
                return tekstBeleska;
            }
            set
            {
                tekstBeleska = value;
            }
        }
        public IzvrseniPregled PregledSaBeleskom
        {
            get
            {
                return pregledSaBeleskom;
            }
            set
            {
                pregledSaBeleskom = value;
            }
        }
        public Pacijent SelektovaniPacijent
        {
            get
            {
                return selektovaniPacijent;
            }
            set
            {
                selektovaniPacijent = value;
            }
        }
        public IzvrseniPregled SelektovanaAnamneza
        {
            get { return _izvrseniPregled; }
            set { _izvrseniPregled = value; NotifyPropertyChanged("IzvrseniPregled"); }
        }
        ObservableCollection<IzvrseniPregled> izvrseniPregleds;
        public ObservableCollection<IzvrseniPregled> IzvrseniPregledi
        {
            get
            {
                return izvrseniPregleds;
            }
            set
            {
                izvrseniPregleds = value;
                NotifyPropertyChanged("IzvrseniPregledi");
            }
        }
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private ICommand command;
        public ICommand MyCommand
        {
            get
            {
                if(command==null)
                {
                    command = new RelayCommand(ExecuteMethod, canExecuteMethod,false);
                }
                return command;
            }
        }

       
        private bool canExecuteMethod(object parametar)
        {
            return true;
        }
        private void ExecuteMethod(object parametar)
        {
           this.IzvrseniPregledi.Remove(this.SelektovanaAnamneza);
             this.pregledSaBeleskom.Beleska = tekstBeleska;
            if (KartonPacijentaService.getInstance().IzmenaIzvrsenogPregleda(pregledSaBeleskom, this.selektovaniPacijent))
            {
                this.IzvrseniPregledi.Add(this.pregledSaBeleskom);
            }
            
        }
    }
}
