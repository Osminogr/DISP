using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Advertiser.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DogovorAdv : ContentPage
    {
        Adv nowUser;
        public DogovorAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }
    }
}