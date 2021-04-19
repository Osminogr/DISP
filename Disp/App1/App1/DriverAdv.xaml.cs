using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverAdv : ContentPage
    {
        string phone;
        //public User nowUser;
        public DriverAdv(string nowNumb)
        {
            phone = nowNumb;
            InitializeComponent();
            //now = nowUser;
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void Button_Click_Dr(object sender, EventArgs e)
        {
            //nowUser.type = false;
            //if (Convert.ToInt32(this.code) == nowUser.code)
            Driver nowUser = new Driver();
            nowUser.phone = phone;
            await Navigation.PushAsync(new RegisterDr(nowUser));
        }

        private async void Button_Click_Adv(object sender, EventArgs e)
        {
            //nowUser.type = true;
            //if (Convert.ToInt32(this.code) == nowUser.code)
            await Navigation.PushAsync(new RegisterAdv(phone));
        }
    }
}