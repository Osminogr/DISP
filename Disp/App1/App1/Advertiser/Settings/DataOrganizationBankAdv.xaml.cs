using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;

namespace App1.Advertiser.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataOrganizationBankAdv : ContentPage
    {
        Adv nowUser;
        public DataOrganizationBankAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            Address.Text = nowUser.company.accountNumber.bankAddress;
            Name.Text = nowUser.company.accountNumber.bankName;
            BIK.Text = nowUser.company.accountNumber.bik;
            NumberK.Text = nowUser.company.accountNumber.bunkNumberK;
            NumberR.Text = nowUser.company.accountNumber.bunkNumberR;

            OverrideTitleView("Настройки", "Сохранить", 90, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                try
                {
                    nowUser.company.accountNumber.bankAddress = Address.Text;
                    nowUser.company.accountNumber.bankName = Name.Text;
                    nowUser.company.accountNumber.bik = BIK.Text;
                    nowUser.company.accountNumber.bunkNumberK = NumberK.Text;
                    nowUser.company.accountNumber.bunkNumberR = NumberR.Text;

                    HttpContent answer = await Server.SaveAccountNumber(nowUser.company.accountNumber);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(AccountNumber))))
                    {
                        await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже.", "Закрыть");
                    }
                    else
                    {
                        Server.SaveAuthObject(nowUser, nowUser.isCompany);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await Navigation.PopAsync();
            })));
        }
    }
}