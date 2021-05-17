
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

namespace App1.RegisterAdvYL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bank : ContentPage
    {
        Adv nowUser;
        public Bank(Adv now)
        {
            nowUser = now;
            nowUser.company.accountNumber = new AccountNumber();
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
                if (NumberK.Text != null && NumberK.Text.Length == 20) b3 = true;
                if (NumberR.Text != null && NumberR.Text.Length == 20) b4 = true;
                if (BIK.Text != null && BIK.Text.Length == 9) b5 = true;

                if (b1 && b2 && b3 && b4 && b5)
                {
                    nowUser.company.accountNumber.bankName = Name.Text;
                    nowUser.company.accountNumber.bankAddress = Address.Text;
                    nowUser.company.accountNumber.bunkNumberK = NumberK.Text;
                    nowUser.company.accountNumber.bunkNumberR = NumberR.Text;
                    nowUser.company.accountNumber.bik = BIK.Text;

                    await Navigation.PushAsync(new RegisterAdvPh.TermOfUse(nowUser));
                }
                else await DisplayAlert("Сообщение", "Необходимо заполнить всю информацию!", "Закрыть");
            })));
        }
    }
}