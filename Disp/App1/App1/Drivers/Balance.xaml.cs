
using App1.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Drivers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Balance : ContentPage
    {
        public Balance()
        {
            InitializeComponent();

            OverrideTitleView("Баланс", 100, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }
    }
}