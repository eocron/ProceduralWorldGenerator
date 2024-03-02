using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Media;
using OxyPlot;

namespace ProceduralWorldGenerator.Views.Splines
{
    public partial class SplineEditorView
    {
        public static readonly DependencyProperty LineBrushProperty =
            DependencyProperty.Register(
                name: nameof(LineBrush),
                propertyType: typeof(Brush),
                ownerType: typeof(SplineEditorView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null, 
                    OnStyleChanged));
        
        public static readonly DependencyProperty GridBrushProperty =
            DependencyProperty.Register(
                name: nameof(GridBrush),
                propertyType: typeof(Brush),
                ownerType: typeof(SplineEditorView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null, 
                    OnStyleChanged));
        
        public static readonly DependencyProperty DataPointsProperty =
            DependencyProperty.Register(
                name: nameof(DataPoints),
                propertyType: typeof(ObservableCollection<Point>),
                ownerType: typeof(SplineEditorView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnDataPointsChanged));
        
        public static readonly DependencyProperty PlotProperty =
            DependencyProperty.Register(
                name: nameof(Plot),
                propertyType: typeof(PlotModel),
                ownerType: typeof(SplineEditorView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null,
                    OnPlotChanged));
        
        public static readonly DependencyProperty LeftClampProperty =
            DependencyProperty.Register(
                name: nameof(LeftClamp),
                propertyType: typeof(SplineEditorClamp),
                ownerType: typeof(SplineEditorView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: SplineEditorClamp.LastValue,
                    OnClampChanged));
        
        public static readonly DependencyProperty RightClampProperty =
            DependencyProperty.Register(
                name: nameof(RightClamp),
                propertyType: typeof(SplineEditorClamp),
                ownerType: typeof(SplineEditorView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: SplineEditorClamp.LastValue,
                    OnClampChanged));
        
        public static readonly DependencyProperty RepeatClampCountProperty =
            DependencyProperty.Register(
                name: nameof(RepeatClampCount),
                propertyType: typeof(int),
                ownerType: typeof(SplineEditorView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: 0,
                    OnClampChanged));
        
        public static readonly DependencyProperty TextForegroundProperty =
            DependencyProperty.Register(
                name: nameof(TextForeground),
                propertyType: typeof(Brush),
                ownerType: typeof(SplineEditorView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: Brushes.Black,
                    OnStyleChanged));

        public Brush TextForeground
        {
            get => (Brush)GetValue(TextForegroundProperty);
            set => SetValue(TextForegroundProperty, value);
        }

        public SplineEditorClamp LeftClamp
        {
            get => (SplineEditorClamp)GetValue(LeftClampProperty);
            set => SetValue(LeftClampProperty, value);
        }
        public SplineEditorClamp RightClamp
        {
            get => (SplineEditorClamp)GetValue(RightClampProperty);
            set => SetValue(RightClampProperty, value);
        }
        public int RepeatClampCount
        {
            get => (int)GetValue(RepeatClampCountProperty);
            set => SetValue(RepeatClampCountProperty, value);
        }

        private static void OnPlotChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SplineEditorView)d).GetBindingExpression(PlotProperty)?.UpdateTarget();
        }

        public PlotModel Plot         
        {
            get => (PlotModel)GetValue(PlotProperty);
            set => SetValue(PlotProperty, value);
        }
        public Brush LineBrush
        {
            get => (Brush)GetValue(LineBrushProperty);
            set => SetValue(LineBrushProperty, value);
        }
        
        public Brush GridBrush
        {
            get => (Brush)GetValue(GridBrushProperty);
            set => SetValue(GridBrushProperty, value);
        }

        public ObservableCollection<Point> DataPoints
        {
            get => (ObservableCollection<Point>)GetValue(DataPointsProperty);
            set
            {
                void OnDataPointsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
                {
                    this.OnDataPointsChanged();
                }
                if (value != null)
                {
                    value.CollectionChanged += OnDataPointsCollectionChanged;
                }

                var prev = (ObservableCollection<Point>)GetValue(DataPointsProperty);
                if (prev != null)
                {
                    prev.CollectionChanged -= OnDataPointsCollectionChanged;
                }

                SetValue(DataPointsProperty, value);
            }
        }

        private static void OnClampChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SplineEditorView)d).OnClampChanged();
        }
        
        private static void OnDataPointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SplineEditorView)d).OnDataPointsChanged();
        }
        
        private static void OnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SplineEditorView)d).OnStyleChanged();
        }
    }
}