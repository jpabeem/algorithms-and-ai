using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class Clock
    {
        public static long GetCurrentTimeInSeconds()
        {
            // 01-01-1970 is unix epoch
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)((DateTime.UtcNow - Jan1st1970).TotalSeconds);
        }

        public static long GetCurrentTimeInMillis()
        {
            // 01-01-1970 is unix epoch
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)((DateTime.UtcNow - Jan1st1970).TotalMilliseconds);
        }
    }
}
