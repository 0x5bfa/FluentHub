namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a required status check for a protected branch, but not any specific run of that check.
    /// </summary>
    public class RequiredStatusCheckDescription : QueryableValue<RequiredStatusCheckDescription>
    {
        internal RequiredStatusCheckDescription(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The App that must provide this status in order for it to be accepted.
        /// </summary>
        public App App => this.CreateProperty(x => x.App, Octokit.GraphQL.Model.App.Create);

        /// <summary>
        /// The name of this status.
        /// </summary>
        public string Context { get; }

        internal static RequiredStatusCheckDescription Create(Expression expression)
        {
            return new RequiredStatusCheckDescription(expression);
        }
    }
}