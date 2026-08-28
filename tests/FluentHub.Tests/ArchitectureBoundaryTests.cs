using System.Text.RegularExpressions;
using FluentHub.Core.Application.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentHub.Tests;

[TestClass]
public sealed partial class ArchitectureBoundaryTests
{
	[TestMethod]
	public void CoreDoesNotReferenceWindowsUiAssemblies()
	{
		var references = typeof(AppRoute).Assembly.GetReferencedAssemblies()
			.Select(reference => reference.Name)
			.Where(name => name is not null &&
				(name.StartsWith("Microsoft.UI", StringComparison.Ordinal)
				|| name.Contains("WindowsAppSDK", StringComparison.Ordinal)))
			.ToList();

		Assert.AreEqual(0, references.Count,
			$"FluentHub.Core references UI assemblies: {string.Join(", ", references)}");
	}

	[TestMethod]
	public void RoutesDoNotCarryInfrastructureModels()
	{
		var invalidProperties = typeof(AppRoute).Assembly.GetExportedTypes()
			.Where(type => type != typeof(AppRoute) && type.IsAssignableTo(typeof(AppRoute)))
			.SelectMany(type => type.GetProperties())
			.Where(property => GetReferencedTypes(property.PropertyType).Any(type =>
				type.Namespace?.StartsWith("FluentHub.Core.Infrastructure", StringComparison.Ordinal) == true))
			.Select(property => $"{property.DeclaringType!.Name}.{property.Name}")
			.ToList();

		Assert.AreEqual(0, invalidProperties.Count,
			$"Routes expose infrastructure models: {string.Join(", ", invalidProperties)}");
	}

	[TestMethod]
	public void PresentationUsesContentPresenterInsteadOfFrameOrPageControls()
	{
		var root = FindRepositoryRoot();
		var presentationRoot = Path.Combine(root, "src", "FluentHub");
		var sourceFiles = Directory.EnumerateFiles(presentationRoot, "*.*", SearchOption.AllDirectories)
			.Where(path => Path.GetExtension(path) is ".cs" or ".xaml")
			.Where(path => !HasPathSegment(path, "obj") && !HasPathSegment(path, "bin"));

		var violations = sourceFiles
			.SelectMany(path => ForbiddenNavigationPrimitiveRegex().Matches(File.ReadAllText(path))
				.Select(match => $"{Path.GetRelativePath(root, path)}: {match.Value}"))
			.ToList();

		Assert.AreEqual(0, violations.Count,
			$"Presentation navigation primitives remain:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
	}

	[TestMethod]
	public void SourceContainsOnlyTwoProductionProjects()
	{
		var root = FindRepositoryRoot();
		var projects = Directory.EnumerateFiles(Path.Combine(root, "src"), "*.csproj", SearchOption.AllDirectories)
			.Select(path => Path.GetRelativePath(root, path).Replace('\\', '/'))
			.OrderBy(path => path, StringComparer.Ordinal)
			.ToList();

		CollectionAssert.AreEqual(
			new[] { "src/FluentHub.Core/FluentHub.Core.csproj", "src/FluentHub/FluentHub.csproj" },
			projects);
	}

	private static string FindRepositoryRoot()
	{
		for (var directory = new DirectoryInfo(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
		{
			if (File.Exists(Path.Combine(directory.FullName, "FluentHub.slnx")))
				return directory.FullName;
		}

		throw new DirectoryNotFoundException("Could not locate the FluentHub repository root.");
	}

	private static bool HasPathSegment(string path, string segment)
		=> path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
			.Contains(segment, StringComparer.OrdinalIgnoreCase);

	private static IEnumerable<Type> GetReferencedTypes(Type type)
	{
		if (type.HasElementType)
		{
			foreach (var referencedType in GetReferencedTypes(type.GetElementType()!))
				yield return referencedType;
		}

		if (type.IsGenericType)
		{
			foreach (var argument in type.GetGenericArguments())
			{
				foreach (var referencedType in GetReferencedTypes(argument))
					yield return referencedType;
			}
		}

		yield return type;
	}

	[GeneratedRegex(@"<\s*(?:Frame|Page)\b|:\s*Page\b|\bMicrosoft\.UI\.Xaml\.Navigation\b|\bFrameNavigationParameter\b", RegexOptions.CultureInvariant)]
	private static partial Regex ForbiddenNavigationPrimitiveRegex();
}
