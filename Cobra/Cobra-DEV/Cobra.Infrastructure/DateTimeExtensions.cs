using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.Infrastructure
{
    public static class DateTimeExtensions
    {
        public static DateTime? ConvertUtcToLocalTime(this DateTime? date, string timezoneIdentifier)
        {
            if (!date.HasValue || string.IsNullOrEmpty(timezoneIdentifier)) return null;

            var tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneIdentifier);

            return TimeZoneInfo.ConvertTimeFromUtc(date.Value, tz);
        }

        public static DateTime? ConvertLocalTimeToUtc(this DateTime? date, string timezoneIdentifier)
        {
            if (!date.HasValue || string.IsNullOrEmpty(timezoneIdentifier)) return null;

            var date2 = DateTime.SpecifyKind(date.Value, DateTimeKind.Unspecified);

            var tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneIdentifier);

            return TimeZoneInfo.ConvertTimeToUtc(date2, tz);
        }

        public static DateTime ConvertUtcToLocalTime(this DateTime date, string timezoneIdentifier)
        {
            if (string.IsNullOrEmpty(timezoneIdentifier)) throw new ArgumentNullException("timezoneIdentifier");

            var tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneIdentifier);

            return TimeZoneInfo.ConvertTimeFromUtc(date, tz);
        }

        public static DateTime ConvertLocalTimeToUtc(this DateTime date, string timezoneIdentifier)
        {
            if (string.IsNullOrEmpty(timezoneIdentifier)) throw new ArgumentNullException("timezoneIdentifier");

            var date2 = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);

            var tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneIdentifier);

            return TimeZoneInfo.ConvertTimeToUtc(date2, tz);
        }

        public static string ToStringInvariant(this DateTime date, string format)
        {
            return date.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this DateTime? date, string format)
        {
            if (!date.HasValue)
                throw new InvalidOperationException("date");

            return date.Value.ToStringInvariant(format);
        }

        public static bool IsBetween(this DateTime date, DateTime start, DateTime end)
        {
            return date >= start && date <= end;
        }

        public static DateTime StartOfDay(this DateTime input)
        {
            return input.Date;
        }

        public static DateTime EndOfDay(this DateTime input)
        {
            return input.Date.AddDays(1).AddMilliseconds(-1);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        public static string ToStringMilliseconds(this DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
        }

    }
}
