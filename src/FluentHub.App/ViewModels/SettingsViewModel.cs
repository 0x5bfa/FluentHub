using FluentHub.App.Models;
using System.Runtime.CompilerServices;
using Windows.Globalization;
using Windows.Storage;

namespace FluentHub.App.ViewModels
{
	public class SettingsViewModel : ObservableObject
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
		public string AccessToken
		{
			get => Get("");
			set => Set(value);
		}

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
			get => Newtonsoft.Json.JsonConvert.DeserializeObject<CustomThemeItem>(
				Get(System.Text.Json.JsonSerializer.Serialize(
					new CustomThemeItem()
					{
						Name = "Default"
					}))
				);
			set => Set(Newtonsoft.Json.JsonConvert.SerializeObject(value));
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
			get => DefaultLanguages.FirstOrDefault(dl => dl.ID == ApplicationLanguages.PrimaryLanguageOverride) ?? DefaultLanguages.FirstOrDefault();
			set => ApplicationLanguages.PrimaryLanguageOverride = value.ID;
		}

		public DefaultLanguageModel CurrentLanguage { get; set; }
			= new DefaultLanguageModel(ApplicationLanguages.PrimaryLanguageOverride);

		#endregion

		#region Read and Save

		public TValue Get<TValue>(TValue defaultValue, [CallerMemberName] string propertyName = null)
		{
			propertyName = propertyName ??
					   throw new ArgumentNullException(nameof(propertyName), "Cannot store property of unnamed.");

			if (localSettings.Values.ContainsKey(propertyName))
			{
				var value = localSettings.Values[propertyName];

				if (value is not TValue tValue)
				{
					// Put the corrected value in settings.
					TValue originalValue = default;
					Set(originalValue, propertyName);

					return originalValue;
				}

				return (TValue)value;
			}

			localSettings.Values[propertyName] = defaultValue;

			return defaultValue;
		}

		public bool Set<TValue>(TValue value, [CallerMemberName] string propertyName = null)
		{
			TValue originalValue = default;

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
