using GamePlay;
using Newtonsoft.Json;

namespace Extension
{
    public static class ObjectExtension
    {
        public static T Convert<T>(this object obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }
    }
}