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

            actInd.IsRunning = true;
            actInd.IsVisible = true;
            gridRoot.IsEnabled = false;
            loadPreferences();
        }

        private async void loadPreferences()
        {
            string json = Preferences.Get("auth", null);

            if (json == null)
            {
                return;
            }

            AuthObject authObject = JsonConvert.DeserializeObject<AuthObject>(json);

            if (authObject == null)
            {
                return;
            }

            NumberEnter.Text = authObject.phone;

            if (authObject.isCompany)
            {
                HttpClient client = new HttpClient();

                var req = await client.GetAsync(Server.url + "adv/" + authObject.phone);
                var resp = await req.Content.ReadAsStringAsync();
                if (resp != null && resp.Contains("Adv"))
                {
                    JObject j = JObject.Parse(resp);
                    Adv adv = JsonConvert.DeserializeObject<Adv>(j["Adv"].ToString());

                    await Navigation.PushAsync(new MainPageAdv(adv));
                }
            }
            else
            {
                HttpClient client = new HttpClient();

                var req = await client.GetAsync(Server.url + "drivers/" + authObject.phone);
                var resp = await req.Content.ReadAsStringAsync();
                if (resp != null && resp.Contains("Drivers"))
                {
                    JObject j = JObject.Parse(resp);
                    Driver drv = JsonConvert.DeserializeObject<App1.Domain.Driver>(j["Drivers"].ToString());

                    await Navigation.PushAsync(new MainPageDr(drv));
                }
            }

            await Task.Delay(1000);
            actInd.IsRunning = false;
            actInd.IsVisible = false;
            gridRoot.IsEnabled = true;
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            if (number != null)// && number.Length == 10)
            {
                string content = String.Format(@" ""phone"" : ""{0}""", number);
                content = @"{ ""CodeReq"" :{ " + content + "} }";

                Server.Request(content, "post", "gcode");

                await Navigation.PushAsync(new СonfirmNumber(number));
            }
        }

        private void Change_number(object sender, EventArgs e)
        {
            number = NumberEnter.Text;
        }
    }
}