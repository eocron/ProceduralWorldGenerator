using ProceduralWorldGenerator.OperationTypes;

namespace ProceduralWorldGenerator.Operations
{
    public abstract class NoiseOperation : IOperation
    {
        [OperationTypeInfo(IsInput = true, Order = -100)]
        public PermutationTableOperationType Permutation { get; set; }
        
        [OperationTypeInfo(IsInput = true, Order = -99)]
        public VectorOperationType Vector { get; set; }
        
        [OperationTypeInfo(IsOutput = true, DisplayName = "f(v)", Order = 100)]
        public FloatOperationType Output { get; set; }
    }
}