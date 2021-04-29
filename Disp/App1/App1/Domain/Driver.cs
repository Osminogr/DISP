using System;

namespace App1.Domain
{
    public class Driver : Entity
    {
        public string drLicPh1;
        public string drLicPh2;

        public string regCertPh1;
        public string regCertPh2;

        public string drLicNumber;
        public string drLicPeriod;
        public string drLicData;

        public Car car;
        public AccountNumber accountNumber;
        public Person person;

        public Driver() { }
    }
}
