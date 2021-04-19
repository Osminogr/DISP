using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Drivers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatDriver : ContentPage
    {
        Driver nowUser;
        public StatDriver(Driver now)
        {
            nowUser = now;
            InitializeComponent();
        }
    }
}