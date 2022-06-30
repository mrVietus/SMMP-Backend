using System;
using System.Text.RegularExpressions;

namespace SMMP.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static TEnum ToEnum<TEnum>(this string str)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), str, true);
        }

        public static bool TryToEnum<TEnum>(this string str, out TEnum result)
        {
            var flag = Enum.TryParse(typeof(TEnum), str, true, out var convertedValue);

            result = flag
                ? (TEnum)convertedValue
                : default;

            return flag;
        }

        public static string WrapInSingleQuotes(this string str)
        {
            var text = Regex.Replace(str, @"[^,\s][^\,]*[^,\s]*", "'$0'");
            return text;
        }
    }
}
