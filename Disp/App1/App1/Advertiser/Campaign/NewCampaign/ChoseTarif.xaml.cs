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
using App1.Templates;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoseTarif : ContentPage
    {
        Compaign compaign;
        bool inCar;
        Tarif selectedTarif;
        List<TarifTemplate> tarifTemplates = new List<TarifTemplate>();
        public ChoseTarif(Compaign now, bool isInCar)
        {
            compaign = now;
            inCar = isInCar;
            InitializeComponent();

            OverrideTitleView("Тарифный план", "Выбрать", -1);

            Request();
        }

        private async void Request()
        {
            try
            {
                List<Tarif> tarifs = await Server.GetTarifs();

                if (tarifs != null && tarifs.Count != 0)
                {
                    foreach(var tarif in tarifs)
                    {
                        if (tarif.isInCar == inCar)
                        {
                            TarifTemplate view = new TarifTemplate(tarif, false)
                            {
                                selectedTarif = OnTarifSelected
                            };
                            tarifsView.Children.Add(view);

                            tarifTemplates.Add(view);
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Сообщение", "В данный момент тарифы недоступны! Попробуйте позже.", "Закрыть");
                    await Navigation.PopAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnTarifSelected(object sender, Tarif tarif)
        {
            this.selectedTarif = tarif;

            if (tarifTemplates.Count > 0)
            {
                foreach (var template in tarifTemplates)
                {
                    if (template.tarifInner.id != selectedTarif.id)
                    {
                        ((Frame)template.FindByName("tarifView")).BorderColor = Color.Transparent;
                    }
                }
            }
        }

        private void OverrideTitleView(string name, string nameAction, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, count, new Command(() => {
                SelectTarif(selectedTarif);
            })));
        }

        public async void SelectTarif(Tarif tarif)
        {
            compaign.tarif = tarif;

            await Navigation.PushAsync(new ConfirmTarif(compaign), true);
        }
    }
}