using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static ProceduralWorldGenerator.Common.DependencyPropertyRegistrar<
    ProceduralWorldGenerator.Common.Controls.Labeled.LabeledIntegerInput>;

namespace ProceduralWorldGenerator.Common.Controls.Labeled
{
    public partial class LabeledIntegerInput : UserControl
    {
        public LabeledIntegerInput()
        {
            InitializeComponent();
            Root.DataContext = this;
        }

        public int Increment
        {
            get => (int)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }

        public int Input
        {
            get => (int)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly DependencyProperty LabelTextProperty =
            RegisterProperty(x => x.LabelText).Default("Empty label");

        public static readonly DependencyProperty InputProperty = RegisterProperty(x => x.Input)
            .BindsTwoWayByDefault()
            .UpdateSource(UpdateSourceTrigger.PropertyChanged);

        public static readonly DependencyProperty MinimumProperty =
            RegisterProperty(x => x.Minimum).Default(int.MinValue);

        public static readonly DependencyProperty MaximumProperty =
            RegisterProperty(x => x.Maximum).Default(int.MaxValue);

        public static readonly DependencyProperty IncrementProperty = RegisterProperty(x => x.Increment).Default(1);
    }
}