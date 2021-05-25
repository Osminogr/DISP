using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using System.Threading.Tasks;
using System.Net.Http;

namespace App1.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class CardDataTemplate : ContentView
    {
        public EventHandler<CardDataTemplate> cardDataHandler;
        public EventHandler<CardData> cardDataDeleteHandler;
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
                    CheckedLogic(showCVV);
                })
            });

            cardDataFrame.GestureRecognizers.Add(new SwipeGestureRecognizer()
            {
                Direction = SwipeDirection.Left,
                Command = new Command(async () =>
                {
                    await btnDelete.ScaleXTo(1, 200, Easing.CubicIn);
                    await Task.Delay(1000);
                    btnDelete.IsVisible = true;
                })
            });

            cardDataFrame.GestureRecognizers.Add(new SwipeGestureRecognizer()
            {
                Direction = SwipeDirection.Right,
                Command = new Command(async () =>
                {
                    await btnDelete.ScaleXTo(0, 200, Easing.CubicOut);
                    await Task.Delay(1000);
                    btnDelete.IsVisible = false;
                })
            });

            if (showCVV)
                CVV.TextChanged += CVV_TextChanged;

            cardChecked.CheckedChanged += delegate
            {

            };

            cardChecked.IsEnabled = false;

            cardChecked.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    CheckedLogic(showCVV);
                })
            });

            cardDataInner = cardData;
            radioButtonInner = cardChecked;
            CVVLabelInner = CVVLabel;
            CVVEntryInnter = CVV;

            btnDelete.Clicked += BtnDelete_ClickedAsync;
        }

        private async void BtnDelete_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                HttpContent httpContent = await Server.DeleteCardData(cardDataInner.id);
                string answer = await httpContent.ReadAsStringAsync();

                if (answer != null && answer.Contains(nameof(CardData)))
                {
                    await Application.Current.MainPage.DisplayAlert("Сообщение", "Карта удалена!", "Закрыть");
                    cardDataDeleteHandler?.Invoke(this, cardDataInner);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Сообщение", "Не удалось выполнить удаление карты! Попробуйте позже.", "Закрыть");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await Application.Current.MainPage.DisplayAlert("Сообщение", "Не удалось выполнить удаление карты! Попробуйте позже.", "Закрыть");
            }

            await btnDelete.ScaleXTo(0, 200, Easing.CubicOut);
            await Task.Delay(1000);
            btnDelete.IsVisible = false;
        }

        private void CheckedLogic(bool showCVVCode)
        {
            cardChecked.IsChecked = true;

            if (showCVVCode)
            {
                CVVLabel.IsVisible = true;
                CVV.IsVisible = true;
            }
            else
            {
                CVVLabel.IsVisible = false;
                CVV.IsVisible = false;
            }

            cardDataHandler?.Invoke(this, this);
        }

        private void CVV_TextChanged(object sender, TextChangedEventArgs e)
        {
            cardDataInner.CVV = ((Entry)sender).Text;
            cardDataHandler?.Invoke(this, this);
        }
    }
}