using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool ContainsAny(this string source, params string[] searchTerms)
        {
            return searchTerms.Any(source.Contains);
        }

        public static string RemoveLineBreaks(this string str)
        {
            return str?.Length > 0 ? str.Replace("\r\n", "") : "";
        }

        public static bool ContainSpecialCharacters(this string name)
        {
            var pattern = new Regex("^[a-zA-Z0-9 ]*$");
            return !pattern.IsMatch(name);
        }

        public static string AddSpaces(this string text)
        {
            return Regex.Replace(text, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
        }

        public static string PrependComma(this string text)
        {
            if (String.IsNullOrEmpty(text)) return text;

            return $", {text}";
        }

        /// <summary>
        /// Cut a string at a specified max length and appends an ellipsis
        /// </summary>
        public static string Cut(this string text, int maxLength)
        {
            return text.Length <= maxLength ? text : text.Substring(0, maxLength).PadRight(3, '.');
        }

        /// <summary>
        /// Performs trimming of all string properties on a type
        /// </summary>
        /// <typeparam name="TSelf">Type we are trimming</typeparam>
        /// <param name="input">Instance of the type we wish to trim</param>
        /// <returns>Trimmed type</returns>
        public static TSelf TrimStringProperties<TSelf>(this TSelf input)
        {
            if (input == null)
                return input;

            var stringProperties = typeof(TSelf).GetProperties()
                .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(input, null);
                if (currentValue != null)
                    stringProperty.SetValue(input, currentValue.Trim(), null);
            }
            return input;
        }

        public static List<string> SplitOnLength(this string text, int splitLength)
        {
            List<string> splitStrings = new List<string>();

            while (!String.IsNullOrEmpty(text))
            {
                try
                {
                    splitStrings.Add(text.Substring(0, splitLength));
                    text = text.Remove(0, splitLength);
                }
                catch (Exception)
                {
                    splitStrings.Add(text.Substring(0));
                    text = "";
                }
            }

            return splitStrings;
        }

        public static int[] ToIntArray(this string text)
        {
            return String.IsNullOrEmpty(text) ? null : (from s in text.Split(',') where !String.IsNullOrEmpty(s) select Convert.ToInt32(s)).ToArray();
        }

        public static string[] ToStringArray(this string text)
        {
            return String.IsNullOrEmpty(text) ? null : text.Split(',').Where(s => !String.IsNullOrEmpty(s)).ToArray();
        }

        public static string ToTitleCase(this string text)
        {
            var textInfo = new CultureInfo(CultureInfo.CurrentCulture.Name, true).TextInfo;
            return textInfo.ToTitleCase(text);
        }

        public static T Deserialize<T>(this string value) => JsonConvert.DeserializeObject<T>(value);

        public static T Deserialize<T>(this string value, JsonSerializerSettings serializerSettings) => JsonConvert.DeserializeObject<T>(value, serializerSettings);

        public static async Task<T> Deserialize<T>(this Task<string> value) => JsonConvert.DeserializeObject<T>(await value);

        /// <summary>
        /// Adds spaces between words where it is camel-cased
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

        public static string StripHtml(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Removes the string Controller for a string value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Clean(this string value)
            => value.Replace("Controller", string.Empty);
    }
}