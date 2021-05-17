using System;

namespace App1.Domain.Json
{
    public class JPayInit 
    {
        public string TerminalKey;
        public string OrderId;
        public int Amount;
        public string IP;
        public string Description;
        public string Token;
        public string Language;
        public string Recurrent;
        public string CustomerKey;
        public string RedirectDueDate;
        public string NotificationURL;
        public string SuccessURL;
        public string FailURL;
        public object PayType;
        public object Receipt;
        public object DATA;

        public JPayInit()
        {
        }
    }
}
