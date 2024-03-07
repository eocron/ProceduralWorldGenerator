using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static ProceduralWorldGenerator.Common.DependencyPropertyRegistrar<
    ProceduralWorldGenerator.Common.Controls.Labeled.LabeledCheckBox>;

namespace ProceduralWorldGenerator.Common.Controls.Labeled
{
    public partial class LabeledCheckBox : UserControl
    {
        public LabeledCheckBox()
        {
            InitializeComponent();
            Root.DataContext = this;
        }

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly DependencyProperty LabelTextProperty =
            RegisterProperty(x => x.LabelText).Default("Empty label");

        public static readonly DependencyProperty IsCheckedProperty = RegisterProperty(x => x.IsChecked)
            .BindsTwoWayByDefault()
            .UpdateSource(UpdateSourceTrigger.PropertyChanged)
            .Default(false);
    }
}