using System;
using System.Collections.Generic;

namespace App1.Domain
{
    public class Message : Entity
    {
        public int status;
        public string text;
        public bool owner;
        public string day;
        public string time;
        public int? receiver;

        public Message() {
        }
    }
}
