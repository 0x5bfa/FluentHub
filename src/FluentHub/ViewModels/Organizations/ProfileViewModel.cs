using FluentHub.Octokit.Queries.Organizations;
using FluentHub.Octokit.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Organizations
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private Organization organization;
        public Organization Organization { get => organization; private set => SetProperty(ref organization, value); }

        public async Task GetOrganization(string org)
        {
            try
            {
                OrganizationQueries queries = new();
                var organization = await queries.GetOverview(org);
                if (organization == null) return;

                Organization = organization;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
