
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
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false;
                if (Name.Text != null) b1 = true;
                if (Address.Text != null) b2 = true;
                if (NumberK.Text != null) b3 = true;
                if (NumberR.Text != null) b4 = true;
                if (BIK.Text != null) b5 = true;

                if (b1 && b2 && b3 && b4 && b5)
                {
                    driver.accountNumber.bankName = Name.Text;
                    driver.accountNumber.bankAddress = Address.Text;
                    driver.accountNumber.bunkNumberK = NumberK.Text;
                    driver.accountNumber.bunkNumberR = NumberR.Text;
                    driver.accountNumber.bik = BIK.Text;

                    await Navigation.PushAsync(new TermsOfUse(driver));
                }
                else await DisplayAlert("Сообщение", "Необходимо заполнить всю информацию!", "Закрыть");
            })));
        }
    }
}