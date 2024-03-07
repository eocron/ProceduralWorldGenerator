using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProceduralWorldGenerator.Common
{
    public static class CommonViewModelSerializer
    {
        public static T Deserialize<T>(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return default;

            return JsonConvert.DeserializeObject<T>(input, Settings);
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }

        private static readonly JsonSerializerSettings Settings = new()
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
            Converters = new List<JsonConverter> { new ObservableCollectionConverter() }
        };
    }
}