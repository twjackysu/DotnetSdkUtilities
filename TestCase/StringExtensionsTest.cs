using DotnetSdkUtilities.StringExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCase
{
    [TestClass]
    public class StringExtensionsTest
    {
        private readonly string myData = "This is confidential data.";
        private readonly string key = "#248GGVNvMvz";

        [TestMethod]
        public void UnprotectedValueSameAsOriginal()
        {
            var protectedValue = myData.Encrypt(key);
            var unprotectedValue = protectedValue.Decrypt(key);
            Assert.AreEqual(myData, unprotectedValue);
            var unprotectedValue2 = Convert.ToBase64String(protectedValue).DecryptFromString(key);
            Assert.AreEqual(myData, unprotectedValue2);
        }
        [TestMethod]
        public void ProtectedValueDifferentFromOriginal()
        {
            var protectedValue = myData.Encrypt(key);
            var protectedValueStr = Convert.ToBase64String(protectedValue);
            Assert.AreNotEqual(myData, Convert.ToBase64String(protectedValue));
        }
    }
}
