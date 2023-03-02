using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrencyAge(this DateTime dateTime)
        {
            var currencyDate = DateTime.UtcNow;
            int age = currencyDate.Year - dateTime.Year;

            return currencyDate < dateTime.AddYears(age) ? age-- : age;
        }
    }
}