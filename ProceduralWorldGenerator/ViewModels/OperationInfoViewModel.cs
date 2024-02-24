using System;
using System.Collections.Generic;
using ProceduralWorldGenerator.Operations;

namespace ProceduralWorldGenerator.ViewModels
{
    public enum OperationType
    {
        Normal,
        Expando,
        Calculator,
        Group,
        Graph
    }

    public class OperationInfoViewModel
    {
        public string? Title { get; set; }
        public OperationType Type { get; set; }
        public IOperation? Operation { get; set; }
        public List<OperationTypeInfoViewModel> Input { get; } = new List<OperationTypeInfoViewModel>();
        public List<OperationTypeInfoViewModel> Output { get; } = new List<OperationTypeInfoViewModel>();
        public uint MinInput { get; set; }
        public uint MaxInput { get; set; }
        public bool IsRuntimeInput { get; set; }
    }

    public class OperationTypeInfoViewModel
    {
        public string Title { get; set; }
        public Type Type { get; set; }
    }
}
