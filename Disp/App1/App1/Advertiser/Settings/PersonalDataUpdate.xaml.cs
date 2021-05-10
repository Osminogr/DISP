
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;

namespace App1.Advertiser.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalDataUpdate : ContentPage
    {
        Adv nowUser;
        public PersonalDataUpdate(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            Name.Text = nowUser.company.director.firstName;
            LastName.Text = nowUser.company.director.lastName;
            Patronymic.Text = nowUser.company.director.patronymic;
            City.Text = nowUser.company.city;
            
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
                    nowUser.company.director.firstName = Name.Text;
                    nowUser.company.director.lastName = LastName.Text;
                    nowUser.company.director.patronymic = Patronymic.Text;
                    nowUser.company.name = string.Format("{0} {1} {2}", nowUser.company.director.lastName, nowUser.company.director.firstName, nowUser.company.director.patronymic);
                    nowUser.company.city = City.Text;

                    HttpContent answer = await Server.SavePerson(nowUser.company.director);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(Person))))
                    {
                        await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                    }
                    else
                    {
                        Server.SaveAuthObject(nowUser, true);
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