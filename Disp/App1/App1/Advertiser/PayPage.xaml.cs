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

            OverrideTitleView("Оплата", "Изменить", 90, -1);

            LoadCardData();
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() => {
                Navigation.PushAsync(new EditPayMethod(nowUser));
            })));
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
                            checkedHandler = OnSelectedCardData
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

        private void OnSelectedCardData(object sender, RadioButton radioButton)
        {
            if (cardTemplates.Count > 0)
            {
                foreach (var ct in cardTemplates)
                {
                    if (ct.radioButtonInner.Id != radioButton.Id)
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