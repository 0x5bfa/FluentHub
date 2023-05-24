// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.Data.Items
{
    public class NavigationBarItem
    {
        public string? Text{ get; init; }

        public string? Glyph { get; init; }

        public Type? PageToNavigate { get; init; }
    }
}
