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
        /// The resolution of time diff is 1 minute
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        static public string GetDataTimeDiff(DateTimeOffset target)
        {
            DateTime now = DateTime.Now;

            int yearDiff = now.Year - target.Year;

            if (target.Month == now.Month && now.Day < target.Day || now.Month < target.Month)
            {
                yearDiff--;
            }

            var diifTimeSpan = now - target;
            int days = (int)Math.Abs(Math.Round(diifTimeSpan.TotalDays));

            string FormattedTimeDiffString = null;
            int timeDiff = 0;

            if (days < 365)
            {
                if ((timeDiff = now.Month + 12 - target.Month) == 0)
                {
                    TimeSpan timeSpanDiff = now - target;

                    if (timeSpanDiff.Days == 0)
                    {
                        if (timeSpanDiff.Hours == 0)
                        {
                            if (timeSpanDiff.Minutes == 0)
                            {
                                FormattedTimeDiffString = string.Format($"now");
                                return FormattedTimeDiffString;
                            }
                            else
                            {
                                FormattedTimeDiffString = string.Format($"{timeSpanDiff.Minutes}min");
                                return FormattedTimeDiffString;
                            }
                        }
                        else
                        {
                            FormattedTimeDiffString = string.Format($"{timeSpanDiff.Hours}h");
                            return FormattedTimeDiffString;
                        }
                    }
                    else
                    {
                        FormattedTimeDiffString = string.Format($"{timeSpanDiff.Days}d");
                        return FormattedTimeDiffString;
                    }
                }
                else
                {
                    FormattedTimeDiffString = string.Format($"{timeDiff}mo");
                    return FormattedTimeDiffString;
                }
            }
            else
            {
                FormattedTimeDiffString = string.Format($"{yearDiff}yr");
                return FormattedTimeDiffString;
            }
        }

    }
}
