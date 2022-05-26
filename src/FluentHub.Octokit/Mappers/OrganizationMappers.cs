using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Mapping
{
    public class OrganizationMappers
    {
        public static List<Organization> MapAll(IEnumerable<OctokitOriginal.Organization> data)
        {
            var result = new List<Organization>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    result.Add(Map(item));
                }
            }

            return result;
        }

        public static Organization Map(OctokitOriginal.Organization data)
        {
            return new Organization()
            {
            };
        }
    }
}
