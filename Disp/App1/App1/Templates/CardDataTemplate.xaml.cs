using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class CardDataTemplate : ContentView
    {
        public EventHandler<CardData> cardDataHandler;
        public EventHandler<RadioButton> checkedHandler;
        public CardData cardDataInner;
        public RadioButton radioButtonInner;
        public Label CVVLabelInner;
        public Entry CVVEntryInnter;

        public CardDataTemplate(CardData cardData, bool showCVV)
        {
            InitializeComponent();

            PAN.Text = String.Format("*{0}", cardData.PAN.Substring(cardData.PAN.Length - 4));

            cardDataFrame.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    cardChecked.IsChecked = true;

                    if (showCVV)
                    {
                        CVVLabel.IsVisible = true;
                        CVV.IsVisible = true;
                    }
                    else
                    {
                        CVVLabel.IsVisible = false;
                        CVV.IsVisible = false;
                    }

                    cardDataHandler?.Invoke(this, cardDataInner);
                    checkedHandler?.Invoke(this, cardChecked);
                })
            });

            cardChecked.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    cardChecked.IsChecked = true;

                    if (showCVV)
                    {
                        CVVLabel.IsVisible = true;
                        CVV.IsVisible = true;
                    }
                    else
                    {
                        CVVLabel.IsVisible = false;
                        CVV.IsVisible = false;
                    }

                    cardDataHandler?.Invoke(this, cardDataInner);
                    checkedHandler?.Invoke(this, cardChecked);
                })
            });

            if (showCVV)
                CVV.TextChanged += CVV_TextChanged;

            cardDataInner = cardData;
            radioButtonInner = cardChecked;
            CVVLabelInner = CVVLabel;
            CVVEntryInnter = CVV;
        }

        private void CVV_TextChanged(object sender, TextChangedEventArgs e)
        {
            cardDataInner.CVV = ((Entry)sender).Text;
            cardDataHandler?.Invoke(this, cardDataInner);
        }
    }
}