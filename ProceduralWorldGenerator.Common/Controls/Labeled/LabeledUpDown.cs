using System.Windows;
using System.Windows.Controls;
namespace ProceduralWorldGenerator.Common.Controls.Labeled
{
    public abstract class LabeledUpDown<T> : Grid
    {
        public static readonly DependencyProperty LabelTextProperty = DependencyPropertyRegistrar<LabeledUpDown<T>>.RegisterProperty(x => x.LabelText);
        public string LabelText   {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly DependencyProperty InputProperty = DependencyPropertyRegistrar<LabeledUpDown<T>>.RegisterProperty(x => x.Input);
        public T Input   {
            get => (T)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }
        
        public static readonly DependencyProperty MinimumProperty = DependencyPropertyRegistrar<LabeledUpDown<T>>.RegisterProperty(x => x.Minimum);
        public T? Minimum   {
            get => (T?)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        
        public static readonly DependencyProperty MaximumProperty = DependencyPropertyRegistrar<LabeledUpDown<T>>.RegisterProperty(x => x.Maximum);
        public T? Maximum   {
            get => (T?)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        
        public static readonly DependencyProperty IncrementProperty = DependencyPropertyRegistrar<LabeledUpDown<T>>.RegisterProperty(x => x.Increment);
        public T Increment   {
            get => (T)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }
    }
}