using System;

namespace App1.Domain.Json
{
    public class JPayStatus 
    {
        public static string New = "NEW";
        public static string Rejected = "REJECTED";
        public static string Confirmed = "CONFIRMED";
        public static string Authorized = "AUTHORIZED";
        public static string Checking = "3DS_CHECKING";

        public JPayStatus()
        {
        }
    }
}
