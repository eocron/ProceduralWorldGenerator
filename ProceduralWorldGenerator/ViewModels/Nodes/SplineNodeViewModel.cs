using OxyPlot;
using OxyPlot.Series;
using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class SplineNodeViewModel : NodeViewModelBase
    {
        private VectorParameterViewModel _input = new VectorParameterViewModel()
        {
            Dimension = 1,
            IsInput = true,
            Title = "v"
        };
        private VectorParameterViewModel _output = new VectorParameterViewModel()
        {
            Dimension = 1,
            Title = "v"
        };

        private PlotModel _oxyplotViewModel;

        public VectorParameterViewModel Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        public VectorParameterViewModel Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }

        public PlotModel OxyplotViewModel
        {
            get => _oxyplotViewModel;
            set => SetProperty(ref _oxyplotViewModel, value);
        }

        public SplineNodeViewModel()
        {
            VariableName = "Spline";
            
            // Create the plot model
            var tmp = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };

            // Create two line series (markers are hidden by default)
            var series1 = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Circle };
            series1.Points.Add(new DataPoint(0, 0));
            series1.Points.Add(new DataPoint(10, 18));
            series1.Points.Add(new DataPoint(20, 12));
            series1.Points.Add(new DataPoint(30, 8));
            series1.Points.Add(new DataPoint(40, 15));

            var series2 = new LineSeries { Title = "Series 2", MarkerType = MarkerType.Square };
            series2.Points.Add(new DataPoint(0, 4));
            series2.Points.Add(new DataPoint(10, 12));
            series2.Points.Add(new DataPoint(20, 16));
            series2.Points.Add(new DataPoint(30, 25));
            series2.Points.Add(new DataPoint(40, 5));


            // Add the series to the plot model
            tmp.Series.Add(series1);
            tmp.Series.Add(series2);

            // Axes are created automatically if they are not defined

            // Set the Model property, the INotifyPropertyChanged event will make the WPF Plot control update its content
            OxyplotViewModel = tmp;
        }
    }
}