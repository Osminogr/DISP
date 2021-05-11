using System;
using Newtonsoft.Json;

namespace App1.Domain
{
    public class Passport : Entity
    {
        public string number;
        public string date;
        public string who;
        public string code;

        public string firstName;
        public string lastName;
        public string patronymic;

        [JsonIgnore]
        public Photo photo1;
        [JsonIgnore]
        public Photo photo2;
        [JsonIgnore]
        public Photo photo3;

        public string urlPhoto1;
        public string urlPhoto2;
        public string urlPhoto3;

        public Passport()
        {

        }
    }
}
