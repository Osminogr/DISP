using App1.Advertiser.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

using Newtonsoft.Json;
using System.Net.Http;
using System.Windows.Input;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoseTarif : ContentPage
    {
        Adv nowUser;
        int type;
        public ChoseTarif(Adv adv)
        {
            this.nowUser = adv;
            InitializeComponent();

            OverrideTitleView("Тарифный план", "Выбрать", -1);

            BindingContext = this;
            type = -1;

            tarif01.GestureRecognizers.Add(new TapGestureRecognizer() {
                Command = new Command(() => {
                    tarif01.BorderColor = Color.FromHex("#FFB800");
                    tarif02.BorderColor = Color.Transparent;
                    tarif03.BorderColor = Color.Transparent;
                    type = 1;
                })
            });

            tarif02.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => {
                    tarif01.BorderColor = Color.Transparent;
                    tarif02.BorderColor = Color.FromHex("#FFB800");
                    tarif03.BorderColor = Color.Transparent;
                    type = 2;
                })
            });

            tarif03.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => {
                    tarif01.BorderColor = Color.Transparent;
                    tarif02.BorderColor = Color.Transparent;
                    tarif03.BorderColor = Color.FromHex("#FFB800");
                    type = 3;
                })
            });
        }

        private void OverrideTitleView(string name, string nameAction, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, count, new Command(() => {
                SelectTarif(type);
            })));
        }

        public async void SelectTarif(int type)
        {
            Tarif tarif = new Tarif();

            if (type == 1)
            {
                tarif.name = "Эконом";
                tarif.amountDay = 60;
                tarif.amountTenDays = 600;
                tarif.amount = 18000;
                tarif.minDays = 30;
                tarif.id = 1;
            }

            if (type == 2)
            {
                tarif.name = "Стандарт";
                tarif.amountDay = 100;
                tarif.amountTenDays = 1000;
                tarif.amount = 30000;
                tarif.minDays = 30;
                tarif.id = 2;
            }

            if (type == 3)
            {
                tarif.name = "Люкс";
                tarif.amountDay = 150;
                tarif.amountTenDays = 1500;
                tarif.amount = 45000;
                tarif.minDays = 30;
                tarif.id = 3;
            }

            Compaign compaign = new Compaign();
            compaign.tarif = tarif;
            compaign.adv = nowUser;

            await Navigation.PushAsync(new ConfirmTarif(compaign));
        }
    }
}