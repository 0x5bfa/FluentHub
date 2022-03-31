using FluentHub.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.StartScreen;

namespace FluentHub.Helpers
{
    public static class JumpListHelper
    {
        public static JumpListItem CreateJumpListItem(string args!!, string displayName!!, string logo!!, string description = "", string groupName = "")
        {
            var item = JumpListItem.CreateWithArguments(args.ToString(), displayName);
            item.Description = description;
            item.GroupName = groupName;
            item.Logo = new Uri(logo);
            return item;
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

        public static async Task ConfigureDefaultJumpListAsync()
        {
            if (JumpList.IsSupported())
            {
                var jumpList = await JumpList.LoadCurrentAsync();
                var argsPrefix = "fluenthub:/";
                var iconPrefix = "ms-appx:///Assets/Icons";
                var textPrefix = "ms-resource:///Resources";
                var preferredIndex = 0;
                AddOrInsert(jumpList, CreateJumpListItem($"{argsPrefix}/profile",
                                                         $"Profile",
                                                         $"{iconPrefix}/Profile.png",
                                                         "",
                                                         "Profile"),
                                                         preferredIndex++);

                AddOrInsert(jumpList, CreateJumpListItem($"{argsPrefix}/notifications",
                                                         $"Notifications",
                                                         $"{iconPrefix}/Notifications.png",
                                                         "",
                                                         "Profile"),
                                                         preferredIndex++);

                AddOrInsert(jumpList, CreateJumpListItem($"{argsPrefix}/activities",
                                                         $"Activities",
                                                         $"{iconPrefix}/Activities.png",
                                                         "",
                                                         "Profile"),
                                                         preferredIndex++);

                AddOrInsert(jumpList, CreateJumpListItem($"{argsPrefix}/issues",
                                                         $"Issues",
                                                         $"{iconPrefix}/Issues.png",
                                                         "",
                                                         "My Work"),
                                                         preferredIndex++);

                AddOrInsert(jumpList, CreateJumpListItem($"{argsPrefix}/pullrequests",
                                                         $"Pull Requests",
                                                         $"{iconPrefix}/PullRequests.png",
                                                         "",
                                                         "My Work"),
                                                         preferredIndex++);

                AddOrInsert(jumpList, CreateJumpListItem($"{argsPrefix}/discussions",
                                                         $"Discussions",
                                                         $"{iconPrefix}/Discussions.png",
                                                         "",
                                                         "My Work"),
                                                         preferredIndex++);

                AddOrInsert(jumpList, CreateJumpListItem($"{argsPrefix}/repositories",
                                                         $"Repositories",
                                                         $"{iconPrefix}/Repositories.png",
                                                         "",
                                                         "My Work"),
                                                         preferredIndex++);

                AddOrInsert(jumpList, CreateJumpListItem($"{argsPrefix}/organizations",
                                                         $"Organizations",
                                                         $"{iconPrefix}/Organizations.png",
                                                         "",
                                                         "My Work"),
                                                         preferredIndex++);

                AddOrInsert(jumpList, CreateJumpListItem($"{argsPrefix}/starred",
                                                         $"Starred repositories",
                                                         $"{iconPrefix}/Starred.png",
                                                         "",
                                                         "My Work"),
                                                         preferredIndex++);
                await jumpList.SaveAsync();
            }
        }

        private static void AddOrInsert(JumpList jumpList, JumpListItem item, int preferredIndex = -1)
        {
            var currentIndex = jumpList
                                    .Items
                                    .IndexOf(x => x.Arguments == item.Arguments);
            if (currentIndex >= 0)
            {
                jumpList.Items[currentIndex] = item;
            }
            else
            {
                jumpList.Items.Add(item);
                currentIndex = jumpList.Items.Count - 1;
            }

            // Move item to preferred index
            if (preferredIndex >= 0 && preferredIndex != currentIndex && preferredIndex < jumpList.Items.Count)
            {
                jumpList.Items.Remove(item);
                jumpList.Items.Insert(preferredIndex, item);
            }
        }
    }
}