﻿using DotnetSdkUtilities.Extensions.ObjectExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCase
{
    [TestClass]
    public class ObjectExtensionsTest
    {
        public readonly A test1;
        public readonly D test2;
        public ObjectExtensionsTest()
        {
            test1 = new A()
            {
                a1 = new B[] {
                    new B(){
                        b1 = new C[]
                        {
                            new C(){ c1 = ".a1[0].b1[0].c1" },
                            new C(){ c1 = ".a1[0].b1[1].c1" },
                            new C(){ c1 = ".a1[0].b1[2].c1" },
                        },
                        b2 = new C(){ c1 = ".a1[0].b2.c1" },
                        b3 = ".a1[0].b3"
                    },
                    new B(){
                        b1 = new C[]
                        {
                            new C(){ c1 = ".a1[1].b1[0].c1" },
                            new C(){ c1 = ".a1[1].b1[1].c1" },
                        },
                        b2 = new C(){ c1 = ".a1[1].b2.c1" },
                        b3 = ".a1[1].b3"
                    }
                },
                a2 = new B()
                {
                    b1 = new C[]
                        {
                            new C(){ c1 = ".a2.b1[0].c1" },
                            new C(){ c1 = ".a2.b1[1].c1" },
                        },
                    b2 = new C() { c1 = ".a2.b2.c1" },
                    b3 = ".a2.b3",
                    b4 = false
                },
                a3 = "a3"
            };

            test2 = new D()
            {
                d1 = new E[][] {
                        new E[] {
                            new E() { e1 = 1 }
                        },
                        new E[] {
                            new E() { e1 = 2 },
                            new E() { e1 = 3 }
                        }
                    }
            };
        }
        [TestMethod]
        public void TwoLayerObjectArrayPropertyPath()
        {
            Assert.AreEqual(test1.a1[0].b1[1].c1, test1.GetValue("a1[0].b1[1].c1"));
        }
        [TestMethod]
        public void SetA1B1C1ValueToWTF()
        {
            var wtf = "WTF";
            test1.SetValue("a1[0].b1[1].c1", wtf);
            Assert.AreEqual(test1.a1[0].b1[1].c1, wtf);
        }
        [TestMethod]
        public void SetA2B4ValueToTrue()
        {
            test1.SetValue("a2.b4", true);
            Assert.AreEqual(test1.a2.b4, true);
        }

        [TestMethod]
        public void ObjectArrayPropertyPath()
        {
            Assert.AreEqual(test1.a2.b1[1].c1, test1.GetValue("a2.b1[1].c1"));
        }

        [TestMethod]
        public void ObjectPropertyPath()
        {
            Assert.AreEqual(test1.a2.b3, test1.GetValue("a2.b3"));
        }
        [TestMethod]
        public void TwoLayerArrayPropertyPath()
        {
            test2.SetValue("d1[1][0].e1", 9);
            Assert.AreEqual(test2.d1[1][0].e1, 9);
            Assert.AreEqual(test2.GetValue("d1[1][0].e1"), 9);
        }
        [TestMethod]
        public void ShouldParseComplexObject()
        {
            var obj = new
            {
                AA = 1,
                BB = "333",
                CC = new int[] { 1, 3, 4 },
                DD = new
                {
                    EE = 3,
                    FF = "222",
                    GG = new int[] { 1, 2, 4 }
                }
            };

            Assert.AreEqual("AA=1,BB=333,CC=1,3,4,DD=EE=3,FF=222,GG=1,2,4", obj.ToCacheKey());
        }
        [TestMethod]
        public void ShouldSupportDictionaryObject()
        {
            var obj = new
            {
                AA = 1,
                BB = "333",
                CC = new int[] { 1, 3, 4 },
                DD = new
                {
                    EE = 3,
                    FF = "222",
                    GG = new int[] { 1, 2, 4 }
                }
            };
            var myDict = new Dictionary<string, dynamic>
            {
                {"apple", 10},
                {"banana", "yellow"},
                {"orange", obj}
            };

            Assert.AreEqual("apple_10,banana_yellow,orange_AA=1,BB=333,CC=1,3,4,DD=EE=3,FF=222,GG=1,2,4", myDict.ToCacheKey());
        }

        [TestMethod]
        public void ShouldParseIEnumerableObject()
        {
            var enumerable = new List<int> { 1, 2, 3, 4, 5 };

            Assert.AreEqual("1,2,3,4,5", enumerable.ToCacheKey());
        }

        [TestMethod]
        public void ShouldParseKeyValuePairEnumerableObject()
        {
            var obj = new
            {
                AA = 1,
                BB = "333",
                CC = new int[] { 1, 3, 4 },
                DD = new
                {
                    EE = 3,
                    FF = "222",
                    GG = new int[] { 1, 2, 4 }
                }
            };
            var item01 = new KeyValuePair<string, int>("apple", 10);
            var item02 = new KeyValuePair<string, string>("banana", "yellow");
            var item03 = new KeyValuePair<string, object>("orange", obj);
            var enumerable = new List<dynamic>
            {
                item01,
                item02,
                item03
            };
            Assert.AreEqual("apple_10,banana_yellow,orange_AA=1,BB=333,CC=1,3,4,DD=EE=3,FF=222,GG=1,2,4", enumerable.ToCacheKey());
        }
        [TestMethod]
        public void HasProperty_WithExistingProperty_ReturnsTrue()
        {
            var person = new Person { Name = "Alice", Age = 30 };
            Assert.IsTrue(person.HasProperty("Name"));
        }

        [TestMethod]
        public void HasProperty_WithNonExistingProperty_ReturnsFalse()
        {
            var person = new Person { Name = "Alice", Age = 30 };
            Assert.IsFalse(person.HasProperty("Height"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HasProperty_WithNullObject_ThrowsArgumentNullException()
        {
            Person person = null;
            person.HasProperty("Name");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HasProperty_WithNullPropertyName_ThrowsArgumentNullException()
        {
            var person = new Person { Name = "Alice", Age = 30 };
            person.HasProperty(null);
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public class A
    {
        public B[] a1 { get; set; }
        public B a2 { get; set; }
        public string a3 { get; set; }
    }
    public class B
    {
        public C[] b1 { get; set; }
        public C b2 { get; set; }
        public string b3 { get; set; }
        public bool b4 { get; set; }
    }
    public class C
    {
        public string c1 { get; set; }
    }
    public class D
    {
        public E[][] d1 { get; set; }
    }
    public class E
    {
        public int e1 { get; set; }

    }
}
