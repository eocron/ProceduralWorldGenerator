using System;
using System.Linq;

namespace ProceduralWorldGenerator.OperationTypes
{
    public class VectorOperationType : OperationTypeBase
    {
        private readonly string _prefix;
        private readonly int[] _dimensions;
        
        public VectorOperationType(params int[] supportedDimensions) : this("v", supportedDimensions){}
        public VectorOperationType(string prefix, params int[] supportedDimensions)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException();

            _prefix = prefix;
            _dimensions = supportedDimensions;
        }

        public override bool IsAssignableFrom(IOperationType operationType)
        {
            if (!base.IsAssignableFrom(operationType))
                return false;


            
            var other = (VectorOperationType)operationType;
            
            if (IsAnyDimension() || other.IsAnyDimension())
                return true;
            return _dimensions.Intersect(other._dimensions).Any();
        }

        protected bool IsAnyDimension()
        {
            return _dimensions == null || _dimensions.Length == 0;
        }

        public override string ToString()
        {
            if (IsAnyDimension())
                return _prefix;
            
            return string.Join(",", _dimensions.Select(x => _prefix + x));
        }
    }
}