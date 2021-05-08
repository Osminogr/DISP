using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App1.RegisterAdvPh
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermOfUse : ContentPage
    {
        Adv adv;
        public TermOfUse(Adv adv)
        {
            this.adv = adv;
            InitializeComponent();

            OverrideTitleView("Соглашение", 80, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void ToAdv(object sender, EventArgs e)
        {
            try
            {
                HttpContent response = await Server.AddAdv(adv);
                string answer = await response.ReadAsStringAsync();

                if (answer != null && answer.Contains(nameof(Adv)))
                {
                    JObject j = JObject.Parse(answer);
                    Adv regAdv = JsonConvert.DeserializeObject<Adv>(j[nameof(Adv)].ToString());
                    Server.SaveAuthObject(regAdv, true);
                    await Navigation.PushAsync(new MainPageAdv(regAdv));
                }
                else
                {
                    await DisplayAlert("Сообщение", "Не удалось выполнить регистрацию! Попробуйте позже.", "Закрыть");
                    await Navigation.PushAsync(new StartPage());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение", "Не удалось выполнить регистрацию! Попробуйте позже.", "Закрыть");
                await Navigation.PushAsync(new StartPage());
            }
        }
    }
}