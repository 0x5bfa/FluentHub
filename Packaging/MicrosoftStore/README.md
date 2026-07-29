# Microsoft Store listings

`Listings` contains the localized, source-controlled fields that are merged into
the current Partner Center draft by the Store deployment workflow. Dynamic
submission fields, package state, and existing listing images remain owned by
Partner Center.

`en-US.json` is the Crowdin source file. Crowdin translations can be enabled for
Store publishing by adding their locale to `locales.json`.

Before enabling a new locale:

1. Add the Store listing language in Partner Center.
2. Upload at least one screenshot for that language.
3. Ensure Crowdin has generated `Listings/<locale>.json`.
4. Add the exact locale to `locales.json`.
5. Run the Store workflow with `submit` disabled and review the resulting draft.

The workflow intentionally fails when a configured locale is missing from the
Partner Center draft. This prevents an incomplete listing without screenshots
from being submitted.
