
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
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false;
                if (Name.Text != null)
                {
                    b1 = true;
                    nowUser.company.accountNumber.bankName = Name.Text;
                }
                if (Address.Text != null)
                {
                    b2 = true;
                    nowUser.company.accountNumber.bankAddress = Address.Text;
                }
                if (NumberK.Text != null)
                {
                    b3 = true;
                    nowUser.company.accountNumber.bunkNumberK = NumberK.Text;
                }
                if (NumberR.Text != null)
                {
                    b4 = true;
                    nowUser.company.accountNumber.bunkNumberR = NumberR.Text;
                }
                if (BIK.Text != null)
                {
                    b5 = true;
                    nowUser.company.accountNumber.bik = BIK.Text;
                }
                if (b1 && b2 && b3 && b4 && b5)
                    Navigation.PushAsync(new RegisterAdvPh.TermOfUse(nowUser));
            })));
        }
    }
}