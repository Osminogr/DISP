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
using System.Security.Cryptography;
using System.IO;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;

namespace App1.Advertiser.Campaign.NewCampaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PayTarif : ContentPage
    {
        Compaign nowUser;
        public PayTarif(Compaign now)
        {
            nowUser = now;
            InitializeComponent();

            OverrideTitleView("Оплата", 90, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private async void Pay(object sender, EventArgs args)
        {
            try
            {
                JPayInit jPayInit = new JPayInit();
                jPayInit.TerminalKey = "1593419054745DEMO";
                jPayInit.OrderId = "100";
                jPayInit.Amount = 1000;
                JPayResponse jPayResponse = await Server.PayInit(jPayInit);

                if (jPayResponse != null)
                {
                    if (jPayResponse.Success)
                    {
                        JPayFinishAuthorize jPayFinishAuthorize = new JPayFinishAuthorize();
                        jPayFinishAuthorize.TerminalKey = jPayInit.TerminalKey;
                        jPayFinishAuthorize.PaymentId = jPayResponse.PaymentId;

                        string card = "PAN=5368290048301438;ExpDate=1121;CardHolder=EVGENIY MELNIKOV;CVV=618";

                        jPayFinishAuthorize.CardData = RsaEncryptWithPublic(card, String.Format("-----BEGIN PUBLIC KEY-----\n{0}\n-----END PUBLIC KEY-----\n", "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAv5yse9ka3ZQE0feuGtemYv3IqOlLck8zHUM7lTr0za6lXTszRSXfUO7jMb+L5C7e2QNFs+7sIX2OQJ6a+HG8kr+jwJ4tS3cVsWtd9NXpsU40PE4MeNr5RqiNXjcDxA+L4OsEm/BlyFOEOh2epGyYUd5/iO3OiQFRNicomT2saQYAeqIwuELPs1XpLk9HLx5qPbm8fRrQhjeUD5TLO8b+4yCnObe8vy/BMUwBfq+ieWADIjwWCMp2KTpMGLz48qnaD9kdrYJ0iyHqzb2mkDhdIzkim24A3lWoYitJCBrrB2xM05sm9+OdCI1f7nPNJbl5URHobSwR94IRGT7CJcUjvwIDAQAB"));

                        jPayFinishAuthorize.Token = CalculateHash256(jPayFinishAuthorize.CardData + "xu14trddhg9zngjo" + jPayFinishAuthorize.PaymentId + jPayFinishAuthorize.TerminalKey);

                        jPayResponse = await Server.PayFinishAuthorize(jPayFinishAuthorize);

                        if (jPayResponse != null)
                        {
                            JPayConfirm jPayConfirm = new JPayConfirm();
                            jPayConfirm.TerminalKey = jPayResponse.TerminalKey;
                            jPayConfirm.PaymentId = jPayResponse.PaymentId;

                            jPayConfirm.Token = CalculateHash256("xu14trddhg9zngjo" + jPayConfirm.PaymentId + jPayConfirm.TerminalKey);

                            jPayResponse = await Server.PayConfirm(jPayConfirm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await DisplayAlert("Сообщение", "Рекламная компания создана! В данный момент ожидаем оплату для ее запуска.", "Закрыть");
            await Navigation.PushAsync(new CampaignsAct(nowUser.adv), true);
        }

        private static string CalculateHash256(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            using (SHA256 mySHA256 = SHA256.Create())
            {
                var hashBytes = mySHA256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes, 0).Replace("-", "").ToLower();
            }
        }

        public string RsaEncryptWithPublic(string clearText, string publicKey)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(clearText);

            var encryptEngine = new Pkcs1Encoding(new RsaEngine());

            using (var txtreader = new StringReader(publicKey))
            {
                var keyParameter = (AsymmetricKeyParameter)new PemReader(txtreader).ReadObject();

                encryptEngine.Init(true, keyParameter);
            }

            var encrypted = Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length));
            return encrypted;

        }
    }
}