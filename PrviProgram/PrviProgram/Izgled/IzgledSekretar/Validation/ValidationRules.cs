using System.Globalization;
using System.Windows.Controls;

namespace PrviProgram.Izgled.IzgledSekretar.Validation
{
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new NameValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["NameError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }
    public class SurnameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new NameValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["SurnameError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }

    public class UCINValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new UCINValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["UCINError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }
    public class CityValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new NameValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["CityError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }

    public class StateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new NameValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["StateError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }

    public class OnlyNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new NumberValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["OnlyNumbersError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }

    public class AddressValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new NameValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["AddressError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }

    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new EmailValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["EmailError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }

    public class ContactValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new ContactValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["ContactError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }

    public class UsernameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new UsernameValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["UsernameError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }

    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value as string;
                ValidationContext validationContext = new ValidationContext();
                validationContext.SetStrategy(new PasswordValidationStrategy());
                if (validationContext.Validate(text)) return new ValidationResult(true, null);
                return new ValidationResult(false, TranslationSource.Instance["PasswordError"]);
            }
            catch
            {
                return new ValidationResult(false, TranslationSource.Instance["UnknownError"]);
            }
        }
    }

}