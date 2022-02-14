using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace FluentHub.ViewModels.AppSettings
{
    public class AboutViewModel
    {
        public string Version
        {
            get
            {
                string version = "Version 1.0.0.0";

                string architecture = Package.Current.Id.Architecture.ToString();

#if DEBUG
                string buildConfiguration = "DEBUG";
#else
                string buildConfiguration = "RELEASE";
#endif

                return $"{version} | {architecture} | {buildConfiguration}";
            }
        }
    }
}
