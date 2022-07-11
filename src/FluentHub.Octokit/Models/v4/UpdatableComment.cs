namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Comments that can be updated.
    /// </summary>
    public interface IUpdatableComment
    {
        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; set; }
    }
}

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIUpdatableComment
    {
        

        public IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; set; }
    }
}