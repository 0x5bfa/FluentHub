using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.StartScreen;

namespace FluentHub.Helpers
{
    public static class JumpListHelper
    {
        public static JumpListItem CreateJumpListItem(string args, string displayName, string logo, string description = "", string groupName = "")
        {
            var item = JumpListItem.CreateWithArguments(args.ToString(), displayName);
            item.Description = description;
            item.GroupName = groupName;
            item.Logo = new Uri(logo);
            return item;
        }
        public static async Task AddToJumpListAsync(string title, string iconPath, string args, string description = "", string groupName = "")
        {
            if (JumpList.IsSupported())
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    throw new ArgumentException($"'{nameof(title)}' cannot be null or blank.", nameof(title));
                }

                if (string.IsNullOrWhiteSpace(iconPath))
                {
                    throw new ArgumentException($"'{nameof(iconPath)}' cannot be null or blank.", nameof(iconPath));
                }

                var jumpList = await JumpList.LoadCurrentAsync();
                var jumpListItem = jumpList.Items.FirstOrDefault(x => x.Arguments == args);
                if (jumpListItem != null)
                {
                    jumpList.Items.Remove(jumpListItem);
                }

                jumpListItem = JumpListItem.CreateWithArguments(args, title);
                jumpListItem.Description = description;
                jumpListItem.Logo = new Uri(iconPath);
                jumpListItem.GroupName = groupName;
                jumpList.Items.Add(jumpListItem);
                await jumpList.SaveAsync();
            }
        }

        public static async Task<bool> RemoveFromJumpListAsync(string args)
        {
            if (JumpList.IsSupported())
            {
                var jumpList = await JumpList.LoadCurrentAsync();
                var jumpListItem = jumpList.Items.FirstOrDefault(x => x.Arguments == args);
                if (jumpListItem != null)
                {
                    jumpList.Items.Remove(jumpListItem);
                    await jumpList.SaveAsync();
                    return true;
                }
            }
            return false;
        }

        public static async Task ClearJumpListAsync()
        {
            if (JumpList.IsSupported())
            {
                var jumpList = await JumpList.LoadCurrentAsync();
                jumpList.Items.Clear();
                await jumpList.SaveAsync();
            }
        }

    }
}