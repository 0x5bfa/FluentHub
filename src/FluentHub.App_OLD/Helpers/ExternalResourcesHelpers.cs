namespace FluentHub.App.Helpers
{
	// TODO: Not supported helpers
	public class ExternalResourcesHelpers
	{
		/*
		public List<CustomThemeItem> Themes = new List<CustomThemeItem>()
		{
			new CustomThemeItem
			{
				Name = "Default",
			},
		};

		public StorageFolder ThemeFolder { get; set; }
		public StorageFolder ImportedThemesFolder { get; set; }

		public string CurrentThemeResources { get; set; }

		public async Task LoadSelectedTheme()
		{
			StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
			ThemeFolder = await appInstalledFolder.GetFolderAsync("Themes");
			ImportedThemesFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Themes", CreationCollisionOption.OpenIfExists);

			if (App.AppSettings.SelectedThemeItem.Path != null)
			{
				await TryLoadThemeAsync(App.AppSettings.SelectedThemeItem);
			}
		}

		public async Task LoadOtherThemesAsync()
		{
			try
			{
				await AddThemesAsync(ThemeFolder);
				await AddThemesAsync(ImportedThemesFolder);
			}
			catch (Exception)
			{
				Debug.WriteLine($"Error loading themes");
			}
		}

		private async Task AddThemesAsync(StorageFolder folder)
		{
			foreach (var file in (await folder.GetFilesAsync()).Where(x => x.FileType == ".xaml"))
			{
				if (!Themes.Exists(t => t.AbsolutePath == file.Path))
				{
					Themes.Add(new CustomThemeItem()
					{
						Name = file.Name.Replace(".xaml", "", StringComparison.Ordinal),
						Path = file.Name,
						AbsolutePath = file.Path,
					});
				}
			}
		}

		public async Task<bool> TryLoadThemeAsync(CustomThemeItem theme)
		{
			try
			{
				var xaml = await TryLoadResourceDictionary(theme);
				if (xaml != null)
				{
					App.Current.Resources.MergedDictionaries.Add(xaml);
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<ResourceDictionary> TryLoadResourceDictionary(CustomThemeItem theme)
		{
			StorageFile file;
			if (theme?.Path == null)
			{
				return null;
			}

			if (theme.AbsolutePath.Contains(ImportedThemesFolder.Path))
			{
				file = await ImportedThemesFolder.GetFileAsync(theme.Path);
				theme.IsImportedTheme = true;
			}
			else
			{
				file = await ThemeFolder.GetFileAsync(theme.Path);
				theme.IsImportedTheme = false;
			}

			var code = await FileIO.ReadTextAsync(file);
			var xaml = XamlReader.Load(code) as ResourceDictionary;
			xaml.Add("CustomThemeID", theme.Key);
			return xaml;
		}

		public async Task UpdateTheme(CustomThemeItem OldTheme, CustomThemeItem NewTheme)
		{
			if (OldTheme.Path != null)
			{
				RemoveTheme(OldTheme);
			}

			if (NewTheme.Path != null)
			{
				await TryLoadThemeAsync(NewTheme);
			}
		}

		public bool RemoveTheme(CustomThemeItem theme)
		{
			try
			{
				App.Current.Resources.MergedDictionaries.Remove(App.Current.Resources.MergedDictionaries.FirstOrDefault(x => x.TryGetValue("CustomThemeID", out var key) && (key as string) == theme.Key));
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	*/
	}
}
