using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Nodify.Shared;

namespace Debug
{
    public class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            Series = new SeriesCollection();
            
            var values = new ChartValues<Point>();

            // Create sine graph
            for (double x = 0; x < 361; x++)
            {
                var point = new Point() {X = x, Y = Math.Sin(x * Math.PI / 180)};
                values.Add(point);
            }
            Series.Add(new LineSeries()
            {
                Configuration = new CartesianMapper<Point>()
                    .X(point => point.X) // Define a function that returns a value that should map to the x-axis
                    .Y(point => point.Y) // Define a function that returns a value that should map to the y-axis
                    .Stroke(point => point.Y > 0.3 ? Brushes.Red : Brushes.LightGreen) // Define a function that returns a Brush based on the current data item
                    .Fill(point => point.Y > 0.3 ? Brushes.Red : Brushes.LightGreen),
                Title = "Series",
                Values = values,
                PointGeometry = null
            });

            SeriesColors = new ColorsCollection();
            SeriesColors.AddRange(new[] {"#FFE65100", "#FFEF6C00", "#FFF57C00", "#FFFB8C00", "#FFFF9800"}
                                  .Select(System.Windows.Media.ColorConverter.ConvertFromString)
                                  .OfType<System.Windows.Media.Color>()
                                  .ToList());
        }

        public SeriesCollection Series { get; }

        public ColorsCollection SeriesColors { get; }
    }
}