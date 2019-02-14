using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataPlay
{
    class IndexValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int num) && num >= 0)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Please enter non-negative integer");
            }
        }
    }
}
