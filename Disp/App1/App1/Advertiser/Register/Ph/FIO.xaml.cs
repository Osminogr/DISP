
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

namespace App1.RegisterAdvPh
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FIO : ContentPage
    {
        string phone;
        Adv adv;
        public FIO(string number)
        {
            phone = number;
            adv = new Adv();
            adv.company = new Company();
            adv.company.phone = phone;
            adv.company.director = new Person();

            InitializeComponent();

            OverrideTitleView("Регистрация", "Дальше", 80, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false;
                if (Name.Text != null) b1 = true;
                if (LastName.Text != null) b2 = true;
                if (Patronymic.Text != null) b3 = true;
                if (Town.Text != null) b4 = true;
                if (Email.Text != null) b5 = true;

                if (b1 && b2 && b3 && b4 && b5)
                {
                    adv.company.director.firstName = Name.Text;
                    adv.company.director.lastName = LastName.Text;
                    adv.company.director.patronymic = Patronymic.Text;
                    adv.company.director.city = Town.Text;
                    adv.company.city = Town.Text;
                    adv.company.email = Email.Text;
                    adv.company.name = string.Format("{0} {1} {2}", adv.company.director.lastName, adv.company.director.firstName, adv.company.director.patronymic);

                    await Navigation.PushAsync(new RegisterAdvPh.TermOfUse(adv));
                }
                else await DisplayAlert("Сообщение", "Необходимо заполнить всю информацию!", "Закрыть");
            })));
        }
    }
}