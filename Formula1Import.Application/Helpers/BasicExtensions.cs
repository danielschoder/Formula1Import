using System.Net;
using System.Text.RegularExpressions;

namespace Formula1Import.Application.Helpers;

public static class BasicExtensions
{
    public static T AddedItem<T>(this ICollection<T> collection, T item)
    {
        collection.Add(item);
        return item;
    }

    public static int ExistingId<T>(this T dictionary, string key)
        where T : Dictionary<string, int>
    {
        dictionary.TryGetValue(key, out int id);
        return id;
    }


    public static string FormattedDriverName(this string name)
    {
        name = Regex.Replace(WebUtility.HtmlDecode(name).Trim(), @"\s+", " ");
        return name.Length >= 3 && name[^3..].All(char.IsUpper)
                    ? name.Substring(0, name.Length - 3)
                    : name;
    }
}
