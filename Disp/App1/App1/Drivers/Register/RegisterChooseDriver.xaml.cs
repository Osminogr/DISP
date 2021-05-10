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

namespace App1.Drivers.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterChooseDriver : ContentPage
    {
        string phone;
        Driver driver;
        public RegisterChooseDriver(string number)
        {
            InitializeComponent();

            phone = number;
            driver = new Driver();
            driver.person = new Person();
            driver.person.phone = phone;
            OverrideTitleView("Регистрация", "Дальше", 80, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                bool b1 = false, b2 = false, b3 = false;

                if (Name.Text != null) b1 = true;
                if (LastName.Text != null) b2 = true;
                if (Patronymic.Text != null) b3 = true;

                if (b1 && b2 && b3)
                {
                    driver.person.firstName = Name.Text;
                    driver.person.lastName = LastName.Text;
                    driver.person.patronymic = Patronymic.Text;

                    await Navigation.PushAsync(new RegisterPasp(driver));
                }
                else await DisplayAlert("Сообщение", "Необходимо заполнить всю информацию!", "Закрыть");
            })));
        }
    }
}