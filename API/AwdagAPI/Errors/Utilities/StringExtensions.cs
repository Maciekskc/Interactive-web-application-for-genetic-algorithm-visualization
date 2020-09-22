using System;
using System.Linq;

namespace Validation.Utilities
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                if (str.Contains("."))
                {
                    var strings = str.Split('.');
                    strings = strings
                        .Select(x => Char.ToLowerInvariant(x[0]) + x.Substring(1))
                        .ToArray();
                    return string.Join(".", strings);
                }


                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }
    }
}
