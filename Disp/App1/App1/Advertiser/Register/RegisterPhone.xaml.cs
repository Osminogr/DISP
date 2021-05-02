using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using App1.RegisterAdvYL;

namespace App1.Advertiser.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPhone : ContentPage
    {
        bool isCompany;
        public RegisterPhone(bool isCompany)
        {
            this.isCompany = isCompany;
            InitializeComponent();
            OverrideTitleView("Регистрация", "Дальше", 80, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() => {
            if (phone.Text != null && phone.Text.Length > 0)
                {
                    if (isCompany)
                    {
                        Navigation.PushAsync(new Organiz(phone.Text));
                    }
                    
                }
            })));
        }
    }
}