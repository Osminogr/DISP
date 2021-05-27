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
using App1.Domain.Json;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCard : ContentPage
    {
        public Entity entity;
        public EventHandler<CardData> cardDataHandler;
        public AddCard(Entity ent)
        {
            InitializeComponent();

            entity = ent;

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

                if (CVV.Text == null || (CVV.Text != null && CVV.Text.Length != 3))
                {
                    await DisplayAlert("Сообщение", "CVV карты задан некорректно!", "Закрыть");
                    return;
                }

                CardData cardData = new CardData();
                cardData.CardHolder = CardHolder.Text;
                cardData.PAN = PAN.Text;
                cardData.ExpDate = ExpDate.Text;
                cardData.idEntity = entity.id;
                cardData.CVV = CVV.Text;

                ShowLoading();

                int typeConfirmCard = await CheckCardPay(cardData);

                if (typeConfirmCard == 1) await AddCardExt(cardData);

                if (typeConfirmCard == 0) await DisplayAlert("Сообщение", "Карта недействительна! Попробуйте позже.", "Закрыть");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Сообщение", "Не удалось добавить карту! Попробуйте позже.", "Закрыть");
                await Navigation.PopAsync(true);
            }
        }

        private async Task AddCardExt(CardData cardData)
        {
            HttpContent response = await Server.AddCardData(cardData);
            string answer = await response.ReadAsStringAsync();

            if (answer != null && answer.Contains(nameof(CardData)))
            {
                await DisplayAlert("Сообщение", "Карта успешно добавлена!", "Закрыть");
                cardDataHandler?.Invoke(this, cardData);
                await Navigation.PopAsync(true);
            }
            else
            {
                await DisplayAlert("Сообщение", "Не удалось добавить карту! Попробуйте позже.", "Закрыть");
                await Navigation.PopAsync(true);
            }

            HideLoading();
        }

        private async Task<int> CheckCardPay(CardData cardData)
        {
            int confirmed = 0;

            Random generator = new Random();

            JPayInit jPayInit = new JPayInit();
            jPayInit.TerminalKey = Server.BankDataAuth.TerminalKey;
            jPayInit.OrderId = String.Format("{0}_{1}", nameof(CardData), generator.Next(100000, 999999).ToString());
            jPayInit.Amount = 100;
            JPayResponse jPayResponse = await Server.PayInit(jPayInit);

            if (jPayResponse != null && jPayResponse.Status == JPayStatus.New && jPayResponse.Success)
            {
                JPayFinishAuthorize jPayFinishAuthorize = new JPayFinishAuthorize();
                jPayFinishAuthorize.TerminalKey = jPayInit.TerminalKey;
                jPayFinishAuthorize.PaymentId = jPayResponse.PaymentId;
                jPayFinishAuthorize.CardData = Server.RsaEncryptWithPublic(cardData.ToCardData(), Server.BankDataAuth.PublicKey);
                jPayFinishAuthorize.Token = Server.CalculateHash256(jPayFinishAuthorize.CardData + Server.BankDataAuth.Password + jPayFinishAuthorize.PaymentId + Server.BankDataAuth.TerminalKey);

                jPayResponse = await Server.PayFinishAuthorize(jPayFinishAuthorize);

                if (jPayResponse != null && jPayResponse.Success)
                {
                    if (jPayResponse.Status == JPayStatus.Confirmed)
                    {
                        JPayCancel jPayCancel = new JPayCancel();
                        jPayCancel.PaymentId = jPayResponse.PaymentId;
                        jPayCancel.TerminalKey = Server.BankDataAuth.TerminalKey;
                        jPayCancel.Token = Server.CalculateHash256(Server.BankDataAuth.Password + jPayFinishAuthorize.PaymentId + Server.BankDataAuth.TerminalKey);

                        jPayResponse = await Server.PayCancel(jPayCancel);

                        if (jPayResponse != null && jPayResponse.Success && jPayResponse.Status == JPayStatus.Refunded) confirmed = 1;
                    }

                    if (jPayResponse.Status == JPayStatus.Authorized)
                    {
                        JPayConfirm jPayConfirm = new JPayConfirm();
                        jPayConfirm.TerminalKey = Server.BankDataAuth.TerminalKey;
                        jPayConfirm.PaymentId = jPayResponse.PaymentId;
                        jPayConfirm.Token = Server.CalculateHash256(Server.BankDataAuth.Password + jPayFinishAuthorize.PaymentId + Server.BankDataAuth.TerminalKey);

                        jPayResponse = await Server.PayConfirm(jPayConfirm);

                        if (jPayResponse != null && jPayResponse.Success && jPayResponse.Status == JPayStatus.Confirmed)
                        {
                            JPayCancel jPayCancel = new JPayCancel();
                            jPayCancel.PaymentId = jPayResponse.PaymentId;
                            jPayCancel.TerminalKey = Server.BankDataAuth.TerminalKey;
                            jPayCancel.Token = Server.CalculateHash256(Server.BankDataAuth.Password + jPayFinishAuthorize.PaymentId + Server.BankDataAuth.TerminalKey);

                            jPayResponse = await Server.PayCancel(jPayCancel);

                            if (jPayResponse != null && jPayResponse.Success && jPayResponse.Status == JPayStatus.Refunded) confirmed = 1;
                        }
                    }

                    if (jPayResponse.Status == JPayStatus.Checking)
                    {
                        JPay3DSChecking jPay3DSChecking = new JPay3DSChecking();
                        jPay3DSChecking.MD = jPayResponse.MD;
                        jPay3DSChecking.PaReq = jPayResponse.PaReq;

                        HttpResponseMessage response = await Server.Pay3DSChecking(jPay3DSChecking, jPayResponse.ACSUrl);

                        if (response != null)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            Payment payment = new Payment();
                            payment.orderId = jPayInit.OrderId;
                            await Navigation.PushAsync(new PayModal3DSChecking(cardData, content, payment) {
                                confirmedStatusHandler = Card3DSPayConfirmedCallback
                            });

                            confirmed = 2;
                        }
                    }
                }
            }

            return confirmed;
        }

        private async void Card3DSPayConfirmedCallback(object sender, CardData cardData)
        {
            if (cardData != null) await AddCardExt(cardData);
            else await DisplayAlert("Сообщение", "Карта недействительна! Попробуйте позже.", "Закрыть");

            HideLoading();
        }

        private void ShowLoading()
        {
            actInd.IsVisible = true;
            actInd.IsRunning = true;
            gridRoot.IsVisible = false;
            btnAdd.IsVisible = false;
        }

        private async void HideLoading()
        {
            await Task.Delay(1000);
            actInd.IsVisible = false;
            actInd.IsRunning = false;
            gridRoot.IsVisible = true;
            btnAdd.IsVisible = true;
        }
    }
}