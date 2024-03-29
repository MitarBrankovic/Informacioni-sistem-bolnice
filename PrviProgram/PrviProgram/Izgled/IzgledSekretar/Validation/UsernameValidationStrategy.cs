﻿using System.Text.RegularExpressions;

namespace PrviProgram.Izgled.IzgledSekretar.Validation
{
    class UsernameValidationStrategy : IValidationStrategy
    {
        public bool Validate(object value)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9]([._-](?![._-])|[a-zA-Z0-9]){3,18}[a-zA-Z0-9]$");
            return regex.IsMatch(value as string);
        }
    }
}
