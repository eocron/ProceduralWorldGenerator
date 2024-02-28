using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Nodify.Shared;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Debug
{
    public enum SplineEditorClamp
    {
        LastValue,
        PingPong,
        Loop
    }
    public class SplineEditorViewModel : ObservableObject
    {
        private SplineEditorClamp _leftClamp;
        private SplineEditorClamp _rightClamp;
        private int _repeatClampCount;

        public string Title
        {
            get => Plot.Title;
            set
            {
                SetNestedProperty(nameof(Plot), Plot.Title, value, () => Plot.Title = value);
                Plot.InvalidatePlot(false);
            }
        }

        public PlotModel Plot { get; }
        public SelectedDataPointViewModel SelectedDataPoint { get; }

        public SplineEditorClamp LeftClamp
        {
            get => _leftClamp;
            set => SetProperty(ref _leftClamp, value);
        }

        public SplineEditorClamp RightClamp
        {
            get => _rightClamp;
            set => SetProperty(ref _rightClamp, value);
        }

        public int RepeatClampCount
        {
            get => _repeatClampCount;
            set => SetProperty(ref _repeatClampCount, value);
        }

        public SplineEditorViewModel()
        {
            LeftClamp = SplineEditorClamp.PingPong;
            RightClamp = SplineEditorClamp.PingPong;
            RepeatClampCount = 10;
            PropertyChanged += OnPropertyChanged;
            
            var plot = new PlotModel { Title = "Spline" };
            SplineSeries = new LineSeries {  MarkerType = MarkerType.Circle, Color = OxyColors.LightBlue};
            SplineInputAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.LightGray,
            };

            SplineOutputAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.LightGray,
            };


            SplineSeries.Points.Add(new DataPoint(0, 0));
            SplineSeries.Points.Add(new DataPoint(1, 1));
            SplineSeries.LabelFormatString = "x {0:F2}\ny {1:F2}";

            
            LeftClampSeries = new LineSeries() { MarkerType = MarkerType.None, LineStyle = LineStyle.Dot, Color = SplineSeries.Color, StrokeThickness = 2};
            RightClampSeries = new LineSeries() { MarkerType = MarkerType.None, LineStyle = LineStyle.Dot, Color = SplineSeries.Color, StrokeThickness = 2};
            
            plot.Axes.Add(SplineInputAxis);
            plot.Axes.Add(SplineOutputAxis);
            plot.Series.Add(SplineSeries);
            plot.Series.Add(LeftClampSeries);
            plot.Series.Add(RightClampSeries);

            Plot = plot;
            SelectedDataPoint = new SelectedDataPointViewModel(SplineSeries);
            SelectedDataPoint.PropertyChanged += OnPropertyChanged;
            ZoomToBest();
            RecalculateClamp();
        }

        public LinearAxis SplineInputAxis { get; set; }

        public LinearAxis SplineOutputAxis { get; set; }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LeftClamp) || e.PropertyName == nameof(RightClamp) ||
                e.PropertyName == nameof(RepeatClampCount) || e.PropertyName == nameof(SelectedDataPointViewModel.CurrentDataPoint))
            {
                RecalculateClamp();
            }
        }

        public LineSeries SplineSeries { get; set; }
        public LineSeries RightClampSeries { get; set; }
        public LineSeries LeftClampSeries { get; set; }

        public void ZoomToBest()
        {
            var maxY = SplineSeries.Points.Max(x => x.Y);
            var maxX = SplineSeries.Points.Max(x => x.X);
            var minY = SplineSeries.Points.Min(x => x.Y);
            var minX = SplineSeries.Points.Min(x => x.X);

            var diffX = Math.Max(1, maxX - minX);
            var diffY = Math.Max(1, minY - maxY);
            var factor = 0.5d;
            SplineInputAxis.Zoom(minX - diffX * factor, maxX + diffX * factor);
            SplineOutputAxis.Zoom(minY - diffY * factor, maxY + diffY * factor);
            Plot.InvalidatePlot(true);
        }

        public void RecalculateClamp()
        {
            LeftClampSeries.Points.Clear();
            LeftClampSeries.Points.AddRange(CalculateClampPoints(SplineSeries.Points, LeftClamp, true, RepeatClampCount));
            RightClampSeries.Points.Clear();
            RightClampSeries.Points.AddRange(CalculateClampPoints(SplineSeries.Points, RightClamp, false, RepeatClampCount));
            Plot.InvalidatePlot(true);
        }

        private static IEnumerable<DataPoint> CalculateClampPoints(List<DataPoint> points, SplineEditorClamp clamp, bool negate, int repeatCount)
        {
            var ordered = points.OrderBy(x=> x.X).ToList();
            var maxPoint = ordered.Last();
            var minPoint = ordered.First();

            var periodX = maxPoint.X - minPoint.X;
            var offsetX = negate ? (minPoint.X - periodX * repeatCount) : maxPoint.X;
            
            if (clamp == SplineEditorClamp.Loop)
            {
                return Enumerable.Range(0, repeatCount)
                    .SelectMany(c => points.Select(p => new DataPoint(p.X + c*periodX + offsetX, p.Y)));
            }

            if(clamp == SplineEditorClamp.LastValue)
            {
                var valueY = (negate ? minPoint : maxPoint).Y;
                return new[] { new DataPoint(offsetX, valueY), new DataPoint(offsetX+repeatCount*periodX, valueY) };
            }
            
            if(clamp == SplineEditorClamp.PingPong)
            {
                return PingPong(points, negate, repeatCount, offsetX, periodX);
            }

            throw new NotSupportedException(clamp.ToString());
        }

        private static IEnumerable<DataPoint> PingPong(List<DataPoint> points, bool negate, int repeatCount, double offsetX, double periodX)
        {
            for(int i = 0; i < repeatCount; i++)
            {
                bool pong = (i + (negate ? ((repeatCount+1)%2) : 0)) % 2 == 0;
                if (pong)
                {
                    for (int j = points.Count-1; j >= 0; j--)
                    {
                        var invertedX = periodX - points[j].X;
                        yield return new DataPoint(invertedX + (i)*periodX + offsetX, points[j].Y);
                    }
                }
                else
                {
                    foreach (var t in points)
                    {
                        yield return new DataPoint(t.X+ offsetX+ i*periodX, t.Y);
                    }
                }
            }
        }
    }
}