using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProceduralWorldGenerator.Common
{
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