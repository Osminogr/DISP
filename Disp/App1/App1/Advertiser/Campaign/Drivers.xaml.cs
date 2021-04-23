
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Drivers : ContentPage
    {
        
        Adv nowUser;
        public Drivers(Adv now)
        {
            nowUser = now;
            InitializeComponent();
            Request();
        }
        public async void Request() 
        {
            
        }
    }
}