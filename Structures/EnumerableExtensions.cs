using System.Text;

namespace Structures;

public static class EnumerableExtensions
{
    public static int GetSequenceHashCode<TItem>(this IEnumerable<TItem?>? list)
    {
        if (list == null) return 0;
        const int seedValue = 0x2D2816FE;
        const int primeNumber = 397;
        return list.Aggregate(seedValue,
            (current, item) => current * primeNumber + (Equals(item, default(TItem)) ? 0 : item?.GetHashCode() ?? 0));
    }

    public static string DeepToString<TItem>(this IEnumerable<TItem?>? list)
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in list ?? new List<TItem?>()) stringBuilder.Append(item).Append(' ');
        return stringBuilder.ToString();
    }
}