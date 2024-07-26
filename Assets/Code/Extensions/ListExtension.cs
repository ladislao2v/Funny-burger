using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Code.Extensions
{
    public static class ListExtension
    {
        public static T PreLast<T>(this List<T> list)
        {
            return list[list.Count - 2];
        }
        
        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) 
                throw new InvalidOperationException(nameof(list));
            
            return list[Random.Range(0, list.Count)];
        }
    }
}