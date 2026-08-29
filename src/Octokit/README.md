# Octokit libraries

This directory contains source-owned GitHub API class libraries used by FluentHub:

- `Transport` provides authenticated HTTP, source-generated JSON operations, GitHub errors, and rate-limit metadata.
- `Rest` is a small Native AOT-compatible client organized by GitHub feature area.
- `GraphQL` currently contains the former core client and generated model in one class library.

`Rest` is a from-scratch implementation for FluentHub's active endpoints. It does not retain the legacy Octokit.NET connection stack, reflection serializer, package graph, or public model surface. Every network operation accepts and forwards a `CancellationToken`, and every JSON request and response uses a generated `JsonSerializerContext`.

The REST client currently covers authenticated users and organizations, received events, notifications, searches, repository branches and tags, README content, commit and pull-request file changes, issue types, repository forks, and repository updates. Add future endpoints as focused feature-client methods with the smallest DTO required by the consumer.

The GraphQL source originated from [`octokit/octokit.graphql.net`](https://github.com/octokit/octokit.graphql.net) commit `0029ddeab2c020bb4efdb49e783bbb3cab08fc38`. Its upstream MIT license is retained. Upstream automation, packaging, tests, samples, documentation, IDE settings, signing keys, and generation tools are intentionally excluded.

## Native AOT requirements

For `Rest` and `Transport` changes:

1. Keep nullable analysis enabled and model optional GitHub fields explicitly.
2. Register each JSON request and response in the REST serialization context.
3. Do not use runtime reflection, dynamic type construction, expression compilation, or reflection-based JSON overloads.
4. Forward cancellation to the underlying `HttpClient` call.
5. Keep transport concerns such as authentication, errors, and rate limits out of feature clients.
6. Build in Release with the trim and AOT analyzers enabled before merging.

These projects are pure class libraries and are not packed as NuGet packages. Tests belong in FluentHub's test project and exercise both the transport boundary and application-facing behavior.
