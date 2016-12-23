using System;

namespace Mvc5Application1.Framework
{
    public static class DateTimeExtension
    {
        public static DateTime RemoveTime(this DateTime dateTime)
        {
            var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return date;
        }
    }
}