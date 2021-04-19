using App1.Advertiser.Campaing;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        }

        public async void Request()
        {

            Grid gr = new Grid();
            gr.Children.Add(new Label { Text = "Тариф: ", TextColor = Color.FromHex("#F39F26") }, 0, 0);
            gr.Children.Add(new Label { Text = "Срок размещения: " }, 1, 0);
            gr.Children.Add(new Label { Text = "Количество автомобилей: " }, 2, 0);
            gr.Children.Add(new Label { Text = "Срок размещения: " }, 3, 0);

            Grid gr1 = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }

            };
            gr1.Children.Add(new Label
            {
                Text = "Водители",
                TextColor = Color.Black,
                FontSize = 18,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 0);

            gr1.Children.Add(new Label
            {
                Text = ">",
                TextColor = Color.FromHex("#F39F26"),
                FontSize = 35,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
            }, 0, 6);

            Button btn = new Button
            {
                BackgroundColor = Color.FromHex("#00000000"),
                CornerRadius = 5,
            };

            btn.Clicked += ToDrivers;

            Grid.SetColumnSpan(btn, 7);
            gr1.Children.Add(btn);


            gr.Children.Add(gr1, 4, 0);
        }
        

        private async void ToDrivers(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drivers(nowUser));
        }

        public async void ToCompl(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CampaignComp());
        }

        public async void NewCampaigns(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Campaign.NewCampaign.ChoseVid(nowUser));
        }
        private async void ToRates(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Rates(nowUser));
        }
        public async void ToHMap(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Campaign.HeatMap(nowUser));
        }

    }
}