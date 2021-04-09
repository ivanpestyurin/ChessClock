using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChessClock
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        private int mainTime;
        private int additionalTime;
        public StartPage()
        {
            InitializeComponent();
        }
        private void slider_main_time_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            mainTime = (int)e.NewValue;
            label_main_time.Text = String.Format("Main time: {0} min", mainTime);
        }

        private void slider_additional_time_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            additionalTime = (int)e.NewValue;
            label_additional_time.Text = String.Format("Time for move: {0} sec", additionalTime);
        }

        private async void start_button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage(mainTime, additionalTime));
        }

        private void exit_button_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}