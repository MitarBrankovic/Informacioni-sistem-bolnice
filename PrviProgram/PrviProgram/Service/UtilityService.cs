﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    class UtilityService
    {
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
