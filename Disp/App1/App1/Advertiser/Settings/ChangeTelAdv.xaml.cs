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
using System.Text.RegularExpressions;

namespace App1.Advertiser.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeTelAdv : ContentPage
    {
        Adv nowUser;
        public EventHandler<string> phoneAdvHandler;
        Dictionary<int, string> numbers = new Dictionary<int, string>();
        public ChangeTelAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();
            OverrideTitleView("Настройки", 90, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        public async void SavePhone(object sender, EventArgs e)
        {
            try
            {
                ShowLoading();

                string phoneCurrent = Server.GetPhoneFromRegex(currentPhone.Text);
                string phoneNew = Server.GetPhoneFromRegex(newPhone.Text);

                if (nowUser.company.phone == phoneCurrent)
                {
                    nowUser.company.phone = phoneNew;

                    HttpContent answer = await Server.SaveCompany(nowUser.company);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(Company))))
                    {
                        HideLoading();
                        await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                    }
                    else
                    {
                        Server.SaveAuthObject(nowUser, true);
                        phoneAdvHandler?.Invoke(this, nowUser.company.phone);

                        await Navigation.PopAsync(true);
                    }
                }
                else
                {
                    HideLoading();
                    await DisplayAlert("Сообщение", "Текущий номер телефона задан неверно!", "Закрыть");
                }
            }
            catch (Exception ex)
            {
                HideLoading();
                Console.WriteLine(ex);
            }
        }

        private void ShowLoading()
        {
            actInd.IsVisible = true;
            actInd.IsRunning = true;
            gridRoot.Opacity = 0.3;
            gridRoot.IsEnabled = false;
        }

        private void HideLoading()
        {
            actInd.IsVisible = false;
            actInd.IsRunning = false;
            gridRoot.Opacity = 1;
            gridRoot.IsEnabled = true;
        }
    }
}