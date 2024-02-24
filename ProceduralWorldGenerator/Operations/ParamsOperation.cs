using System;

namespace ProceduralWorldGenerator.Operations
{
    public class ParamsOperation : IOperation
    {
        private readonly Func<double[], double> _func;

        public ParamsOperation(Func<double[], double> func) => _func = func;

        public double Execute(params double[] operands)
            => _func.Invoke(operands);
    }
}
