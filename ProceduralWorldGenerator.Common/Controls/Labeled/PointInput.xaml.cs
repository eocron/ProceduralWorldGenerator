using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static ProceduralWorldGenerator.Common.DependencyPropertyRegistrar<ProceduralWorldGenerator.Common.Controls.Labeled.PointInput>;
namespace ProceduralWorldGenerator.Common.Controls.Labeled
{
    public partial class PointInput : UserControl
    {
        public static readonly DependencyProperty InputXProperty = RegisterProperty(x => x.InputX)
            .BindsTwoWayByDefault()
            .UpdateSource(UpdateSourceTrigger.PropertyChanged)
            .Default(0);
        public double InputX
        {
            get => (double)GetValue(InputXProperty);
            set
            {
                SetValue(InputXProperty, value);
                SetValue(InputProperty, new Point(value, Input.Y));
            }
        }

        public static readonly DependencyProperty InputYProperty = RegisterProperty(x => x.InputY)
            .BindsTwoWayByDefault()
            .UpdateSource(UpdateSourceTrigger.PropertyChanged)
            .Default(0);
        public double InputY
        {
            get => (double)GetValue(InputYProperty);
            set
            {
                SetValue(InputYProperty, value);
                SetValue(InputProperty, new Point(Input.X, value));
            }
        }

        public static readonly DependencyProperty InputProperty = RegisterProperty(x => x.Input)
            .BindsTwoWayByDefault()
            .UpdateSource(UpdateSourceTrigger.PropertyChanged)
            .Default(new Point(0,0));
        public Point Input
        {
            get => (Point)GetValue(InputProperty);
            set
            {
                SetValue(InputProperty, value);
                SetValue(InputXProperty, value.X);
            }
        }

        public PointInput()
        {
            InitializeComponent();
            Root.DataContext = this;
        }
    }
}