// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Octokit.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class GeneratedGraphQLOperationGenerator : IIncrementalGenerator
{
	private const string AttributeMetadataName = "Octokit.GraphQL.GeneratedGraphQLOperationAttribute`1";
	private const string OperationMetadataName = "Octokit.GraphQL.GraphQLOperation`1";

	private static readonly DiagnosticDescriptor InvalidDeclaration = new(
		"OGQL001",
		"Invalid generated GraphQL operation declaration",
		"GraphQL operation field '{0}' must be a const string in a partial class",
		"Octokit.GraphQL",
		DiagnosticSeverity.Error,
		isEnabledByDefault: true);

	private static readonly DiagnosticDescriptor InvalidDocument = new(
		"OGQL002",
		"Invalid GraphQL document",
		"GraphQL operation field '{0}' is invalid: {1}",
		"Octokit.GraphQL",
		DiagnosticSeverity.Error,
		isEnabledByDefault: true);

	private static readonly DiagnosticDescriptor MemberCollision = new(
		"OGQL003",
		"Generated GraphQL operation member already exists",
		"Type '{0}' already contains a member named '{1}'",
		"Octokit.GraphQL",
		DiagnosticSeverity.Error,
		isEnabledByDefault: true);

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var operations = context.SyntaxProvider.ForAttributeWithMetadataName(
			AttributeMetadataName,
			static (node, _) => node is VariableDeclaratorSyntax,
			static (attributeContext, cancellationToken) => CreateCandidate(attributeContext, cancellationToken));

		context.RegisterSourceOutput(operations, static (sourceContext, candidate) =>
		{
			if (candidate.Diagnostic is { } diagnostic)
			{
				sourceContext.ReportDiagnostic(diagnostic);
				return;
			}

			var validation = GraphQLDocumentParser.Parse(candidate.Document!);
			if (!validation.IsValid)
			{
				sourceContext.ReportDiagnostic(Diagnostic.Create(
					InvalidDocument,
					candidate.Location,
					candidate.FieldName,
					validation.Error));
				return;
			}

			var source = Render(candidate, validation);
			sourceContext.AddSource(candidate.HintName, SourceText.From(source, Encoding.UTF8));
		});
	}

	private static OperationCandidate CreateCandidate(
		GeneratorAttributeSyntaxContext context,
		CancellationToken cancellationToken)
	{
		var field = (IFieldSymbol)context.TargetSymbol;
		var location = field.Locations.FirstOrDefault() ?? Location.None;
		var responseType = context.Attributes[0].AttributeClass?.TypeArguments.FirstOrDefault();
		var containingTypes = GetContainingTypes(field.ContainingType, cancellationToken);

		if (!field.IsConst || field.Type.SpecialType != SpecialType.System_String ||
			field.ConstantValue is not string document ||
			responseType is null || responseType.TypeKind == TypeKind.Error ||
			containingTypes.Length == 0 || containingTypes.Any(type => !type.IsPartial))
		{
			return OperationCandidate.FromDiagnostic(
				field.Name,
				location,
				Diagnostic.Create(InvalidDeclaration, location, field.Name));
		}

		var propertyName = field.Name + "Operation";
		if (field.ContainingType.GetMembers(propertyName).Length > 0)
		{
			return OperationCandidate.FromDiagnostic(
				field.Name,
				location,
				Diagnostic.Create(
					MemberCollision,
					location,
					field.ContainingType.ToDisplayString(),
					propertyName));
		}

		var operationDefinition = context.SemanticModel.Compilation.GetTypeByMetadataName(OperationMetadataName);
		if (operationDefinition is null)
		{
			return OperationCandidate.FromDiagnostic(
				field.Name,
				location,
				Diagnostic.Create(InvalidDeclaration, location, field.Name));
		}

		var operationType = operationDefinition.Construct(responseType);
		var qualifiedTypeName = operationType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
		var namespaceName = field.ContainingNamespace.IsGlobalNamespace
			? null
			: field.ContainingNamespace.ToDisplayString();

		return new OperationCandidate(
			field.Name,
			propertyName,
			document,
			qualifiedTypeName,
			GetAccessibility(field.DeclaredAccessibility),
			namespaceName,
			containingTypes,
			location,
			CreateHintName(field),
			diagnostic: null);
	}

	private static ImmutableArray<ContainingType> GetContainingTypes(
		INamedTypeSymbol containingType,
		CancellationToken cancellationToken)
	{
		var stack = new Stack<INamedTypeSymbol>();
		for (var current = containingType; current is not null; current = current.ContainingType)
			stack.Push(current);

		var builder = ImmutableArray.CreateBuilder<ContainingType>();
		while (stack.Count > 0)
		{
			cancellationToken.ThrowIfCancellationRequested();
			var type = stack.Pop();
			var isPartial = type.DeclaringSyntaxReferences.Any(reference =>
				reference.GetSyntax(cancellationToken) is TypeDeclarationSyntax declaration &&
				declaration.Modifiers.Any(SyntaxKind.PartialKeyword));
			builder.Add(new ContainingType(
				type.Name,
				GetAccessibility(type.DeclaredAccessibility),
				GetTypeKeyword(type),
				type.IsStatic,
				type.TypeParameters.Select(parameter => parameter.Name).ToImmutableArray(),
				isPartial));
		}

		return builder.ToImmutable();
	}

	private static string Render(OperationCandidate candidate, GraphQLDocument graphQL)
	{
		var builder = new StringBuilder();
		builder.AppendLine("// <auto-generated/>");
		builder.AppendLine("#nullable enable");
		builder.AppendLine();

		var indent = string.Empty;
		if (candidate.NamespaceName is { Length: > 0 } namespaceName)
		{
			builder.Append("namespace ").Append(namespaceName).AppendLine();
			builder.AppendLine("{");
			indent = "\t";
		}

		foreach (var type in candidate.ContainingTypes)
		{
			builder.Append(indent);
			if (type.Accessibility.Length > 0)
				builder.Append(type.Accessibility).Append(' ');
			if (type.IsStatic)
				builder.Append("static ");
			builder.Append("partial ").Append(type.Keyword).Append(' ').Append(type.Name);
			if (type.TypeParameters.Length > 0)
				builder.Append('<').Append(string.Join(", ", type.TypeParameters)).Append('>');
			builder.AppendLine();
			builder.Append(indent).AppendLine("{");
			indent += "\t";
		}

		builder.Append(indent);
		if (candidate.Accessibility.Length > 0)
			builder.Append(candidate.Accessibility).Append(' ');
		builder.Append("static ").Append(candidate.OperationTypeName).Append(' ')
			.Append(candidate.PropertyName).Append(" { get; } = new(")
			.Append(candidate.FieldName).Append(", ")
			.Append(SymbolDisplay.FormatLiteral(graphQL.OperationName, quote: true)).Append(", ")
			.Append("global::Octokit.GraphQL.GraphQLOperationType.")
			.Append(graphQL.OperationType).AppendLine(");");

		for (var index = candidate.ContainingTypes.Length - 1; index >= 0; index--)
		{
			indent = indent.Substring(0, indent.Length - 1);
			builder.Append(indent).AppendLine("}");
		}

		if (candidate.NamespaceName is { Length: > 0 })
			builder.AppendLine("}");

		return builder.ToString();
	}

	private static string CreateHintName(IFieldSymbol field)
	{
		var name = field.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) + "." + field.Name;
		var builder = new StringBuilder(name.Length + 5);
		foreach (var character in name)
			builder.Append(char.IsLetterOrDigit(character) ? character : '_');
		return builder.Append(".g.cs").ToString();
	}

	private static string GetAccessibility(Accessibility accessibility)
	{
		switch (accessibility)
		{
			case Accessibility.Public:
				return "public";
			case Accessibility.Internal:
				return "internal";
			case Accessibility.Private:
				return "private";
			case Accessibility.Protected:
				return "protected";
			case Accessibility.ProtectedAndInternal:
				return "private protected";
			case Accessibility.ProtectedOrInternal:
				return "protected internal";
			default:
				return string.Empty;
		}
	}

	private static string GetTypeKeyword(INamedTypeSymbol type)
	{
		if (type.TypeKind == TypeKind.Struct)
			return type.IsRecord ? "record struct" : "struct";
		return type.IsRecord ? "record" : "class";
	}

	private sealed class OperationCandidate
	{
		public OperationCandidate(
			string fieldName,
			string propertyName,
			string? document,
			string operationTypeName,
			string accessibility,
			string? namespaceName,
			ImmutableArray<ContainingType> containingTypes,
			Location location,
			string hintName,
			Diagnostic? diagnostic)
		{
			FieldName = fieldName;
			PropertyName = propertyName;
			Document = document;
			OperationTypeName = operationTypeName;
			Accessibility = accessibility;
			NamespaceName = namespaceName;
			ContainingTypes = containingTypes;
			Location = location;
			HintName = hintName;
			Diagnostic = diagnostic;
		}

		public string FieldName { get; }
		public string PropertyName { get; }
		public string? Document { get; }
		public string OperationTypeName { get; }
		public string Accessibility { get; }
		public string? NamespaceName { get; }
		public ImmutableArray<ContainingType> ContainingTypes { get; }
		public Location Location { get; }
		public string HintName { get; }
		public Diagnostic? Diagnostic { get; }

		public static OperationCandidate FromDiagnostic(
			string fieldName,
			Location location,
			Diagnostic diagnostic)
		{
			return new(
				fieldName,
				string.Empty,
				document: null,
				operationTypeName: string.Empty,
				accessibility: string.Empty,
				namespaceName: null,
				containingTypes: ImmutableArray<ContainingType>.Empty,
				location,
				hintName: string.Empty,
				diagnostic);
		}
	}

	private sealed class ContainingType
	{
		public ContainingType(
			string name,
			string accessibility,
			string keyword,
			bool isStatic,
			ImmutableArray<string> typeParameters,
			bool isPartial)
		{
			Name = name;
			Accessibility = accessibility;
			Keyword = keyword;
			IsStatic = isStatic;
			TypeParameters = typeParameters;
			IsPartial = isPartial;
		}

		public string Name { get; }
		public string Accessibility { get; }
		public string Keyword { get; }
		public bool IsStatic { get; }
		public ImmutableArray<string> TypeParameters { get; }
		public bool IsPartial { get; }
	}
}
