
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;

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
                bool b1 = false, b2 = false, b3 = false, b4 = false;

                if (Name.Text != null) b1 = true;
                if (LastName.Text != null) b2 = true;
                if (Patronymic.Text != null) b3 = true;
                if (City.Text != null) b4 = true;

                if (b1 && b2 && b3 && b4)
                {
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
                else
                {
                    await DisplayAlert("Сообщение", "Не все поля заполнены корректно!", "Закрыть");
                }
            })));
        }
    }
}