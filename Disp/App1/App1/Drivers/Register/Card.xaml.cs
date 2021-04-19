
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Card : ContentPage
    {
        Driver nowUser;
        public Card(Driver now)
        {
            nowUser = now;
            InitializeComponent();
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Дальше"
            };
            tb.Clicked += async (s, e) =>
            {
                if (Numb.Text != null)
                {
                    nowUser.card = Numb.Text;
                    await Navigation.PushAsync(new TermsOfUse(nowUser));
                }
            };
            ToolbarItems.Add(tb);
        }
    }
}