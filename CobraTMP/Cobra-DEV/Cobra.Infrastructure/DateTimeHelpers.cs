using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.Infrastructure
{
    public static class DateTimeHelpers
    {
        public static DateTime? TryParseNullable(string text)
        {
            DateTime date;

            return DateTime.TryParse(text, out date) ? date : (DateTime?)null;
        }

        public static DateTime TryParse(string text)
        {
            DateTime date;

            if (DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }

            throw new ApplicationException("Invalid date - cannot parse {0}".FormatInvariant(text));
        }

        public static DateTime TryParseExact(string text, string[] formats)
        {
            DateTime date;

            if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }

            throw new ApplicationException("Invalid date - cannot parse {0}".FormatInvariant(text));
        }

        public static DateTime? TryParseExact(string text, string[] formats, bool returnNullForInvalid)
        {
            DateTime date;

            if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }

            if (returnNullForInvalid) return null;

            throw new ApplicationException("Invalid date - cannot parse {0}".FormatInvariant(text));
        }
    }

    public class DateTimeRange
    {
        public DateTime From;
        public DateTime To;
    }
}
