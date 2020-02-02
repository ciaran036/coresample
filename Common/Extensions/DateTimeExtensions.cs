using System;

namespace Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Retrieves the date/time for the start of the week
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <param name="startOfWeek">Start of the week</param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            while (dateTime.DayOfWeek != startOfWeek)
                dateTime = dateTime.AddDays(-1);
            return dateTime;
        }

        /// <summary>
        /// Converts <see cref="TimeSpan"/> objects to a simple human-readable string.  Examples: 3.1 seconds, 2 minutes, 4.23 hours, etc.
        /// </summary>
        /// <param name="span">The timespan.</param>
        /// <param name="significantDigits">Significant digits to use for output.</param>
        /// <returns></returns>
        public static string ToHumanTimeString(this TimeSpan span, int significantDigits = 3)
        {
            var format = "G" + significantDigits;
            return span.TotalMilliseconds < 1000 ? span.TotalMilliseconds.ToString(format) + " milliseconds"
                : (span.TotalSeconds < 60 ? span.TotalSeconds.ToString(format) + " seconds"
                    : (span.TotalMinutes < 60 ? span.TotalMinutes.ToString(format) + " minutes"
                        : (span.TotalHours < 24 ? span.TotalHours.ToString(format) + " hours"
                            : span.TotalDays.ToString(format) + " days")));
        }

        public static bool CompareDates(DateTime start, DateTime end, Func<DateTime, DateTime, bool> func)
        {
            return func(start, end);
        }
    }
}
