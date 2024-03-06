using System.Collections.Generic;
using System.Windows;

namespace ProceduralWorldGenerator.Helpers
{
    public class SplineNodeViewModelHelper
    {
        public static IEnumerable<Point> GetLinearDataPoints()
        {
            return new[] { new Point(0, 0), new Point(1, 1) };
        }

        public static IEnumerable<Point> GetReversedLinearDataPoints()
        {
            return new[] { new Point(1, 1), new Point(0, 0) };
        }
    }
}