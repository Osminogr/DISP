using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using App1.Templates;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TarifDetail : ContentPage
    {
        Tarif tarif;
        public TarifDetail(Tarif now)
        {
            tarif = now;
            InitializeComponent();

            OverrideTitleView(tarif.name, 90, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }
    }
}