using System;
namespace App1.Domain
{
    public class AuthObject
    {
        string phone;
        bool isCompany;

        public AuthObject()
        {
        }

        public string getPhone()
        {
            return phone;
        }

        public void setPhone(string value)
        {
            phone = value;
        }

        public bool getIsCompany()
        {
            return isCompany;
        }

        public void setIsCompany(bool value)
        {
            isCompany = value;
        }
    }
}
