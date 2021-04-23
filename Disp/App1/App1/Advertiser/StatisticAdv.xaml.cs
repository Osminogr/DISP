using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticAdv : ContentPage
    {
        Adv nowUser;
        public StatisticAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }
    }
}