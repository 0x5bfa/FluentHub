using System;

namespace FluentHub.Octokit.Generation.Models
{
    public class EnumValueModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeprecated { get; set; }
        public string DeprecationReason { get; set; }
    }
}
