using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Extensions
{
    public static class AttributeExtensions
    {
        public static TOut GetCustomAttribute<T, TOut>(Expression<Func<T>> property) where TOut : Attribute
        {
            var memberExpression = property.Body as MemberExpression;

            var type = memberExpression.Member.ReflectedType;

            var propertyName = memberExpression?.Member.Name;

            if (string.IsNullOrEmpty(propertyName)) return null;

            var prop = type.GetProperty(propertyName);

            var customAttributes = prop.GetCustomAttributes(typeof(TOut), true);

            if (!customAttributes.Any()) return null;

            var attr = (TOut)prop.GetCustomAttributes(typeof(TOut), true)[0];
            return attr;
        }

        public static bool HasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.GetCustomAttribute<TAttribute>() != null;
        }
    }
}
