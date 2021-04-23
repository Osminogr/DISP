using Xamarin.Forms;
using Xamarin.Essentials;
using Newtonsoft.Json;
using App1.Domain;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace App1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartPage()) { BarBackgroundColor = Color.FromRgb(255, 184, 0) };
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
