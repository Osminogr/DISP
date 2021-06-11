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
    }
}