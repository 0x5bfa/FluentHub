using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Uwp.Models
{
    public class PinnableRepository
    {
        public bool IsPinned { get; set; }

        public Repository PinnableItem { get; set; }
    }
}
