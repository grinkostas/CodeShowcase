using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extentions
{
    public static class IEnumerableExtensions
    {
        public static Dictionary<TKey, TValue> CloneDictionary<TKey, TValue>
            (this Dictionary<TKey, TValue> original) where TValue : System.ICloneable
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(original.Count,
                original.Comparer);

            foreach (KeyValuePair<TKey, TValue> entry in original)
                result.Add(entry.Key, (TValue)entry.Value.Clone());

            return result;
        }

        public static Dictionary<TKey, TValue> Copy<TKey, TValue>
            (this Dictionary<TKey, TValue> original)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(original.Count,
                original.Comparer);

            foreach (KeyValuePair<TKey, TValue> entry in original)
                result.Add(entry.Key, entry.Value);

            return result;
        }
        
        public static bool Has<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var sourceItem in source)
            {
                if (predicate(sourceItem))
                    return true;
            }

            return false;
        }

        public static bool Has<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate,
            out TSource item)
        {
            item = default;
            foreach (var sourceItem in source)
            {
                if (predicate(sourceItem) == false)
                    continue;
                item = sourceItem;
                return true;
            }

            return false;
        }

        public static T Random<T>(this List<T> source)
        {
            if (source.Count == 0)
                return default;
            if (source.Count == 1)
                return source[0];
            int index = UnityEngine.Random.Range(0, source.Count);
            return source[index];
        }

        public static void ChangeActive(this List<GameObject> objects, bool active)
        {
            foreach (var gameObject in objects)
            {
                gameObject.SetActive(active);
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static List<T> OrderByDistance<T>(this List<T> list, Vector3 point) where T : Component
        {
            return list.OrderBy(x => (point - x.transform.position).sqrMagnitude).ToList();
        }
        
        public static List<T> OrderByDistanceDescending<T>(this List<T> list, Vector3 point) where T : Component
        {
            return list.OrderByDescending(x => (point - x.transform.position).sqrMagnitude).ToList();
        }
    }
}
