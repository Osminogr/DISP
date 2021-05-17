using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

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
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new Tarifs(nowUser, false), true);
                })
            });
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }
    }
}