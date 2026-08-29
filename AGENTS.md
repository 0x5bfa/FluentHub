# AGENTS.md

## Project Structure
- `src/FluentHub`: WinUI 3 presentation layer and Windows-specific adapters. Keep production code under `Views`, `ViewModels`, `Extensions`, `Utils`, `Controls`, `Helpers`, `Data`, `Converters`, or `Services`; resource and tooling folders are exempt.
- `src/FluentHub.Core`: UI-independent application layer (GitHub API clients, queries, mutations, authorization, app-facing contracts, business services, and reusable utilities).
- `docs`: contributor-facing setup and feature documentation.

## Coding Convensions
- Use C# with nullable reference types respected; avoid `null`-unsafe code paths.
- Follow existing naming and style in each project; keep changes minimal and focused.
- Save text files as UTF-8 with CRLF line endings; keep `*.csproj` files as UTF-8 with BOM.
- Prefer async/await end-to-end for I/O and network flows; avoid blocking calls on UI paths.
- Do not commit secrets, access tokens, refresh tokens, or machine-local credentials. OAuth client IDs are public identifiers.
- Add or update concise comments only where behavior is non-obvious.

## Build & Validation
- Restore and build solution before finalizing changes:
  - `dotnet restore FluentHub.slnx`
  - `dotnet build FluentHub.slnx -c Debug`
- For auth/sign-in related changes, validate:
  - sign-in flow starts correctly
  - token is stored and reused
  - signed-in user identity resolves successfully
- If UI was changed, verify the corresponding sign-in screens load without XAML/runtime errors.
