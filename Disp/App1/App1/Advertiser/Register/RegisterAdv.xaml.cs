
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterAdv : ContentPage
    {
        Adv nowUser = new Adv();
        public RegisterAdv(string number)
        {
            nowUser.company.phone = number;
            InitializeComponent();
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Дальше"
            };
            tb.Clicked += async (s, e) =>
            {
                if (Cb1.IsChecked)
                {
                    if (City.SelectedIndex != -1)
                    {
                        nowUser.company.city = City.Items[City.SelectedIndex];
                        await Navigation.PushAsync(new RegisterAdvPh.FIO(nowUser));
                    }
                }
                else
                {
                    if (Cb2.IsChecked || Cb3.IsChecked)
                    {
                        if (City.SelectedIndex != -1)
                        {
                            nowUser.company.city = City.Items[City.SelectedIndex];
                            await Navigation.PushAsync(new RegisterAdvYL.Organiz(nowUser));
                        }
                    }
                }
            };
            ToolbarItems.Add(tb);
        }

        void Cb1_Change(object sender, CheckedChangedEventArgs e)
        {
            Cb2.IsChecked = false;
            Cb3.IsChecked = false;
            nowUser.c_type = 1;
        }

        void Cb2_Change(object sender, CheckedChangedEventArgs e)
        {
            Cb1.IsChecked = false;
            Cb3.IsChecked = false;
            //Cb2.IsChecked = true;
            nowUser.c_type = 2;
        }

        void Cb3_Change(object sender, CheckedChangedEventArgs e)
        {
            Cb1.IsChecked = false;
            Cb2.IsChecked = false;
            //Cb3.IsChecked = true;
            nowUser.c_type = 3;
        }
    }
}