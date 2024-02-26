using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreateChunkNodeViewModel: CreateDimensionNodeViewModelBase<ChunkNodeViewModel>
    {
        public CreateChunkNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}