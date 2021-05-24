using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using App1.Domain.Json;
using System.Net.Http;
using App1.Templates;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayTarif : ContentPage
    {
        Compaign compaign;
        CardData selectedCardData;
        List<CardDataTemplate> cardTemplates = new List<CardDataTemplate>();
        public PayTarif(Compaign now)
        {
            compaign = now;
            InitializeComponent();

            OverrideTitleView("Оплата", 90, -1);

            newCard.GestureRecognizers.Add(new TapGestureRecognizer() {
                Command = new Command(async () => {
                    await Navigation.PushAsync(new AddCard(compaign.adv) {
                        cardDataHandler = OnAddCardFromAdd
                    }, true);
                })
            });

            LoadCardData();
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private void OnAddCardFromAdd(object sender, CardData data)
        {
            LoadCardData();
        }

        private async void LoadCardData()
        {
            try
            {
                List<CardData> cardDatas = await Server.GetCardDatas(compaign.adv);

                if (cardDatas != null && cardDatas.Count != 0)
                {
                    cardsList.Children.Clear();

                    foreach (var card in cardDatas)
                    {
                        CardDataTemplate cardDataTemplate = new CardDataTemplate(card, true)
                        {
                            cardDataHandler = OnSelectedCardData,
                            cardDataDeleteHandler = OnAddCardFromAdd
                        };
                        cardTemplates.Add(cardDataTemplate);

                        cardsList.Children.Add(cardDataTemplate);
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
            selectedCardData = cardDataTemplate.cardDataInner;

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

        private async void Pay(object sender, EventArgs args)
        {
            try
            {
                if (selectedCardData == null)
                {
                    await DisplayAlert("Сообщение", "Выбирите способ оплаты!", "Закрыть");
                    return;
                }

                if ((selectedCardData != null && selectedCardData.CVV == null) || (selectedCardData != null && selectedCardData.CVV != null && selectedCardData.CVV.Length != 3))
                {
                    await DisplayAlert("Сообщение", "Код CVV задан некорректно!", "Закрыть");
                    return;
                }

                ScRoot.Opacity = 0;
                actInd.IsRunning = true;
                actInd.IsVisible = true;

                Random generator = new Random();

                CardData cardData = selectedCardData;
                cardData.PAN = cardData.PAN.Replace(" ", "").ToUpper();
                cardData.ExpDate = cardData.ExpDate.Replace("/", "");

                Payment payment = new Payment();
                payment.orderId = String.Format("{0}{1}_{2}", nameof(Adv), compaign.adv.id, generator.Next(100000, 999999).ToString());
                payment.amount = compaign.tarif.amount;
                payment.idCardData = cardData.id;
                payment.card = String.Format("*{0}", cardData.PAN.Substring(cardData.PAN.Length - 4));

                JPayInit jPayInit = new JPayInit();
                jPayInit.TerminalKey = Server.BankDataAuth.TerminalKey;
                jPayInit.OrderId = payment.orderId;
                jPayInit.Amount = Int16.Parse(payment.amount) * 100;
                JPayResponse jPayResponse = await Server.PayInit(jPayInit);

                if (jPayResponse != null && jPayResponse.Status == JPayStatus.New && jPayResponse.Success)
                {
                    payment.paymentId = jPayResponse.PaymentId.ToString();

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
                            payment.date = DateTime.Now.Date.ToShortDateString();

                            await Server.AddPayment(payment, compaign.adv.id);

                            await AddCompaign();
                            return;
                        }

                        if (jPayResponse.Status == JPayStatus.Authorized)
                        {
                            JPayConfirm jPayConfirm = new JPayConfirm();
                            jPayConfirm.TerminalKey = Server.BankDataAuth.TerminalKey;
                            jPayConfirm.PaymentId = jPayResponse.PaymentId;
                            jPayConfirm.Token = Server.CalculateHash256(Server.BankDataAuth.Password + jPayFinishAuthorize.PaymentId + Server.BankDataAuth.TerminalKey);

                            jPayResponse = await Server.PayConfirm(jPayConfirm);

                            if (jPayResponse != null && jPayResponse.Success)
                            {
                                if (jPayResponse.Status == JPayStatus.Confirmed)
                                {
                                    payment.date = DateTime.Now.Date.ToShortDateString();

                                    await Server.AddPayment(payment, compaign.adv.id);

                                    await AddCompaign();
                                    return;
                                }

                                if (jPayResponse.Status == JPayStatus.Rejected)
                                {
                                    await DisplayAlert("Сообщение", String.Format("Не удалось выполнить подтверждение платежа({0})! Попробуйте позже.", jPayResponse.Message), "Закрыть");
                                    await Task.Delay(1000);
                                    ScRoot.Opacity = 1;
                                    actInd.IsRunning = false;
                                    actInd.IsVisible = false;
                                    return;
                                }
                            }
                            else
                            {
                                await DisplayAlert("Сообщение", "Не удалось выполнить подтверждение платежа! Попробуйте позже.", "Закрыть");
                                await Task.Delay(1000);
                                ScRoot.Opacity = 1;
                                actInd.IsRunning = false;
                                actInd.IsVisible = false;
                                return;
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
                                await Navigation.PushAsync(new PayModal3DSChecking(compaign, content, payment));

                                ScRoot.Opacity = 1;
                                actInd.IsRunning = false;
                                actInd.IsVisible = false;
                                return;
                            }
                            else
                            {
                                await DisplayAlert("Сообщение", "Не удалось выполнить создание платежа! Попробуйте позже.", "Закрыть");
                                await Task.Delay(1000);
                                ScRoot.Opacity = 1;
                                actInd.IsRunning = false;
                                actInd.IsVisible = false;
                                return;
                            }
                        }

                        if (jPayResponse.Status == JPayStatus.Rejected)
                        {
                            await DisplayAlert("Сообщение", String.Format("Не удалось выполнить создание платежа({0})! Попробуйте позже.", jPayResponse.Message), "Закрыть");
                            await Task.Delay(1000);
                            ScRoot.Opacity = 1;
                            actInd.IsRunning = false;
                            actInd.IsVisible = false;
                            return;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Сообщение", "Не удалось выполнить создание платежа! Попробуйте позже.", "Закрыть");
                        await Task.Delay(1000);
                        ScRoot.Opacity = 1;
                        actInd.IsRunning = false;
                        actInd.IsVisible = false;
                        return;
                    }
                }
                else
                {
                    await DisplayAlert("Сообщение", "Не удалось выполнить создание платежа! Попробуйте позже.", "Закрыть");
                    await Task.Delay(1000);
                    ScRoot.Opacity = 1;
                    actInd.IsRunning = false;
                    actInd.IsVisible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                await DisplayAlert("Сообщение", "Не удалось выполнить запуск рекламной компании! Попробуйте позже.", "Закрыть");
                await Navigation.PushAsync(new CampaignsAct(compaign.adv));
            }
        }

        private async Task AddCompaign()
        {
            HttpContent response = await Server.AddCompaign(compaign);
            string answer = await response.ReadAsStringAsync();

            if (answer != null && answer.Contains(nameof(Compaign)))
            {
                await DisplayAlert("Сообщение", "Оплачено! Рекламная компания создана.", "Закрыть");

                Alert alert = new Alert();
                alert.header = "Сообщение";
                alert.title = "Создание рекламной компании";
                alert.text = "Ваша рекламная компания запустится " + DateTime.Now.ToShortDateString() + " в 09:00.";

                await Server.AddAlert(alert, compaign.adv.id);

                await Navigation.PushAsync(new CampaignsAct(compaign.adv), true);
            }
            else
            {
                await DisplayAlert("Сообщение", "Не удалось выполнить запуск рекламной компании! Попробуйте позже.", "Закрыть");
                await Navigation.PushAsync(new CampaignsAct(compaign.adv));
            }
        }
    }
}