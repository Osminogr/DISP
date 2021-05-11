using System;

namespace App1.Domain
{
    public class Driver : Entity
    {
        public DriverLicence driverLicence;
        public Car car;
        public AccountNumber accountNumber;
        public Person person;


        public Driver() { }
    }
}
