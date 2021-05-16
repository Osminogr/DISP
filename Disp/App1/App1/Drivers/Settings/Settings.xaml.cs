using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        Driver nowUser;
        public Settings(Driver now)
        {
            nowUser = now;

            InitializeComponent();
            OverrideTitleView("Настройки", 90, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void PersonalDataUpdate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drivers.Settings.PersonalDataUpdate(nowUser));
        }

        private async void PasportDataUpdate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drivers.Settings.PasportDataUpdate(nowUser));
        }

        private async void DrLicUpdate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drivers.Settings.DrLicUpdate(nowUser));
        }

        private async void CarUpdate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drivers.Settings.CarUpdate(nowUser));
        }

        private async void BankUpdate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drivers.Settings.BankUpdate(nowUser));
        }

        private async void ContractAdverService(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drivers.Settings.ContractAdverService(nowUser));
        }
    }
}