using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Common.Extensions
{
    public static class Extensions
    {
        public static bool IsAnyOf<T>(this T source, params T[] list)
        {
            source.Guard(nameof(source));
            return list.Contains(source);
        }

        public static bool ContainsAnyOf<T>(this T source, params T[] list)
        {
            source.Guard(nameof(source));
            return list.Any(list.Contains);
        }

        public static void Guard(this object obj, string message)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        /// <summary>
        /// Converts any object to JSON
        /// </summary>
        /// <param name="theObject">The object to be serialised to JSON</param>
        /// <param name="jsonConverters">Optionally, specify a converter. By default, date times are converted to ISO-8601 date format (e.g. 2008-04-12T12:53Z)</param>
        /// <param name="contractResolver">Optionally, specify a contract resolver. By default, property names are resolved as camelCase.</param>
        /// <returns></returns>
        public static string ToJson<T>(this T theObject, List<JsonConverter> jsonConverters = null, IContractResolver contractResolver = null)
        {
            var converters = jsonConverters ?? new List<JsonConverter> { new IsoDateTimeConverter() };
            var resolver = contractResolver ?? new CamelCasePropertyNamesContractResolver();
            
            return JsonConvert.SerializeObject(theObject, new JsonSerializerSettings
            {
                Converters = converters,
                ContractResolver = resolver,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static string GetDescription(this Type type)
        {
            var descriptions = (DescriptionAttribute[])
                type.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptions.Length == 0 ? null : descriptions[0].Description;
        }

        public static string GetDisplayName(this PropertyInfo propertyInfo)
        {
            var displayAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.GetName();
        }
    }
}
