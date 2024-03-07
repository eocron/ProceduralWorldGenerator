using System;
using System.Collections.Generic;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public sealed class NodeConnectionEqualityComparer : IEqualityComparer<ParameterViewModelBase>
    {
        public static readonly IEqualityComparer<ParameterViewModelBase>
            Instance = new NodeConnectionEqualityComparer();
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
    }
}