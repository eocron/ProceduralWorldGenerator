namespace ProceduralWorldGenerator.Common.Controls.Labeled
{
    public partial class LabeledSingleInput : LabeledUpDown<float>
    {
        public LabeledSingleInput()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}