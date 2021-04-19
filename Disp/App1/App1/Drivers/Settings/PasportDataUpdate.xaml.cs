
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            number.Text = nowUser.paspNumber;
            Data.Text = nowUser.paspData;
            Org.Text = nowUser.paspOrg;
            Code.Text = nowUser.paspCode;

            ToolbarItem tb = new ToolbarItem
            {
                Text = "Сохранить"
            };
            tb.Clicked += async (s, e) =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false;
                if (number.Text != null)
                {
                    b1 = true;
                    nowUser.paspNumber = number.Text;
                }
                if (Data.Text != null)
                {
                    b2 = true;
                    nowUser.paspData = Data.Text;
                }
                if (Org.Text != null)
                {
                    b3 = true;
                    nowUser.paspOrg = Org.Text;
                }
                if (Code.Text != null)
                {
                    b4 = true;
                    nowUser.paspCode = Code.Text;
                }
                if (b1 && b2 && b3 && b4)
                    await Navigation.PopAsync();
            };
            ToolbarItems.Add(tb);
        }
    }
}