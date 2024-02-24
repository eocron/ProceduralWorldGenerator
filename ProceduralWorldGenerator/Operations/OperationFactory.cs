using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.OperationTypes;
using ProceduralWorldGenerator.ViewModels;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.Operations
{
    public static class OperationFactory
    {
        public static List<OperationInfoViewModel> GetOperationsInfo()
        {
            List<OperationInfoViewModel> result = new List<OperationInfoViewModel>();

            foreach (var candidate in Assembly.GetAssembly(typeof(OperationFactory)).GetTypes()
                         .Where(x => x.IsAssignableTo(typeof(IOperation)) && !x.IsAbstract)
                         .Select(x=> new
                         {
                             type = x,
                             info = x.GetCustomAttribute<OperationInfoAttribute>()
                         }))
            {
                OperationInfoViewModel op = new OperationInfoViewModel
                {
                    Title = candidate.info.DisplayName ?? candidate.type.Name
                };

                op.Type = OperationType.Normal;
                op.Operation = (IOperation)Activator.CreateInstance(candidate.type);
                op.IsRuntimeInput = candidate.info.IsRuntimeInput;
                var allProps = candidate
                    .type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(x => new
                    {
                        prop = x,
                        info = x.GetCustomAttribute<OperationTypeInfoAttribute>(),
                        typeInfo = x.PropertyType.GetCustomAttribute<OperationTypeInfoAttribute>(),
                        opType = (IOperationType)x.GetMethod.Invoke(op.Operation, null)
                    })
                    .Where(x=> x.prop.CanRead && x.prop.CanWrite)
                    .OrderBy(x=> x.info?.Order ?? x.typeInfo?.Order ?? 0)
                    .ToList();
                foreach (var prop in allProps.Where(x=> x.info.IsInput))
                {
                    op.Input.Add(Convert(prop.prop, prop.info, prop.typeInfo, prop.opType));
                }
                
                foreach (var prop in allProps.Where(x=> x.info.IsOutput))
                {
                    op.Output.Add(Convert(prop.prop, prop.info, prop.typeInfo, prop.opType));
                }

                result.Add(op);
            }

            return result;
        }

        private static OperationTypeInfoViewModel Convert(PropertyInfo propertyInfo, OperationTypeInfoAttribute info,
            OperationTypeInfoAttribute typeInfo, IOperationType opType)
        {
            return new OperationTypeInfoViewModel()
            {
                Title = info?.DisplayName ?? typeInfo?.DisplayName ?? opType.ToString(),
                OperationType = opType
            };
        }

        public static OperationViewModel GetOperation(OperationInfoViewModel info)
        {
            var input = info.Input.Select(i => new ConnectorViewModel
            {
                Title = i.Title,
                OperationType = i.OperationType
            });
            
            var output = info.Output.Select(i => new ConnectorViewModel
            {
                Title = i.Title,
                OperationType = i.OperationType
            });

            switch (info.Type)
            {
                case OperationType.Expando:
                    var o = new ExpandoOperationViewModel
                    {
                        MaxInput = 100,
                        MinInput = 0,
                        Title = info.Title,
                        Operation = info.Operation
                    };
                    o.Output.AddRange(output);
                    o.Input.AddRange(input);
                    return o;

                case OperationType.Group:
                    return new OperationGroupViewModel
                    {
                        Title = info.Title,
                    };

                default:
                {
                    var op = new OperationViewModel
                    {
                        Title = info.Title,
                        Operation = info.Operation,
                        IsRuntimeInput = info.IsRuntimeInput,
                    };

                    op.Output.AddRange(output);
                    op.Input.AddRange(input);
                    return op;
                }
            }
        }
    }
}
