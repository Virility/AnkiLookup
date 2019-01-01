using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiLookup.Core.Extensions
{
    public static class ForEachAsyncExtensions
    {
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int count, Func<T, Task> body)
        {
            return Task.WhenAll(
                Partitioner.Create(source).GetPartitions(count).
                Select(partition =>
                    Task.Run(async delegate
                    {
                        using (partition)
                            while (partition.MoveNext())
                                await body(partition.Current);
                    })));
        }
    }
}
