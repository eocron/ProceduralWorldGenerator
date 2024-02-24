namespace ProceduralWorldGenerator.OperationTypes
{
    public abstract class OperationTypeBase : IOperationType
    {
        public virtual bool IsAssignableFrom(IOperationType operationType)
        {
            return operationType != null && operationType.GetType() == this.GetType();
        }
    }
}