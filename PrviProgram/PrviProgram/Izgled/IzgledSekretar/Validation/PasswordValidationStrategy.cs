using System.Text.RegularExpressions;

namespace PrviProgram.Izgled.IzgledSekretar.Validation
{
    class PasswordValidationStrategy : IValidationStrategy
    {
        public bool Validate(object value)
        {
            Regex regex = new Regex(@"^.{6,}$");
            return regex.IsMatch(value as string);
        }
    }
}
