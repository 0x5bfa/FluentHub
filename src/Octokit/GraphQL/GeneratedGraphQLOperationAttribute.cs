// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Diagnostics;

namespace Octokit.GraphQL;

/// <summary>
/// Marks a constant GraphQL document for build-time validation and operation generation.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
[Conditional("OCTOKIT_GENERATOR_ATTRIBUTES")]
public sealed class GeneratedGraphQLOperationAttribute<TData> : Attribute
{
}
