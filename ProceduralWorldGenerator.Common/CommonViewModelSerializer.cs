using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProceduralWorldGenerator.Common
{
    public static class CommonViewModelSerializer
    {
        private static readonly JsonSerializerSettings Settings = new()
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
            Converters = new List<JsonConverter>(){new ObservableCollectionConverter()}
        };
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }

        public static T Deserialize<T>(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return default;

            return JsonConvert.DeserializeObject<T>(input, Settings);
        }
    }
}