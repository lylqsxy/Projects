using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cobra.Infrastructure
{
    public static class StringExtensions
    {
        public static string FormatInvariant(this string value, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, value, args);
        }

        public static string ComputeMd5Hash(this string value)
        {
            using (var md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", string.Empty);
            }
        }

        public static string Wordify(this string value)
        {
            var r = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");

            return r.Replace(value, " ${x}");
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        /// Safe trim method that can be called on null strings.
        /// </summary>
        /// <param name="value">string to be trimmed, if it's not null.</param>
        /// <returns>trimmed string or string.empty if the value is null.</returns>
        public static string GetTrimmed(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
        }
    }
}
