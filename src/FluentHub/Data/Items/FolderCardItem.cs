// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Models
{
	public class FolderCardItem : ObservableObject
	{
		private BitmapImage _thumbnail = default!;
		public BitmapImage Thumbnail { get => _thumbnail; set => SetProperty(ref _thumbnail, value); }

		private string _text = default!;
		public string Text { get => _text; set => SetProperty(ref _text, value); }

		private string _path = default!;
		public string Path { get => _path; set => SetProperty(ref _path, value); }

		private string _tag = default!;
		public string Tag { get => _tag; set => SetProperty(ref _tag, value); }
	}
}
