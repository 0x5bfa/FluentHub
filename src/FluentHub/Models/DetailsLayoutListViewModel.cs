using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Models
{
    public class DetailsLayoutListViewModel
    {
        public string ObjectTypeIconGlyph { get; set; }

        public string ObjectName { get; set; }

        public string ObjectLatestCommitMessage { get; set; }

        public string ObjectUpdatedAtHumanized { get; set; }

        public string ObjectTag { get; set; }
    }
}
