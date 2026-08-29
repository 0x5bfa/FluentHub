namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An object that is a member of an enterprise.
    /// </summary>
    public class EnterpriseMember : QueryableValue<EnterpriseMember>, IUnion
    {
        internal EnterpriseMember(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// An account for a user who is an admin of an enterprise or a member of an enterprise through one or more organizations.
            /// </summary>
            public Selector<T> EnterpriseUserAccount(Func<EnterpriseUserAccount, T> selector) => default;

            /// <summary>
            /// A user is an individual's account on GitHub that owns repositories and can make new content.
            /// </summary>
            public Selector<T> User(Func<User, T> selector) => default;
        }

        internal static EnterpriseMember Create(Expression expression)
        {
            return new EnterpriseMember(expression);
        }
    }
}