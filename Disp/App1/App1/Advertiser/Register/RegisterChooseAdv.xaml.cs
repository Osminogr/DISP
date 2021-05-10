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
using App1.RegisterAdvYL;
using App1.RegisterAdvPh;

namespace App1.Advertiser.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterChooseAdv : ContentPage
    {
        string phone;
        public RegisterChooseAdv(string number)
        {
            InitializeComponent();

            phone = number;
            OverrideTitleView("Регистрация", "Дальше", 80, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() => {
                if (person.IsChecked)
                {
                    Navigation.PushAsync(new FIO(phone));
                }

                if (company.IsChecked)
                {
                    Navigation.PushAsync(new Organiz(phone, false));
                }

                if (agent.IsChecked)
                {
                    Navigation.PushAsync(new Organiz(phone, true));
                }
            })));
        }
    }
}