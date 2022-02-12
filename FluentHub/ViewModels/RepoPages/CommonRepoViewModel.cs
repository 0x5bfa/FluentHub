using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.RepoPages
{
    public class CommonRepoViewModel
    {
        public long RepositoryId { get; set; }

        public string BranchName { get; set; } = "main";

        public string Path { get; set; } = "/";

        private bool isRootDir = true;
        public bool IsRootDir
        {
            get => isRootDir;
            set
            {
                if (value == true) IsFile = false;
                isRootDir = value;
            }
        }

        private bool isFile = false;
        public bool IsFile
        {
            get => isFile;
            set
            {
                if (value == true) IsRootDir = false;
                isFile = value;
            }
        }

    }
}
