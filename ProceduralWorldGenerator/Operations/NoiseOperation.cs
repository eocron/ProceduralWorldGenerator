using ProceduralWorldGenerator.OperationTypes;

namespace ProceduralWorldGenerator.Operations
{
    public abstract class NoiseOperation : IOperation
    {
        [OperationTypeInfo(IsInput = true, Order = -100)]
        public PermutationTableOperationType Permutation { get; set; } = new PermutationTableOperationType();

        [OperationTypeInfo(IsInput = true, Order = -99)]
        public VectorOperationType Input { get; set; } = new VectorOperationType(1,2,3);

        [OperationTypeInfo(IsOutput = true, Order = 100)]
        public VectorOperationType Output { get; set; } = new VectorOperationType(1);
    }
}