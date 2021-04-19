
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterDrLic : ContentPage
    {
        Driver nowUser = new Driver();
        public RegisterDrLic(Driver now)
        {
            nowUser = now;
            InitializeComponent();
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Дальше"
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