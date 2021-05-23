using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using App1.Templates;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCard : ContentPage
    {
        Compaign compaign;
        public EventHandler<CardData> cardDataHandler;
        public AddCard(Compaign now)
        {
            compaign = now;
            InitializeComponent();

            OverrideTitleView("Добавление карты", 60, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void Add(object sender, EventArgs args)
        {
            try
            {
                if (PAN.Text == null || (PAN.Text != null && PAN.Text.Length != 19))
                {
                    await DisplayAlert("Сообщение", "Номер карты задан некорректно!", "Закрыть");
                    return;
                }

                if (CardHolder.Text == null || (CardHolder.Text != null && CardHolder.Text.Length < 5) || (CardHolder.Text != null && !Regex.IsMatch(CardHolder.Text.Trim(), @"^[a-zA-Z\s]+$")))
                {
                    await DisplayAlert("Сообщение", "Имя владельца карты задано некорректно!", "Закрыть");
                    return;
                }

                if (ExpDate.Text == null || (ExpDate.Text != null && ExpDate.Text.Length != 5))
                {
                    await DisplayAlert("Сообщение", "Срок действия карты задан некорректно!", "Закрыть");
                    return;
                }

                CardData cardData = new CardData();
                cardData.CardHolder = CardHolder.Text;
                cardData.PAN = PAN.Text;
                cardData.ExpDate = ExpDate.Text;
                cardData.idEntity = compaign.adv.id;

                HttpContent response = await Server.AddCardData(cardData);
                string answer = await response.ReadAsStringAsync();

                if (answer != null && answer.Contains(nameof(CardData)))
                {
                    await DisplayAlert("Сообщение", "Карта успешно добавлена!", "Закрыть");
                    await Navigation.PopAsync(true);
                }
                else
                {
                    await DisplayAlert("Сообщение", "Не удалось добавить карту! Попробуйте позже.", "Закрыть");
                    cardDataHandler?.Invoke(this, cardData);
                    await Navigation.PopAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Сообщение", "Не удалось добавить карту! Попробуйте позже.", "Закрыть");
                await Navigation.PopAsync(true);
            }
        }
    }
}