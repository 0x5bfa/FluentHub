using System;
using System.Collections.Generic;
using System.Reflection;

namespace Octokit.GraphQL.Core.Syntax
{
    public interface ISelectionSet : ISyntaxNode
    {
        IList<ISyntaxNode> Selections { get; }
    }
}
