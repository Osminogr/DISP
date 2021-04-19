
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            Numb.Text = nowUser.card;
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Сохранить"
            };
            tb.Clicked += async (s, e) =>
            {
                if (Numb.Text != null)
                {
                    nowUser.card = Numb.Text;
                    await Navigation.PopAsync();
                }
            };
            ToolbarItems.Add(tb);
        }
    }
}