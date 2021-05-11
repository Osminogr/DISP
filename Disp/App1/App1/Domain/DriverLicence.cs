using System;
using Newtonsoft.Json;

namespace App1.Domain
{
    public class DriverLicence : Entity
    {
        [JsonIgnore]
        public Photo photo1;
        [JsonIgnore]
        public Photo photo2;
        [JsonIgnore]
        public Photo photo3;

        public string number;
        public string period;
        public string date;

        public string urlPhoto1;
        public string urlPhoto2;
        public string urlPhoto3;

        public DriverLicence() { }
    }
}
