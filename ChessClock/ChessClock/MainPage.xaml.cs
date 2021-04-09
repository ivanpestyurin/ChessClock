using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.SimpleAudioPlayer;


namespace ChessClock
{
    public partial class MainPage : ContentPage
    {
        PlayerTimer playerTimer1;
        PlayerTimer playerTimer2;
        //ISimpleAudioPlayer kickSound;
        //ISimpleAudioPlayer clapSound;
        public MainPage(int mainTimeMin, int additionalTimSec)
        {
            playerTimer1 = new PlayerTimer(mainTimeMin, additionalTimSec);
            playerTimer2 = new PlayerTimer(mainTimeMin, additionalTimSec);

            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 100), Callback);

            InitializeComponent();
        }

        private bool Callback()
        {
            playerTimer1.CountTime();
            playerTimer2.CountTime();

            if (playerTimer1.Time.TotalMilliseconds <= 0 || playerTimer2.Time.TotalMilliseconds <= 0)
            {
                ExitGame();
                return false;
            }

            PrintTime(playerTimer1, button_first_player);
            PrintTime(playerTimer2, button_second_player);

            return true;
        }

        private void PrintTime(PlayerTimer playerTimer, Button button)
        {
            if (playerTimer.Time.TotalSeconds >= 60)
                button.Text = string.Format("{0:D2}:{1:D2}", playerTimer.Time.Minutes, playerTimer.Time.Seconds);
            else if (playerTimer.Time.TotalSeconds <= 15)
            {
                button.Text = string.Format("{0:D2}:{1:D1}", playerTimer.Time.Seconds, (int)Math.Round((double)playerTimer.Time.Milliseconds / 100, 1));
                //Transform(button);
            }
            else
            {
                button.Text = string.Format("{0:D2}", playerTimer.Time.Seconds);
            }
        }

        // ?
        private void Transform(Button button)
        {
            button.TextColor = Color.Red;
            button.FontAttributes = FontAttributes.Bold;
            button.FontSize = 40;
        }

        // to do
        private async void ExitGame()
        {
            await DisplayAlert("Время закончилось", "Отлично сыграно!", "Спасибо!");
            await Navigation.PopModalAsync();
        }

        private void button_first_player_Clicked(object sender, EventArgs e)
        {
            Sound.PlaySound(Sound.SoundType.Kick);
            playerTimer1.Stop();
            button_first_player.IsEnabled = false;
            playerTimer1.AddTime();

            row0.Height = new GridLength(6, GridUnitType.Star);
            row1.Height = new GridLength(4, GridUnitType.Star);

            playerTimer2.Start();
            button_second_player.IsEnabled = true;
        }

        private void button_second_player_Clicked(object sender, EventArgs e)
        {
            Sound.PlaySound(Sound.SoundType.KickClap);
            playerTimer2.Stop();
            button_second_player.IsEnabled = false;
            playerTimer2.AddTime();

            row0.Height = new GridLength(4, GridUnitType.Star);
            row1.Height = new GridLength(6, GridUnitType.Star);

            playerTimer1.Start();
            button_first_player.IsEnabled = true;
        }
    }
}
