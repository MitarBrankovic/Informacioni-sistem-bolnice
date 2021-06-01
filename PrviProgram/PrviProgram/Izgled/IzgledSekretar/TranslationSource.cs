using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Data;

namespace PrviProgram.Izgled.IzgledSekretar
{
    public class TranslationSource : INotifyPropertyChanged
    {
        private static readonly TranslationSource instance = new TranslationSource();

        public static TranslationSource Instance
        {
            get { return instance; }
        }

        private readonly ResourceManager resManager = new ResourceManager("PrviProgram.Izgled.IzgledSekretar.Resources.language", Assembly.GetExecutingAssembly());
        private CultureInfo currentCulture = null;

        public string this[string key]
        {
            get { return this.resManager.GetString(key, this.currentCulture); }
        }

        public CultureInfo CurrentCulture
        {
            get { return this.currentCulture; }
            set
            {
                if (this.currentCulture != value)
                {
                    this.currentCulture = value;
                    var @event = this.PropertyChanged;
                    if (@event != null)
                    {
                        @event.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class LocExtension
        : Binding
    {
        public LocExtension(string name)
            : base("[" + name + "]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = TranslationSource.Instance;
        }
    }
}

// resgen.exe language.sr-LATN.resx
// al -target:lib -embed:language.sr-LATN.resources -culture:culture -out:sr-LATN/PrviProgram.resources.dll
// resgen.exe language.en-US.resx
// al -target:lib -embed:language.en-US.resources -culture:culture -out:en-US/PrviProgram.resources.dll

