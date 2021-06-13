
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using System.Collections.Generic;
using App1.Utils;
using App1.Templates;

namespace App1.Advertiser.Campaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Drivers : ContentPage
    {

        Compaign compaign;
        List<Driver> drivers;
        public Drivers(Compaign compaign)
        {
            this.compaign = compaign;
            InitializeComponent();
            BindingContext = this;

            drivers = compaign.drivers;

            OverrideTitleView("Водители", 80, -1);

            loadListDrivers();
        }

        private void loadListDrivers()
        {
            if (drivers != null && drivers.Count > 0)
            {
                foreach (var dr in drivers)
                {
                    View view = new DriversTemplate(dr);
                    view.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() => {
                            Navigation.PushAsync(new Statistic(dr, compaign), true);
                        })
                    });
                    driversList.Children.Add(view);
                }
            }
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }
    }
}