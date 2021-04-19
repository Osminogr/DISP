using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Advertiser.Campaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeatMap : ContentPage
    {
        Adv nowUser;
        public HeatMap(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }
    }
}