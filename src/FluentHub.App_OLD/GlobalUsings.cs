// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

// First party
global using global::System;
global using global::System.Collections.Generic;
global using global::System.Collections.ObjectModel;
global using global::System.Linq;
global using global::System.Text;
global using global::System.Threading;
global using global::System.Threading.Tasks;
global using global::Microsoft.Extensions.DependencyInjection;

// Windows Community Toolkit
global using global::CommunityToolkit.Mvvm.ComponentModel;
global using global::CommunityToolkit.Mvvm.DependencyInjection;
global using global::CommunityToolkit.Mvvm.Input;
global using global::CommunityToolkit.Mvvm.Messaging;

// FluentHub.App
global using global::FluentHub.App.Data.EventArgs;
global using global::FluentHub.App.Data.Factories;
global using global::FluentHub.App.Data.Items;
global using global::FluentHub.App.Data.Parameters;
global using global::FluentHub.App.Helpers;
global using global::FluentHub.App.Services;
global using global::FluentHub.Core.Data.Enums;
global using global::FluentHub.Octokit.Models.v3;
global using global::FluentHub.Octokit.Models.v4;

// Third-party
global using global::Humanizer;
global using global::Octokit.GraphQL;
global using OctokitOriginal = global::Octokit;
