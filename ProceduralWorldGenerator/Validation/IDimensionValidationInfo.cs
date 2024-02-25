using System.Collections.Generic;

namespace ProceduralWorldGenerator.Validation
{
    public interface IDimensionValidationInfo
    {
        int MinDimension { get; }
        int MaxDimension { get; }
        IReadOnlySet<int> AllowedDimensions { get;  }
    }
}