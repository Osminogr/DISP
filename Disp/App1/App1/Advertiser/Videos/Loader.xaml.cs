using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loader : ContentPage
    {
        Adv nowUser;
        public Loader(Adv now)
        {
            nowUser = now;
            InitializeComponent();
            LoadForm.Source = "http://46.101.167.149:8003/api/loader/" + now.phone;
        }
    }
}