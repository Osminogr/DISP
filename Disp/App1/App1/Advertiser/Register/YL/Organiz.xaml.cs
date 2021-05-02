
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

namespace App1.RegisterAdvYL
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Organiz : ContentPage
    {
        Adv nowUser = new Adv();
        public Organiz(string phone)
        {
            nowUser.company = new Company();
            nowUser.company.phone = phone;
            nowUser.company.director = new Person();
            nowUser.company.manager = new Person();
            nowUser.isCompany = true;

            InitializeComponent();

            OverrideTitleView("Регистрация", "Дальше", 80, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() =>
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
                if (FioDirector.Text != null)
                {
                    b6 = true;
                    nowUser.company.director.lastName = FioDirector.Text;
                }
                if (PositionDirector.Text != null)
                {
                    b7 = true;
                    nowUser.company.director.position = PositionDirector.Text;
                }
                if (FioManager.Text != null)
                {
                    b8 = true;
                    nowUser.company.manager.lastName = FioManager.Text;
                }
                if (PositionManager.Text != null)
                {
                    b9 = true;
                    nowUser.company.manager.position = PositionManager.Text;
                }

                if (b1 && b2 && b3 && b4 && b5 && b6 && b7 && b8 && b9)
                    Navigation.PushAsync(new RegisterAdvYL.Bank(nowUser));
                else
                {
                     DisplayAlert("Сообщение", "Необходимо заполнить всю информацию.", "OK");
                }
            })));
        }
    }
}