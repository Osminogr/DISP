
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalDataUpdate : ContentPage
    {
        Driver nowUser;
        public PersonalDataUpdate(Driver now)
        {
            nowUser = now;
            InitializeComponent();
            Name.Text = nowUser.name;
            LastName.Text = nowUser.lastname;
            Patronymic.Text = nowUser.patronymic;
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Сохранить"
            };
            tb.Clicked += async (s, e) =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false;

                if (Name.Text != null)
                {
                    b1 = true;
                    nowUser.name = Name.Text;
                }
                if (LastName.Text != null)
                {
                    b2 = true;
                    nowUser.lastname = LastName.Text;
                }
                if (Patronymic.Text != null)
                {
                    b3 = true;
                    nowUser.patronymic = Patronymic.Text;
                }
                if (city.SelectedIndex != -1)
                {
                    b4 = true;
                    nowUser.city = city.Items[city.SelectedIndex];
                }
                if (b1 && b2 && b3 && b4)
                    await Navigation.PopAsync();
            };
            ToolbarItems.Add(tb);
        }
    }
}