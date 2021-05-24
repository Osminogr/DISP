using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;
using System;
using System.Text.RegularExpressions;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalDataUpdate : ContentPage
    {
        Driver nowUser;
        public PersonalDataUpdate(Driver now)
        {
            nowUser = now;
            InitializeComponent();

            Name.Text = nowUser.person.firstName;
            LastName.Text = nowUser.person.lastName;
            Patronymic.Text = nowUser.person.patronymic;
            City.Text = nowUser.person.city;
            
            OverrideTitleView("Личные данные", "Сохранить", 45, -1);
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

                    if(!Regex.IsMatch(City.Text, @"^[a-zA-Z]+$") && !Regex.IsMatch(City.Text, @"^[а-яА-Я]+$"))
                    {
                        await DisplayAlert("Сообщение", "Город не должен содержать цифры!", "Закрыть");
                        return;
                    }

                    nowUser.person.firstName = Name.Text;
                    nowUser.person.lastName = LastName.Text;
                    nowUser.person.patronymic = Patronymic.Text;
                    nowUser.person.city = City.Text;

                    HttpContent answer = await Server.SavePerson(nowUser.person);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(Person))))
                    {
                        await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                    }
                    else
                    {
                        Server.SaveAuthObject(nowUser, false);
                        await Navigation.PopAsync(true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                }
            })));
        }
    }
}