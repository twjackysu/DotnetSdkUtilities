using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DotnetSdkUtilities.IConfigurationBuilderExtensions;
using System.Threading.Tasks;

namespace TestCase
{
    [TestClass]
    public class IConfigurationBuilderExtensionsTest
    {
        [TestMethod]
        public void AddProtectedJsonFileTest()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string password = "testPassword";
            string path = "testPath.json";
            bool optional = true;
            bool reloadOnChange = false;
            Regex[] encryptedKeyExpressions = new Regex[] { new Regex("test") };

            var result = configurationBuilder.AddProtectedJsonFile(password, path, optional, reloadOnChange, encryptedKeyExpressions);
            
            Assert.IsTrue(result.Sources.Count == 1);
        }
    }
}
