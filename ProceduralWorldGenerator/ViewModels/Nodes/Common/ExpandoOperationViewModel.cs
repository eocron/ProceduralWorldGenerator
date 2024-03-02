using Nodify.Shared;
using ProceduralWorldGenerator.ViewModels.Connections;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public class ExpandoNodeViewModel : GeneratorNodeViewModel
    {
        public ExpandoNodeViewModel()
        {
            AddInputCommand = new RequeryCommand(
                () => Input.Add(new NodeConnectorViewModel()),
                () => Input.Count < MaxInput);

            RemoveInputCommand = new RequeryCommand(
                () => Input.RemoveAt(Input.Count - 1),
                () => Input.Count > MinInput);
            
            AddOutputCommand = new RequeryCommand(
                () => Output.Add(new NodeConnectorViewModel()),
                () => Output.Count < MaxInput);

            RemoveOutputCommand = new RequeryCommand(
                () => Output.RemoveAt(Input.Count - 1),
                () => Output.Count > MinInput);
        }

        public INodifyCommand AddInputCommand { get; }
        public INodifyCommand RemoveInputCommand { get; }
        
        public INodifyCommand AddOutputCommand { get; }
        public INodifyCommand RemoveOutputCommand { get; }

        private uint _minInput = 0;
        public uint MinInput
        {
            get => _minInput;
            set => SetProperty(ref _minInput, value);
        }

        private uint _maxInput = uint.MaxValue;
        public uint MaxInput
        {
            get => _maxInput;
            set => SetProperty(ref _maxInput, value);
        }
    }
}
