﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TombLib.Utils
{
    public static class CollectionUtils
    {
        public static int ReferenceIndexOf<T>(this IList<T> list, T needle)
        {
            // This is not implemented for IEnumerable on purpose to avoid abuse of this method on non ordered containers.
            // (HashSet, Dictionary, ...)

            if (needle == null)
                return -1;

            for (int i = 0; i < list.Count; ++i)
                if (ReferenceEquals(list[i], needle))
                    return i;

            return -1;
        }

        public static TValue TryGetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> rooms, TKey key, TValue @default = default(TValue))
        {
            TValue result;
            if (rooms.TryGetValue(key, out result))
                return result;
            return @default;
        }

        public static IEnumerable<T> Unwrap<T>(this T[,] array)
        {
            for (int x = 0; x < array.GetLength(0); ++x)
                for (int y = 0; y < array.GetLength(1); ++y)
                    yield return array[x, y];
        }

        public static T TryGet<T>(this T[] array, int index0) where T : class
        {
            if ((index0 < 0) || (index0 >= array.GetLength(0)))
                return null;
            return array[index0];
        }

        public static T TryGet<T>(this T[,] array, int index0, int index1) where T : class
        {
            if ((index0 < 0) || (index0 >= array.GetLength(0)))
                return null;
            if ((index1 < 0) || (index1 >= array.GetLength(1)))
                return null;
            return array[index0, index1];
        }

        public static T TryGet<T>(this T[,,] array, int index0, int index1, int index2) where T : class
        {
            if ((index0 < 0) || (index0 >= array.GetLength(0)))
                return null;
            if ((index1 < 0) || (index1 >= array.GetLength(1)))
                return null;
            if ((index2 < 0) || (index2 >= array.GetLength(2)))
                return null;
            return array[index0, index1, index2];
        }

        public static T FindFirstAfterWithWrapAround<T>(this IEnumerable<T> list, Func<T, bool> IsPrevious, Func<T, bool> Matches) where T : class
        {
            bool ignoreMatches = true;

            // Search for matching objects after the previous one
            foreach (T obj in list)
            {
                if (ignoreMatches)
                {
                    if (IsPrevious(obj))
                        ignoreMatches = false;
                    continue;
                }

                // Does it match
                if (Matches(obj))
                    return obj;
            }

            // Search for any matching objects
            foreach (T obj in list)
                if (Matches(obj))
                    return obj;

            return null;
        }
    }
}
