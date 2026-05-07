using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FrontEnd.Dialogs
{
    /// <summary>
    /// Logique d'interaction pour VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : Window
    {
        private DispatcherTimer mainDispatcher = new();
        private bool isPlaying = false;
        public VideoPlayer()
        {
            InitializeComponent();

            mainDispatcher.Tick += Timer_tick;
            mainDispatcher.Interval = TimeSpan.FromSeconds(1);
            mainDispatcher.Start();
        }

        private void Timer_tick(object? sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                string max_duration = mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                txt_duration.Text = $"{mediaPlayer.Position.ToString(@"mm\:ss") } / {max_duration}";
            }
        }

        private void Video_Ended(object? sender, EventArgs e)
        {
            if (mediaPlayer.Source != null)
            {
                mainDispatcher.Stop();
            }
        }

        private void Main_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                mediaPlayer.Pause();
                mainDispatcher.Stop();
                isPlaying = false;
                MainButton.Content = "▶";
                
            }
            else
            {
                mediaPlayer.Play();
                mainDispatcher.Start();
                isPlaying = true;
                MainButton.Content = "⏸️";
            }
        }

        private void End_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                mediaPlayer.Pause();
                mediaPlayer.Position = mediaPlayer.NaturalDuration.TimeSpan - TimeSpan.FromMilliseconds(1);
                mediaPlayer.Play();
                isPlaying = false;
            }   
        }

        private void Beginning_Button_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
            mediaPlayer.Play();
            isPlaying = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainDispatcher.Stop();
            mainDispatcher.IsEnabled = false;
        }
    }
}
