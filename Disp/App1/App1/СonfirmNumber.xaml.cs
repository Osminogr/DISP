using System;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using App1.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class СonfirmNumber : ContentPage
    {
        string code;
        string number;

        public string LabelText
        {
            get { return LabelText; }
            set
            {
                LabelText = value;
                OnPropertyChanged(nameof(LabelText));
            }
        }

        public СonfirmNumber(string phone)
        {
            number = phone;
            InitializeComponent();
            BindingContext = this;

            Label1.Text = "+7 (" + number[0] + number[1] + number[2] + ") " + number[3] + number[4] + number[5] + "-" + number[6] + number[7] + "-" + number[8] + number[9];
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void CodeReq(object sender, EventArgs e)
        {
            string content = String.Format(@" ""phone"" : ""{0}""", number);
            content = @"{ ""CodeReq"" :{ " + content + "} }";
            Server.Request(content, "post", "gcode");
            reCode.IsEnabled = false;
            reCode.Text = "Код отправлен повторно";
        }

        public async void ChangeNumber(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage());

        }

        protected void Change_code(object sender, EventArgs e)
        {
            this.code = CodeEnter.Text;

        }

        private async void Button_Click(object sender, EventArgs e)
        {
            if (code != null && code.Length == 4)
            {
                HttpClient client = new HttpClient();

                var answer = await client.GetAsync(Server.url + "gcode/?code=" + code + "&phone=" + number);
                var responseBody = await answer.Content.ReadAsStringAsync();

                if (responseBody.Length == 13)
                {
                    bool load = false;

                    var req = await client.GetAsync(Server.url + "adv/" + number);
                    var resp = await req.Content.ReadAsStringAsync();

                    if (resp != null && resp.Length > 0)
                    {
                        if (resp.Contains("Adv"))
                        {
                            JObject j = JObject.Parse(resp);
                            Adv adv = JsonConvert.DeserializeObject<Adv>(j["Adv"].ToString());

                            if (adv != null)
                            {
                                load = true;
                                AuthObject authObject = new AuthObject();
                                authObject.phone = adv.company.phone;
                                authObject.isCompany = true;
                                Preferences.Set("auth", JsonConvert.SerializeObject(authObject));
                                await Navigation.PushAsync(new MainPageAdv(adv));
                            }
                        }
                    }

                    if (!load)
                    {
                        req = await client.GetAsync(Server.url + "drivers/" + number);
                        resp = await req.Content.ReadAsStringAsync();

                        if (resp != null && resp.Length > 0)
                        {
                            if (resp.Contains("Drivers"))
                            {
                                JObject j = JObject.Parse(resp);
                                App1.Domain.Driver drv = JsonConvert.DeserializeObject<App1.Domain.Driver>(j["Drivers"].ToString());

                                if (drv != null)
                                {
                                    load = true;

                                    AuthObject authObject = new AuthObject();
                                    authObject.phone = drv.person.phone;
                                    authObject.isCompany = false;
                                    Preferences.Set("auth", JsonConvert.SerializeObject(authObject));
                                    //await Navigation.PushAsync(new MainPageDr(drv));
                                }
                            }
                        }
                        
                    }

                    if (!load)
                    {
                        await Navigation.PushAsync(new DriverAdv(number));
                    }
                }
            }
        }
    }
}