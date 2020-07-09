using System.Collections;

namespace Computer_Wifi_Remote_Library.Extensions
{
    public static class EnumerableExtension
    {
        public static T ElementAt<T>(this IEnumerable items, int index)
        {
            var iterator = items.GetEnumerator();
            for (var i = 0; i <= index; i++, iterator.MoveNext()) ;
            return (T)iterator.Current;
        }
    }
}
