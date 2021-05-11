using System;

namespace App1.Domain
{
    public class Tarif : Entity
    {
        public string amount;
        public string amountDay;
        public string amountTenDays;
        public string minDays;
        public bool isInCar;
        public string minMonitor;
        public string maxMonitor;
        public bool isAgent;

        public Tarif()
        {
        }
    }
}
