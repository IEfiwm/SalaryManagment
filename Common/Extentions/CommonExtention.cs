using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Common.Extentions
{
    public static class CommonExtention
    {
        public static bool HasAttribute<T>(this MemberInfo element) where T : Attribute
        {
            return element.GetCustomAttribute<T>() != null;
        }

        public static string ToJsonResponse(this object model)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                //TypeNameHandling = TypeNameHandling.Objects,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(model);
        }

        public static string ToJson(this object model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public static string GetEnumDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

    }
}
