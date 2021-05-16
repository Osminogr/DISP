using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Threading;

namespace App1
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        string number;

        public StartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            loadPreferences();
        }

        private async void loadPreferences()
        {
            string json = Preferences.Get(Server.AUTH_OBJECT, null);

            if (json == null) return;

            AuthObject authObject = JsonConvert.DeserializeObject<AuthObject>(json);

            if (authObject == null) return;

            if (authObject.isCompany) NumberEnter.Text = authObject.adv.company.phone;
            else NumberEnter.Text = authObject.driver.person.phone;

            ShowLoading(true);

            if (authObject.isCompany)
            {
                if (authObject.adv != null) await Navigation.PushAsync(new MainPageAdv(authObject.adv));
            }
            else
            {
                if (authObject.driver != null) await Navigation.PushAsync(new MainPageDr(authObject.driver));
            }

            await Task.Delay(1000);
            ShowLoading(false);
        }

        private async void Enter(object sender, EventArgs e)
        {
            if (number != null && number.Length == 18)
            {
                ShowLoading(true);

                string phone = Server.GetPhoneFromRegex(String.Format("+7 {0}", number));

                HttpResponseMessage answer = await Server.CreateCodeReq(phone);
                await Navigation.PushAsync(new СonfirmNumber(phone));

                ShowLoading(false);
            }
        }

        private void Change_number(object sender, EventArgs e)
        {
            number = NumberEnter.Text;
        }

        private void ShowLoading(bool show)
        {
            actInd.IsRunning = show;
            actInd.IsVisible = show;
            gridRoot.IsEnabled = !show;
        }
    }
}