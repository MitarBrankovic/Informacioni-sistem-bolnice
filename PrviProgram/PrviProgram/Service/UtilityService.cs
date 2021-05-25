using System;
using System.Collections.Generic;

namespace Service
{
    public class UtilityService
    {
        public List<string> nizVremena = new List<string> { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00",
                                         "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00",
                                         "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00",
                                         "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00",
                                         "18:00:00", "18:30:00", "19:00:00", "19:30:00" };

        public UtilityService() { }

        public string GenerisanjeSifre()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var Random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }

        public bool IsNumber(String st)
        {
            try
            {
                int.Parse(st);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int IntParser(string broj)
        {
            int retVal;
            return int.TryParse(broj, out retVal) ? retVal : default;
        }

    }
}
