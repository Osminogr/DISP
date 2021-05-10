
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Collections.Generic;
using App1.Domain;
using App1.Utils;
using System.Threading;
using App1.Templates;

namespace App1.Drivers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alerts : ContentPage
    {
        Driver nowUser;
        List<Alert> existsAlerts = new List<Alert>();
        bool running;
        private readonly SynchronizationContext _context;

        public Alerts(Driver now)
        {
            InitializeComponent();
            nowUser = now;

            OverrideTitleView("Оповещения", -1);

            _context = SynchronizationContext.Current;

            running = true;
            Thread t = new Thread(new ThreadStart(GetAlerts));
            t.Start();
        }

        private async void GetAlerts()
        {
            while (running)
            {
                try
                {
                    List<Alert> alerts = await Server.GetAlerts(nowUser);

                    if (alerts != null && alerts.Count != 0)
                    {
                        List<View> templates = new List<View>();
                        foreach (var alert in alerts)
                        {
                            if (!IsAlertExists(alert))
                            {
                                templates.Add(new AlertTemplate(alert));
                                existsAlerts.Add(alert);
                            }
                        }

                        if (templates.Count > 0)
                        {
                            foreach (var view in templates)
                            {
                                _context.Send(status => AlertsRoot.Children.Add(view), null);
                                _context.Send(async status => await ScrollAlertsRoot.ScrollToAsync(AlertsRoot, ScrollToPosition.End, true), null);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                Thread.Sleep(1000);
            }
        }

        private bool IsAlertExists(Alert alert)
        {
            bool exists = false;

            if (existsAlerts.Count > 0)
            {
                foreach (var m in existsAlerts)
                {
                    if (m.id == alert.id) exists = true;
                }
            }

            return exists;
        }

        private void OverrideTitleView(string name, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, count));
        }
    }
}