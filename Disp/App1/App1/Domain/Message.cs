using System;
using System.Collections.Generic;

namespace App1.Domain
{
    public class Message : Entity
    {
        public int status;
        public string text;
        public Entity owner;
        public string day;
        public string time;
        public Entity receiver;

        public Message() {
        }
    }
}
