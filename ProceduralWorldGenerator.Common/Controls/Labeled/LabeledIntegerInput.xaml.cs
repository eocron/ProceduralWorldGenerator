using System.Windows.Controls;

namespace ProceduralWorldGenerator.Common.Controls.Labeled
{
    public partial class LabeledIntegerInput : LabeledUpDown<int>
    {
        public LabeledIntegerInput()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}