using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace ProceduralWorldGenerator.Common
{
    public class ObservableCollectionConverter<T> : JsonConverter<ObservableCollection<T>>
    {
        public override void WriteJson(JsonWriter writer, ObservableCollection<T>? value, JsonSerializer serializer)
        {
            var list = value?.AsEnumerable().ToList();
            serializer.Serialize(writer, list);
        }

        public override ObservableCollection<T>? ReadJson(JsonReader reader, Type objectType, ObservableCollection<T>? existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            var obj = serializer.Deserialize<List<T>>(reader);
            if (obj == null)
                return null;
            
            if (hasExistingValue)
            {
                existingValue.Clear();
                existingValue.AddRange(obj);
                return existingValue;
            }
            else
            {
                return new ObservableCollection<T>(obj);
            }
        }
    }
    public static class ObjectHelper
    {
        private static readonly JsonSerializerSettings Settings = new()
        {
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Error,
            Converters = new List<JsonConverter>()
            {
            }
        };
        public static T DeepClone<T>(T obj)
        {
            if (Equals(obj, default))
                return default;
            var copy = JsonConvert.SerializeObject((object)obj, Settings);
            return JsonConvert.DeserializeObject<T>(copy, Settings);
        }
    }
}