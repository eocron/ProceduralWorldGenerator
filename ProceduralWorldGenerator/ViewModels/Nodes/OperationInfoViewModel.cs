using System.Collections.Generic;
using ProceduralWorldGenerator.Operations;
using ProceduralWorldGenerator.OperationTypes;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public enum OperationType
    {
        Normal,
        Expando,
        Group
    }

    public class OperationInfoViewModel
    {
        public string? Title { get; set; }
        public OperationType Type { get; set; }
        public IOperation? Operation { get; set; }
        public List<OperationTypeInfoViewModel> Input { get; } = new List<OperationTypeInfoViewModel>();
        public List<OperationTypeInfoViewModel> Output { get; } = new List<OperationTypeInfoViewModel>();
        public bool IsRuntimeInput { get; set; }
    }

    public class OperationTypeInfoViewModel
    {
        public string Title { get; set; }
        public IOperationType OperationType { get; set; }
    }
}
