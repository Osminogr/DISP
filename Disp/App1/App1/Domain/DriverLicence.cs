using System;

namespace App1.Domain
{
    public class DriverLicence : Entity
    {
        public Photo photo1;
        public Photo photo2;
        public Photo photo3;

        public string number;
        public string period;
        public string date;

        public DriverLicence() { }
    }
}
