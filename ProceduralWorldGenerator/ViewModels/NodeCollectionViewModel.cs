using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Chunk;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Permutation;
using ProceduralWorldGenerator.ViewModels.Nodes.SimplexNoise;
using ProceduralWorldGenerator.ViewModels.Nodes.Spline;
using ProceduralWorldGenerator.ViewModels.Nodes.ValueNoise;
using ProceduralWorldGenerator.ViewModels.Nodes.Vector;
using ProceduralWorldGenerator.ViewModels.Nodes.WorleyNoise;

namespace ProceduralWorldGenerator.ViewModels
{
    public class NodeCollectionViewModel
    {
        private readonly Dictionary<Type, CreateMenuViewModelBase> _createNodeViewModels = new();
        private readonly List<GeneratorPreviewNodeViewModel> _previews = new();

        public NodeCollectionViewModel()
        {
            Bind<PermutationTableNodeViewModel, CreatePermutationTableNodeViewModel>("Permutation table", "Variable responsible for 'randomness' of noises. Noises use this as random seed.");
            Bind<VectorNodeViewModel, CreateVectorNodeViewModel>("Vector", "N-dimensional vector of float numbers.");
            Bind<ChunkNodeViewModel, CreateChunkNodeViewModel>("Chunk", "N-dimensional rectangle. Returns N-dimensional vector from its output starting from offset up to specified size.");
            Bind<WorleyNoiseNodeViewModel, CreateWorleyNoiseNodeViewModel>("Worley noise", "1/2/3-dimensional Worley noise. Receives vector and outputs noise as 1-dimensional vector.");
            Bind<ValueNoiseNodeViewModel, CreateValueNoiseNodeViewModel>("Value noise", "1/2/3-dimensional Value noise. Receives vector and outputs noise as 1-dimensional vector.");
            Bind<SimplexNoiseNodeViewModel, CreateSimplexNoiseNodeViewModel>("Simplex noise", "1/2/3-dimensional Simplex noise. Receives vector and outputs noise as 1-dimensional vector.");
            Bind<SplineNodeViewModel, CreateSplineNodeViewModel>("Spline", "Function which can map 1-dimensional vector to another 1-dimensional vector.");
        }
        
        public IEnumerable<GeneratorPreviewNodeViewModel> GetNodePreviews()
        {
            return _previews;
        }

        public IEnumerable<CreateMenuViewModelBase> GetCreateMenus()
        {
            return _createNodeViewModels.Values;
        }

        public CreateMenuViewModelBase GetCreateMenuViewModel(Type type)
        {
            return _createNodeViewModels[type];
        }
        
        public static GeneratorNodeViewModel CreateGeneratorNodeViewModel(NodeViewModelBase template)
        {
            var type = template.GetType();
            
            var op = new GeneratorNodeViewModel();
            op.NodeModel = template;
            op.Title = template.Title;
            var allProps = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType.IsAssignableTo(typeof(ParameterViewModelBase)))
                .Select(x => new
                {
                    prop = x,
                    value = (ParameterViewModelBase)x.GetMethod.Invoke(template, null)
                })
                .OrderBy(x => x.value.Order)
                .ToList();
            
            foreach (var row in allProps.Where(x=> x.value.IsInput))
            {
                op.Input.Add(Convert(row.prop, row.value));
            }
                
            foreach (var row in allProps.Where(x=> !x.value.IsInput))
            {
                op.Output.Add(Convert(row.prop, row.value));
            }

            return op;
        }

        private void Bind<TModel, TCreateModel>(string title, string description)
            where TModel : NodeViewModelBase, new()
            where TCreateModel : CreateNodeMenuViewModelBase<TModel>, new()
        {
            _createNodeViewModels.Add(typeof(TModel), new TCreateModel());
            _previews.Add(GeneratorPreviewNodeViewModel.Create<TModel>(title, description));
        }

        private static NodeConnectorViewModel Convert(PropertyInfo propertyInfo, ParameterViewModelBase model)
        {
            return new NodeConnectorViewModel()
            {
                Title = model.Title ?? propertyInfo.Name,
                ParameterViewModel = model,
            };
        }

        public NodeViewModelBase CreateTemplateNodeViewModel(Type nodeType)
        {
            return (NodeViewModelBase)Activator.CreateInstance(nodeType);
        }
    }
}