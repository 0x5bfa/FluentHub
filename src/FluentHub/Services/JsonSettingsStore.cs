// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentHub.Services;

public sealed class JsonSettingsStore
{
	private readonly SemaphoreSlim _gate = new(1, 1);

	public JsonSettingsStore(string? filePath = null)
	{
		FilePath = filePath ?? GetDefaultFilePath();
	}

	public string FilePath { get; }

	public string? SignedInUserName { get; private set; }

	public string? AccessToken { get; private set; }

	public bool HasSession
		=> !string.IsNullOrWhiteSpace(SignedInUserName) &&
		   !string.IsNullOrWhiteSpace(AccessToken);

	public async Task LoadAsync(CancellationToken cancellationToken = default)
	{
		await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			if (!File.Exists(FilePath))
			{
				ClearInMemory();
				return;
			}

			await using var stream = new FileStream(
				FilePath,
				FileMode.Open,
				FileAccess.Read,
				FileShare.Read,
				bufferSize: 4096,
				useAsync: true);

			var state = await JsonSerializer.DeserializeAsync(
				stream,
				JsonSettingsJsonContext.Default.SettingsState,
				cancellationToken).ConfigureAwait(false);

			SignedInUserName = state?.SignedInUserName;
			AccessToken = state?.AccessToken;
		}
		catch (FileNotFoundException)
		{
			ClearInMemory();
		}
		catch (DirectoryNotFoundException)
		{
			ClearInMemory();
		}
		catch (JsonException)
		{
			ClearInMemory();
		}
		finally
		{
			_gate.Release();
		}
	}

	public async Task SaveSessionAsync(
		string login,
		string accessToken,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(login);
		ArgumentException.ThrowIfNullOrWhiteSpace(accessToken);

		await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			SignedInUserName = login.Trim();
			AccessToken = accessToken;
			await SaveUnsafeAsync(cancellationToken).ConfigureAwait(false);
		}
		finally
		{
			_gate.Release();
		}
	}

	public async Task ClearAsync(CancellationToken cancellationToken = default)
	{
		await _gate.WaitAsync(cancellationToken).ConfigureAwait(false);
		try
		{
			ClearInMemory();
			if (File.Exists(FilePath))
			{
				File.Delete(FilePath);
			}
		}
		finally
		{
			_gate.Release();
		}
	}

	public void ClearInMemory()
	{
		SignedInUserName = null;
		AccessToken = null;
	}

	private async Task SaveUnsafeAsync(CancellationToken cancellationToken)
	{
		var directory = Path.GetDirectoryName(FilePath);
		if (!string.IsNullOrWhiteSpace(directory))
		{
			Directory.CreateDirectory(directory);
		}

		var temporaryPath = FilePath + ".tmp";
		try
		{
			await using (var stream = new FileStream(
				temporaryPath,
				FileMode.Create,
				FileAccess.Write,
				FileShare.None,
				bufferSize: 4096,
				useAsync: true))
			{
				var state = new SettingsState
				{
					SignedInUserName = SignedInUserName,
					AccessToken = AccessToken,
				};

				await JsonSerializer.SerializeAsync(
					stream,
					state,
					JsonSettingsJsonContext.Default.SettingsState,
					cancellationToken).ConfigureAwait(false);
				await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
			}

			File.Move(temporaryPath, FilePath, overwrite: true);
		}
		finally
		{
			try
			{
				if (File.Exists(temporaryPath))
				{
					File.Delete(temporaryPath);
				}
			}
			catch (IOException)
			{
				// The settings write already succeeded or will be retried later.
			}
		}
	}

	private static string GetDefaultFilePath()
	{
		var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		return Path.Combine(localAppData, "FluentHub", "settings.json");
	}
}

public sealed class SettingsState
{
	public string? SignedInUserName { get; set; }

	public string? AccessToken { get; set; }
}

[JsonSourceGenerationOptions(
	PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
	WriteIndented = true)]
[JsonSerializable(typeof(SettingsState))]
internal partial class JsonSettingsJsonContext : JsonSerializerContext
{
}
