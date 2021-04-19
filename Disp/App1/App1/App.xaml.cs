using Xamarin.Forms;

namespace App1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new MainPage(new User()));
            //MainPage = new Drivers.Videos();
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
