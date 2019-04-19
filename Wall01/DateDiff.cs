using System;
using System.Collections.Generic;
using System.Text;
using static System.String;

namespace Wall01
{
    public class DateDiff: IDateDiff
    {

        public string GetTimeSincePosted(Post post)
        {
            return GetTimeBetween(post.Timestamp, DateTime.Now);

        }

        public string GetTimeBetween(DateTime t1, DateTime t2)
        {
            var timeDiff= t2.Subtract(t1);
            if (timeDiff.Days >= 1)
            {
                return "(more than 24 hours ago)";
            }
            if (timeDiff.Hours > 0)
            {
                return GetFormattedTimeBetween(timeDiff.Hours, "hour");
            }
            if (timeDiff.Minutes > 0)
            {
                var value = timeDiff.Minutes;
                var timeUnitSingular = "minute";
                return GetFormattedTimeBetween(value, timeUnitSingular);
            }
            return GetFormattedTimeBetween(timeDiff.Seconds, "second");

        }

        private static string GetFormattedTimeBetween(int value, string timeUnitSingular)
        {
            var plural = value == 1 ? Empty : "s";

            return $"({value} {timeUnitSingular}{plural} ago)";
        }
    }
}
