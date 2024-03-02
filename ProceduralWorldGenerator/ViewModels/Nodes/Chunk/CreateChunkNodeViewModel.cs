using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Chunk
{
    public class CreateChunkNodeViewModel: CreateDimensionNodeViewModelBase<ChunkNodeViewModel>
    {
        public CreateChunkNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}