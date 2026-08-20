# Microsoft Store listings

`Listings` contains the localized, source-controlled fields that are merged into
the current draft through the Store Submission API. Dynamic submission fields,
package state, and existing listing images remain owned by Partner Center.

`en-US.json` is the Crowdin source file. Crowdin translations can be enabled for
Store publishing by adding their locale to `locales.json`.

Before enabling a new locale:

1. Add the Store listing language in Partner Center.
2. Upload at least one screenshot for that language.
3. Ensure Crowdin has generated `Listings/<locale>.json`.
4. Add the exact locale to `locales.json`.
5. Run the Store workflow with `submit` disabled and review the resulting draft.

The Partner Center draft must contain exactly the locales listed in
`locales.json`. The workflow fails when a configured locale is missing or an
unmanaged locale remains enabled. This prevents an incomplete listing without
source-controlled text and screenshots from blocking certification.

After uploading a package, `PendingUpload` is the expected staged state for a
`--noCommit` draft. Partner Center populates validated version and language
values while processing a committed submission. The workflow waits with backoff
for package readiness and then verifies listing persistence with backoff for up
to five minutes.

With `submit` disabled, verification is against the Store Submission API and the
submission remains `PendingCommit`. The Partner Center editor can continue to
show the previously committed package and listing values in this state. Enable
`submit` only when the draft is ready to be committed to certification; the
updated values become Store-visible after Partner Center finishes processing
and certification.

If a run stops after uploading a package, rerun the workflow with
`replace_existing_draft` disabled. When the existing draft contains the same
package filename, the workflow resumes that draft instead of replacing it. Set
`artifact_run_id` to a prior Store workflow run whose build succeeded to reuse
that run's package artifact and skip another package build while diagnosing or
resuming a deployment.
