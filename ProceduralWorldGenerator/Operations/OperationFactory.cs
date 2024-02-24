using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.OperationTypes;
using ProceduralWorldGenerator.ViewModels;

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
                        typeInfo = x.PropertyType.GetCustomAttribute<OperationTypeInfoAttribute>()
                    })
                    .Where(x=> x.prop.CanRead && x.prop.CanWrite)
                    .OrderBy(x=> x.info?.Order ?? x.typeInfo?.Order ?? 0)
                    .ToList();
                foreach (var prop in allProps.Where(x=> x.info.IsInput))
                {
                    op.Input.Add(Convert(prop.prop, prop.info, prop.typeInfo));
                }
                
                foreach (var prop in allProps.Where(x=> x.info.IsOutput))
                {
                    op.Output.Add(Convert(prop.prop, prop.info, prop.typeInfo));
                }

                result.Add(op);
            }

            return result;
        }

        private static OperationTypeInfoViewModel Convert(PropertyInfo propertyInfo, OperationTypeInfoAttribute info, OperationTypeInfoAttribute typeInfo)
        {
            return new OperationTypeInfoViewModel()
            {
                Title = info?.DisplayName ?? typeInfo?.DisplayName ?? propertyInfo.Name,
                Type = propertyInfo.PropertyType
            };
        }

        public static OperationViewModel GetOperation(OperationInfoViewModel info)
        {
            var input = info.Input.Select(i => new ConnectorViewModel
            {
                Title = i.Title,
                OperationType = i.Type
            });
            
            var output = info.Output.Select(i => new ConnectorViewModel
            {
                Title = i.Title,
                OperationType = i.Type
            });

            switch (info.Type)
            {
                case OperationType.Calculator:
                    return new CalculatorOperationViewModel
                    {
                        Title = info.Title,
                        Operation = info.Operation,
                    };

                case OperationType.Expando:
                    var o = new ExpandoOperationViewModel
                    {
                        MaxInput = info.MaxInput,
                        MinInput = info.MinInput,
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

                case OperationType.Graph:
                    return new OperationGraphViewModel
                    {
                        Title = info.Title,
                        DesiredSize = new Size(420, 250)
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
