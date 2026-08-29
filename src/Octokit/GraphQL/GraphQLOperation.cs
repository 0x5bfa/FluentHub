// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

namespace Octokit.GraphQL;

public enum GraphQLOperationType
{
	Query,
	Mutation,
	Subscription,
}

/// <summary>
/// Describes a GraphQL document validated and bound to its response type at build time.
/// </summary>
public readonly struct GraphQLOperation<TData>
{
	private readonly string? _document;
	private readonly string? _name;
	private readonly GraphQLOperationType _type;

	public GraphQLOperation(string document, string name, GraphQLOperationType type)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(document);
		ArgumentException.ThrowIfNullOrWhiteSpace(name);
		if (!Enum.IsDefined(type))
			throw new ArgumentOutOfRangeException(nameof(type));

		_document = document;
		_name = name;
		_type = type;
	}

	public string Document
	{
		get
		{
			return _document ?? throw new InvalidOperationException("The GraphQL operation is not initialized.");
		}
	}

	public string Name
	{
		get
		{
			return _name ?? throw new InvalidOperationException("The GraphQL operation is not initialized.");
		}
	}

	public GraphQLOperationType Type
	{
		get
		{
			_ = Document;
			return _type;
		}
	}

	public override string ToString()
	{
		return Name;
	}
}
