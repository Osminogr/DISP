using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverAdv : ContentPage
    {
        string phone;
        //public User nowUser;
        public DriverAdv(string nowNumb)
        {
            phone = nowNumb;
            InitializeComponent();
            //now = nowUser;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Button_Click_Dr(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var req = await client.GetAsync(Server.url + "drivers/" + phone);
            var resp = await req.Content.ReadAsStringAsync();
            if (resp != null && resp.Contains("Drivers"))
            {
                JObject j = JObject.Parse(resp);
                Driver drv = JsonConvert.DeserializeObject<App1.Domain.Driver>(j["Drivers"].ToString());

                await Navigation.PushAsync(new MainPageDr(drv));
            }
        }

        private async void Button_Click_Adv(object sender, EventArgs e)
        {
            //nowUser.type = true;
            //if (Convert.ToInt32(this.code) == nowUser.code)
            await Navigation.PushAsync(new RegisterAdv(phone));
        }
    }
}