using Ticket.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket.Utility.Validation
{
    public class EnumValidationAttribute: ValidationAttribute
    {
        public EnumValidationAttribute(Type eunmType)
        {
            EnumType = eunmType;
        }

        public Type EnumType { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!EnumType.IsEnum)
            {
                throw new InvalidOperationException();
            }
            if ((string)value != string.Empty)
            {
                var method = typeof(EnumHelper).GetMethod("IsValid").MakeGenericMethod(EnumType);
                var isValid = (bool)method.Invoke(this, new object[] { value.ToString() });
                if (!isValid)
                {
                    var formatErrorMessage = FormatErrorMessage(validationContext.DisplayName);
                    var memberNames = new List<string> { validationContext.MemberName };
                    return new ValidationResult(formatErrorMessage, memberNames);
                }
            }
            return ValidationResult.Success;
        }
    }
}
