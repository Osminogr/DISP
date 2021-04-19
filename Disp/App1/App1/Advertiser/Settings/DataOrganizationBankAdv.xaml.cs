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
    public partial class DataOrganizationBankAdv : ContentPage
    {
        Adv nowUser;
        public DataOrganizationBankAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }
    }
}