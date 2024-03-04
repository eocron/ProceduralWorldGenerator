using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using ProceduralWorldGenerator.Common;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Spline;

namespace ProceduralWorldGenerator.Views.Splines
{
    public partial class SplineEditorView : UserControl
    {
        public SplineEditorView()
        {
            InitializeComponent();
            DataContext = this;
            
            var plot = new PlotModel { Title = null };
            IInterpolationAlgorithm interpolation = null;
            _splineSeries = new LineSeries {  MarkerType = MarkerType.Circle, InterpolationAlgorithm = interpolation};
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
            _splineSeries.LabelFormatString = "x {0:F2}\ny {1:F2}";
            
            _leftClampSeries = new LineSeries() { MarkerType = MarkerType.None, LineStyle = LineStyle.Dot, Color = _splineSeries.Color, StrokeThickness = 2, InterpolationAlgorithm = interpolation};
            _rightClampSeries = new LineSeries() { MarkerType = MarkerType.None, LineStyle = LineStyle.Dot, Color = _splineSeries.Color, StrokeThickness = 2, InterpolationAlgorithm = interpolation};
            
            plot.Axes.Add(_splineInputAxis);
            plot.Axes.Add(_splineOutputAxis);
            plot.Series.Add(_splineSeries);
            plot.Series.Add(_leftClampSeries);
            plot.Series.Add(_rightClampSeries);
            plot.DefaultFont = "Courier New";

            Plot = plot;
            ZoomToBest();
            RecalculateClamp();
            
            _mouseController = new PlotMouseController(_splineSeries);
            _mouseController.OnDataChanged += (s,e)=> OnDataPointsChangedFromMouse();
        }
        private readonly PlotMouseController _mouseController;
        private readonly LinearAxis _splineInputAxis;
        private readonly LinearAxis _splineOutputAxis;
        private readonly LineSeries _splineSeries;
        private readonly LineSeries _rightClampSeries;
        private readonly LineSeries _leftClampSeries;
        
        private void OnDataPointsChangedFromMouse()
        {
            ChangeValue(v =>
            {
                if (DataPoints != null)
                {
                    ChangeDataPoints(DataPoints, _splineSeries.Points.Select(x => new EditablePointViewModel(x.X, x.Y)));
                }
                
                RecalculateClamp();
            });
        }

        private void ChangeDataPoints(BindingList<EditablePointViewModel> target,
            IEnumerable<EditablePointViewModel> source)
        {
            target.Clear();
            target.AddRange(source);
        }

        private void OnDataPointsSet(BindingList<EditablePointViewModel> prev, BindingList<EditablePointViewModel> next)
        {
            ChangeValue(_ =>
            {
                _splineSeries.Points.Clear();
                if (next != null)
                {
                    _splineSeries.Points.AddRange(next.OrderBy(x => x.X).Select(x => new DataPoint(x.X, x.Y)));
                }

                ZoomToBest();
                RecalculateClamp();

                if (prev != next)
                {
                    if (prev != null)
                    {
                        prev.ListChanged -= OnDataPointsCollectionChanged;
                    }

                    if (next != null)
                    {
                        next.ListChanged += OnDataPointsCollectionChanged;
                    }
                }
            });
        }

        private void OnDataPointsCollectionChanged(object? sender, ListChangedEventArgs ee)
        {
            if (ee.ListChangedType == ListChangedType.ItemChanged)
            {
                ChangeValue(_ =>
                {
                    var collection = (BindingList<EditablePointViewModel>)sender;
                    _splineSeries.Points.Clear();
                    if (collection != null)
                    {
                        _splineSeries.Points.AddRange(collection.OrderBy(x => x.X)
                            .Select(x => new DataPoint(x.X, x.Y)));
                    }
                    
                    ZoomToBest();
                    RecalculateClamp();
                });
            }
        }

        private void OnStyleChanged()
        {
            ChangeValue(v =>
            {
                var lineColor = LineBrush.ToOxyColor();
                var foregroundColor = PlotForeground.ToOxyColor();
                var textColor = foregroundColor;
                var gridColor = GridBrush.ToOxyColor();
                var axisColor = gridColor;
                
                _splineSeries.Color = lineColor;
                _leftClampSeries.Color = lineColor;
                _rightClampSeries.Color = lineColor;
                
                foreach (var a in v.Axes)
                {
                    a.MajorGridlineColor = gridColor;
                    a.ExtraGridlineColor = gridColor;
                    a.MinorGridlineColor = gridColor;
                    a.TextColor = textColor;
                    a.AxislineColor = axisColor;
                    a.TicklineColor = axisColor;
                }

                v.SelectionColor = OxyColors.Green;
                v.PlotAreaBorderColor = axisColor;
                v.TextColor = textColor;
            });
        }

        private void ChangeValue(Action<PlotModel> action)
        {
            if (Monitor.IsEntered(_sync))
            {
                throw new Exception("Recurrent change of plot.");
            }

            var plot = Plot;
            lock (_sync)
            {
                action(plot);
            }

            plot?.InvalidatePlot(true);
            GetBindingExpression(PlotProperty)?.UpdateTarget();
        }

        private readonly object _sync = new object();
        
        private void ZoomToBest()
        {
            if(_splineSeries.Points.Count == 0)
                return;
            
            var maxY = _splineSeries.Points.Max(x => x.Y);
            var maxX = _splineSeries.Points.Max(x => x.X);
            var minY = _splineSeries.Points.Min(x => x.Y);
            var minX = _splineSeries.Points.Min(x => x.X);

            var diffX = Math.Max(1, maxX - minX);
            var diffY = Math.Max(1, minY - maxY);
            var factor = 0.5d;
            _splineInputAxis.Zoom(minX - diffX * factor, maxX + diffX * factor);
            _splineOutputAxis.Zoom(minY - diffY * factor, maxY + diffY * factor);
        }

        private void RecalculateClamp()
        {
            _leftClampSeries.Points.Clear();
            _leftClampSeries.Points.AddRange(CalculateClampPoints(_splineSeries.Points, LeftClamp, true,
                RepeatClampCount));
            _rightClampSeries.Points.Clear();
            _rightClampSeries.Points.AddRange(CalculateClampPoints(_splineSeries.Points, RightClamp, false,
                RepeatClampCount));
        }

        private static IEnumerable<DataPoint> CalculateClampPoints(List<DataPoint> points, SplineEditorClamp clamp, bool negate, int repeatCount)
        {
            if (points.Count == 0)
                return Enumerable.Empty<DataPoint>();
            if(repeatCount <= 0)
                return Enumerable.Empty<DataPoint>();
            
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

        private void OnClampChanged()
        {
            ChangeValue(_ =>
            {
                RecalculateClamp();
            });
        }
    }
}