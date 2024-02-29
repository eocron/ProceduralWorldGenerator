using System;
using Nodify.Shared;
using OxyPlot;

namespace ProceduralWorldGenerator.Views
{
    public class PlotViewModel : ObservableWrapper<PlotModel>
    {
        public override void ChangeValue(Action<PlotModel> changeAction)
        {
            Value.InvalidatePlot(true);
            base.ChangeValue(changeAction);
        }
    }
}