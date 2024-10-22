using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotnetSdkUtilities.Extensions.DateTimeExtensions;
using System;

namespace TestCase
{
    [TestClass]
    public class DateTimeExtensionsTest
    {
        [TestMethod]
        public void ToEpochMilliseconds_ShouldReturnCorrectValue_ForUtcDateTime()
        {
            var dateTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var result = dateTime.ToEpochMilliseconds();

            var expectedMilliseconds = 1704067200000UL;
            Assert.AreEqual(expectedMilliseconds, result);
        }

        [TestMethod]
        public void ToEpochMilliseconds_ShouldConvertLocalTime_ToUtcAndReturnCorrectValue()
        {
            var localDateTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Local);

            var result = localDateTime.ToEpochMilliseconds();

            var expectedUtcDateTime = localDateTime.ToUniversalTime();
            var expectedMilliseconds = (ulong)(expectedUtcDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            Assert.AreEqual(expectedMilliseconds, result);
        }

        [TestMethod]
        public void ToEpochMilliseconds_ShouldConvertUnspecifiedKind_ToUtcAndReturnCorrectValue()
        {
            var unspecifiedDateTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

            var result = unspecifiedDateTime.ToEpochMilliseconds();

            var expectedUtcDateTime = unspecifiedDateTime.ToUniversalTime();
            var expectedMilliseconds = (ulong)(expectedUtcDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            Assert.AreEqual(expectedMilliseconds, result);
        }

        [TestMethod]
        public void ToEpochMilliseconds_ShouldReturnZero_ForEpochTime()
        {
            var epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var result = epochDateTime.ToEpochMilliseconds();

            Assert.AreEqual(0UL, result);
        }
    }
}
