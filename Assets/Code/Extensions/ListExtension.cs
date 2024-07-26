using System.Collections.Generic;

namespace Code.Extensions
{
    public static class ListExtension
    {
        public static T PreLast<T>(this List<T> list)
        {
            return list[list.Count - 2];
        }
    }
}