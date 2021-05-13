using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using App1.Advertiser.Settings;

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

            nameAdv.Text = nowUser.company.name;
            phoneAdv.Text = String.Format("+7 ({0}{1}{2}) {3}{4}{5}-{6}{7}-{8}{9}", nowUser.company.phone[0], nowUser.company.phone[1],
                nowUser.company.phone[2], nowUser.company.phone[3], nowUser.company.phone[4], nowUser.company.phone[5],
                nowUser.company.phone[6], nowUser.company.phone[7], nowUser.company.phone[8], nowUser.company.phone[9]);

            if (!nowUser.isCompany)
            {
                personDataShow.IsVisible = true;
                companyDataShow.IsVisible = false;
            }
            else
            {
                companyDataShow.IsVisible = true;
                personDataShow.IsVisible = false;
            }

            OverrideTitleView("Настройки", 90, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void CompanyDataUpdate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CompanyDataSettingAdv(nowUser)
            {
                nameAdvHandler = OnNameAdvChanged
            }, true);
        }

        private async void PersonDataUpdate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PersonalDataUpdate(nowUser));
        }

        private void OnNameAdvChanged(object sender, string nameAdv)
        {
            this.nameAdv.Text = nameAdv;
        }

        private void OnPhoneAdvChanged(object sender, string phoneAdv)
        {
            this.phoneAdv.Text = String.Format("+7 ({0}{1}{2}) {3}{4}{5}-{6}{7}-{8}{9}", phoneAdv[0], phoneAdv[1],
                phoneAdv[2], phoneAdv[3], phoneAdv[4], phoneAdv[5],
                phoneAdv[6], phoneAdv[7], phoneAdv[8], phoneAdv[9]);
        }

        private async void ChangeEmailAdv(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeEmailAdv(nowUser), true);
        }

        private async void ChangeTelAdv(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangeTelAdv(nowUser)
            {
                phoneAdvHandler = OnPhoneAdvChanged
            }, true);
        }

        private async void PasswordChangeAdv(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordChangeAdv(nowUser), true);
        }

        private async void DataOrganizationBankAdv_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DataOrganizationBankAdv(nowUser), true);
        }

        private async void DogovorAdv_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DogovorAdv(nowUser), true);
        }

        private void ToggledShowDriverAdReq(object sender, EventArgs e)
        {
            Server.SaveShowDriverAdReq(showDriversAdReq.IsToggled);
        }
    }
}