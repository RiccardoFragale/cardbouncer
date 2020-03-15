using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardBouncer.Frontend.Extensions
{
    public static class DatetimeExtensions
    {
        /// <summary>
        /// Calculates the age, give a birthdate.
        /// </summary>
        /// <param name="birthDate">The date of birth.</param>
        /// <returns>A number representing the age.</returns>
        /// <remarks>https://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c</remarks>
        public static int CalculateAge(this DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;

            // For leap years we need this
            if (birthDate > DateTime.Now.AddYears(-age))
                age--;

            return age;
        }
    }
}
