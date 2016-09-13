using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.Infrastructure
{
    public static class EnumerableExtensions
    {

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }

        public static void ForEach<T1, T2>(this IEnumerable<T1> source, Action<T1, T2> action, T2 arg)
        {
            foreach (T1 element in source)
            {
                action(element, arg);
            }
        }

        public static void ForEach<T1, T2, T3>(this IEnumerable<T1> source, Action<T1, T2, T3> action, T2 arg1, T3 arg2)
        {
            foreach (T1 element in source)
            {
                action(element, arg1, arg2);
            }
        }

        public static IEnumerable<TSource> Distinct<TSource, TResult>(this IEnumerable<TSource> items, Func<TSource, TResult> selector)
        {
            var set = new HashSet<TResult>();
            foreach (var item in items)
            {
                var hash = selector(item);

                if (set.Contains(hash)) continue;

                set.Add(hash);
                yield return item;
            }
        }


        /// <summary>
        /// Returns true if the generic <see cref="IEnumerable"/> object is null or contains 0 element,
        /// otherwise false.
        /// </summary>
        /// <typeparam name="T">The Type</typeparam>
        /// <param name="source">A generic <see cref="IEnumerable"/> object.</param>
        /// <returns>
        /// True if the generic <see cref="IEnumerable"/> object is null or empty.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Returns true if the  <see cref="IEnumerable"/> object is null or contains 0 element, 
        /// otherwise false.
        /// </summary>
        /// <param name="source"><see cref="IEnumerable"/> object.</param>
        /// <returns>True if the <see cref="IEnumerable"/> object is null or empty.</returns>
        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            // it is null.
            if (source == null)
            {
                return true;
            }

            // check if there is any element. returns false if any as not empty.
            var enumerator = source.GetEnumerator();

            return !enumerator.MoveNext();
        }

        public static Type GetItemType<T>(this IEnumerable<T> enumerable)
        {
            return typeof(T);
        }

    }
}
