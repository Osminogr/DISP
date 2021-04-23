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
    public partial class SettingsAdv : ContentPage
    {
        Adv nowUser;

        public SettingsAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }

        private async void CompanyDataUpdate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompanyDataSettingAdv(nowUser));
        }
        private async void ChangeEmailAdv(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeEmailAdv(nowUser));
        }
        private async void ChangeTelAdv(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeTelAdv(nowUser));
        }
        private async void PasswordChangeAdv(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordChangeAdv(nowUser));
        }
        private async void DataOrganizationBankAdv_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataOrganizationBankAdv(nowUser));
        }
        private async void DogovorAdv_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DogovorAdv(nowUser));
        }

    }
}