// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Data.Enums;

namespace FluentHub.App.Data.Items
{
    public class NavigationBarItem
    {
        public string? Text{ get; init; }

        public string? Glyph { get; init; }

        public Type? PageToNavigate { get; init; }

        public NavigationBarItemKey PageItemKey { get; init; }
    }
}
