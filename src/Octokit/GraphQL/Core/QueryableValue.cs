using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public class QueryableValue<T> : IQueryableValue<T>
    {
        private Expression expression;

        public QueryableValue(Expression expression)
        {
            this.expression = expression ?? Expression.Constant(this);
        }

        public Expression Expression => expression;
    }
}
