
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Collections.Generic;
using App1.Domain;
using App1.Utils;

namespace App1.Advs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alerts : ContentPage
    {
        Adv nowUser;
        List<Alert> alerts;
        public Alerts(Adv now)
        {
            InitializeComponent();
            nowUser = now;
            Request();

            OverrideTitleView("Оповещения", -1);
        }

        public async void Request()
        {
            alerts = await Server.GetAlerts(nowUser);
        }

        private void OverrideTitleView(string name, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, count));
        }
    }
}