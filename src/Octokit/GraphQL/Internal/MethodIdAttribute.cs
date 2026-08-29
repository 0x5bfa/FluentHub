using System;

namespace Octokit.GraphQL.Internal
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class MethodIdAttribute : Attribute
    {
        public MethodIdAttribute(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
