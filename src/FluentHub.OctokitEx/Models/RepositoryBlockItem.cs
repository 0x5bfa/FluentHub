﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Models
{
    public class RepositoryBlockItem
    {
        public string Name { get; set; }

        public string Owner { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public string LicenseName { get; set; }

        public int ForkCount { get; set; }

        public int IssueCount { get; set; }

        public int PullCount { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
