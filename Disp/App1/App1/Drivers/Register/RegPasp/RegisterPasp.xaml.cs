
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

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
                    nowUser.person.passport.number = number.Text;
                }
                if (Data.Text != null)
                {
                    b2 = true;
                    nowUser.person.passport.date = Data.Text;
                }
                if (Org.Text != null)
                {
                    b3 = true;
                    nowUser.person.passport.who = Org.Text;
                }
                if (Code.Text != null)
                {
                    b4 = true;
                    nowUser.person.passport.code = Code.Text;
                }
                if (b1 && b2 && b3 && b4)
                    await Navigation.PushAsync(new RegisterPaspPh(nowUser));
            };
            ToolbarItems.Add(tb);
        }
    }
}