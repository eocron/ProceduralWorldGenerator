using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static ProceduralWorldGenerator.Common.DependencyPropertyRegistrar<ProceduralWorldGenerator.Common.Controls.Labeled.LabeledSingleInput>;
namespace ProceduralWorldGenerator.Common.Controls.Labeled
{
    public partial class LabeledSingleInput : UserControl
    {
        public static readonly DependencyProperty LabelTextProperty = RegisterProperty(x => x.LabelText).Default("Empty label");
        public string LabelText   {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly DependencyProperty InputProperty = RegisterProperty(x => x.Input)
            .BindsTwoWayByDefault()
            .UpdateSource(UpdateSourceTrigger.PropertyChanged);
        public float Input   {
            get => (float)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }
        
        public static readonly DependencyProperty MinimumProperty = RegisterProperty(x => x.Minimum).Default(float.MinValue);
        public float Minimum   {
            get => (float)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        
        public static readonly DependencyProperty MaximumProperty = RegisterProperty(x => x.Maximum).Default(float.MaxValue);
        public float Maximum   {
            get => (float)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        
        public static readonly DependencyProperty IncrementProperty = RegisterProperty(x => x.Increment).Default(1);
        public float Increment   {
            get => (float)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }
        
        public LabeledSingleInput()
        {
            InitializeComponent();
            Root.DataContext = this;
        }
    }
}