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
using System.Globalization;
using System.Text.RegularExpressions;
using App1.Domain.Json;
using Newtonsoft.Json;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayModal3DSChecking : ContentPage
    {
        Compaign compaign;
        Payment payment;
        public PayModal3DSChecking(Compaign now, string httpContent, Payment pay)
        {
            compaign = now;
            payment = pay;
            InitializeComponent();

            OverrideTitleView("Оплата", 60, -1);

            webView.Source = new HtmlWebViewSource()
            {
                Html = httpContent
            };

            webView.Navigated += WebView_NavigatedAsync;
        }

        private async void WebView_NavigatedAsync(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                string result = await ((WebView)sender).EvaluateJavaScriptAsync("document.body.innerHTML");

                if (result != null && result.Length != 0)
                {
                    if (result.Contains("Success") && result.Contains("PaymentId") && result.Contains(payment.orderId))
                    {
                        ((WebView)sender).Opacity = 0;
                        actInd.IsRunning = true;
                        actInd.IsVisible = true;

                        string json = result.Substring(result.IndexOf("{"), result.IndexOf("}") - result.IndexOf("{") + 1);
                        json = json.Replace("\\", "");

                        JPayResponse jPayResponse = JsonConvert.DeserializeObject<JPayResponse>(json);

                        if (jPayResponse != null)
                        {
                            if (jPayResponse.Status == JPayStatus.Confirmed)
                            {
                                payment.date = DateTime.Now.Date.ToShortDateString();
                                await AddCompaign();
                                return;
                            }

                            if (jPayResponse.Status == JPayStatus.Authorized)
                            {
                                JPayConfirm jPayConfirm = new JPayConfirm();
                                jPayConfirm.TerminalKey = Server.BankDataAuth.TerminalKey;
                                jPayConfirm.PaymentId = jPayResponse.PaymentId;
                                jPayConfirm.Token = Server.CalculateHash256(Server.BankDataAuth.Password + jPayResponse.PaymentId + Server.BankDataAuth.TerminalKey);

                                jPayResponse = await Server.PayConfirm(jPayConfirm);

                                if (jPayResponse != null && jPayResponse.Success)
                                {
                                    if (jPayResponse.Status == JPayStatus.Confirmed)
                                    {
                                        payment.date = DateTime.Now.Date.ToShortDateString();
                                        await AddCompaign();
                                        return;
                                    }

                                    if (jPayResponse.Status == JPayStatus.Rejected)
                                    {
                                        await DisplayAlert("Сообщение", String.Format("Не удалось выполнить подтверждение платежа({0})! Попробуйте позже.", jPayResponse.Message), "Закрыть");
                                        await Navigation.PopAsync(true);
                                        return;
                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Сообщение", "Не удалось выполнить подтверждение платежа! Попробуйте позже.", "Закрыть");
                                    await Navigation.PopAsync(true);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Сообщение", "Не удалось выполнить подтверждение платежа! Попробуйте позже.", "Закрыть");
                await Navigation.PopAsync(true);
            }
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async Task AddCompaign()
        {
            HttpContent response = await Server.AddCompaign(compaign);
            string answer = await response.ReadAsStringAsync();

            if (answer != null && answer.Contains(nameof(Compaign)))
            {
                await Task.Delay(1000);
                await DisplayAlert("Сообщение", "Оплачено! Рекламная компания создана.", "Закрыть");
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