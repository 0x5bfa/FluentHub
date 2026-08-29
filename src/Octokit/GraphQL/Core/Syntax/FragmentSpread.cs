namespace Octokit.GraphQL.Core.Syntax
{
    public class FragmentSpread : ISyntaxNode
    {
        public string Name { get; }

        public FragmentSpread(string name)
        {
            Name = name;
        }
    }
}