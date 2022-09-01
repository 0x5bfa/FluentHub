using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Uwp.Models
{
    public class MergedCalendarDays
    {
        public string Color { get; set; }
        public int ContributionCount { get; set; }
        public ContributionLevel ContributionLevel { get; set; }
        public int Weekday { get; set; }

        public bool IsVaild { get; set; }
    }
}
