using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardBouncer.Frontend.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            var isNotNullOrEmpty = false;
            if (collection != null && collection.Any())
            {
                isNotNullOrEmpty = true;
            }

            return isNotNullOrEmpty;
        }
    }
}
