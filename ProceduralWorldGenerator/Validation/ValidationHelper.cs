namespace ProceduralWorldGenerator.Validation
{
    public class ValidationHelper
    {
        public static bool IsDimensionAllowed(int value, IDimensionModel info)
        {
            
            if (value < info.MinDimension || value > info.MaxDimension)
            {
                return false;
            }

            return true;
        }
    }
}