using System;
using System.Collections.Generic;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.Helpers
{
    /// <summary>
    /// Checks if connection possible between two parameters
    /// </summary>
    public sealed class ParameterTypeEqualityComparer : IEqualityComparer<ParameterViewModelBase>
    {
        public bool Equals(ParameterViewModelBase x, ParameterViewModelBase y)
        {
            if (x.GetType() != y.GetType())
                return false;
            var xx = x as IDimensionParameter;
            var yy = y as IDimensionParameter;
            return xx?.Dimension == yy?.Dimension;
        }

        public int GetHashCode(ParameterViewModelBase obj)
        {
            return HashCode.Combine(obj.GetType(), (obj as IDimensionParameter)?.Dimension);
        }

        public static readonly IEqualityComparer<ParameterViewModelBase>
            Instance = new ParameterTypeEqualityComparer();
    }
}