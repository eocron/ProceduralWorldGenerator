using Newtonsoft.Json;

namespace ProceduralWorldGenerator.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Wrapper<T>
    {
        [JsonProperty]
        public T Item { get; set; }
        
        public Wrapper(){}

        public Wrapper(T value = default(T))
        {
            Item = value;
        }

        public static implicit operator Wrapper<T>(T item)
        {
            return new Wrapper<T>(item);
        }

        public static implicit operator T(Wrapper<T> item)
        {
            if (null != item)
                return item.Item;

            return default(T);
        }
    }
}