using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Models.Items
{
    public class DetailsLayoutListViewItem
    {
        public string ObjectTypeIconGlyph { get; set; }

        public string ObjectName { get; set; }

        public string ObjectLatestCommitMessage { get; set; }

        public string ObjectUpdatedAtHumanized { get; set; }
    }
}
