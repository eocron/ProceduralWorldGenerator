using System;
using System.Collections.Generic;
using ProceduralWorldGenerator.ViewModels.Connections;

namespace ProceduralWorldGenerator.ViewModels
{
    public sealed class NodeConnectorEqualityComparer : IEqualityComparer<NodeConnectorViewModel>
    {
        public static readonly IEqualityComparer<NodeConnectorViewModel> Instance = new NodeConnectorEqualityComparer();
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