using System.Diagnostics.CodeAnalysis;

namespace Filippa;

internal static class EnumerableExtensions
{
    [DoesNotReturn]
    public static IEnumerable<T> ToLoop<T>(this IEnumerable<T> sequence)
    {
        while (true)
        {
            foreach (var item in sequence)
            {
                yield return item;
            }
        }
    }

    public static T PickAny<T>(this IReadOnlyList<T> items, Random random)
    {
        var index = random.Next(0, items.Count);

        return items[index];
    }

    public static T PickAny<T>(this IReadOnlyList<T> items) => PickAny(items, new Random());
}