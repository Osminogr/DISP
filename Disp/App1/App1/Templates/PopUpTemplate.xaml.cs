using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.Templates
{
    public partial class PopUpTemplate: ContentPage
    {
        public static readonly BindableProperty messageProperty = BindableProperty.Create(nameof(message), typeof(string), typeof(PopUpTemplate), default(string));

        public string message
        {
            get => (string)GetValue(messageProperty);
            set => SetValue(messageProperty, value);
        }

        public PopUpTemplate()
        {
            InitializeComponent();
        }

        public async void ShowAlertAsync(string text, View view)
        {
            view.IsVisible = true;
            message = text;
            await view.FadeTo(1, 1000, Easing.CubicIn);
            await Task.Delay(3000);
            await view.FadeTo(0, 1000, Easing.CubicOut);
            view.IsVisible = false;
        }

        public async void GoToAlerts(object sender, EventArgs args)
        {
            if (Server.GetAuthObject().adv != null)
                await Navigation.PushAsync(new App1.Advs.Alerts(Server.GetAuthObject().adv), true);
            else await Navigation.PushAsync(new App1.Drivers.Alerts(Server.GetAuthObject().driver), true);
        }
    }
}