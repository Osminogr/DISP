using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1.Advertiser.Campaing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rates : ContentPage
    {
        Adv nowUser;
        public Rates(Adv now)
        {
            nowUser = now;
            InitializeComponent();
            Request();
        }

        public async void Request() 
        { 
            
        }
    }
}