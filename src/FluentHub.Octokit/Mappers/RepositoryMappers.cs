using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Mapping
{
    public class RepositoryMappers
    {
        public static List<Repository> MapAll(IEnumerable<OctokitOriginal.Repository> data)
        {
            var result = new List<Repository>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    result.Add(Map(item));
                }
            }

            return result;
        }

        public static Repository Map(OctokitOriginal.Repository data)
        {
            return new Repository()
            {
            };
        }
    }
}
