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
    public partial class ChangeEmailAdv : ContentPage
    {
        Adv nowUser;
        public ChangeEmailAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            OverrideTitleView("Настройки", 90, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private void GetCodeBtn_Clicked(object sender, EventArgs e)
        {
            Lb_verification.IsVisible = true;
            Entr_verification.IsVisible = true;
            GetCodeBtn.Text = "Готово";
        }
    }
}