using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProceduralWorldGenerator.Helpers;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Grouping;
using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels
{
    public static class NodePreviewProvider
    {
        private static readonly List<NodeViewModelBase> List = new();
        static NodePreviewProvider()
        {
            Bind<PermutationTableNodeViewModel>();
            Bind<VectorNodeViewModel>();
            Bind<ChunkNodeViewModel>();
            Bind<WorleyNoiseNodeViewModel>();
            Bind<ValueNoiseNodeViewModel>();
            Bind<SimplexNoiseNodeViewModel>();
        }

        private static void Bind<T>(Action<T> configurePreview = null)
            where T : NodeViewModelBase
        {
            var instance = Activator.CreateInstance<T>();
            configurePreview?.Invoke(instance);
            List.Add(instance);
        }

        public static IEnumerable<NodeViewModelBase> GetPreviews()
        {
            return List;
        }

        public static OperationViewModel CreateNodeViewModel<T>(T preview, Action<T> configure = null)
            where T : NodeViewModelBase
        {
            return InternalCreateNodeViewModel(preview, configure);
        }

        private static OperationViewModel InternalCreateNodeViewModel<T>(T preview, Action<T> configure)
        where T : NodeViewModelBase
        {
            var clone = ObjectHelper.DeepCopy(preview);
            configure(clone);
            var type = preview.GetType();
            
            var op = new OperationViewModel();
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

        private static ConnectorViewModel Convert(PropertyInfo propertyInfo, ParameterViewModelBase model)
        {
            return new ConnectorViewModel()
            {
                Title = model.Title ?? propertyInfo.Name,
                ParameterViewModel = model,
            };
        }
    }
}