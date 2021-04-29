
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BankUpdate : ContentPage
    {
        Driver nowUser;
        public BankUpdate(Driver now)
        {
            nowUser = now;
            InitializeComponent();
            Numb.Text = nowUser.accountNumber.cardNumber;
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Сохранить"
            };
            tb.Clicked += async (s, e) =>
            {
                if (Numb.Text != null)
                {
                    nowUser.accountNumber.cardNumber = Numb.Text;
                    await Navigation.PopAsync();
                }
            };
            ToolbarItems.Add(tb);
        }
    }
}