# Application architecture

FluentHub uses two production projects. The project boundary is the dependency boundary; folders and namespaces provide the layers within each project.

## Project responsibilities

- `FluentHub` contains WinUI presentation, shell composition, feature views and view models, and Windows-specific services.
- `FluentHub.Core` contains UI-independent application contracts, typed navigation state, business services, GitHub infrastructure, caching, and reusable utilities.

`FluentHub.Core` must not reference WinUI or Windows App SDK assemblies. Presentation code may depend on Core, never the reverse.

## Folder layout

```text
src/FluentHub
  Views/<Area>                    UserControl-based screens, windows, and dialogs
  ViewModels/<Area>               Presentation state, commands, and screen lifecycle
  Extensions                     Extension methods and dependency-injection registration
  Utils                          Small reusable utilities and logging
  Controls                       Reusable UI controls and control resource dictionaries
  Helpers                        Platform and application-lifecycle helpers
  Data                           Presentation models, navigation/tab state, and serialization
  Converters                     XAML value converters
  Services                       Navigation and Windows-specific application services

src/FluentHub.Core
  Application/Abstractions       UI-independent ports
  Application/Common             Results, errors, paging, and common helpers
  Application/Features           Business services grouped by feature
  Application/Models             Application-facing models
  Application/Navigation         Typed routes and navigation journal
  DependencyInjection            Core service registrations
  Infrastructure/Caching         File-backed cache implementation
  Infrastructure/GitHub          GitHub clients, transport models, queries, and mutations
```

The folders listed for `FluentHub` are the only logical code folders in the presentation project. Packaging and tooling directories such as `Assets`, `Strings`, and `Properties` remain separate because WinUI and MSIX consume their paths directly.

## Navigation and screen lifetime

Navigation uses immutable `AppRoute` records. Routes contain stable identifiers such as owner, repository, number, SHA, tag, ref, and path; they do not contain WinUI types or GitHub response objects.

Each tab owns a `NavigationJournal<AppRoute>`. Back and forward navigation therefore restore semantic destinations rather than retained visual elements. `NavigationService` serializes navigation per tab, cancels superseded work, and restores the previous journal snapshot if screen activation fails.

`ScreenFactory` is the explicit route-to-screen registry. A screen is a `UserControl` hosted by a `ContentPresenter`; the application does not use `Frame` or `Page` navigation. Every screen receives an async lifecycle through `IScreen` and is created in its own dependency-injection scope. Replacing or closing a screen cancels its work, deactivates it, and disposes that scope.

To add a screen:

1. Add or extend an `AppRoute` in Core using identifiers only.
2. Create a `ScreenView` or `NavigableView` under `Views/<Area>`.
3. Implement the route-aware view-model lifecycle through `IScreenViewModel<AppRoute>`.
4. Register the view model in the FluentHub composition root.
5. Add the route mapping to `ScreenFactory`.

## Cross-cutting boundaries

- `IUserSession` is the application-facing authentication/session contract; GitHub session mechanics stay in Infrastructure.
- `ICacheService`, cache keys, serializers, and policies are application contracts; disk persistence and GitHub cache serialization stay in Infrastructure.
- `Result<T>` and `AppError` provide an explicit error vocabulary for application operations.
- Navigation activation errors leave the previous screen available and are surfaced through per-tab shell chrome.

Architecture tests enforce the two-project production boundary, Core's independence from WinUI, identifier-only routes, and the absence of `Frame`/`Page` navigation primitives.
