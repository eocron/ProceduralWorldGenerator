using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static ProceduralWorldGenerator.Common.DependencyPropertyRegistrar<ProceduralWorldGenerator.Common.Controls.Labeled.LabeledStringInput>;

namespace ProceduralWorldGenerator.Common.Controls.Labeled
{
    public partial class LabeledStringInput : UserControl
    {
        public static readonly DependencyProperty LabelTextProperty = RegisterProperty(x => x.LabelText);
        public string LabelText   {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly DependencyProperty InputTextProperty = RegisterProperty(x => x.InputText)
            .BindsTwoWayByDefault()
            .UpdateSource(UpdateSourceTrigger.PropertyChanged);
        public string InputText   {
            get => (string)GetValue(InputTextProperty);
            set => SetValue(InputTextProperty, value);
        }
        
        public LabeledStringInput()
        {
            InitializeComponent();
            Root.DataContext = this;
        }
    }
}