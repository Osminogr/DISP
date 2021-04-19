using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Advertiser.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayPage : ContentPage
    {
        Adv nowUser;
        public PayPage(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }

        private async void tb_edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPayMethod(nowUser));
        }
    }
}