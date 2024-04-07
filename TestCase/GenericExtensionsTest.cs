using DotnetSdkUtilities.GenericExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCase
{
    public class YourObj
    {
        public string P1 { get; set; }
        public int P2 { get; set; }
        public YourObj2 P3 { get; set; }
    }
    public class YourObj2
    {
        public int P1 { get; set; }
        public int P2 { get; set; }
    }
    [TestClass]
    public class GenericExtensionsTest
    {
        [TestMethod]
        public void EachLayerValueSame()
        {
            var test1_1 = new YourObj { P1 = "5", P2 = 6, P3 = new YourObj2 { P1 = 666, P2 = 777 } };
            var test1_2 = new YourObj { P1 = "5", P2 = 6, P3 = new YourObj2 { P1 = 666, P2 = 777 } };

            Assert.IsTrue(test1_1.EqualsExt(test1_2));
        }

        [TestMethod]
        public void EachLayerValueSame2()
        {
            var test2_1 = new YourObj { P1 = "5", P3 = new YourObj2 { P1 = 666, P2 = 777 } };
            var test2_2 = new YourObj { P1 = "5", P3 = new YourObj2 { P1 = 666, P2 = 777 } };

            Assert.IsTrue(test2_1.EqualsExt(test2_2));
        }

        [TestMethod]
        public void EachLayerValueDiff()
        {
            var test3_1 = new YourObj { P1 = "5", P2 = 6, P3 = new YourObj2 { P1 = 666, P2 = 666 } };
            var test3_2 = new YourObj { P1 = "5", P2 = 5, P3 = new YourObj2 { P1 = 666, P2 = 777 } };

            Assert.IsFalse(test3_1.EqualsExt(test3_2));
        }

        [TestMethod]
        public void EachLayerValueDiff2()
        {
            var test4_1 = new YourObj { P1 = "5", P2 = 6, P3 = new YourObj2 { P1 = 666, P2 = 777 } };
            var test4_2 = new YourObj { P1 = "5", P2 = 5, P3 = new YourObj2 { P1 = 666 } };

            Assert.IsFalse(test4_1.EqualsExt(test4_2));
        }
    }
}
