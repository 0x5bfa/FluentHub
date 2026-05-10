# AGENTS.md

## Project Structure
- `src/FluentHub.App`: WinUI 3 application layer (views, view models, app services, app lifecycle).
- `src/FluentHub.Octokit`: GitHub API access layer (Octokit clients, queries, mutations, authorization).
- `src/FluentHub.Core`: shared constants, enums, and reusable core utilities.
- `src/FluentHub.Octokit.Generator` and `src/FluentHub.Octicons.Generator`: code generation utilities; treat generated output carefully.
- `docs`: contributor-facing setup and feature documentation.

## Coding Convensions
- Use C# with nullable reference types respected; avoid `null`-unsafe code paths.
- Follow existing naming and style in each project; keep changes minimal and focused.
- Prefer async/await end-to-end for I/O and network flows; avoid blocking calls on UI paths.
- Do not commit secrets, tokens, or machine-local credentials (for example `AppCredentials.config` values).
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
