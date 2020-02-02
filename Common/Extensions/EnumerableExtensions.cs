using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Split a set into multiple sets
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int itemsPerSet)
        {
            var sourceArray = source as T[] ?? source.ToArray();
            var result = new List<IEnumerable<T>>();
            for (var index = 0; index < sourceArray.Length; index += itemsPerSet)
            {
                result.Add(sourceArray.Skip(index).Take(itemsPerSet));
            }
            return result;
        }

        public static IEnumerable<TOutput> Map<TInput, TOutput>(this IEnumerable<TInput> collection, Func<TInput, TOutput> mapMethod)
        {
            return collection.Select(mapMethod).ToList();
        }

        public static List<TOutput> Map<TInput, TOutput>(this List<TInput> items, Func<TInput, TOutput> mapMethod)
        {
            return items.Select(mapMethod).ToList();
        }

        /// <summary>
        /// Takes input of a single item and returns an IEnumerable of the type containing the item
        /// </summary>
        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}
