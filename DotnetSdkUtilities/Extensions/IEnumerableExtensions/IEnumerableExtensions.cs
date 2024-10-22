using System;
using System.Collections.Generic;
using DotnetSdkUtilities.Extensions.TypeExtensions;

namespace DotnetSdkUtilities.Extensions.IEnumerableExtensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 找到內部屬性最大值的第一個項目
        /// </summary>
        public static T FindFirstMaxPropertyItem<T, TProperty>(this IEnumerable<T> source, Func<T, TProperty> selector) where TProperty : IComparable<TProperty>
        {
            T maxObject = default(T);
            TProperty max = typeof(TProperty).GetMinValue<TProperty>();
            foreach (var item in source)
            {
                if (selector(item).CompareTo(max) > 0)
                {
                    max = selector(item);
                    maxObject = item;
                }
            }
            return maxObject;
        }
        /// <summary>
        /// 找到內部屬性最大值的全部項目
        /// </summary>
        public static IEnumerable<T> FindMaxPropertyItems<T, TProperty>(this IEnumerable<T> source, Func<T, TProperty> selector) where TProperty : IComparable<TProperty>
        {
            var max = typeof(TProperty).GetMinValue<TProperty>();
            var maxPropertyItems = new List<T>();
            foreach (var item in source)
            {
                var property = selector(item);
                var compareResult = property.CompareTo(max);
                if (compareResult > 0)
                {
                    maxPropertyItems.Clear();
                    max = property;
                    maxPropertyItems.Add(item);
                }
                else if (compareResult == 0)
                {
                    maxPropertyItems.Add(item);
                }
            }
            foreach (var item in maxPropertyItems)
            {
                yield return item;
            }
        }
        /// <summary>
        /// 找到內部屬性最小值的第一個項目
        /// </summary>
        public static T FindFirstMinPropertyItem<T, TProperty>(this IEnumerable<T> source, Func<T, TProperty> selector) where TProperty : IComparable<TProperty>
        {
            T maxObject = default(T);
            TProperty min = typeof(TProperty).GetMaxValue<TProperty>();
            foreach (var item in source)
            {
                if (selector(item).CompareTo(min) < 0)
                {
                    min = selector(item);
                    maxObject = item;
                }
            }
            return maxObject;
        }
        /// <summary>
        /// 找到內部屬性最小值的全部項目
        /// </summary>
        public static IEnumerable<T> FindMinPropertyItems<T, TProperty>(this IEnumerable<T> source, Func<T, TProperty> selector) where TProperty : IComparable<TProperty>
        {
            var min = typeof(TProperty).GetMaxValue<TProperty>();
            var maxPropertyItems = new List<T>();
            foreach (var item in source)
            {
                var property = selector(item);
                var compareResult = property.CompareTo(min);
                if (compareResult < 0)
                {
                    maxPropertyItems.Clear();
                    min = property;
                    maxPropertyItems.Add(item);
                }
                else if (compareResult == 0)
                {
                    maxPropertyItems.Add(item);
                }
            }
            foreach (var item in maxPropertyItems)
            {
                yield return item;
            }
        }
    }
}
