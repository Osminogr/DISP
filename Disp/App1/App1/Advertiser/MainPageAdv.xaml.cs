using App1.Advertiser;
using App1.Advertiser.Settings;
using Plugin.Geolocator;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using App1.Domain;
using Xamarin.Essentials;
using App1.Advertiser.Campaign;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageAdv : ContentPage
    {
        Adv nowUser;
        public MainPageAdv(Adv now)
        {
            nowUser = now;

            MoveMap();
            InitializeComponent();

            actInd.IsRunning = true;
            actInd.IsVisible = true;
            gridRoot.IsEnabled = false;

            NavigationPage.SetHasNavigationBar(this, false);
            SideBar.IsVisible = false;
            SideBarBottom.IsVisible = true;
        }

        private async void MoveMap()
        {
            var locator = CrossGeolocator.Current;
            Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();
            position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                            Distance.FromMiles(1)));

            await Task.Delay(1000);
            actInd.IsRunning = false;
            actInd.IsVisible = false;
            gridRoot.IsEnabled = true;
        }

        public async void Activate(object sender, EventArgs e)
        {
            await DisplayAlert("Ошибка", "Аккаунт не подтверждён", "OK");
        }

        public void Open(object sender, EventArgs e)
        {
            SideBar.IsEnabled = true;
            SideBar.IsVisible = true;
        }

        public void Close(object sender, EventArgs e)
        {
            SideBar.IsEnabled = false;
            SideBar.IsVisible = false;
        }

        public void OpenBottom(object sender, EventArgs e)
        {
            SideBarBottom.IsEnabled = true;
            SideBarBottom.IsVisible = true;
        }

        public void CloseBottom(object sender, EventArgs e)
        {
            SideBarBottom.IsEnabled = false;
            SideBarBottom.IsVisible = false;
        }

        public async void Videos(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideosAct(nowUser));
        }

        public async void Alerts(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Advs.Alerts(nowUser));
        }

        public async void Chat(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Advs.Chat(nowUser));
        }

        public async void Statistic(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Statistic(null));
        }

        public async void PayMethod(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PayPage(nowUser));
        }

        public async void NewСampaign(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CampaignsAct(nowUser));
        }

        public async void Settings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsAdv(nowUser));
        }

        public async void Exit(object sender, EventArgs e)
        {
            Preferences.Clear();
            await Navigation.PopToRootAsync();
        }


        public async void Switched(object sender, EventArgs e)
        {

        }

        public async void LoadVideo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideosAct(nowUser));
        }

        public async void NewCompaign(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Advertiser.Campaign.NewCampaign.ChoseTarif(nowUser));
        }

        public async void LoadStats(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Statistic(null));
        }
    }
}