using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

namespace App1.Advertiser.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataOrganizationBankAdv : ContentPage
    {
        Adv nowUser;
        public DataOrganizationBankAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();
            OverrideTitleView("Настройки", "Сохранить", 90, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() =>
            {
                Navigation.PopAsync();
            })));
        }
    }
}