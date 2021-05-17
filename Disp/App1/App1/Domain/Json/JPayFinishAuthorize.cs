using System;

namespace App1.Domain.Json
{
    public class JPayFinishAuthorize
    {
        public string TerminalKey;
        public string CardData;
        public string EncryptedPaymentData;
        public int Amount;
        public string DATA;
        public string InfoEmail;
        public string IP;
        public int PaymentId;
        public string Phone;
        public bool SendEmail;
        public object Route;
        public object Source;
        public string Token;

        public JPayFinishAuthorize()
        {
        }
    }
}
