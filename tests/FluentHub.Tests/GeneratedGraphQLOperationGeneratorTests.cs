// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octokit.Generators;
using Octokit.GraphQL;

namespace FluentHub.Tests;

[TestClass]
public sealed class GeneratedGraphQLOperationGeneratorTests
{
	[TestMethod]
	public void ParserAcceptsNamedOperationWithVariablesAndFragment()
	{
		var document = GraphQLDocumentParser.Parse("""
			query Repository($owner: String!, $name: String!) {
			  repository(owner: $owner, name: $name) { ...RepositoryFields }
			}
			fragment RepositoryFields on Repository { id name }
			""");

		Assert.IsTrue(document.IsValid, document.Error);
		Assert.AreEqual("Repository", document.OperationName);
		Assert.AreEqual("Query", document.OperationType);
	}

	[TestMethod]
	public void ParserRejectsUndeclaredVariable()
	{
		var document = GraphQLDocumentParser.Parse("query Viewer { user(login: $login) { id } }");

		Assert.IsFalse(document.IsValid);
		StringAssert.Contains(document.Error, "variable '$login' is used but not declared");
	}

	[TestMethod]
	public void ParserRejectsMissingFragment()
	{
		var document = GraphQLDocumentParser.Parse("query Viewer { viewer { ...UserFields } }");

		Assert.IsFalse(document.IsValid);
		StringAssert.Contains(document.Error, "fragment 'UserFields' is referenced but not defined");
	}

	[TestMethod]
	public void ParserRejectsUnclosedFieldArguments()
	{
		var document = GraphQLDocumentParser.Parse("query Viewer { user(login: \"octocat\" { id } }");

		Assert.IsFalse(document.IsValid);
		StringAssert.Contains(document.Error, "is closed by '}'");
	}

	[TestMethod]
	public void GeneratorCreatesTypedOperationProperty()
	{
		const string source = """
			using Octokit.GraphQL;
			namespace GeneratorTest;

			public sealed class Response;

			public partial class Operations
			{
			    [GeneratedGraphQLOperation<Response>]
			    private const string Repository = "query Repository { viewer { login } }";

			    public static GraphQLOperation<Response> GetOperation()
			    {
			        return RepositoryOperation;
			    }
			}
			""";

		var compilation = CreateCompilation(source);
		GeneratorDriver driver = CSharpGeneratorDriver.Create(new GeneratedGraphQLOperationGenerator());
		driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);

		Assert.HasCount(0, diagnostics);
		var errors = outputCompilation.GetDiagnostics().Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error).ToList();
		Assert.HasCount(0, errors, string.Join(Environment.NewLine, errors));

		var generatedSource = driver.GetRunResult().GeneratedTrees.Single().GetText().ToString();
		StringAssert.Contains(generatedSource, "RepositoryOperation");
		StringAssert.Contains(generatedSource, "GraphQLOperationType.Query");
	}

	[TestMethod]
	public void GeneratorReportsInvalidDocument()
	{
		const string source = """
			using Octokit.GraphQL;
			namespace GeneratorTest;

			public sealed class Response;

			public partial class Operations
			{
			    [GeneratedGraphQLOperation<Response>]
			    private const string Repository = "query Repository { user(login: $login) { id } }";
			}
			""";

		var compilation = CreateCompilation(source);
		GeneratorDriver driver = CSharpGeneratorDriver.Create(new GeneratedGraphQLOperationGenerator());
		driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out _);

		var diagnostic = driver.GetRunResult().Diagnostics.Single(item => item.Id == "OGQL002");
		StringAssert.Contains(diagnostic.GetMessage(), "variable '$login' is used but not declared");
	}

	private static CSharpCompilation CreateCompilation(string source)
	{
		var trustedPlatformAssemblies = (string?)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")
			?? throw new InvalidOperationException("The trusted platform assembly list is unavailable.");
		var paths = trustedPlatformAssemblies.Split(Path.PathSeparator).ToHashSet(StringComparer.OrdinalIgnoreCase);
		paths.Add(typeof(GraphQLOperation<>).Assembly.Location);
		var references = paths.Select(path => MetadataReference.CreateFromFile(path));

		return CSharpCompilation.Create(
			"GeneratorTestAssembly",
			[CSharpSyntaxTree.ParseText(source, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Latest))],
			references,
			new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
	}
}
