using System;
using System.Collections.Generic;

namespace App1.Domain
{
    public class Payment : Entity
    {
        public string date;
        public string amount;
        public string card;
        public string paymentId;
        public string orderId;
        public int idCardData;

        public Payment() {
        }
    }
}
