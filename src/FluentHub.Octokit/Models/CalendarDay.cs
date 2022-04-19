using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace FluentHub.Octokit.Models
{
    public class CalendarDay
    {
        public int WeekDay { get; set; }

        public int ContributionCount { get; set; }

        public ContributionLevel ContributionLevel { get; set; }

        public string Color { get; set; }

        public bool Visibility { get; set; }
    }
}
