using System.ComponentModel;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
{
    public enum SplineEditorClamp
    {
        [Description("Use edge value")]
        LastValue,
        [Description("Ping-Pong loop")]
        PingPong,
        [Description("Loop")]
        Loop
    }
}