using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoPage : ContentPage
    {
        public LogoPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Navigation.PushAsync(new StartPage());
        }
    }
}