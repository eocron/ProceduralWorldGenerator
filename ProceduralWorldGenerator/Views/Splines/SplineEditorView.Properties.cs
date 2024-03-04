using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using OxyPlot;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Spline;
using static ProceduralWorldGenerator.Common.DependencyPropertyRegistrar<ProceduralWorldGenerator.Views.Splines.SplineEditorView>;

namespace ProceduralWorldGenerator.Views.Splines
{
    public partial class SplineEditorView
    {
        public static readonly DependencyProperty LineBrushProperty =
            RegisterProperty(x => x.LineBrush)
                .OnChange(OnStyleChanged);

        public static readonly DependencyProperty GridBrushProperty =
            RegisterProperty(x => x.GridBrush)
                .OnChange(OnStyleChanged);

        public static readonly DependencyProperty DataPointsProperty =
            RegisterProperty(x => x.DataPoints)
                .OnChange(OnDataPointsChanged)
                .BindsTwoWayByDefault()
                .Default(null);

        public static readonly DependencyProperty PlotProperty =
            RegisterProperty(x => x.Plot)
                .OnChange(OnPlotChanged)
                .BindsTwoWayByDefault();

        public static readonly DependencyProperty LeftClampProperty =
            RegisterProperty(x => x.LeftClamp)
                .Default(SplineEditorClamp.LastValue)
                .OnChange(OnClampChanged);

        public static readonly DependencyProperty RightClampProperty =
            RegisterProperty(x => x.RightClamp)
                .Default(SplineEditorClamp.LastValue)
                .OnChange(OnClampChanged);
        
        public static readonly DependencyProperty RepeatClampCountProperty =
            RegisterProperty(x => x.RepeatClampCount)
                .Default(0)
                .OnChange(OnClampChanged);

        public static readonly DependencyProperty PlotForegroundProperty =
            RegisterProperty(x => x.PlotForeground)
                .Default(Brushes.Black)
                .OnChange(OnStyleChanged);

        public Brush PlotForeground
        {
            get => (Brush)GetValue(PlotForegroundProperty);
            set => SetValue(PlotForegroundProperty, value);
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

        public BindingList<EditablePointViewModel> DataPoints
        {
            get => (BindingList<EditablePointViewModel>)GetValue(DataPointsProperty);
            set => SetValue(DataPointsProperty, value);
        }
        
        private static void OnClampChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SplineEditorView)d).OnClampChanged();
        }
        
        private static void OnDataPointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var prev = (BindingList<EditablePointViewModel>)e.OldValue;
            var next = (BindingList<EditablePointViewModel>)e.NewValue;
            ((SplineEditorView)d).OnDataPointsSet(prev, next);
        }
        
        private static void OnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SplineEditorView)d).OnStyleChanged();
        }
    }
}