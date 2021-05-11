using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Advertiser.Campaign;
using App1.Advertiser;

namespace App1.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class TarifTemplate: ContentView
    {
        public EventHandler<Tarif> selectedTarif;
        public Tarif tarifInner;
        public TarifTemplate(Tarif tarif, bool info)
        {
            InitializeComponent();

            if (info)
            {
                tarifView.IsVisible = false;
                tarifViewInfo.IsVisible = true;

                tarifNameInfo.Text = tarif.name;

                tarifViewInfo.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = new Command(() => {
                        Navigation.PushAsync(new TarifDetail(tarif), true);
                    })
                });
            }
            else
            {
                tarifView.IsVisible = true;
                tarifViewInfo.IsVisible = false;

                tarifName.Text = tarif.name;
                tarifAmount.Text = String.Format("Минимальная стоимость размещения: {0}Р", tarif.amount);
                tarifDays.Text = String.Format("Минимальный срок размещения: {0} дней", tarif.minDays);
                tarifAmountDay.Text = String.Format("День показа на одном экране: {0}Р", tarif.amountDay);
                tarifAmountTenDays.Text = String.Format("День показа на десяти экранах: {0}Р", tarif.amountTenDays);

                tarifView.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = new Command(() => {
                        tarifView.BorderColor = Color.FromHex("#FFB800");

                        selectedTarif?.Invoke(this, tarif);
                    })
                });
            }

            tarifInner = tarif;
        }
    }
}