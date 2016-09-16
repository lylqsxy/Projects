using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Helplers
{
    public static class DateTimeHelper
    {
        public static bool IsExpired(DateTime? date)
        {
            var dateNeedCompare = date.HasValue ? date.Value : DateTime.UtcNow.AddDays(-1);
            if (DateTime.Compare(DateTime.UtcNow, dateNeedCompare) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
