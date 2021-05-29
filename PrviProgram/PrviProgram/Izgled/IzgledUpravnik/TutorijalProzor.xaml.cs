using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    /// <summary>
    /// Interaction logic for TutorijalProzor.xaml
    /// </summary>
    public partial class TutorijalProzor : UserControl
    {
        private DispatcherTimer timer;
        bool isDragging = false;
        public TutorijalProzor()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                timelineSlider.Value = Player.Position.TotalSeconds;
            }
        }

         private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
            "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
            "- ENTER: Potvrda akcije selektovanog dugmeta. \n" +
            "- ESCAPE: Povratak na pocetni prozor. \n" +
            "- LCTRL/RCTRL: Vracanje fokusa na dugmice kada je fokus na tabeli.\n" +
            "- F1: Otvaranje Pomoc dijaloga. \n" +

            "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                , "HELP");
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        private void Button_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void timelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Position = TimeSpan.FromSeconds(timelineSlider.Value);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video files (*.mpg; *.mpeg; *.avi; *.mp4)| *.mpg; *.mpeg; *.avi; *.mp4";
            if (openFileDialog.ShowDialog() == true)
            {
                Player.Source = new Uri(openFileDialog.FileName);
            }
            Player.Play();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Player.Play();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            Player.Pause();
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Volume = (double)volumeSlider.Value;
        }

        private void Player_MediaEnded(object sender, RoutedEventArgs e)
        {
            Player.Stop();
        }

        private void timelineSlider_DragStarted(object sender, DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void timelineSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            isDragging = false;
            Player.Position = TimeSpan.FromSeconds(timelineSlider.Value);
        }

        private void Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (Player.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = Player.NaturalDuration.TimeSpan;
                timelineSlider.Maximum = ts.TotalSeconds;
                timelineSlider.SmallChange = 1;
                timelineSlider.LargeChange = Math.Min(10, ts.Seconds / 10);
            }
            timer.Start();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Open.Focus();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.RightCtrl)
            {
                if (Open.IsFocused)
                {
                    Play.Focus();
                }
                else if (Play.IsFocused)
                {
                    Pause.Focus();
                }
                else if (Pause.IsFocused)
                {
                    Stop.Focus();
                }
                else if (Stop.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (Nazad.IsFocused)
                {
                    Open.Focus();
                }
                else if (!Pomoc.IsFocused || !Nazad.IsFocused || !Open.IsFocused || !Pause.IsFocused || !Stop.IsFocused || !Play.IsFocused)
                {
                    Open.Focus();
                }
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused)
                {
                    Stop.Focus();
                }
                else if (Stop.IsFocused)
                {
                    Pause.Focus();
                }
                else if (Pause.IsFocused)
                {
                    Play.Focus();
                }
                else if (Play.IsFocused)
                {
                    Open.Focus();
                }
                else if (Open.IsFocused)
                {
                    Nazad.Focus();
                }
                else if (!Pomoc.IsFocused || !Nazad.IsFocused || !Open.IsFocused || !Pause.IsFocused || !Stop.IsFocused || !Play.IsFocused)
                {
                    Open.Focus();
                }
            }
            else if (e.Key == Key.Escape)
            {
                (this.Parent as Grid).Children.Remove(this);
            }
            else if (e.Key == Key.F1)
            {
                MessageBox.Show(
                "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
                "- ENTER: Potvrda akcije selektovanog dugmeta. \n" +
                "- ESCAPE: Povratak na pocetni prozor. \n" +
                "- LCTRL/RCTRL: Vracanje fokusa na dugmice kada je fokus na tabeli.\n" +
                "- F1: Otvaranje Pomoc dijaloga. \n" +

                "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                    , "HELP");
            }
            else if (Pomoc.IsFocused || Nazad.IsFocused || Open.IsFocused || Pause.IsFocused || Stop.IsFocused || Play.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (e.Key == Key.Space || e.Key == Key.N)
                e.Handled = true;
            else if (e.Key == Key.S)
            {
                timelineSlider.Focus();
            }
            else if (e.Key == Key.V)
            {
                volumeSlider.Focus();
            }
        }
    }
}
