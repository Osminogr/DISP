
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

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
                    nowUser.company.name = Name.Text;
                }
                if (Address.Text != null)
                {
                    b2 = true;
                    nowUser.company.address = Address.Text;
                }
                if (Ogrn.Text != null)
                {
                    b3 = true;
                    nowUser.company.ogrn = Ogrn.Text;
                }
                if (Inn.Text != null)
                {
                    b4 = true;
                    nowUser.company.inn = Inn.Text;
                }
                if (Kpp.Text != null)
                {
                    b5 = true;
                    nowUser.company.kpp = Kpp.Text;
                }
                if (DirFIO.Text != null)
                {
                    b6 = true;
                    nowUser.company.director.lastName = DirFIO.Text;
                }
                if (DirPost.Text != null)
                {
                    b7 = true;
                    nowUser.company.director.position = DirPost.Text;
                }
                if (Cb.IsChecked)
                {
                    b8 = true;
                    b9 = true;
                    nowUser.company.manager.lastName = nowUser.company.director.lastName;
                    nowUser.company.manager.position = nowUser.company.director.position;
                }
                else
                {
                    if (ContFIO.Text != null)
                    {
                        b8 = true;
                        nowUser.company.manager.lastName = ContFIO.Text;
                    }
                    if (ContPost.Text != null)
                    {
                        b9 = true;
                        nowUser.company.manager.position = ContPost.Text;
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