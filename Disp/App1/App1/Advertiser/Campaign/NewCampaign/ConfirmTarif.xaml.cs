using App1.Advertiser.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

using Newtonsoft.Json;
using System.Net.Http;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmTarif : ContentPage
    {
        Compaign compaign;
        public ConfirmTarif(Compaign now)
        {
            compaign = now;
            InitializeComponent();
            BindingContext = this;

            Amount.Text = String.Format("Итого: {0}Р", compaign.tarif.amount);
            tarifDays.Text = compaign.tarif.minDays;
            tarifMonitor.Text = "10";

            OverrideTitleView(compaign.tarif.name, "Дальше", -1);
        }

        private void OverrideTitleView(string name, string nameAction, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, 80, count, new Command(() => {
                Navigation.PushAsync(new ChoseVid(compaign));
            })));
        }
    }
}