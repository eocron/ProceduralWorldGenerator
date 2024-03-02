using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nodify.Shared;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Chunk;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Expression;
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
        private readonly Dictionary<Type, Func<NodeViewModelBase>> _templateFactories = new();
        private readonly Dictionary<Type, CreateMenuViewModelBase> _createNodeViewModels = new();
        private readonly Dictionary<Type, GeneratorPreviewNodeViewModel> _previews = new();

        public NodeCollectionViewModel()
        {
            Bind<PermutationTableNodeViewModel, CreatePermutationTableNodeViewModel>(
                "Permutation table", 
                "Variable responsible for 'randomness' of noises. Noises use this as random seed.",
                x =>
                {
                    x.VariableName = "Permutation table";
                    x.Permutation.Title = "rnd";
                });
            Bind<VectorNodeViewModel, CreateVectorNodeViewModel>(
                "Vector", 
                "N-dimensional vector of float numbers.",
                x =>
                {
                    x.VariableName = "newVariable";
                    x.Value.Title = "out";
                });
            Bind<ChunkNodeViewModel, CreateChunkNodeViewModel>(
                "Chunk", 
                "N-dimensional rectangle. Returns N-dimensional vector from its output starting from offset up to specified size.",
                x =>
                {
                    x.VariableName = "Chunk";
                    x.Offset.Title = "offset";
                    x.Size.Title = "size";
                    x.Position.Title = "out";
                });
            Bind<WorleyNoiseNodeViewModel, CreateWorleyNoiseNodeViewModel>(
                "Worley noise", 
                "1/2/3-dimensional Worley noise. Receives vector and outputs noise as 1-dimensional vector.",
                x =>
                {
                    x.VariableName = "Worley noise";
                    x.Permutation.Title = "rnd";
                    x.Combination.Title = "combination";
                    x.Distance.Title = "distance";
                    x.Output.Title = "out";
                    x.Input.Title = "in";
                    x.Dimension = 1;
                });
            Bind<ValueNoiseNodeViewModel, CreateValueNoiseNodeViewModel>(
                "Value noise", 
                "1/2/3-dimensional Value noise. Receives vector and outputs noise as 1-dimensional vector.",
                x =>
                {
                    x.VariableName = "Value noise";
                    x.Permutation.Title = "rnd";
                    x.Output.Title = "out";
                    x.Input.Title = "in";
                    x.Dimension = 1;
                });
            Bind<SimplexNoiseNodeViewModel, CreateSimplexNoiseNodeViewModel>(
                "Simplex noise", 
                "1/2/3-dimensional Simplex noise. Receives vector and outputs noise as 1-dimensional vector.",
                x =>
                {
                    x.VariableName = "Simplex noise";
                    x.Permutation.Title = "rnd";
                    x.Output.Title = "out";
                    x.Input.Title = "in";
                    x.Dimension = 1;
                });
            Bind<SplineNodeViewModel, CreateSplineNodeViewModel>(
                "Spline", 
                "Function which can map 1-dimensional vector to another 1-dimensional vector.",
                x =>
                {
                    x.VariableName = "Spline";
                    x.Output.Title = "out";
                    x.Input.Title = "in";
                    x.Spline.DataPoints.AddRange(SplineNodeViewModelHelper.GetLinearDataPoints());
                });
            Bind<ExpressionNodeViewModel, CreateExpressionNodeViewModel>(
                "Expression", 
                "Function which can map N-dimensional vector to another M-dimensional vector using math expressions.",
                x =>
                {
                    x.VariableName = "Expression";
                    x.InputDimension = 1;
                    x.OutputDimension = 1;
                    x.TransformExpressions[0].Item = "x1";
                });
        }
        
        public IEnumerable<GeneratorPreviewNodeViewModel> GetNodePreviews()
        {
            return _previews.Values;
        }

        public IEnumerable<CreateMenuViewModelBase> GetCreateMenus()
        {
            return _createNodeViewModels.Values;
        }

        public CreateMenuViewModelBase GetCreateMenuViewModel(Type type)
        {
            return _createNodeViewModels[type];
        }
        
        public NodeViewModelBase CreateTemplateNodeViewModel(Type nodeType)
        {
            return _templateFactories[nodeType]();
        }
        
        public GeneratorNodeViewModel CreateGeneratorNodeViewModel(NodeViewModelBase template)
        {
            var type = template.GetType();
            
            var op = new GeneratorNodeViewModel
            {
                NodeModel = template
            };
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

        private void Bind<TModel, TCreateModel>(string title, string description, Action<TModel> configureInstance = null)
            where TModel : NodeViewModelBase, new()
            where TCreateModel : CreateNodeMenuViewModelBase<TModel>, new()
        {
            var key = typeof(TModel);
            _createNodeViewModels.Add(key, new TCreateModel());
            _previews.Add(key, GeneratorPreviewNodeViewModel.Create<TModel>(title, description));
            _templateFactories.Add(key, () =>
            {
                var obj = new TModel();
                configureInstance?.Invoke(obj);
                return obj;
            });
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