
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

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
            Name.Text = nowUser.person.firstName;
            LastName.Text = nowUser.person.lastName;
            Patronymic.Text = nowUser.person.patronymic;
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
                    nowUser.person.firstName = Name.Text;
                }
                if (LastName.Text != null)
                {
                    b2 = true;
                    nowUser.person.lastName = LastName.Text;
                }
                if (Patronymic.Text != null)
                {
                    b3 = true;
                    nowUser.person.patronymic = Patronymic.Text;
                }
                if (city.SelectedIndex != -1)
                {
                    b4 = true;
                    nowUser.person.city = city.Items[city.SelectedIndex];
                }
                if (b1 && b2 && b3 && b4)
                    await Navigation.PopAsync();
            };
            ToolbarItems.Add(tb);
        }
    }
}