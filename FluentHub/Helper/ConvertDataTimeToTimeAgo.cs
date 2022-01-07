using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Healper
{
    public class ConvertDataTimeToTimeAgo
    {
        /// <summary>
        /// The resolution of time diff is one second
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        static public string GetDataTimeDiff(DateTimeOffset target)
        {

            var ts = new TimeSpan(DateTime.Now.Ticks - target.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 60)
            {
                if (ts.Seconds <= 5)
                {
                    return "now";
                }
                else
                {
                    return string.Format("{0}s", ts.Seconds);
                }
            }
            else if (delta < 120)
            {
                return "1min";
            }
            else if (delta < 2700) // 45 * 60
            {
                return string.Format("{0}min", ts.Minutes);
            }
            else if (delta < 5400) // 90 * 60
            {
                return "1h";
            }
            else if (delta < 86400) // 24 * 60 * 60
            {
                return string.Format("{0}h", ts.Hours);
            }
            else if (delta < 172800) // 48 * 60 * 60
            {
                return "1d";
            }
            else if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return string.Format("{0}d", ts.Days);
            }
            else if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));

                if (months <= 1)
                {
                    return "1mo";
                }
                else
                {
                    return string.Format("{0}mo", months);
                }
            }

            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));

            if (years <= 1)
            {
                return "1yr";
            }
            else
            {
                return string.Format("{0}yr", years);
            }
        }

    }
}
