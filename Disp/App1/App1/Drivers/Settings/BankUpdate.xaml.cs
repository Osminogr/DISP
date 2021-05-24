using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System;
using System.Net.Http;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BankUpdate : ContentPage
    {
        Driver nowUser;
        public BankUpdate(Driver now)
        {
            nowUser = now;
            InitializeComponent();

            address.Text = nowUser.accountNumber.bankAddress;
            nameBank.Text = nowUser.accountNumber.bankName;
            bik.Text = nowUser.accountNumber.bik;
            accNumberC.Text = nowUser.accountNumber.bunkNumberK;
            accNumberR.Text = nowUser.accountNumber.bunkNumberR;

            OverrideTitleView("Банковские реквизиты", "Сохранить", 30, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                try
                {
                    if (nameBank.Text == null || (nameBank.Text != null && nameBank.Text.Length < 5))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения наименования Банка!", "Закрыть");
                        return;
                    }

                    if (address.Text == null || (address.Text != null && address.Text.Length < 5))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения адресса Банка!", "Закрыть");
                        return;
                    }

                    if (accNumberR.Text == null || (accNumberR.Text != null && accNumberR.Text.Length != 20))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения Расчет. Счета Банка!", "Закрыть");
                        return;
                    }

                    if (accNumberC.Text == null || (accNumberC.Text != null && accNumberC.Text.Length != 20))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения Корр. Счета Банка!", "Закрыть");
                        return;
                    }

                    if (bik.Text == null || (bik.Text != null && bik.Text.Length != 9))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения БИК Банка!", "Закрыть");
                        return;
                    }

                    nowUser.accountNumber.bankAddress = address.Text;
                    nowUser.accountNumber.bankName = nameBank.Text;
                    nowUser.accountNumber.bik = bik.Text;
                    nowUser.accountNumber.bunkNumberK = accNumberC.Text;
                    nowUser.accountNumber.bunkNumberR = accNumberR.Text;

                    HttpContent answer = await Server.SaveAccountNumber(nowUser.accountNumber);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(AccountNumber))))
                    {
                        await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже.", "Закрыть");
                    }
                    else
                    {
                        Server.SaveAuthObject(nowUser, false);
                        await Navigation.PopAsync(true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже.", "Закрыть");
                }
            })));
        }
    }
}