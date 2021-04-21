using System;
namespace App1.Domain
{
    public class Person : Entity
    {
        public string firstName, lastName, patronymic;
        public string city;
        public string phone;
        public Passport passport;
        public string position;

        public Person()
        {
        }
    }
}
