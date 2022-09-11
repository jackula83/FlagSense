using Framework2.Application.Core.Requests;
using Framework2.Core.Extensions;
using Framework2.Domain.Core.Requests;
using Newtonsoft.Json;

namespace FlagService.Core.Extensions
{
    public static partial class Extension
    {
        public static string Serialise(this object @object) => JsonConvert.SerializeObject(@object);
        public static T? Deserialise<T>(this string serial) => JsonConvert.DeserializeObject<T>(serial);
        public static T CreateRequest<T>(this FxControllerRequest request)
            where T: FxRequest, new()
        {
            var result = new T();
            return result.Tap(x => x.CorrelationId = request.CorrelationId);
        }
    }
}
