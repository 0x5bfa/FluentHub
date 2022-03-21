using FluentHub.Octokit.Queries.Organization;
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
        private OrganizationOverviewItem organization;
        public OrganizationOverviewItem Organization
        {
            get => organization;
            private set => SetProperty(ref organization, value);
        }

        public async Task GetOrganization(string org)
        {
            try
            {
                OrganizationQueries queries = new();
                Organization = await queries.GetOverview(org);
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
