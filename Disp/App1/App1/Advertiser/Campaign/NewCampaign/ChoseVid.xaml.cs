using App1.Advertiser.Campaing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoseVid : ContentPage
    {
        Adv nowUser;
        public ChoseVid(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }
        private async void ToConf(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Confirm(nowUser));
        }

    }
}