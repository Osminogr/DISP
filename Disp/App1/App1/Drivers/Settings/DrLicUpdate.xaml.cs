
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

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
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Сохранить"
            };
            tb.Clicked += async (s, e) =>
            {
                bool b1 = false, b2 = false, b3 = false;
                if (Number.Text != null)
                {
                    b1 = true;
                    driver.driverLicence.number = Number.Text;
                }
                if (Data.Text != null)
                {
                    b2 = true;
                    driver.driverLicence.date = Data.Text;
                }
                if (Period.Text != null)
                {
                    b3 = true;
                    driver.driverLicence.period = Period.Text;
                }

                if (b1 && b2 && b3)
                    await Navigation.PushAsync(new App1.Settings(driver));
            };
            ToolbarItems.Add(tb);
        }
    }
}