using System;

namespace Octokit.GraphQL.Core
{
    public class GraphQLException : Exception
    {
        public GraphQLException()
        {
        }

        public GraphQLException(string message) : base(message)
        {
        }
    }
}