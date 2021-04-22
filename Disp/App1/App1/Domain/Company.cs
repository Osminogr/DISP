using System;
namespace App1.Domain
{
    public class Company : Entity
    {
        public string city;
        public string phone;
        public string address;
        public string ogrn;
        public string inn;
        public string kpp;

        public AccountNumber accountNumber;

        public Person director;
        public Person manager;

        public Company()
        {
        }
    }
}
