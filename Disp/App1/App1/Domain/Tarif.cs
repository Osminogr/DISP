using System;

namespace App1.Domain
{
    public class Tarif : Entity
    {
        public long amount;
        public int amountDay;
        public int amountTenDays;
        public int minDays;

        public Tarif()
        {
        }
    }
}
