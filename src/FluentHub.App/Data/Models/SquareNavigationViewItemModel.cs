using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.Models
{
	public class SquareNavigationViewItemModel : ObservableObject
	{
		public SquareNavigationViewItemModel() { }

		public SquareNavigationViewItemModel(string name, string glyphPrimary, string glyphSecondary, bool isSelected = false, BitmapImage image = null)
		{
			Name = name;
			GlyphPrimary = glyphPrimary;
			GlyphSecondary = glyphSecondary;
			IsSelected = isSelected;
			Thumbnail = image;
		}

		private string _name;
		public string Name { get => _name; set => SetProperty(ref _name, value); }

		private string _glyphPrimary;
		public string GlyphPrimary { get => _glyphPrimary; set => SetProperty(ref _glyphPrimary, value); }

		private string _glyphSecondary;
		public string GlyphSecondary { get => _glyphSecondary; set => SetProperty(ref _glyphSecondary, value); }

		private bool _useOcticon;
		public bool UseOcticon { get => _useOcticon; set => SetProperty(ref _useOcticon, value); }

		private bool _isSelected;
		public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }

		private BitmapImage _thumbnail;
		public BitmapImage Thumbnail { get => _thumbnail; set => SetProperty(ref _thumbnail, value); }
	}
}
