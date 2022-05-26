using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Mapping
{
    public class UserMappers
    {
        public static List<User> MapAll(IEnumerable<OctokitOriginal.User> data)
        {
            var result = new List<User>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    result.Add(Map(item));
                }
            }

            return result;
        }

        public static User Map(OctokitOriginal.User data)
        {
            return new User()
            {
            };
        }
    }
}
