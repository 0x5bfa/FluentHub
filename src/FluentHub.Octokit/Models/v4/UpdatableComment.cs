namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Comments that can be updated.
    /// </summary>
    public interface IUpdatableComment
    {        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        List<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class UpdatableComment : IUpdatableComment
    {
        public List<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; set; }
    }
}