using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Controls
{
	public sealed partial class ReadmeContentBlock : UserControl
	{
		public static readonly DependencyProperty BaseUrlProperty =
			DependencyProperty.Register(
				nameof(BaseUrl),
				typeof(string),
				typeof(ReadmeContentBlock),
				new PropertyMetadata(null));

		public static readonly DependencyProperty MarkdownProperty =
			DependencyProperty.Register(
				nameof(Markdown),
				typeof(string),
				typeof(ReadmeContentBlock),
				new PropertyMetadata(string.Empty));

		public static readonly DependencyProperty EditUriProperty =
			DependencyProperty.Register(
				nameof(EditUri),
				typeof(Uri),
				typeof(ReadmeContentBlock),
				new PropertyMetadata(null));

		public string? BaseUrl
		{
			get => (string?)GetValue(BaseUrlProperty);
			set => SetValue(BaseUrlProperty, value);
		}

		public string Markdown
		{
			get => (string)GetValue(MarkdownProperty);
			set => SetValue(MarkdownProperty, value);
		}

		public Uri? EditUri
		{
			get => (Uri?)GetValue(EditUriProperty);
			set => SetValue(EditUriProperty, value);
		}

		public ReadmeContentBlock()
			=> InitializeComponent();

		private async void OnEditButtonClick(object sender, RoutedEventArgs e)
		{
			if (EditUri is not null)
				_ = await Windows.System.Launcher.LaunchUriAsync(EditUri);
		}
	}
}
