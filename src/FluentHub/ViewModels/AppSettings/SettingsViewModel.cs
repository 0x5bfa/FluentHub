using FluentHub.Core.Application;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using System.Runtime.CompilerServices;
using Windows.Globalization;
using Windows.Storage;

namespace FluentHub.ViewModels.AppSettings
{
	public class SettingsViewModel : ObservableObject, IAccountStore
	{
		public SettingsViewModel()
		{
			var supportedLang = ApplicationLanguages.ManifestLanguages;
			DefaultLanguages = new ObservableCollection<DefaultLanguageModel> { new DefaultLanguageModel(null) };

			foreach (var lang in supportedLang)
			{
				DefaultLanguages.Add(new DefaultLanguageModel(lang));
			}
		}

		private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

		#region Sign in
		public bool SetupCompleted
		{
			get => Get(false);
			set => Set(value);
		}

		public bool SetupProgress
		{
			get => Get(false);
			set => Set(value);
		}

		public string SignedInUserName
		{
			get => Get("");
			set => Set(value);
		}

		public string SignedInUserLogins // Divided with comma ','
		{
			get => Get("");
			set => Set(value);
		}
		#endregion

		#region App settings

		public string AppTheme
		{
			get => Get("Default");
			set => Set(value);
		}

		public CustomThemeItem SelectedThemeItem
		{
			get
			{
				var defaultTheme = new CustomThemeItem()
				{
					Name = "Default",
					Path = string.Empty,
					AbsolutePath = string.Empty,
				};

				var typeInfo = Data.Serialization.AppJsonSerializerContext.Default.CustomThemeItem;
				var json = Get(System.Text.Json.JsonSerializer.Serialize(defaultTheme, typeInfo));

				return System.Text.Json.JsonSerializer.Deserialize(json, typeInfo) ?? defaultTheme;
			}
			set => Set(System.Text.Json.JsonSerializer.Serialize(
				value,
				Data.Serialization.AppJsonSerializerContext.Default.CustomThemeItem));
		}

		#endregion

		#region Settings

		public string AppVersion
		{
			get => Get("Unknown version");
			set => Set(value);
		}

		public bool UseDetailsView
		{
			get => Get(true);
			set => Set(value);
		}

		public ObservableCollection<DefaultLanguageModel> DefaultLanguages { get; private set; }

		public DefaultLanguageModel DefaultLanguage
		{
			get => DefaultLanguages.FirstOrDefault(dl => dl.ID == ApplicationLanguages.PrimaryLanguageOverride) ?? DefaultLanguages[0];
			set => ApplicationLanguages.PrimaryLanguageOverride = value.ID;
		}

		public DefaultLanguageModel CurrentLanguage { get; set; }
			= new DefaultLanguageModel(ApplicationLanguages.PrimaryLanguageOverride);

		#endregion

		#region Read and Save

		public TValue Get<TValue>(TValue defaultValue, [CallerMemberName] string? propertyName = null)
		{
			propertyName = propertyName ??
					   throw new ArgumentNullException(nameof(propertyName), "Cannot store property of unnamed.");

			if (localSettings.Values.ContainsKey(propertyName))
			{
				var value = localSettings.Values[propertyName];

				if (value is not TValue tValue)
				{
					// Put the corrected value in settings.
					Set(defaultValue, propertyName);

					return defaultValue;
				}

				return tValue;
			}

			localSettings.Values[propertyName] = defaultValue;

			return defaultValue;
		}

		public bool Set<TValue>(TValue value, [CallerMemberName] string? propertyName = null)
		{
			propertyName = propertyName ??
					   throw new ArgumentNullException(nameof(propertyName), "Cannot store property of unnamed.");

			TValue originalValue = value;

			if (localSettings.Values.ContainsKey(propertyName))
			{
				originalValue = Get(originalValue, propertyName);
				localSettings.Values[propertyName] = value;

				if (!SetProperty(ref originalValue, value, propertyName))
				{
					return false;
				}
			}
			else
			{
				localSettings.Values[propertyName] = value;
			}

			return true;
		}

		#endregion
	}
}
