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
using App1.Utils;

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

            videoPlayer.Name = compaign.video.name;
            videoPlayer.Url = compaign.video.url;

            OverrideTitleView(compaign.tarif.name, -1);
        }

        private void OverrideTitleView(string name, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, 80, count));
        }

        public async void NewCampaigns(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(compaign);
            json = "{\"Compaign\": " + json + "}";
            Server.Request(json, "post", "adreqcount");
            await Navigation.PushAsync(new CampaignsAct(compaign.adv));
        }
    }
}