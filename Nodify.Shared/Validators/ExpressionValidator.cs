using System.Globalization;
using System.Windows.Controls;

namespace Nodify.Shared.Validators
{
    public class  ExpressionValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (string.IsNullOrWhiteSpace(str))
            {
                return new ValidationResult(false, "Empty expression");
            }
            return new ValidationResult(true, null);
        }
    }
}