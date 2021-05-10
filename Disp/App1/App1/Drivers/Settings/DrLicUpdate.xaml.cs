
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrLicUpdate : ContentPage
    {
        Driver driver;
        public DrLicUpdate(Driver now)
        {
            driver = now;
            InitializeComponent();

            Number.Text = driver.driverLicence.number;
            Data.Text = driver.driverLicence.date;
            Period.Text = driver.driverLicence.period;

            OverrideTitleView("Водит. удостоверение", "Сохранить", 15, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                bool b1 = false, b2 = false, b3 = false;
                if (Number.Text != null) b1 = true;
                if (Data.Text != null) b2 = true;
                if (Period.Text != null) b3 = true;

                if (b1 && b2 && b3)
                {
                    driver.driverLicence.number = Number.Text;
                    driver.driverLicence.date = Data.Text;
                    driver.driverLicence.period = Period.Text;

                    HttpContent answer = await Server.SaveDriverLicence(driver.driverLicence);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(DriverLicence))))
                    {
                        await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                    }
                    else
                    {
                        Server.SaveAuthObject(driver, false);
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