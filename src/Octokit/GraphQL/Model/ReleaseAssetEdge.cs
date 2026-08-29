namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class ReleaseAssetEdge : QueryableValue<ReleaseAssetEdge>
    {
        internal ReleaseAssetEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ReleaseAsset Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.ReleaseAsset.Create);

        internal static ReleaseAssetEdge Create(Expression expression)
        {
            return new ReleaseAssetEdge(expression);
        }
    }
}