using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProceduralWorldGenerator.Helpers;
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
        private readonly Dictionary<Type, CreateMenuViewModelBase> _createNodeViewModels;
        private readonly List<NodeViewModelBase> _list;
        private readonly GeneratorViewModel _generatorModel;

        public GeneratorViewModel GeneratorModel => _generatorModel;
        public IReadOnlyDictionary<Type, CreateMenuViewModelBase> CreateNodeViewModels => _createNodeViewModels;
        public IReadOnlyList<NodeViewModelBase> List => _list;

        public NodeCollectionViewModel(GeneratorViewModel model)
        {
            _generatorModel = model;
            _createNodeViewModels = new Dictionary<Type, CreateMenuViewModelBase>();
            _list = new List<NodeViewModelBase>();

            Bind<PermutationTableNodeViewModel, CreatePermutationTableNodeViewModel>();
            Bind<VectorNodeViewModel, CreateVectorNodeViewModel>();
            Bind<ChunkNodeViewModel, CreateChunkNodeViewModel>();
            Bind<WorleyNoiseNodeViewModel, CreateWorleyNoiseNodeViewModel>();
            Bind<ValueNoiseNodeViewModel, CreateValueNoiseNodeViewModel>();
            Bind<SimplexNoiseNodeViewModel, CreateSimplexNoiseNodeViewModel>();
            Bind<SplineNodeViewModel, CreateSplineNodeViewModel>();
        }

        private void Bind<TModel, TCreateModel>(Action<TModel> configurePreview = null)
            where TModel : NodeViewModelBase
            where TCreateModel : CreateMenuViewModelBase
        {
            var instance = Activator.CreateInstance<TModel>();
            configurePreview?.Invoke(instance);
            _list.Add(instance);
            _createNodeViewModels.Add(instance.GetType(),
                (TCreateModel)Activator.CreateInstance(typeof(TCreateModel), new object[] { GeneratorModel }));
        }

        public static GeneratorNodeViewModel CreateNodeViewModel<T>(T preview, Action<T> configure = null)
            where T : NodeViewModelBase
        {
            return InternalCreateNodeViewModel(preview, configure);
        }

        private static GeneratorNodeViewModel InternalCreateNodeViewModel<T>(T preview, Action<T> configure)
            where T : NodeViewModelBase
        {
            var clone = ObjectHelper.DeepCopy(preview);
            configure(clone);
            var type = preview.GetType();
            
            var op = new GeneratorNodeViewModel();
            op.NodeModel = clone;
            op.Title = clone.Title;
            var allProps = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType.IsAssignableTo(typeof(ParameterViewModelBase)))
                .Select(x => new
                {
                    prop = x,
                    value = (ParameterViewModelBase)x.GetMethod.Invoke(clone, null)
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

        private static NodeConnectorViewModel Convert(PropertyInfo propertyInfo, ParameterViewModelBase model)
        {
            return new NodeConnectorViewModel()
            {
                Title = model.Title ?? propertyInfo.Name,
                ParameterViewModel = model,
            };
        }
    }
}