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
using System.Text.RegularExpressions;

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
                try
                {
                    if (Name.Text == null || (Name.Text != null && Name.Text.Length < 5))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения имени!", "Закрыть");
                        return;
                    }

                    if (LastName.Text == null || (LastName.Text != null && LastName.Text.Length < 5))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения фамилии!", "Закрыть");
                        return;
                    }

                    if (Patronymic.Text == null || (Patronymic.Text != null && Patronymic.Text.Length < 5))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения отчества!", "Закрыть");
                        return;
                    }

                    if (!Regex.IsMatch(Name.Text, @"^[a-zA-Z]+$") && !Regex.IsMatch(Name.Text, @"^[а-яА-Я]+$"))
                    {
                        await DisplayAlert("Сообщение", "Имя не должно содержать цифры!", "Закрыть");
                        return;
                    }

                    if (!Regex.IsMatch(LastName.Text, @"^[a-zA-Z]+$") && !Regex.IsMatch(LastName.Text, @"^[а-яА-Я]+$"))
                    {
                        await DisplayAlert("Сообщение", "Фамилия не должно содержать цифры!", "Закрыть");
                        return;
                    }

                    if (!Regex.IsMatch(Patronymic.Text, @"^[a-zA-Z]+$") && !Regex.IsMatch(Patronymic.Text, @"^[а-яА-Я]+$"))
                    {
                        await DisplayAlert("Сообщение", "Отчество не должно содержать цифры!", "Закрыть");
                        return;
                    }

                    driver.person.firstName = Name.Text;
                    driver.person.lastName = LastName.Text;
                    driver.person.patronymic = Patronymic.Text;

                    await Navigation.PushAsync(new RegisterPasp(driver));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await DisplayAlert("Сообщение", "Непредвиденная ошибка! Попробуйте позже.", "Закрыть");
                }
            })));
        }
    }
}