using System;
using System.Reflection;

namespace Common.Extentions
{
    public static class CommonExtention
    {
        public static bool HasAttribute<T>(this MemberInfo element) where T : Attribute
        {
            return element.GetCustomAttribute<T>() != null;
        }
    }
}
