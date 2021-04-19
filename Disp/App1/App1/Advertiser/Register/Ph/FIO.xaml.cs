
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.RegisterAdvPh
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FIO : ContentPage
    {
        Adv nowUser;
        public FIO(Adv now)
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
                string fio = "";
                if (Name.Text != null)
                {
                    b1 = true;
                    fio += Name.Text;
                }
                if (LastName.Text != null)
                {
                    b2 = true;
                    fio += LastName.Text;
                }
                if (Patronymic.Text != null)
                {
                    b3 = true;
                    fio += Patronymic.Text;
                }
                if (b1 && b2 && b3)
                {
                    nowUser.fioDir = fio;
                    nowUser.name = fio;
                    await Navigation.PushAsync(new RegisterAdvPh.TermOfUse(nowUser));
                }
            };
            ToolbarItems.Add(tb);
        }
    }
}