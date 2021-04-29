using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Advertiser.Campaign;

namespace App1.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class CompaignTemplate: ContentView
    {
        Compaign compaign;
        public CompaignTemplate(Entity entity)
        {
            compaign = (Compaign) entity;
            InitializeComponent();

            gridDrivers.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => { Navigation.PushAsync(new Advertiser.Campaign.Drivers(compaign)); })
            });

            gridHMap.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => { Navigation.PushAsync(new Advertiser.Campaign.HeatMap(compaign)); })
            });
        }

        private async void ToRates(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Rates(compaign.adv, compaign.tarif));
        }
    }
}