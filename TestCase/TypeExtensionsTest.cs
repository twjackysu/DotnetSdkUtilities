using DotnetSdkUtilities.Extensions.TypeExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCase
{
    [TestClass]
    public class TypeExtensionsTest
    {
        [TestMethod]
        public void IsImplementGenericInterfaceTest()
        {
            Type type = typeof(List<int>);
            Type @interface = typeof(IEnumerable<>);

            bool result = type.IsImplementGenericInterface(@interface);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetMaxValueTest()
        {
            Type type = typeof(decimal);

            decimal result = type.GetMaxValue<decimal>();

            Assert.AreEqual(decimal.MaxValue, result);
        }

        [TestMethod]
        public void GetMinValueTest()
        {
            Type type = typeof(decimal);

            decimal result = type.GetMinValue<decimal>();

            Assert.AreEqual(decimal.MinValue, result);
        }
    }
}
