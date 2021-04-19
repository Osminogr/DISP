
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPasp : ContentPage
    {
        Driver nowUser = new Driver();
        public RegisterPasp(Driver now)
        {
            nowUser = now;
            InitializeComponent();
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Дальше"
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
                    await Navigation.PushAsync(new RegisterPaspPh(nowUser));
            };
            ToolbarItems.Add(tb);
        }
    }
}