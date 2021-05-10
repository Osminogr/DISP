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

        public static readonly BindableProperty VideoNameProperty = BindableProperty.Create(
                                                              "videoName",
                                                              typeof(string),
                                                              typeof(CompaignTemplate),
                                                              string.Empty);

        public string videoName
        {
            get => (string)GetValue(VideoNameProperty);
            set => SetValue(VideoNameProperty, value);
        }

        public static readonly BindableProperty UrlProperty = BindableProperty.Create(
                                                              "url",
                                                              typeof(string),
                                                              typeof(CompaignTemplate),
                                                              string.Empty);

        public string url
        {
            get => (string)GetValue(UrlProperty);
            set => SetValue(UrlProperty, value);
        }

        public CompaignTemplate(Entity entity)
        {
            compaign = (Compaign) entity;

            url = compaign.video.url;
            videoName = compaign.video.name;

            InitializeComponent();

            gridDrivers.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => { Navigation.PushAsync(new Advertiser.Campaign.Drivers(compaign)); })
            });

            gridHMap.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => { Navigation.PushAsync(new Advertiser.Campaign.HeatMap(compaign)); })
            });

            tarifName.Text = compaign.tarif.name;
            tarifAmount.Text = String.Format("Минимальная стоимость размещения: {0}Р", compaign.tarif.amount);
            tarifAmountDay.Text = String.Format("День показа на одном экране: {0}Р", compaign.tarif.amountDay);
            tarifDay.Text = String.Format("Минимальный срок размещения: {0} дней", compaign.tarif.minDays);
            tarifAmountTenDays.Text = String.Format("День показа на десяти экранах: {0}Р", compaign.tarif.amountTenDays);
        }

        private async void ToRates(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Rates(compaign.adv, compaign.tarif));
        }
    }
}