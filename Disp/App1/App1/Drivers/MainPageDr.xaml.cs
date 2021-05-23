using App1.Drivers;
using Plugin.Geolocator;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using App1.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDr : ContentPage
    {
        Driver nowUser;
        bool running;
        private readonly SynchronizationContext _context;

        public MainPageDr(Driver now)
        {
            nowUser = now;

            InitializeComponent();

            MoveMap();

            NavigationPage.SetHasNavigationBar(this, false);
            SideBar.IsVisible = false;
            SideBarBottom.IsVisible = true;

            bottomName.Text = String.Format("Привет {0} {1} {2}!", nowUser.person.lastName, nowUser.person.firstName, nowUser.person.patronymic);
            UserName.Text = String.Format("{0} {1} {2}!", nowUser.person.lastName, nowUser.person.firstName, nowUser.person.patronymic);

            LoadVideos();

            _context = SynchronizationContext.Current;
        }

        private async void SendCoords()
        {
            while (running)
            {
                try
                {
                    Plugin.Geolocator.Abstractions.Position position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(1));

                    await Server.AddCoord(new Coords()
                    {
                        lat = position.Latitude.ToString(),
                        ltd = position.Longitude.ToString(),
                        fioDriver = String.Format("{0} {1} {2}", nowUser.person.lastName, nowUser.person.firstName, nowUser.person.patronymic),
                        car = String.Format("{0} {1} {2}", nowUser.car.mark, nowUser.car.model, nowUser.car.regNumberCar),
                        idDriver = nowUser.id
                    });
                    await Task.Delay(60000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private async void LoadVideos()
        {
            try
            {
                List<Video> videos = await Server.GetVideos(nowUser, true);

                if (videos != null)
                {
                    if (videos.Count == 0) VideosCount.Text = "У Вас нет видеороликов";
                    else VideosCount.Text = String.Format("У Вас {0} видеоролик(ов)", videos.Count);
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
                Plugin.Geolocator.Abstractions.Position position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(1));
                Pin pin = new Pin()
                {
                    Position = new Position(position.Latitude, position.Longitude),
                    Label = "Мое местоположение"
                };

                map.Pins.Add(pin);
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
            Grid.SetRow(SideBarBottom, 8);
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

        private void OpenLeftMenu()
        {
            SideBar.IsEnabled = true;
            SideBar.IsVisible = true;
            menuBtn.IsVisible = false;
        }

        private void CloseLeftMenu()
        {
            SideBar.IsEnabled = false;
            SideBar.IsVisible = false;
            menuBtn.IsVisible = true;
        }

        public void Activate(object sender, EventArgs e)
        {
            if (btnActivate.Text == "Активировать")
            {
                running = true;
                Thread t = new Thread(new ThreadStart(SendCoords));
                t.Start();
                btnActivate.Text = "Деактивировать";
            }
            else
            {
                running = false;
                btnActivate.Text = "Активировать";
            }
        }

        public void Open(object sender, EventArgs e)
        {
            CloseBottomMenu();
            OpenLeftMenu();
        }

        public void Close(object sender, EventArgs e)
        {
            CloseLeftMenu();
        }

        public async void Videos(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Videos(nowUser));
        }

        public async void Balance(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Balance());
        }

        public async void Alerts(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drivers.Alerts(nowUser));
        }

        public async void StatDriver_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drivers.StatDriver(nowUser));
        }

        public async void Chat(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Chat(nowUser));
        }

        public async void Settings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings(nowUser));
        }

        public async void Exit(object sender, EventArgs e)
        {
            running = false;
            Server.ClearAuthObject();
            await Navigation.PushAsync(new StartPage());
        }
    }
}