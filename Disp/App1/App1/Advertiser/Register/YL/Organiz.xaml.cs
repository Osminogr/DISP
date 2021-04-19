
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.RegisterAdvYL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Organiz : ContentPage
    {
        Adv nowUser;
        public Organiz(Adv now)
        {
            nowUser = now;
            InitializeComponent();
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Дальше"
            };
            tb.Clicked += async (s, e) =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, b6 = false, b7 = false, b8 = false, b9 = false;
                if (Name.Text != null)
                {
                    b1 = true;
                    nowUser.name = Name.Text;
                }
                if (Address.Text != null)
                {
                    b2 = true;
                    nowUser.address = Address.Text;
                }
                if (Ogrn.Text != null)
                {
                    b3 = true;
                    nowUser.ogrn = Ogrn.Text;
                }
                if (Inn.Text != null)
                {
                    b4 = true;
                    nowUser.inn = Inn.Text;
                }
                if (Kpp.Text != null)
                {
                    b5 = true;
                    nowUser.kpp = Kpp.Text;
                }
                if (DirFIO.Text != null)
                {
                    b6 = true;
                    nowUser.fioDir = DirFIO.Text;
                }
                if (DirPost.Text != null)
                {
                    b7 = true;
                    nowUser.dirPost = DirPost.Text;
                }
                if (Cb.IsChecked)
                {
                    b8 = true;
                    b9 = true;
                    nowUser.fioCont = nowUser.fioDir;
                    nowUser.contPost = nowUser.dirPost;
                }
                else
                {
                    if (ContFIO.Text != null)
                    {
                        b8 = true;
                        nowUser.fioCont = ContFIO.Text;
                    }
                    if (ContPost.Text != null)
                    {
                        b9 = true;
                        nowUser.contPost = ContPost.Text;
                    }
                }
                if (b1 && b2 && b3 && b4 && b5 && b6 && b7 && b8 && b9)
                    await Navigation.PushAsync(new RegisterAdvYL.Bank(nowUser));
            };
            ToolbarItems.Add(tb);
        }

        public void TheSame(object sender, CheckedChangedEventArgs e)
        {
            if (Cb.IsChecked)
                Contact.IsVisible = false;
            else
                Contact.IsVisible = true;
        }
    }
}