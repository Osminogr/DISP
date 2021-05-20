using System;

namespace App1.Domain.Json
{
    public class JPay3DSChecking 
    {
        public string MD;
        public string PaReq;
        public string TermUrl;

        public JPay3DSChecking()
        {
            TermUrl = "https://securepay.tinkoff.ru/Submit3DSAuthorization";
        }
    }
}
