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
        /// <param name="dataTimeTarget"></param>
        /// <returns></returns>
        static public string GetDataTimeDiff(DateTime dataTimeTarget)
        {
            DateTime dataTimeNow = DateTime.Now;

            string FormattedTimeDiffString = null;
            int timeDiff = 0;

            if ((timeDiff = dataTimeNow.Year - dataTimeTarget.Year) == 0)
            {
                if ((timeDiff = dataTimeNow.Month - dataTimeTarget.Month) == 0)
                {
                    TimeSpan timeSpanDiff = dataTimeNow - dataTimeTarget;

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
                FormattedTimeDiffString = string.Format($"{timeDiff}yr");
                return FormattedTimeDiffString;
            }
        }

    }
}
