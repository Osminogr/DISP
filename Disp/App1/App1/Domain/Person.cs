using System;
using Newtonsoft.Json;

namespace App1.Domain
{
    public class Person : Entity
    {
        public string firstName, lastName, patronymic;
        public string city;
        public string phone;
        public Passport passport;
        public string position;

        [JsonIgnore]
        public Photo selfPhoto;

        public string urlSelfPhoto;

        public Person()
        {
        }
    }
}
