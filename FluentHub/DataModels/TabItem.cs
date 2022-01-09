using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.DataModels
{
    public class TabItem
    {
        private string _header;
        public string Header
        {
            get => _header;
            set => _header = value;
        }

        private FontIconSource _iconSource;
        public FontIconSource IconSource
        {
            get => _iconSource;
            set => _iconSource = value;
        }

        private string _originalPageUrl;
        public string OriginalPageUrl
        {
            get => _originalPageUrl;
            set => _originalPageUrl = value;
        }
    }
}
