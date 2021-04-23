using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

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
        }

        private void GetCodeBtn_Clicked(object sender, EventArgs e)
        {
            Lb_verification.IsVisible = true;
            Entr_verification.IsVisible = true;
            GetCodeBtn.Text = "Готово";
        }
    }
}