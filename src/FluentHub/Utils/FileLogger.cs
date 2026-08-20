using System.Globalization;
using System.IO;

namespace FluentHub.Utils
{
	internal sealed class FileLogger : ILogger, IDisposable
	{
		private readonly object _syncRoot = new();
		private readonly StreamWriter _writer;
		private bool _disposed;

		internal FileLogger(string path)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(path);

			var directory = Path.GetDirectoryName(path)
				?? throw new ArgumentException("The log path must include a directory.", nameof(path));

			Directory.CreateDirectory(directory);
			_writer = new StreamWriter(
				new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite),
				new UTF8Encoding(encoderShouldEmitUTF8Identifier: false))
			{
				AutoFlush = true
			};
		}

		public void Debug(string message) => Write("DEBUG", message);
		public void Debug(string message, params object[] args) => Write("DEBUG", message, args: args);

		public void Info(string message) => Write("INFO", message);
		public void Info(string message, params object[] args) => Write("INFO", message, args: args);

		public void Warn(string message) => Write("WARN", message);
		public void Warn(string message, params object[] args) => Write("WARN", message, args: args);

		public void Error(string message) => Write("ERROR", message);
		public void Error(string message, params object[] args) => Write("ERROR", message, args: args);
		public void Error(Exception exception) => Write("ERROR", exception.Message, exception);
		public void Error(string message, Exception exception) => Write("ERROR", message, exception);

		public void Fatal(string message) => Write("FATAL", message);
		public void Fatal(string message, params object[] args) => Write("FATAL", message, args: args);
		public void Fatal(Exception exception) => Write("FATAL", exception.Message, exception);
		public void Fatal(string message, Exception exception) => Write("FATAL", message, exception);

		public void Dispose()
		{
			lock (_syncRoot)
			{
				if (_disposed)
					return;

				_writer.Dispose();
				_disposed = true;
			}
		}

		private void Write(string level, string message, Exception? exception = null, object[]? args = null)
		{
			var formattedMessage = FormatMessage(message, args);

			lock (_syncRoot)
			{
				if (_disposed)
					return;

				try
				{
					_writer.Write(DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff zzz", CultureInfo.InvariantCulture));
					_writer.Write(" [");
					_writer.Write(level);
					_writer.Write("] ");
					_writer.WriteLine(formattedMessage);

					if (exception is not null)
						_writer.WriteLine(exception);
				}
				catch (IOException ioException)
				{
					System.Diagnostics.Debug.WriteLine($"Failed to write application log: {ioException}");
				}
			}
		}

		private static string FormatMessage(string message, object[]? args)
		{
			if (args is null || args.Length == 0)
				return message;

			try
			{
				return string.Format(CultureInfo.CurrentCulture, message, args);
			}
			catch (FormatException)
			{
				return $"{message} [{string.Join(", ", args)}]";
			}
		}
	}
}
