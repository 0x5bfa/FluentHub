using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Models
{
    public class ProjectProgress
    {
        public int DoneCount { get; set; }
        public double DonePercentage { get; set; }
        public bool Enabled { get; set; }
        public int InProgressCount { get; set; }
        public double InProgressPercentage { get; set; }
        public int TodoCount { get; set; }
        public double TodoPercentage { get; set; }
    }
}
