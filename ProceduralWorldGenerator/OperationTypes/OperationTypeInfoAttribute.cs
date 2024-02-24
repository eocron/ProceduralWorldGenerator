using System;

namespace ProceduralWorldGenerator.OperationTypes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class OperationTypeInfoAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public bool IsInput { get; set; }
        public bool IsOutput { get; set; }
        
        public int Order { get; set; }
    }
}