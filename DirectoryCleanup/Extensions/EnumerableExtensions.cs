using System;
using System.Collections.Generic;
using System.Linq;

namespace DirectoryCleanup.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            foreach (T item in source)
            {
                yield return item;

                IEnumerable<T> children = selector(item);
                if (children == null)
                    continue;

                foreach (T descendant in children.SelectManyRecursive(selector))
                {
                    yield return descendant;
                }
            }
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static HashSet<TResult> ToHashSet<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return new HashSet<TResult>(source.Select(selector));
        }
    }
}