namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an object which can take actions on GitHub. Typically a User or Bot.
    /// </summary>
    public interface IActor
    {        /// <summary>
        /// A URL pointing to the actor's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        string AvatarUrl { get; set; }

        /// <summary>
        /// The username of the actor.
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// The HTTP path for this actor.
        /// </summary>
        string ResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for this actor.
        /// </summary>
        string Url { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class Actor : IActor
    {
        public string AvatarUrl { get; set; }

        public string Login { get; set; }

        public string ResourcePath { get; set; }

        public string Url { get; set; }
    }
}