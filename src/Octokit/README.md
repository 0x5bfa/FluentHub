# Octokit libraries

This directory contains three source-owned class libraries used by FluentHub:

- `Rest` builds the `Octokit` compatibility assembly.
- `GraphQL` builds one `Octokit.GraphQL` assembly containing the former core client and generated models.
- `Transport` is FluentHub's reflection-free HTTP and serialization foundation.

The REST source originated from [`octokit/octokit.net`](https://github.com/octokit/octokit.net) commit `7fa5b0fe4a18c9b981b21290c3ca9320b2d6415b`. The GraphQL source originated from [`octokit/octokit.graphql.net`](https://github.com/octokit/octokit.graphql.net) commit `0029ddeab2c020bb4efdb49e783bbb3cab08fc38`.

Only production source and the upstream licenses are retained. Upstream repository automation, packaging, tests, samples, documentation, IDE settings, signing keys, and generation tools are intentionally excluded. FluentHub owns the project structure and future changes from this point forward.

## Native AOT strategy

The legacy REST client performs reflection-based object materialization, while the legacy GraphQL client builds generic methods and expression trees at runtime. Both patterns produce trimming and dynamic-code warnings and cannot be treated as Native AOT compatible without redesigning their public APIs.

The compatibility projects remain nullable-oblivious until their APIs are replaced or deliberately annotated. New transport DTOs must enable nullable analysis and model GitHub's response contract explicitly; application code should not add null-forgiving operators merely to accommodate inferred annotations from legacy source.

`Octokit.Transport` is the replacement foundation. It requires source-generated `JsonTypeInfo<T>` metadata for every typed response, avoids runtime type construction, enables `IsAotCompatible`, and verifies that its references are AOT compatible. FluentHub's authenticated `GET /user` flow already uses this path.

Migration should proceed endpoint by endpoint:

1. Define the smallest response DTO needed by the feature.
2. Add the DTO to a `JsonSerializerContext`.
3. Use `GitHubHttpClient.GetAsync` or `ExecuteGraphQLAsync` with the generated `JsonTypeInfo<T>`.
4. Remove the corresponding legacy Octokit call and its model dependency.
5. Remove the legacy clients after all call sites have migrated.

Do not suppress trimming or dynamic-code warnings to label the legacy projects AOT safe. The compatibility claim belongs only to code paths that pass the analyzers without suppressions.

The compatibility libraries are not intended to be packed as NuGet packages. Tests belong in FluentHub's test projects and should exercise the application-facing API surface that remains during migration.
