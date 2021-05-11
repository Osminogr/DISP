using System;
using Newtonsoft.Json;

namespace App1.Domain
{
    public class Car : Entity
    {
        public string mark;
        public string model;
        public string dataCar;
        public string color;
        public string vin;
        public string regNumberCar;
        public string carNumber;

        [JsonIgnore]
        public Photo photo1;
        [JsonIgnore]
        public Photo photo2;
        [JsonIgnore]
        public Photo photo3;

        public string urlPhoto1;
        public string urlPhoto2;
        public string urlPhoto3;

        public Car() { }
    }
}
