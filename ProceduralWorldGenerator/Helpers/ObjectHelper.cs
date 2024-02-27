using Newtonsoft.Json;

namespace ProceduralWorldGenerator.Helpers
{
    public static class ObjectHelper
    {
        private static readonly JsonSerializerSettings DeepClone = new()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        public static T DeepCopy<T>(T obj)
        {
            if (Equals(obj, default))
                return default;
            var copy = JsonConvert.SerializeObject((object)obj, DeepClone);
            return JsonConvert.DeserializeObject<T>(copy, DeepClone);
        }
    }
}