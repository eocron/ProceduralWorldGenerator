using System;
using System.Windows;
using System.Windows.Media;
using OxyPlot.Wpf;
using ProceduralWorldGenerator.ViewModels.Splines;

namespace ProceduralWorldGenerator.Views
{
    public class SplineEditorPlotView : PlotView
    {
        public static readonly DependencyProperty LineColorProperty =
            DependencyProperty.Register(
                name: nameof(LineColor),
                propertyType: typeof(Brush),
                ownerType: typeof(SplineEditorPlotView),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null, 
                    OnLineColorChanged));
        
        public Brush LineColor
        {
            get => (Brush)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }

        private static void OnLineColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newColor = ((Brush)e.NewValue).ToOxyColor();
            var view = ((SplineEditorPlotView)d);
            var parentView = (SplineEditorView)view.Parent;
            ((SplineEditorViewModel)parentView.DataContext).LineColor = newColor.ToBrush();
            view.InvalidatePlot();
            view.GetBindingExpression(ModelProperty)?.UpdateTarget();
            throw new NotImplementedException("Still DataContext is not yet initialized....");
        }
    }
}