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

        public static readonly BindableProperty tarifNameProperty = BindableProperty.Create(
                                                              "tarifName",
                                                              typeof(string),
                                                              typeof(CompaignTemplate),
                                                              string.Empty);

        public string tarifName
        {
            get => (string)GetValue(tarifNameProperty);
            set => SetValue(tarifNameProperty, value);
        }

        public static readonly BindableProperty amountProperty = BindableProperty.Create(
                                                              "amount",
                                                              typeof(string),
                                                              typeof(CompaignTemplate),
                                                              string.Empty);

        public string amount
        {
            get => (string)GetValue(amountProperty);
            set => SetValue(amountProperty, value);
        }

        public CompaignTemplate(Entity entity)
        {
            compaign = (Compaign) entity;

            url = compaign.video.url;
            tarifName = compaign.tarif.name;
            videoName = compaign.video.name;
            amount = compaign.tarif.amount.ToString();

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