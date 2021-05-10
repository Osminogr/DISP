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

            if (nowUser.company.accountNumber == null) nowUser.company.accountNumber = new AccountNumber();

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
                    bool v1 = false, v2 = false, v3 = false, v4 = false, v5 = false;

                    if (Name.Text.Length != 0) v1 = true;
                    if (Address.Text.Length != 0) v2 = true;
                    if (NumberK.Text.Length == 20) v3 = true;
                    if (NumberR.Text.Length == 20) v4 = true;
                    if (BIK.Text.Length == 9) v5 = true;

                    if (v1 && v2 && v3 && v4 && v5)
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
                            Server.SaveAuthObject(nowUser, true);

                            await Navigation.PopAsync(true);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Сообщение", "Не все поля заполнены корректно!", "Закрыть");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            })));
        }
    }
}