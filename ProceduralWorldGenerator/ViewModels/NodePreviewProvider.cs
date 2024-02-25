using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Control;
using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels
{
    public static class NodePreviewProvider
    {
        private static Dictionary<NodeViewModelBase, Func<OperationViewModel>> _list = new Dictionary<NodeViewModelBase, Func<OperationViewModel>>();
        static NodePreviewProvider()
        {
            Bind<PermutationTableNodeViewModel>();
            Bind<VectorNodeViewModel>();
            Bind<WorleyNoiseNodeViewModel>();
            Bind<ValueNoiseNodeViewModel>();
            Bind<SimplexNoiseNodeViewModel>();
        }

        private static void Bind<T>(Action<T> configurePreview = null)
            where T : NodeViewModelBase
        {
            var instance = Activator.CreateInstance<T>();
            configurePreview?.Invoke(instance);
            _list.Add(instance, () => InternalCreateNodeViewModel(instance));
        }

        public static IEnumerable<NodeViewModelBase> GetPreviews()
        {
            return _list.Keys;
        }

        public static OperationViewModel CreateNodeViewModel(NodeViewModelBase preview)
        {
            return _list[preview]();
        }

        private static OperationViewModel InternalCreateNodeViewModel(NodeViewModelBase preview)
        {
            var type = preview.GetType();
            
            var op = new OperationViewModel();
            op.Title = preview.Title;
            var allProps = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType.IsAssignableTo(typeof(ParameterViewModelBase)))
                .Select(x => new
                {
                    prop = x,
                    value = (ParameterViewModelBase)x.GetMethod.Invoke(preview, null)
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
            };
        }
    }
}