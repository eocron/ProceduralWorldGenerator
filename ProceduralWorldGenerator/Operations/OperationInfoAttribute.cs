using System;

namespace ProceduralWorldGenerator.Operations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OperationInfoAttribute : Attribute
    {
        public string DisplayName { get; set; }
        
        /// <summary>
        /// Checks if this operation is provided during runtime
        /// </summary>
        public bool IsRuntimeInput { get; set; }
    }
}