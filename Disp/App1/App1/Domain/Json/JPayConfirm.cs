using System;

namespace App1.Domain.Json
{
    public class JPayConfirm
    {
        public string TerminalKey;
        public string IP;
        public int PaymentId;
        public string Token;
        public int Amount;
        public object Receipt;

        public JPayConfirm()
        {
        }
    }
}
