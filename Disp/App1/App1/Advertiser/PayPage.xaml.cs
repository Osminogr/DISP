using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using App1.Templates;
using App1.Advertiser.Campaign.NewCampaign;

namespace App1.Advertiser.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayPage : ContentPage
    {
        Adv nowUser;
        List<CardDataTemplate> cardTemplates = new List<CardDataTemplate>();
        public PayPage(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            OverrideTitleView("Оплата", 90, -1);

            newCard.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () => {
                    await Navigation.PushAsync(new AddCard(nowUser)
                    {
                        cardDataHandler = OnAddCardFromAdd
                    }, true);
                })
            });

            LoadCardData();
        }

        private void OnAddCardFromAdd(object sender, CardData data)
        {
            LoadCardData();
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void LoadCardData()
        {
            try
            {
                List<CardData> cardDatas = await Server.GetCardDatas(nowUser);

                if (cardDatas != null && cardDatas.Count != 0)
                {
                    cardDataList.Children.Clear();

                    foreach (var card in cardDatas)
                    {
                        CardDataTemplate cardDataTemplate = new CardDataTemplate(card, false) {
                            cardDataHandler = OnSelectedCardData,
                            cardDataDeleteHandler = OnAddCardFromAdd
                        };

                        cardTemplates.Add(cardDataTemplate);

                        cardDataList.Children.Add(cardDataTemplate);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnSelectedCardData(object sender, CardDataTemplate cardDataTemplate)
        {
            if (cardTemplates.Count > 0)
            {
                foreach (var ct in cardTemplates)
                {
                    if (ct.radioButtonInner.Id != cardDataTemplate.radioButtonInner.Id)
                    {
                        ct.radioButtonInner.IsChecked = false;
                        ct.CVVEntryInnter.IsVisible = false;
                        ct.CVVLabelInner.IsVisible = false;
                    }
                }
            }
        }
    }
}