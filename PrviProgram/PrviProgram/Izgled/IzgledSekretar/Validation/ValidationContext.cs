using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Izgled.IzgledSekretar.Validation
{
    class ValidationContext
    {
        private IValidationStrategy validationStrategy;

        public ValidationContext() { }

        public ValidationContext(IValidationStrategy validationStrategy)
        {
            this.validationStrategy = validationStrategy;
        }

        public void SetStrategy(IValidationStrategy validationStrategy)
        {
            this.validationStrategy = validationStrategy;
        }

        public bool Validate(object value)
        {
            return validationStrategy.Validate(value);
        }

    }
}
