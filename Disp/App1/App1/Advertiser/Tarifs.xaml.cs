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
    public partial class Tarifs : ContentPage
    {
        Adv nowUser;
        public Tarifs(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            OverrideTitleView("Тарифный план", 60, -1);

            LoadTarifs();
        }

        private async void LoadTarifs()
        {
            List<Tarif> tarifs = await Server.GetTarifs();

            if (tarifs != null && tarifs.Count > 0)
            {
                foreach (var tarif in tarifs)
                {
                    TarifTemplate tarifTemplate = new TarifTemplate(tarif, true);
                    tarifsRoot.Children.Add(tarifTemplate);
                }
            }
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }
    }
}