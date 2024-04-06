using DotnetSdkUtilities.IEnumerableExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetSdkUtilities.IEnumerableExtensionsTestCase
{
    [TestClass]
    public class IEnumerableExtensionsTest
    {
        public readonly List<YourObj> testData;
        public IEnumerableExtensionsTest()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            testData = new List<YourObj>() {
                new YourObj() { P1 = 5, P2 = new YourObj2(){ P1 = 2, P2 = new DateTime(2018,5,3)} },
                new YourObj() { P1 = 3, P2 = new YourObj2(){ P1 = 1, P2 = new DateTime(2018,10,15)} },
                new YourObj() { P1 = 1, P2 = new YourObj2(){ P1 = 0, P2 = new DateTime(2018,10,15)} },
                new YourObj() { P1 = 8, P2 = new YourObj2(){ P1 = 0, P2 = new DateTime(2018,8,10)} },
                new YourObj() { P1 = 3, P2 = new YourObj2(){ P1 = 0, P2 = new DateTime(2018,5,9)} }
            };
            for (int i = 0; i < 50000; i++)
            {
                testData.Add(new YourObj() { P1 = random.Next(50), P2 = new YourObj2() { P1 = random.Next(50), P2 = new DateTime(2018, random.Next(1, 10), random.Next(1, 27)) } });
            }
        }
        public class YourObj
        {
            public int P1 { get; set; }
            public YourObj2 P2 { get; set; }
        }
        public class YourObj2
        {
            public int P1 { get; set; }
            public DateTime P2 { get; set; }
        }
        [TestMethod]
        public void FindAllMaxSubLayerClassDatetime()
        {
            var maxTimeObjs = testData.FindMaxPropertyItems(x => x.P2.P2).ToList();
            Assert.AreEqual(maxTimeObjs.Count, 2);
        }
        [TestMethod]
        public void FindFirstMaxSubLayerClassDatetime()
        {
            var obj = testData.FindFirstMaxPropertyItem(x => x.P2.P2);
            Assert.AreEqual(obj.P1, 3);
        }
    }
}
