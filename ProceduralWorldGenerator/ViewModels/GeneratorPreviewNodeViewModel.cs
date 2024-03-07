using System;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels
{
    public class GeneratorPreviewNodeViewModel : ObservableObject
    {
        public static GeneratorPreviewNodeViewModel Create<T>(string title, string description)
            where T : NodeViewModelBase
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description));

            return new GeneratorPreviewNodeViewModel
            {
                NodeType = typeof(T),
                Description = description,
                Title = title
            };
        }

        public string Description { get; private set; }
        public string Title { get; private set; }
        public Type NodeType { get; private set; }
    }
}