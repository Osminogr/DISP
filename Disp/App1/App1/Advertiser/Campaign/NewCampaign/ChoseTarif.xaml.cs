using App1.Advertiser.Campaing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

using Newtonsoft.Json;
using System.Net.Http;
using System.Windows.Input;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoseTarif : ContentPage
    {
        Compaign compaign;
        public ChoseTarif(Compaign compaign)
        {
            this.compaign = compaign;
            InitializeComponent();

            BindingContext = this;
        }

        public async void SelectTarif(object sender, EventArgs e)
        {
            Tarif tarif = new Tarif();
            int type = int.Parse((string)((Button)sender).CommandParameter);

            if (type == 1)
            {
                tarif.amountDay = 60;
                tarif.amountTenDays = 600;
                tarif.amount = 18000;
                tarif.minDays = 30;
            }

            if (type == 2)
            {
                tarif.amountDay = 100;
                tarif.amountTenDays = 1000;
                tarif.amount = 30000;
                tarif.minDays = 30;
            }

            if (type == 3)
            {
                tarif.amountDay = 150;
                tarif.amountTenDays = 1500;
                tarif.amount = 45000;
                tarif.minDays = 30;
            }

            compaign.tarif = tarif;

            await Navigation.PushAsync(new Confirm(compaign));
        }
    }
}