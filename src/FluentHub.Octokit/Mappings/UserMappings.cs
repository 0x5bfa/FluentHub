using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Mapping
{
    public static class UserMappings
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
                AvatarUrl = data.AvatarUrl,
                Bio = data.Bio,
                Company = data.Company,
                Email = data.Email,
                IsViewer = data.SiteAdmin,
                Location = data.Location,
                Login = data.Login,
                Name = data.Name,
                WebsiteUrl = data.Blog,
                IsOrganization = data.Type == OctokitOriginal.AccountType.Organization ? true : false,
                FollowersTotalCount = data.Followers,
                FollowingTotalCount = data.Following,
            };
        }
    }
}
