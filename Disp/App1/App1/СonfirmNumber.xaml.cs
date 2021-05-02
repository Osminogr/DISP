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

        private void ShowLoading(bool show)
        {
            actInd.IsRunning = show;
            actInd.IsVisible = show;
            gridRoot.IsEnabled = !show;
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
            ShowLoading(true);
            Server.CreateCodeReq(number);
            reCode.IsEnabled = false;
            reCode.Text = "Код отправлен повторно";
            ShowLoading(false);
        }

        public async void ChangeNumber(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage());
        }

        protected void Change_code(object sender, EventArgs e)
        {
            this.code = CodeEnter.Text;
        }

        private async void Complete(object sender, EventArgs e)
        {
            if (code != null && code.Length == 4)
            {
                Entity entity = await Server.GetEntity(code, number);

                if (entity != null)
                {
                    if (entity.GetType() == typeof(Adv))
                    {
                        AuthObject authObject = new AuthObject();
                        authObject.adv = (Adv)entity;
                        authObject.isCompany = true;
                        Preferences.Set(Server.AUTH_OBJECT, JsonConvert.SerializeObject(authObject));
                        await Navigation.PushAsync(new MainPageAdv((Adv)entity));
                    }
                    else
                    {
                        AuthObject authObject = new AuthObject();
                        authObject.driver = (Driver)entity;
                        authObject.isCompany = false;
                        Preferences.Set(Server.AUTH_OBJECT, JsonConvert.SerializeObject(authObject));
                        await Navigation.PushAsync(new MainPageDr((Driver)entity));
                    }
                }
                else
                {
                    await Navigation.PushAsync(new Register(number));
                }
            }
        }
    }
}