using System;

namespace App1.Domain.Json
{
    public class JPayResponse 
    {
        public string TerminalKey;
        public string OrderId;
        public int Amount;
        public bool Success;
        public string Status;
        public int PaymentId;
        public string ErrorCode;
        public string PaymentURL;
        public string Message;
        public string Details;
        public string CardId;
        public string OriginalAmount;
        public string NewAmount;
        public string ACSUrl;
        public string MD;
        public string PaReq;
        public string PaRes;

        public JPayResponse()
        {
        }
    }
}
