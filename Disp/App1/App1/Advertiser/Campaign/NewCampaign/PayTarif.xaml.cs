using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayTarif : ContentPage
    {
        Compaign nowUser;
        public PayTarif(Compaign now)
        {
            nowUser = now;
            InitializeComponent();

            OverrideTitleView("Оплата", 90, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void Pay(object sender, EventArgs args)
        {
            await DisplayAlert("Сообщение", "Рекламная компания создана! В данный момент ожидаем оплату для ее запуска.", "Закрыть");
            await Navigation.PushAsync(new CampaignsAct(nowUser.adv), true);
        }
    }
}