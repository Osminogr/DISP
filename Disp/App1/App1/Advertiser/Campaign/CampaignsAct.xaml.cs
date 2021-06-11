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
            Request(true);

            OverrideTitleView("Мои компании", -1);
        }

        private void OverrideTitleView(string name, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, count));
        }

        public async void Request(bool isActive)
        {
            compaignAct.Children.Clear();
            compaignCompl.Children.Clear();
            bool loaded = false;

            List<Compaign> list = await Server.GetCompaigns(nowUser.id);

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (isActive)
                    {
                        if (item.active && item.paid)
                        {
                            compaignAct.Children.Add(new CompaignTemplate(item));
                            loaded = true;
                        }
                    }
                    /*
                    else
                    {
                        if (!item.active)
                        {
                            compaignCompl.Children.Add(new CompaignTemplate(item));
                            loaded = true;
                        }
                    }
                    */
                }
            }

            if (!loaded)
            {
                Label label = new Label();
                label.Text = "У Вас нет рекламных компаний";
                label.HorizontalOptions = LayoutOptions.Center;
                label.VerticalOptions = LayoutOptions.Center;

                if (isActive) compaignAct.Children.Add(label);
                else compaignCompl.Children.Add(label);

                OverrideTitleView("Мои компании", -1);
            }
        }

        public void ToCompl(object sender, EventArgs e)
        {
            compaignActCont.IsVisible = false;
            compaignComplCont.IsVisible = true;
            btnAct.TextColor = Color.FromHex("#BCBCBC");
            btnCompl.TextColor = Color.FromHex("#F39F26");
            Request(false);
        }

        public void ToAct(object sender, EventArgs e)
        {
            compaignComplCont.IsVisible = false;
            compaignActCont.IsVisible = true;
            btnAct.TextColor = Color.FromHex("#F39F26");
            btnCompl.TextColor = Color.FromHex("#BCBCBC");
            Request(true);
        }

        public async void NewCampaigns(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Campaign.NewCampaign.ChoseVid(nowUser));
        }
    }
}