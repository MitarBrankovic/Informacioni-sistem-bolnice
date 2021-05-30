using System;
using System.Windows;

namespace PrviProgram
{
    public partial class App : Application
    {
        public ResourceDictionary ThemeDictionary
        {
            get { return Current.Resources.MergedDictionaries[0]; }
        }

        public void ChangeTheme(Uri uri)
        {
            ThemeDictionary.MergedDictionaries.Clear();
            ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }

    }
}
