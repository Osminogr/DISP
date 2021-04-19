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
    public partial class Confirm : ContentPage
    {
        Adv nowUser;
        public Confirm(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }
        public async void NewCampaigns(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CampaignsAct(nowUser));
        }
    }
}