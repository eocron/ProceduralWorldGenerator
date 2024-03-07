using System;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.Views.Splines
{
    public class PlotMouseController : ObservableObject
    {
        public PlotMouseController(LineSeries series)
        {
            _series = series;
            _series.PlotModel.MouseDown += OnMouseDown;
            _series.PlotModel.MouseMove += OnMouseMove;
            _series.PlotModel.MouseUp += OnMouseUp;
            _series.PlotModel.MouseLeave += OnMouseLeave;
            InitiationDistance = 0.02f;
        }

        private DataPoint ClampWithinRegion(DataPoint targetPosition)
        {
            var lowerBound = SelectedDataPointIndex > 0
                ? _series.Points[SelectedDataPointIndex - 1].X
                : double.NegativeInfinity;
            var upperBound = SelectedDataPointIndex < _series.Points.Count - 1
                ? _series.Points[SelectedDataPointIndex + 1].X
                : double.PositiveInfinity;

            return new DataPoint(Math.Max(lowerBound, Math.Min(targetPosition.X, upperBound)), targetPosition.Y);
        }

        private void CreatePoint(int insertIndex, DataPoint point)
        {
            IsDragging = false;
            SelectedDataPointIndex = insertIndex;
            _series.Points.Insert(insertIndex, point);
            _series.PlotModel.InvalidatePlot(true);
            OnDataChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DeletePoint(int deleteIndex)
        {
            if (deleteIndex < 0 || deleteIndex > _series.Points.Count || _series.Points.Count == 0)
                return;

            IsDragging = false;
            SelectedDataPointIndex = deleteIndex;
            _series.Points.RemoveAt(deleteIndex);
            _series.PlotModel.InvalidatePlot(true);
            OnDataChanged?.Invoke(this, EventArgs.Empty);
        }

        private void EndDrag()
        {
            Mouse.OverrideCursor = _previousCursor;
            _previousCursor = null;
            IsDragging = false;
            OnDataChanged?.Invoke(this, EventArgs.Empty);
        }

        private void MovePointTo(DataPoint targetDataPoint)
        {
            _series.Points.RemoveAt(SelectedDataPointIndex);
            _series.Points.Insert(SelectedDataPointIndex, targetDataPoint);
            _series.PlotModel.InvalidatePlot(true);
            OnDataChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnMouseDown(object? sender, OxyMouseDownEventArgs e)
        {
            var dp = Axis.InverseTransform(e.Position, _series.XAxis, _series.YAxis);
            if (e.HitTestResult?.Item != null && e.HitTestResult.Item is DataPoint)
            {
                var dataPointOffset = e.HitTestResult.Index;
                var dragAndDropIndex = (int)Math.Round(dataPointOffset);
                var isWithinInitiationDistance = Math.Abs(dataPointOffset - dragAndDropIndex) <= InitiationDistance;
                var isDragAndDropInitiated = isWithinInitiationDistance && e.ClickCount == 1 && !e.IsAltDown;
                var isCreateInitiated = e.ClickCount > 1;
                var idDeletedInitiated = isWithinInitiationDistance && e.ClickCount == 1 && e.IsAltDown;
                if (isDragAndDropInitiated)
                {
                    StartDrag(dragAndDropIndex);
                    e.Handled = true;
                }
                else if (isCreateInitiated && !e.IsAltDown)
                {
                    var insertIndex = (int)Math.Ceiling(dataPointOffset);
                    CreatePoint(insertIndex, Axis.InverseTransform(e.Position, _series.XAxis, _series.YAxis));
                    e.Handled = true;
                }
                else if (idDeletedInitiated)
                {
                    DeletePoint(dragAndDropIndex);
                    e.Handled = true;
                }
            }
        }

        private void OnMouseLeave(object? sender, OxyMouseEventArgs e)
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
                var targetDataPoint =
                    ClampWithinRegion(Axis.InverseTransform(e.Position, _series.XAxis, _series.YAxis));
                var prev = _series.Points[SelectedDataPointIndex];
                if (!targetDataPoint.Equals(prev))
                {
                    MovePointTo(targetDataPoint);
                    e.Handled = true;
                }
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

        private void StartDrag(int dataPointIndex)
        {
            _previousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Hand;
            SelectedDataPointIndex = dataPointIndex;
            IsDragging = true;
            OnDataChanged?.Invoke(this, EventArgs.Empty);
        }

        public int SelectedDataPointIndex
        {
            get => _selectedDataPointIndex;
            set => SetProperty(ref _selectedDataPointIndex, value);
        }

        private bool IsDragging
        {
            get => _isDragging;
            set => SetProperty(ref _isDragging, value);
        }

        private float InitiationDistance
        {
            get => _initiationDistance;
            set => SetProperty(ref _initiationDistance, value);
        }

        private readonly LineSeries _series;

        public EventHandler OnDataChanged;
        private bool _isDragging;

        private Cursor? _previousCursor;
        private float _initiationDistance;
        private int _selectedDataPointIndex;
    }
}