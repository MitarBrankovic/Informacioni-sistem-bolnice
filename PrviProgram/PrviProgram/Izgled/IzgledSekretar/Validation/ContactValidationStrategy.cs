using System.Text.RegularExpressions;

namespace PrviProgram.Izgled.IzgledSekretar.Validation
{
    class ContactValidationStrategy : IValidationStrategy
    {
        public bool Validate(object value)
        {
            Regex regex = new Regex(@"\+?\d[\d\s]{6,15}\d$");
            return regex.IsMatch(value as string);
        }
    }
}
