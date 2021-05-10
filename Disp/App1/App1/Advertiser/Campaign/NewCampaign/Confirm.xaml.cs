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

            tarifAmount.Text = String.Format("{0}Р", compaign.tarif.amount);
            tarifDays.Text = String.Format("{0} дней", compaign.tarif.minDays);
            tarifMonitor.Text = String.Format("{0} экранов", 10);
        }

        private void OverrideTitleView(string name, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, 80, count));
        }

        public async void NewCampaigns(object sender, EventArgs e)
        {
            HttpContent response = await Server.AddCompaign(compaign);
            string answer = await response.ReadAsStringAsync();

            if (answer != null && answer.Contains(nameof(Compaign)))
            {
                await Navigation.PushAsync(new CampaignsAct(compaign.adv));
            }
            else
            {
                await DisplayAlert("Сообщение", "Не удалось выполнить запуск рекламной компании! Попробуйте позже.", "Закрыть");
                await Navigation.PushAsync(new CampaignsAct(compaign.adv));
            }
        }
    }
}