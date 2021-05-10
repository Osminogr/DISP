
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasportDataUpdate : ContentPage
    {
        Driver nowUser;
        public PasportDataUpdate(Driver now)
        {
            nowUser = now;
            InitializeComponent();

            number.Text = nowUser.person.passport.number;
            Data.Text = nowUser.person.passport.date;
            Org.Text = nowUser.person.passport.who;
            Code.Text = nowUser.person.passport.code;

            OverrideTitleView("Паспортные данные", "Сохранить", 40, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false;
                if (number.Text != null) b1 = true;
                if (Data.Text != null) b2 = true;
                if (Org.Text != null) b3 = true;
                if (Code.Text != null) b4 = true;

                if (b1 && b2 && b3 && b4)
                {
                    nowUser.person.passport.number = number.Text;
                    nowUser.person.passport.date = Data.Text;
                    nowUser.person.passport.who = Org.Text;
                    nowUser.person.passport.code = Code.Text;

                    HttpContent answer = await Server.SavePassport(nowUser.person.passport);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(Passport))))
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