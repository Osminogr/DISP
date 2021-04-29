using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

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

            OverrideTitleView("Настройки", 90, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void GetLinkEmailUser_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Подтвердите изменения",
                 "На вашу почту отправлено письмо с ссылкой на подтверждение нового пароля.",
                 "OK");

        }
    }
}