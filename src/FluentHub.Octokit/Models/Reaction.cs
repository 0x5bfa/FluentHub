﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphqlmodel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models
{
    public class Reaction
    {
        public graphqlmodel.ReactionContent Content { get; set; }

        public string ReactorLogin { get; set; }
    }
}