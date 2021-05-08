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
    public partial class ChangeTelAdv : ContentPage
    {
        Adv nowUser;
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
                if (nowUser.isCompany)
                {
                    if (nowUser.company.phone == currentPhone.Text)
                    {
                        nowUser.company.phone = newPhone.Text;

                        HttpContent answer = await Server.SaveCompany(nowUser.company);
                        string response = await answer.ReadAsStringAsync();

                        if (response == null || (response != null && !response.Contains(nameof(Company))))
                        {
                            await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                        }
                        else
                        {
                            Server.SaveAuthObject(nowUser, nowUser.isCompany);
                        }

                        await Navigation.PopAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}