
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Card : ContentPage
    {
        Driver driver;
        public Card(Driver now)
        {
            driver = now;
            driver.accountNumber = new AccountNumber();
            InitializeComponent();
            OverrideTitleView("Регистрация", "Дальше", 80, -1);
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

                    driver.accountNumber.bankName = nameBank.Text;
                    driver.accountNumber.bankAddress = address.Text;
                    driver.accountNumber.bunkNumberK = accNumberR.Text;
                    driver.accountNumber.bunkNumberR = accNumberC.Text;
                    driver.accountNumber.bik = bik.Text;

                    await Navigation.PushAsync(new TermsOfUse(driver));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await DisplayAlert("Сообщение", "Непредвиденная ошибка! Попробуйте позже.", "Закрыть");
                }
            })));
        }
    }
}