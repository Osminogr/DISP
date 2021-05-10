using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsOfUse : ContentPage
    {
        Driver driver;
        public TermsOfUse(Driver now)
        {
            driver = now;

            InitializeComponent();
            OverrideTitleView("Соглашение", 80, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void ToDriver(object sender, EventArgs e)
        {
            try
            {
                HttpContent response = await Server.AddDriver(driver);
                string answer = await response.ReadAsStringAsync();

                if (answer != null && answer.Contains(nameof(Driver)))
                {
                    JObject j = JObject.Parse(answer);
                    Driver driverAdv = JsonConvert.DeserializeObject<Driver>(j[nameof(Driver)].ToString());
                    Server.SaveAuthObject(driverAdv, false);
                    await Navigation.PushAsync(new MainPageDr(driverAdv));
                }
                else
                {
                    await DisplayAlert("Сообщение", "Не удалось выполнить регистрацию! Попробуйте позже.", "Закрыть");
                    await Navigation.PushAsync(new StartPage());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Сообщение", "Не удалось выполнить регистрацию! Попробуйте позже.", "Закрыть");
                await Navigation.PushAsync(new StartPage());
            }
        }
    }
}