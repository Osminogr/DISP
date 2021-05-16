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
    public partial class TarifPlan : ContentPage
    {
        Adv nowUser;
        public TarifPlan(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            OverrideTitleView("Тарифный план", 60, -1);

            tarifInCar.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Navigation.PushAsync(new Tarifs(nowUser, true), true);
                })
            });

            tarifOutCar.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Navigation.PushAsync(new Tarifs(nowUser, false), true);
                })
            });
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }
    }
}