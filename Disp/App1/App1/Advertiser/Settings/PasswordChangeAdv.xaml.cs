using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1.Advertiser.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordChangeAdv : ContentPage
    {
        Adv nowUser;
        public PasswordChangeAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }

        private async void GetLinkEmailUser_Clicked(object sender, EventArgs e)
        {
           await DisplayAlert("Подтвердите изменения",
                "На вашу почту отправлено письмо с ссылкой на подтверждение нового пароля.",
                "OK");
            
        }
    }
}