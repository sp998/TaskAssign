using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssign.Utils
{
    public static class TimeUtils
    {
        public static string GetTime(TimeSpan timeDuration)
        {
            var totalTime = new StringBuilder();
            if (timeDuration.Days >= 1)
            {
                totalTime.Append(timeDuration.Days);
                totalTime.Append("d");
            }
            if (timeDuration.Hours >= 1)
            {
                totalTime.Append(timeDuration.Hours);
                totalTime.Append("h");
            }

            if (timeDuration.Minutes >= 1)
            {
                totalTime.Append(timeDuration.Minutes);
                totalTime.Append("m");

            }
                totalTime.Append(timeDuration.Seconds);
                totalTime.Append("s");
            

            return totalTime.ToString();
        }

    }
}
