using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Models
{
    public class IssueOverviewItem
    {
        public string Name { get; set; }

        public string Owner { get; set; }

        public bool IsClosed { get; set; }

        public List<LabelOverviewItem> Labels { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
