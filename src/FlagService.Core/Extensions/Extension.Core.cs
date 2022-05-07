using Newtonsoft.Json;

namespace FlagService.Core.Extensions
{
    public static partial class Extension
    {
        public static string Serialise(this object @object) => JsonConvert.SerializeObject(@object);
        public static T? Deserialise<T>(this string serial) => JsonConvert.DeserializeObject<T>(serial);
    }
}
