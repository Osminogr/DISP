using System;
using System.Collections.Generic;

namespace App1.Domain
{
    public class CardData : Entity
    {
        public string PAN;
        public string CVV;
        public string ExpDate;
        public string CardHolder;

        public CardData() {
        }

        public string ToCardData()
        {
            return String.Format("PAN={0};ExpDate={1};CardHolder={2};CVV={3}", PAN, ExpDate, CardHolder, CVV);
        }
    }
}
