using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Helpers
{
    public class InternetConnectionHelpers
    {
        private static bool IsAvailablePrevious { get; set; } = false;

        public InternetConnectionHelpers()
        {
            NetworkInformation.NetworkStatusChanged += OnNetworkStatusChanged;
        }

        public void OnNetworkStatusChanged(object sender)
        {
            var networkInfo = NetworkInformation.GetInternetConnectionProfile();

            if (networkInfo?.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.InternetAccess)
            {
                if (IsAvailablePrevious == false) return;
                Log.Error("Network connection was lost.");
                IsAvailablePrevious = false;
            }
            else
            {
                if (IsAvailablePrevious == true) return;
                Log.Information("Internet connection is back.");
                IsAvailablePrevious = true;
            }
        }
    }
}
