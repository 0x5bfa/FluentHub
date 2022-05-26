using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Mapping
{
    public class NotificationMapping
    {
        public static List<Notification> MapAll(IEnumerable<OctokitOriginal.Notification> data)
        {
            var result = new List<Notification>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    result.Add(Map(item));
                }
            }

            return result;
        }

        public static Notification Map(OctokitOriginal.Notification data)
        {
            return new Notification()
            {
            };
        }
    }
}
