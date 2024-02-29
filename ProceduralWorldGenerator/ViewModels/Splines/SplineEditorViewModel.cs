using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using Nodify.Shared;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using ProceduralWorldGenerator.Views;

namespace ProceduralWorldGenerator.ViewModels.Splines
{
    public class SplineEditorViewModel : ObservableObject
    {
        private readonly PlotMouseController _selectedDataPoint;
        private readonly LinearAxis _splineInputAxis;
        private readonly LinearAxis _splineOutputAxis;
        private readonly LineSeries _splineSeries;
        private readonly LineSeries _rightClampSeries;
        private readonly LineSeries _leftClampSeries;
        
        private SplineEditorClamp _leftClamp;
        private SplineEditorClamp _rightClamp;
        private int _repeatClampCount;
        private PlotViewModel _plot;

        public Brush LineColor
        {
            get => _splineSeries.Color.ToBrush();
            set
            {
                Plot.ChangeValue(v =>
                {
                    var oxyColor = value.ToOxyColor();
                    _splineSeries.Color = oxyColor;
                    _leftClampSeries.Color = oxyColor;
                    _rightClampSeries.Color = oxyColor;
                    OnPropertyChanged(nameof(Plot));
                    RisePropertyChanged(v, nameof(v.Series));
                });
            }
        }

        public PlotViewModel Plot
        {
            get
            {
                return _plot;
            }
            set => _plot = value;
        }

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
            PropertyChanged += OnMouseControllerPropertyChanged;
            
            var plot = new PlotModel { Title = null };
            _splineSeries = new LineSeries {  MarkerType = MarkerType.Circle};
            _splineInputAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.LightGray,
            };

            _splineOutputAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.LightGray,
            };


            _splineSeries.Points.Add(new DataPoint(0, 0));
            _splineSeries.Points.Add(new DataPoint(1, 1));
            _splineSeries.LabelFormatString = "x {0:F2}\ny {1:F2}";

            
            _leftClampSeries = new LineSeries() { MarkerType = MarkerType.None, LineStyle = LineStyle.Dot, Color = _splineSeries.Color, StrokeThickness = 2};
            _rightClampSeries = new LineSeries() { MarkerType = MarkerType.None, LineStyle = LineStyle.Dot, Color = _splineSeries.Color, StrokeThickness = 2};
            
            plot.Axes.Add(_splineInputAxis);
            plot.Axes.Add(_splineOutputAxis);
            plot.Series.Add(_splineSeries);
            plot.Series.Add(_leftClampSeries);
            plot.Series.Add(_rightClampSeries);

            Plot = new PlotViewModel() { Value = plot };
            _selectedDataPoint = new PlotMouseController(_splineSeries);
            _selectedDataPoint.PropertyChanged += OnMouseControllerPropertyChanged;
            ZoomToBest();
            Redraw();
        }

        private void OnMouseControllerPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Redraw();
        }


        private void ZoomToBest()
        {
            Plot.ChangeValue(_ =>
            {
                var maxY = _splineSeries.Points.Max(x => x.Y);
                var maxX = _splineSeries.Points.Max(x => x.X);
                var minY = _splineSeries.Points.Min(x => x.Y);
                var minX = _splineSeries.Points.Min(x => x.X);

                var diffX = Math.Max(1, maxX - minX);
                var diffY = Math.Max(1, minY - maxY);
                var factor = 0.5d;
                _splineInputAxis.Zoom(minX - diffX * factor, maxX + diffX * factor);
                _splineOutputAxis.Zoom(minY - diffY * factor, maxY + diffY * factor);
            });
        }

        private void Redraw()
        {
            Plot.ChangeValue(_ =>
            {
                _leftClampSeries.Points.Clear();
                _leftClampSeries.Points.AddRange(CalculateClampPoints(_splineSeries.Points, LeftClamp, true, RepeatClampCount));
                _rightClampSeries.Points.Clear();
                _rightClampSeries.Points.AddRange(CalculateClampPoints(_splineSeries.Points, RightClamp, false, RepeatClampCount));
            });
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