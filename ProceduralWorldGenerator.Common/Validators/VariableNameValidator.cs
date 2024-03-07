using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ProceduralWorldGenerator.Common.Validators
{
    public class VariableNameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (string.IsNullOrWhiteSpace(str)) return new ValidationResult(false, "Empty variable name");

            if (!Regex.IsMatch(str, "^[_]*[a-z][a-z0-9_]*",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase))
                return new ValidationResult(false, "Allowed only well-defined variable names: _foo1, bAr");

            return new ValidationResult(true, null);
        }
    }
}