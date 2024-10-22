using DotnetSdkUtilities.Extensions.TypeExtensions;
using System;
using System.Reflection;

namespace DotnetSdkUtilities.Extensions.GenericExtensions
{
    public static class GenericExtensions
    {
        public static bool EqualsExt<T>(this T obj1, T obj2)
        {
            foreach (PropertyInfo propertyInfo in obj1.GetType().GetProperties())
            {
                var value = propertyInfo.GetValue(obj1);
                var value2 = propertyInfo.GetValue(obj2);
                if ((value != null && value2 == null) || (value == null && value2 != null))
                    return false;
                //Func<Type, bool> CheckIsImplementIEquatable = 
                //    (type) => 
                //    type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEquatable<>));
                if (propertyInfo.PropertyType.IsImplementGenericInterface(typeof(IEquatable<>)))
                {
                    if (!value.Equals(value2))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!EqualsExt(value, value2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
