using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

using Newtonsoft.Json;
using System.Net.Http;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Confirm : ContentPage
    {
        Compaign compaign;
        public Confirm(Compaign compaign)
        {
            this.compaign = compaign;

            InitializeComponent();

            BindingContext = this;

            labelTextVideo.Text = compaign.video.name;
            videoPlayer.Source = compaign.video.url;
            amount.Text = string.Format("Итого: {0}Р", compaign.tarif.amount.ToString());
            minDays.Text = string.Format("Срок размещения: {0} дней", compaign.tarif.minDays.ToString());
        }

        public async void NewCampaigns(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(compaign);
            await Navigation.PushAsync(new CampaignsAct(compaign.adv));
        }
    }
}