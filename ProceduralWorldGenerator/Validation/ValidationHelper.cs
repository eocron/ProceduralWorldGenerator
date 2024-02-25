namespace ProceduralWorldGenerator.Validation
{
    public class ValidationHelper
    {
        public static bool IsDimensionTextAllowed(string text, IDimensionValidationInfo info)
        {
            if (string.IsNullOrWhiteSpace(text) || !int.TryParse(text, out var value))
            {
                return false;
            }

            return IsDimensionAllowed(value, info);
        }
        
        public static bool IsDimensionAllowed(int value, IDimensionValidationInfo info)
        {
            
            if (value < info.MinDimension || value > info.MaxDimension)
            {
                return false;
            }

            if (info.AllowedDimensions != null && !info.AllowedDimensions.Contains(value))
            {
                return false;
            }

            return true;
        }
    }
}