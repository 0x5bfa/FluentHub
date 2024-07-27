// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.Models
{
	public class FolderCardItem : ObservableObject
	{
		private BitmapImage _thumbnail;
		public BitmapImage Thumbnail { get => _thumbnail; set => SetProperty(ref _thumbnail, value); }

		private string _text;
		public string Text { get => _text; set => SetProperty(ref _text, value); }

		private string _path;
		public string Path { get => _path; set => SetProperty(ref _path, value); }

		private string _tag;
		public string Tag { get => _tag; set => SetProperty(ref _tag, value); }
	}
}
