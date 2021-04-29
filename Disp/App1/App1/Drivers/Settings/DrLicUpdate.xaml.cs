
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrLicUpdate : ContentPage
    {
        Driver nowUser;
        public DrLicUpdate(Driver now)
        {
            nowUser = now;
            InitializeComponent();
            Number.Text = nowUser.drLicNumber;
            Data.Text = nowUser.drLicData;
            Period.Text = nowUser.drLicPeriod;
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
                    nowUser.drLicNumber = Number.Text;
                }
                if (Data.Text != null)
                {
                    b2 = true;
                    nowUser.drLicData = Data.Text;
                }
                if (Period.Text != null)
                {
                    b3 = true;
                    nowUser.drLicPeriod = Period.Text;
                }

                if (b1 && b2 && b3)
                    await Navigation.PushAsync(new RegisterDrLicPh(nowUser));
            };
            ToolbarItems.Add(tb);
        }
    }
}