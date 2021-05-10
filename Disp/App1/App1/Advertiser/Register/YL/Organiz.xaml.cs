
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
        public Organiz(string phone, bool isAgent)
        {
            nowUser.company = new Company();
            nowUser.company.phone = phone;
            nowUser.company.director = new Person();
            nowUser.company.manager = new Person();
            nowUser.isAgent = isAgent;
            nowUser.isCompany = true;

            InitializeComponent();

            OverrideTitleView("Регистрация", "Дальше", 80, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, b6 = false, b7 = false, b8 = false, b9 = false;
                if (Name.Text != null) b1 = true;
                if (Address.Text != null) b2 = true;
                if (Ogrn.Text != null) b3 = true;
                if (Inn.Text != null) b4 = true;
                if (Kpp.Text != null) b5 = true;
                if (FioDirector.Text != null) b6 = true;
                if (PositionDirector.Text != null) b7 = true;
                if (FioManager.Text != null) b8 = true;
                if (PositionManager.Text != null) b9 = true;

                if (b1 && b2 && b3 && b4 && b5 && b6 && b7 && b8 && b9)
                {
                    nowUser.company.name = Name.Text;
                    nowUser.company.address = Address.Text;
                    nowUser.company.ogrn = Ogrn.Text;
                    nowUser.company.inn = Inn.Text;
                    nowUser.company.kpp = Kpp.Text;
                    nowUser.company.director.lastName = FioDirector.Text;
                    nowUser.company.director.position = PositionDirector.Text;
                    nowUser.company.manager.lastName = FioManager.Text;
                    nowUser.company.manager.position = PositionManager.Text;

                    Navigation.PushAsync(new RegisterAdvYL.Bank(nowUser));
                }
                else DisplayAlert("Сообщение", "Необходимо заполнить всю информацию!", "Закрыть");
            })));
        }
    }
}