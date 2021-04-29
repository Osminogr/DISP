using System;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using App1.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Enter(object sender, EventArgs e)
        {
            gridRoot.IsEnabled = false;
            actInd.IsRunning = true;
            actInd.IsVisible = true;
            await Task.Delay(1000);
            await Navigation.PushAsync(new StartPage());
        }

        private async void Register(object sender, EventArgs e)
        {
            gridRoot.IsEnabled = false;
            actInd.IsRunning = true;
            actInd.IsVisible = true;
            await Task.Delay(1000);
            await Navigation.PushAsync(new RegisterAdv(string.Empty));
        }
    }
}