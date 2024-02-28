using System;
using System.Windows.Input;
using Nodify.Shared;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Debug
{
    public class SelectedDataPointViewModel : ObservableObject
    {
        private readonly LineSeries _series;
        private int _selectedDataPointIndex;
        private bool _isDragging;
        private float _initiationDistance;
        private DataPoint _startDataPoint;

        private Cursor? _previousCursor;
        public DataPoint StartDataPoint
        {
            get => _startDataPoint;
            private set => SetProperty(ref _startDataPoint, value);
        }
        
        

        public DataPoint CurrentDataPoint => _series.Points[SelectedDataPointIndex];

        public int SelectedDataPointIndex
        {
            get => _selectedDataPointIndex;
            set
            {
                var prev = _selectedDataPointIndex;
                SetProperty(ref _selectedDataPointIndex, value);
                if (prev != value)
                {
                    OnPropertyChanged(nameof(CurrentDataPoint));
                }
            }
        }

        public bool IsDragging
        {
            get => _isDragging;
            set => SetProperty(ref _isDragging, value);
        }

        public float InitiationDistance
        {
            get => _initiationDistance;
            set => SetProperty(ref _initiationDistance, value);
        }

        public SelectedDataPointViewModel(LineSeries series)
        {
            _series = series;
            _series.PlotModel.MouseDown += OnMouseDown;
            _series.PlotModel.MouseMove += OnMouseMove;
            _series.PlotModel.MouseUp += OnMouseUp;
            _series.PlotModel.MouseLeave += OnMouseLeave;
            InitiationDistance = 0.02f;
        }

        private void OnMouseLeave(object? sender, OxyMouseEventArgs e)
        {
            if (IsDragging)
            {
                EndDrag();
                e.Handled = true;
            }
        }

        private void OnMouseUp(object? sender, OxyMouseEventArgs e)
        {
            if (IsDragging)
            {
                EndDrag();
                e.Handled = true;
            }
        }

        private void OnMouseMove(object? sender, OxyMouseEventArgs e)
        {
            if (IsDragging)
            {
                var targetDataPoint = ClampWithinRegion(Axis.InverseTransform(e.Position, _series.XAxis, _series.YAxis));
                var prev = _series.Points[SelectedDataPointIndex];
                if (!targetDataPoint.Equals(prev))
                {
                    _series.Points.RemoveAt(SelectedDataPointIndex);
                    _series.Points.Insert(SelectedDataPointIndex, targetDataPoint);
                    OnPropertyChanged(nameof(CurrentDataPoint));
                    _series.PlotModel.InvalidatePlot(true);
                    e.Handled = true;
                }
            }
        }

        private DataPoint ClampWithinRegion(DataPoint targetPosition)
        {
            var lowerBound = SelectedDataPointIndex > 0 ? _series.Points[SelectedDataPointIndex - 1].X : double.NegativeInfinity;
            var upperBound = ((SelectedDataPointIndex < _series.Points.Count - 1)
                ? _series.Points[SelectedDataPointIndex + 1].X
                : double.PositiveInfinity);

            return new DataPoint(Math.Max(lowerBound, Math.Min(targetPosition.X, upperBound)), targetPosition.Y);
        }

        private void OnMouseDown(object? sender, OxyMouseDownEventArgs e)
        {
            if (e.HitTestResult?.Item != null && e.HitTestResult.Item is DataPoint)
            {
                var dataPointOffset = e.HitTestResult.Index;
                var dragAndDropIndex = (int)Math.Round(dataPointOffset);
                var isWithinInitiationDistance = Math.Abs(dataPointOffset - dragAndDropIndex) <= InitiationDistance;
                var isDragAndDropInitiated = isWithinInitiationDistance && e.ClickCount == 1;
                var isCreateInitiated = e.ClickCount > 1;
                if (isDragAndDropInitiated)
                {
                    StartDrag(dragAndDropIndex);
                    e.Handled = true;
                }
                else if (isCreateInitiated)
                {
                    var insertIndex = (int)Math.Ceiling(dataPointOffset);
                    CreatePoint(insertIndex, Axis.InverseTransform(e.Position, _series.XAxis, _series.YAxis));
                    e.Handled = true;
                }
            }
        }

        private void CreatePoint(int insertIndex, DataPoint point)
        {
            IsDragging = false;
            StartDataPoint = point;
            SelectedDataPointIndex = insertIndex;
            _series.Points.Insert(insertIndex, point);
            _series.PlotModel.InvalidatePlot(true);
        }

        private void EndDrag()
        {
            Mouse.OverrideCursor = _previousCursor;
            _previousCursor = null;
            IsDragging = false;
        }

        private void StartDrag(int dataPointIndex)
        {
            _previousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Hand;
            StartDataPoint = _series.Points[dataPointIndex];
            SelectedDataPointIndex = dataPointIndex;
            IsDragging = true;
        }
    }
}