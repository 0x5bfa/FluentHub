using System;
using System.Linq;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public interface IQueryableValue
    {
        Expression Expression { get; }
    }

    public interface IQueryableValue<out T> : IQueryableValue
    {
    }
}
