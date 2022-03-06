using FluentHub.Helpers;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentHub.Models.Items
{
    public class RepoListItem
    {
        public long RepoId { get; set; }

        public Repository Repository { get; set; }
    }
}
