
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.RegisterAdvYL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bank : ContentPage
    {
        Adv nowUser;
        public Bank(Adv now)
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
                if (Name.Text != null)
                {
                    b1 = true;
                    nowUser.name = Name.Text;
                }
                if (Address.Text != null)
                {
                    b2 = true;
                    nowUser.BankAddress = Address.Text;
                }
                if (NumberK.Text != null)
                {
                    b3 = true;
                    nowUser.BunkNumberK = NumberK.Text;
                }
                if (NumberR.Text != null)
                {
                    b4 = true;
                    nowUser.BunkNumberR = NumberR.Text;
                }
                if (b1 && b2 && b3 && b4)
                    await Navigation.PushAsync(new RegisterAdvPh.TermOfUse(nowUser));
            };
            ToolbarItems.Add(tb);
        }
    }
}