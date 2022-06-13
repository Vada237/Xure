using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Xure.App.Infrastructure
{
    public static class SessionExtensions           
        {
            public static void SetJson(this ISession session, string key, object value)
            {
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                session.SetString(key, JsonConvert.SerializeObject(value));
            }
            public static T GetJson<T>(this ISession session, string key)
            {
                var value = session.GetString(key);
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }
        }
}
