using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
namespace Common.Extensions
{
    public static class EnumExtensions
    {
        #region Public methods

        public static string DisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }

        public static string Description(this Enum source)
        {
            var field = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : source.ToString();
        }

        public static List<TEnum> FilterEnumWithAttributeOf<TEnum, TAttribute>() where TEnum : struct where TAttribute : class
        {
            return typeof(TEnum).GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.GetCustomAttributes(typeof(TAttribute), false).Length > 0)
                .Select(field => (TEnum)field.GetValue(null)).ToList();
        }

        public static TEnum GetEnumValueFromDescription<TEnum>(this string description) where TEnum : struct, Enum
        {
            var type = typeof(TEnum);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (string.Equals(attribute.Description, description, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return (TEnum)field.GetValue(null);
                    }
                        
                }
                else
                {
                    if (string.Equals(field.Name, description, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return (TEnum) field.GetValue(null);
                    }
                }
            }
            throw new ArgumentException("Not found.", description);
        }

        public static string Description<TEnum>(this TEnum source) where TEnum : struct, Enum
            => typeof(TEnum).GetField(source.ToString()).Description();

        public static string Description(this FieldInfo source)
            => source.GetCustomAttribute<DescriptionAttribute>()?.Description ?? source.Name;

        #endregion

    }
}
