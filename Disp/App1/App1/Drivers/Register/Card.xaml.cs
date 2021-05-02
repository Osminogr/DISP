
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Card : ContentPage
    {
        Driver driver;
        public Card(Driver now)
        {
            driver = now;
            driver.accountNumber = new AccountNumber();
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
                    driver.accountNumber.bankName = Name.Text;
                }
                if (Address.Text != null)
                {
                    b2 = true;
                    driver.accountNumber.bankAddress = Address.Text;
                }
                if (NumberK.Text != null)
                {
                    b3 = true;
                    driver.accountNumber.bunkNumberK = NumberK.Text;
                }
                if (NumberR.Text != null)
                {
                    b4 = true;
                    driver.accountNumber.bunkNumberR = NumberR.Text;
                }
                if (BIK.Text != null)
                {
                    b5 = true;
                    driver.accountNumber.bik = BIK.Text;
                }
                if (b1 && b2 && b3 && b4 && b5)
                    Navigation.PushAsync(new TermsOfUse(driver));
            })));
        }
    }
}