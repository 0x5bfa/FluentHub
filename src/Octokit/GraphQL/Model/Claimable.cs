namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An object which can have its data claimed or claim data from another.
    /// </summary>
    public class Claimable : QueryableValue<Claimable>, IUnion
    {
        internal Claimable(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A placeholder user for attribution of imported data on GitHub.
            /// </summary>
            public Selector<T> Mannequin(Func<Mannequin, T> selector) => default;

            /// <summary>
            /// A user is an individual's account on GitHub that owns repositories and can make new content.
            /// </summary>
            public Selector<T> User(Func<User, T> selector) => default;
        }

        internal static Claimable Create(Expression expression)
        {
            return new Claimable(expression);
        }
    }
}