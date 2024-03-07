using System;
using System.Globalization;
using System.Windows.Controls;
using NCalc;

namespace ProceduralWorldGenerator.Common.Validators
{
    public class ExpressionValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (string.IsNullOrWhiteSpace(str)) return new ValidationResult(false, "Empty expression");

            try
            {
                var expression = new Expression(str);
                if (expression.HasErrors()) return new ValidationResult(false, expression.Error);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, e.Message);
            }

            return new ValidationResult(true, null);
        }
    }
}