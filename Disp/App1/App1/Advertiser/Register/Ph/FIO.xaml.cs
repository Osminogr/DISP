
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
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false;
                if (Name.Text != null)
                {
                    b1 = true;
                    adv.company.director.firstName = Name.Text;
                }
                if (LastName.Text != null)
                {
                    b2 = true;
                    adv.company.director.lastName = LastName.Text;
                }
                if (Patronymic.Text != null)
                {
                    b3 = true;
                    adv.company.director.patronymic = Patronymic.Text;
                }
                if(Town.Text != null)
                {
                    b4 = true;
                    adv.company.city = Town.Text;
                }
                if (Email.Text != null)
                {
                    b5 = true;
                    adv.company.email = Email.Text;
                }
               
                if (b1 && b2 && b3 && b4 && b5)
                {
                    Navigation.PushAsync(new RegisterAdvPh.TermOfUse(adv));
                }
            })));
        }
    }
}