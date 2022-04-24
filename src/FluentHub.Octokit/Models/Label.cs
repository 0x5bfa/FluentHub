using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace FluentHub.Octokit.Models
{
    public class Label
    {
        public string Color { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public SolidColorBrush ColorBrush { get; set; }
    }
}
