using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace App1.Advertiser.Campaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeatMap : ContentPage
    {
        Compaign compaign;
        public HeatMap(Compaign now)
        {
            compaign = now;
            MoveMap();
            InitializeComponent();

            OverrideTitleView("Тепловая карта", 60, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void MoveMap()
        {
            var locator = CrossGeolocator.Current;
            Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();
            position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                            Distance.FromMiles(1)));
        }
    }
}