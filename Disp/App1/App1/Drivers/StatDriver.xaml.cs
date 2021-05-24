using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

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
            OverrideTitleView("Статистика", 70, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }
    }
}