using App1.Advertiser.Campaign;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Templates;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using App1.Utils;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CampaignsAct : ContentPage
    {
        Adv nowUser;
        public CampaignsAct(Adv now)
        {
            nowUser = now;
            InitializeComponent();
            Request();

            OverrideTitleView("Мои компании", -1);
        }

        private void OverrideTitleView(string name, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, count));
        }

        public async void Request()
        {
            List<Compaign> list = await Server.GetCompaigns(nowUser.id);

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item.active)
                        compaignAct.Children.Add(new CompaignTemplate(item));
                    else
                        compaignCompl.Children.Add(new CompaignTemplate(item));
                }
            }
        }

        public void ToCompl(object sender, EventArgs e)
        {
            compaignActCont.IsVisible = false;
            compaignComplCont.IsVisible = true;
            btnAct.TextColor = Color.FromHex("#BCBCBC");
            btnCompl.TextColor = Color.FromHex("#F39F26");
        }

        public void ToAct(object sender, EventArgs e)
        {
            compaignComplCont.IsVisible = false;
            compaignActCont.IsVisible = true;
            btnAct.TextColor = Color.FromHex("#F39F26");
            btnCompl.TextColor = Color.FromHex("#BCBCBC");
        }

        public async void NewCampaigns(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Campaign.NewCampaign.ChoseTarif(nowUser));
        }
    }
}