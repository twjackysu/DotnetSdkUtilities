using System;

namespace DotnetSdkUtilities.Extensions.DateTimeExtensions
{
    public static class DateTimeExtension
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static ulong ToEpochMilliseconds(this DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                dateTime = dateTime.ToUniversalTime();
            }
            return (ulong)(dateTime - UnixEpoch).TotalMilliseconds;
        }
    }
}
