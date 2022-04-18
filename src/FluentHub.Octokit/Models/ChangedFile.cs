using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class ChangedFile
    {
        public string Patch { get; set; }
        public string ChangeType { get; set; }
        public string FileName { get; set; }
        public string PreviousFileName { get; set; }

        public int TotalLineCount { get; set; }
        public int LineAdditions { get; set; }
        public int LineDeletions { get; set; }
    }
}
