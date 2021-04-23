using System;
using System.Collections.Generic;

namespace App1.Domain
{
    public class Compaign : Entity
    {
        public Adv adv;
        public VideoObj video;
        public List<Driver> drivers;
        public Tarif tarif;
        public bool paid;

        public Compaign()
        {
        }
    }
}
