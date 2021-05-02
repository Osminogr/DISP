using System;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using App1.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;
using App1.Utils;
using App1.Advertiser.Register;
using App1.Drivers.Register;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        string phone;
        public Register(string number)
        {
            InitializeComponent();

            phone = number;
            OverrideTitleView("Регистрация", "Дальше", 80, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() => {
                if (driver.IsChecked)
                {
                    Navigation.PushAsync(new RegisterChooseDriver(phone));
                }

                if (adv.IsChecked)
                {
                    Navigation.PushAsync(new RegisterChooseAdv(phone));
                }
            })));
        }
    }
}