using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MalekServer3.Utilities
{

    public static class ExtensionMethods
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
    }
}