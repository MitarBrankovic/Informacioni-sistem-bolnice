using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Service
{
    public class UtilityService
    {
        public List<string> nizVremena = new List<string> { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00",
                                         "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00",
                                         "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00",
                                         "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00",
                                         "18:00:00", "18:30:00", "19:00:00", "19:30:00" };

        public UtilityService() { }

        public string GenerisanjeSifre()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var Random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }

        public bool IsNumber(String st)
        {
            try
            {
                int.Parse(st);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int IntParser(string broj)
        {
            int retVal;
            return int.TryParse(broj, out retVal) ? retVal : default;
        }

        public void LogOutUpravnikaUserControl(UserControl user)
        {
            MessageBoxResult rsltMessageBox = MessageBox.Show("Da li ste sigurni da zelite da se izlogujete?", "Log out",
        MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    Window parentWindow = Window.GetWindow(user);
                    parentWindow.Close();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        public void InicijalizacijaToolTipa(object sender)
        {
            ToolTip tooltip = (ToolTip)(sender as Control).ToolTip;
            tooltip.PlacementTarget = (UIElement)sender;
            tooltip.Placement = PlacementMode.Right;
            tooltip.PlacementRectangle = new Rect(0, (sender as Control).Height, 0, 0);
            tooltip.IsOpen = (sender as Control).IsKeyboardFocusWithin;
        }

    }
}
