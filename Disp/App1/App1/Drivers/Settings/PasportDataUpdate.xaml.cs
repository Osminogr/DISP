
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

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
                    await Navigation.PopAsync();
            };
            ToolbarItems.Add(tb);
        }
    }
}