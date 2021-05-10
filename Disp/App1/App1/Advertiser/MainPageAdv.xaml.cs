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
using System.Collections.Generic;

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

            companyName.Text = now.company.name;

            AuthObject authObject = Server.GetAuthObject();
            if (authObject != null) showAllDrivers.IsToggled = authObject.showAllDrivers;

            LoadStatsVideos();
        }

        private async void LoadStatsVideos()
        {
            try
            {
                List<Video> videos = await Server.GetVideos(nowUser, false);
                if (videos != null)
                {
                    if (videos.Count == 0) videosCount.Text = "У Вас нет видеороликов";
                    else videosCount.Text = String.Format("У Вас {0} видеоролик(ов)", videos.Count);
                }

                List<Compaign> compaigns = await Server.GetCompaigns(nowUser.id);
                if (compaigns != null)
                {
                    if (compaigns.Count == 0) compaignCount.Text = "У Вас нет рекламных компаний";
                    else compaignCount.Text = String.Format("У Вас {0} рекламных компаний", compaigns.Count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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
            CloseBottomMenu();
            OpenLeftMenu();
        }

        private void OpenLeftMenu()
        {
            SideBar.IsEnabled = true;
            SideBar.IsVisible = true;
            menuBtn.IsVisible = false;
        }

        public void Close(object sender, EventArgs e)
        {
            CloseLeftMenu();
        }

        private void CloseLeftMenu()
        {
            SideBar.IsEnabled = false;
            SideBar.IsVisible = false;
            menuBtn.IsVisible = true;
        }

        public void OpenBottom(object sender, EventArgs e)
        {
            CloseLeftMenu();
            OpenBottomMenu();
        }

        public void CloseBottom(object sender, EventArgs e)
        {
            CloseBottomMenu();
        }

        private void CloseBottomMenu()
        {
            SideBarBottom.IsEnabled = false;
            Grid.SetRow(SideBarBottom, 7);
            Grid.SetRowSpan(SideBarBottom, 1);
            SideBarBottom.Margin = new Thickness(0, 20, 0, -60);
        }

        private void OpenBottomMenu()
        {
            SideBarBottom.IsEnabled = true;
            Grid.SetRow(SideBarBottom, 5);
            Grid.SetRowSpan(SideBarBottom, 4);
            SideBarBottom.Margin = new Thickness(0, 0, 0, -60);
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
            Server.ClearAuthObject();
            await Navigation.PushAsync(new StartPage());
        }


        public void Switched(object sender, EventArgs e)
        {
            Server.SaveShowAllDrivers(showAllDrivers.IsToggled);
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