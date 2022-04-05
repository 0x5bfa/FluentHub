using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class ContributionCalender
    {
        public int TotalContributions { get; set; }

        // The query gets the week and day separately,
        // but the data is processed together in relation to when the page is displayed.
        public List<CalendarDay> ContributionDays { get; set; } = new();
    }
}
