using System;

namespace Extension
{
    public static class TypeExtension
    {
        public static string ConvertTypeToKeyName(this Type type)
        {
            return $"[{type.Name}]";
        }
    }
}