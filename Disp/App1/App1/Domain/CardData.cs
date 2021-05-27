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
        public int idEntity;

        public CardData() {
        }

        public string ToCardData()
        {
            string panFormat = PAN.Replace(" ", "").ToUpper();
            string expDateFormat = ExpDate.Replace("/", "");

            return String.Format("PAN={0};ExpDate={1};CardHolder={2};CVV={3}", panFormat, expDateFormat, CardHolder, CVV);
        }
    }
}
