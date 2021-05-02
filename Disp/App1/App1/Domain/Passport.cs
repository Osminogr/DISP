using System;

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

        public Photo photo1;
        public Photo photo2;
        public Photo photo3;

        public Passport()
        {

        }
    }
}
