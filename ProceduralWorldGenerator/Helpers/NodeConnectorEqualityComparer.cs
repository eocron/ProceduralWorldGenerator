using System;
using System.Collections.Generic;
using ProceduralWorldGenerator.ViewModels.Connections;

namespace ProceduralWorldGenerator.Helpers
{
    /// <summary>
    /// Checks if connector is same
    /// </summary>
    public sealed class NodeConnectorViewModelEqualityComparer : IEqualityComparer<NodeConnectorViewModel>
    {
        public static readonly IEqualityComparer<NodeConnectorViewModel> Instance = new NodeConnectorViewModelEqualityComparer();
        public bool Equals(NodeConnectorViewModel x, NodeConnectorViewModel y)
        {
            return x.NodeParameterId == y.NodeParameterId;
        }

        public int GetHashCode(NodeConnectorViewModel obj)
        {
            return HashCode.Combine(obj.NodeParameterId);
        }
    }
}