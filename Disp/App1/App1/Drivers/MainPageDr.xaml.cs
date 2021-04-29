using App1.Drivers;
using Plugin.Geolocator;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDr : ContentPage
    {
        Driver nowUser;
        public MainPageDr(Driver now)
        {
            nowUser = now;

            MoveMap();
            InitializeComponent();
            UserName.Text = nowUser.person.firstName + " " + nowUser.person.lastName;

            NavigationPage.SetHasNavigationBar(this, false);
        }


        private async void MoveMap()
        {
            var locator = CrossGeolocator.Current;
            Plugin.Geolocator.Abstractions.Position position;
            position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                            Distance.FromMiles(1)));

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
            await Navigation.PopToRootAsync();
        }
    }
}