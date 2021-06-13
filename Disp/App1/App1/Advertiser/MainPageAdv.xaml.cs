using App1.Advertiser;
using App1.Advertiser.Settings;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Advertiser.Campaign;
using System.Threading.Tasks;
using System.Collections.Generic;
using App1.Templates;
using System.Threading;
using Plugin.Geolocator;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageAdv : PopUpTemplate
    {
        Adv nowUser;
        bool running = false;
        private readonly SynchronizationContext _context;
        public MainPageAdv(Adv now)
        {
            if (now == null)
            {
                try
                {
                    nowUser = Server.GetAuthObject().adv;

                    if (nowUser == null)
                    {
                        Navigation.PushAsync(new StartPage());
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Navigation.PushAsync(new StartPage());
                    return;
                }
            }
            else nowUser = now;

            InitializeComponent();

            _context = SynchronizationContext.Current;

            gridRoot.Opacity = 0;

            MoveMap();

            NavigationPage.SetHasNavigationBar(this, false);
            SideBar.IsVisible = false;
            SideBarBottom.IsVisible = true;

            companyName.Text = now.company.name;
            bottomName.Text = String.Format("Привет {0}!", now.company.name);

            AuthObject authObject = Server.GetAuthObject();
            if (authObject != null) showAllDrivers.IsToggled = authObject.showAllDrivers;

            LoadStatsVideos();
        }

        protected override void OnDisappearing()
        {
            running = false;
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            running = true;
            Thread thrd = new Thread(Callback);
            thrd.Start();

            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            Server.ClearAuthObject();
            return base.OnBackButtonPressed();
        }

        private async void Callback()
        {
            while (running)
            {
                await Task.Delay(10000);
                List<Alert> alerts = await Server.GetAlerts(nowUser);
                if (alerts != null && alerts.Count != Server.GetAuthObject().countAlerts)
                {
                    _context.Send(status => ShowAlertAsync(String.Format("У Вас {0} новое(ых) оповещение(й)!", alerts.Count - Server.GetAuthObject().countAlerts), (View)this.GetTemplateChild("alertViewPopUp")), null);
                    _context.Send(status => alertsLabel.Text = String.Format("Оповещения({0})", alerts.Count - Server.GetAuthObject().countAlerts), null);
                    Server.SaveCountAlertsAuthObject(alerts.Count, alerts.Count - Server.GetAuthObject().countAlerts);
                }

                if (Server.GetAuthObject().countNewAlerts == 0) _context.Send(status => alertsLabel.Text = "Оповещения", null);
            }
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
            try
            {
                await LoadDriversMapAsync();

                var locator = CrossGeolocator.Current;
                Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();
                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(1000);
            actInd.IsRunning = false;
            actInd.IsVisible = false;
            gridRoot.Opacity = 1;
        }

        private async Task<bool> LoadDriversMapAsync()
        {
            try
            {
                List<Coords> coords = await Server.GetDriverCoords();

                if (coords != null && coords.Count != 0)
                {
                    foreach (var crd in coords)
                    {
                        Pin pin = new Pin()
                        {
                            Position = new Position(Double.Parse(crd.lat), Double.Parse(crd.ltd)),
                            Label = crd.fioDriver,
                            Address = crd.car,
                            StyleId = crd.idDriver.ToString()
                        };

                        pin.InfoWindowClicked += Pin_InfoWindowClickedAsync;

                        map.Pins.Add(pin);
                    }

                    //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Double.Parse(coords[0].lat), Double.Parse(coords[0].ltd)), Distance.FromMeters(100)));
                }

                await Task.Delay(1000);

                return true;
            }
            catch (Exception ex)
            {
                await Task.Delay(1000);

                Console.WriteLine(ex);

                return false;
            }
        }

        private async void Pin_InfoWindowClickedAsync(object sender, PinClickedEventArgs e)
        {
            Driver driver = await Server.GetDriver(ushort.Parse(((Pin)sender).StyleId));

            await Navigation.PushAsync(new StatisticAdv(nowUser), true);
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
            SideBarBottom.Margin = new Thickness(0, -10, 0, -20);
        }

        private void OpenBottomMenu()
        {
            SideBarBottom.IsEnabled = true;
            Grid.SetRow(SideBarBottom, 5);
            Grid.SetRowSpan(SideBarBottom, 4);
            SideBarBottom.Margin = new Thickness(0, -10, 0, -20);
        }

        public async void Videos(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideosAct(nowUser), true);
        }

        public async void Tarifs(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TarifPlan(nowUser), true);
        }

        public async void Alerts(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Advs.Alerts(nowUser), true);
        }

        public async void Chat(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Advs.Chat(nowUser), true);
        }

        public async void Statistic(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatisticAdv(nowUser), true);
        }

        public async void PayMethod(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PayPage(nowUser), true);
        }

        public async void NewСampaign(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CampaignsAct(nowUser), true);
        }

        public async void Settings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsAdv(nowUser), true);
        }

        public async void Exit(object sender, EventArgs e)
        {
            Server.ClearAuthObject();
            await Navigation.PushAsync(new StartPage(), true);
        }

        public void Switched(object sender, EventArgs e)
        {
            Server.SaveShowAllDrivers(showAllDrivers.IsToggled);
        }

        public async void LoadVideo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideosAct(nowUser), true);
        }

        public async void NewCompaign(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Advertiser.Campaign.NewCampaign.ChoseVid(nowUser), true);
        }

        public async void LoadStats(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatisticAdv(nowUser), true);
        }
    }
}