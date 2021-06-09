using System.Text.RegularExpressions;

namespace PrviProgram.Izgled.IzgledSekretar.Validation
{
    class UCINValidationStrategy : IValidationStrategy
    {
        public bool Validate(object value)
        {
            Regex regex = new Regex(@"\d{13}$");
            return regex.IsMatch(value as string);
        }
    }
}

