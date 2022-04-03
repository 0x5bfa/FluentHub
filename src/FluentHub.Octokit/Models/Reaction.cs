using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphqlmodel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Models
{
    public class Reaction
    {
        public int ThumbsUpCount { get; set; }
        public int ThumbsDownCount { get; set; }
        public int LaughCount { get; set; }
        public int HoorayCount { get; set; }
        public int ConfusedCount { get; set; }
        public int HeartCount { get; set; }
        public int RocketCount { get; set; }
        public int EyesCount { get; set; }

        public bool ViewerReactThumbsUp { get; set; }
        public bool ViewerReactThumbsDown { get; set; }
        public bool ViewerReactLaugh { get; set; }
        public bool ViewerReactHooray { get; set; }
        public bool ViewerReactConfused { get; set; }
        public bool ViewerReactHeart { get; set; }
        public bool ViewerReactRocket { get; set; }
        public bool ViewerReactEyes { get; set; }

        public List<string> ThumbsUpActors { get; set; } = new();
        public List<string> ThumbsDownActors { get; set; } = new();
        public List<string> LaughActors { get; set; } = new();
        public List<string> HoorayActors { get; set; } = new();
        public List<string> ConfusedActors { get; set; } = new();
        public List<string> HeartActors { get; set; } = new();
        public List<string> RocketActors { get; set; } = new();
        public List<string> EyesActors { get; set; } = new();
    }
}
