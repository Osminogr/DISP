using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1.Advertiser.Campaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rates : ContentPage
    {
        Adv nowUser;
        Tarif tarif;
        public Rates(Adv now, Tarif tarif)
        {
            nowUser = now;
            this.tarif = tarif;
            
            InitializeComponent();

            if (tarif != null)
            {
                if (tarif.id == 1)
                {
                    tarifEconom.BackgroundColor = Color.Silver;
                }

                if (tarif.id == 2)
                {
                    tarifStandard.BackgroundColor = Color.Silver;
                }

                if (tarif.id == 3)
                {
                    tarifLux.BackgroundColor = Color.Silver;
                }
            }
        }
    }
}