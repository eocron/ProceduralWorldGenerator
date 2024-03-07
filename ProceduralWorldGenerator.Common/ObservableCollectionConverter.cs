using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace ProceduralWorldGenerator.Common
{
    public sealed class ObservableCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return IsOfType(objectType, typeof(NodifyObservableCollection<>)) ||
                   IsOfType(objectType, typeof(ObservableCollection<>)) ||
                   IsOfType(objectType, typeof(BindingList<>));
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            var obj = serializer.Deserialize<List<object>>(reader);
            if (obj == null)
                return null;

            try
            {
                if (TryConvertTo(objectType, typeof(NodifyObservableCollection<>), existingValue,
                        () => CreateObservable(objectType, obj), x => UpdateObservable(x, obj), out var result) ||
                    TryConvertTo(objectType, typeof(ObservableCollection<>), existingValue,
                        () => CreateObservable(objectType, obj), x => UpdateObservable(x, obj), out result) ||
                    TryConvertTo(objectType, typeof(BindingList<>), existingValue,
                        () => CreateObservable(objectType, obj), x => UpdateObservable(x, obj), out result))
                    return result;
            }
            catch (Exception e)
            {
            }

            throw new NotSupportedException(objectType.ToString());
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var list = ((IEnumerable)value)?.Cast<object>().ToList();
            serializer.Serialize(writer, list);
        }

        private object CreateObservable(Type objectType, List<object> objects)
        {
            var result = Activator.CreateInstance(objectType);
            objectType.GetMethod("AddRange", BindingFlags.Instance | BindingFlags.Public)
                .Invoke(result, new object?[] { objects.AsEnumerable() });
            return result;
        }

        private static bool IsOfType(Type objectType, Type targetType)
        {
            return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == targetType;
        }

        private bool TryConvertTo(Type objectType, Type targetType, object? existingValue, Func<object> onCreate,
            Action<object> onUpdate, out object? result)
        {
            result = null;
            if (!IsOfType(objectType, targetType)) return false;

            if (existingValue != null)
            {
                onUpdate(existingValue);
                result = existingValue;
            }
            else
            {
                result = onCreate();
            }

            return true;
        }

        private void UpdateObservable(object x, List<object> objects)
        {
            x.GetType().GetMethod("Clear", BindingFlags.Instance | BindingFlags.Public).Invoke(x, null);
            foreach (var obj in objects)
                x.GetType().GetMethod("Add", BindingFlags.Instance | BindingFlags.Public).Invoke(x, new[] { obj });
        }
    }
}