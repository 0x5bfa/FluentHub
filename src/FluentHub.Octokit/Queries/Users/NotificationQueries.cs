using GraphQL;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;

namespace FluentHub.Octokit.Queries.Users
{
    public class NotificationQueries
    {
        public async Task<List<Notification>> GetAllAsync(OctokitV3.NotificationsRequest request = null, OctokitV3.ApiOptions options = null)
        {
            var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            Wrappers.NotificationWrapper wrapper = new();
            var notifications = wrapper.WrapAsync(response);

            var fragments = GetGetheredRepositoryFragment(notifications);

            var request2 = new GraphQLRequest
            {
                Query = @$"query {{ {fragments} }}",
            };

            var response2 = await App.GraphQLHttpClient.SendQueryAsync<DynamicClass>(request2);
            List<Repository> zippedData = new();

            for (int idx = 0; idx < 30; idx++)
            {
                var data = response2.Data[$"repo{idx}"];
                Repository result = new();
                Parse(0, data as JToken, result);
                string parsedData = ParsedJsonString.ToString();
                zippedData.Add(result);
            }

            var mappedNotifications = Map(notifications, zippedData);

            return mappedNotifications;
        }

        private string GetGetheredRepositoryFragment(IReadOnlyList<Notification> notifications)
        {
            string getheredFragments = "";

            for (int index = 0; index < notifications.Count; index++)
            {
                switch (notifications.ElementAt(index).Subject.Type)
                {
                    case NotificationSubjectType.Discussion:
                    case NotificationSubjectType.Commit:
                    case NotificationSubjectType.Release:
                        break;
                    case NotificationSubjectType.Issue:
                        {
                            var issueFragment =
                                @$"
repo{index}: repository(name: ""{notifications.ElementAt(index).Repository.Name}"", owner: ""{notifications.ElementAt(index).Repository.Owner.Login}"") {{
  Issue: issue(number: {notifications.ElementAt(index).Subject.Number}) {{
    id
    number
    state
    stateReason
  }}
}}";
                            getheredFragments += (issueFragment + "\n");
                            break;
                        }
                    case NotificationSubjectType.PullRequest:
                        {
                            var prFragment =
                                @$"
repo{index}: repository(name: ""{notifications.ElementAt(index).Repository.Name}"", owner: ""{notifications.ElementAt(index).Repository.Owner.Login}"") {{
  PullRequest: pullRequest(number: {notifications.ElementAt(index).Subject.Number}) {{
    id
    number
    isDraft
    state
  }}
}}";
                            getheredFragments += (prFragment + "\n");
                            break;
                        }
                }

            }

            return getheredFragments;
        }

        private List<Notification> Map(List<Notification> notifications, IReadOnlyList<Repository> details)
        {
            int index = 0;

            foreach (var item in notifications)
            {
                switch (item.Subject.Type)
                {
                    case NotificationSubjectType.Discussion:
                    case NotificationSubjectType.Commit:
                    case NotificationSubjectType.Release:
                        break;
                    case NotificationSubjectType.Issue:
                        {
                            item.Subject.Number = details.ElementAt(index).Issue.Number;

                            switch (details.ElementAt(index).Issue.State)
                            {
                                case IssueState.Open:
                                    {
                                        item.Subject.Type = NotificationSubjectType.IssueOpen;
                                        break;
                                    }
                                case IssueState.Closed:
                                    {
                                        switch (details.ElementAt(index).Issue.StateReason)
                                        {
                                            case IssueStateReason.Completed:
                                                item.Subject.Type = NotificationSubjectType.IssueClosedAsCompleted;
                                                break;
                                            case IssueStateReason.NotPlanned:
                                                item.Subject.Type = NotificationSubjectType.IssueClosedAsNotPlanned;
                                                break;
                                        }
                                        break;
                                    }
                            }
                            break;
                        }
                    case NotificationSubjectType.PullRequest:
                        {
                            item.Subject.Number = details.ElementAt(index).PullRequest.Number;

                            switch (details.ElementAt(index).PullRequest.State)
                            {
                                case PullRequestState.Open:
                                    {
                                        item.Subject.Type = details.ElementAt(index).PullRequest.IsDraft ?
                                            NotificationSubjectType.PullRequestDraft :
                                            NotificationSubjectType.PullRequestOpen;
                                        break;
                                    }
                                case PullRequestState.Closed:
                                    {
                                        item.Subject.Type = NotificationSubjectType.PullRequestClosed;
                                        break;
                                    }
                                case PullRequestState.Merged:
                                    {
                                        item.Subject.Type = NotificationSubjectType.PullRequestMerged;
                                        break;
                                    }
                            }
                            break;
                        }
                }

                item.Subject.TypeHumanized = item.Subject.Type.ToString();
                index++;
            }

            return notifications;
        }

        private class DynamicClass : DynamicObject
        {
            private Dictionary<string, dynamic> dicProperties = new Dictionary<string, dynamic>();

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                this.dicProperties[binder.Name] = value;
                return true;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                return this.dicProperties.TryGetValue(binder.Name, out result);
            }

            public object this[string propertyName]
            {
                get
                {
                    object result;
                    this.dicProperties.TryGetValue(propertyName, out result);
                    return result;
                }
            }

            public int PropX { get; set; }
        }

        StringBuilder ParsedJsonString { get; set; } = new();

        private void Parse(int padding, JToken jtoken, Repository res)
        {
            if (jtoken is JValue)
            {
                JValue jvalue = (JValue)jtoken;
                string str = $"value = {jvalue.Value}";
                ParsedJsonString.AppendLine(str.PadLeft(str.Length + padding));
            }
            else if (jtoken is JObject)
            {
                bool isPR = false;

                foreach (KeyValuePair<string, JToken> kvp in (JObject)jtoken)
                {
                    if (kvp.Value is JValue)
                    {
                        JValue jvalue = (JValue)kvp.Value;
                        string str = $"name = {kvp.Key}, value = {jvalue.Value}";

                        // Whether PR or Issue
                        if (kvp.Key == "id" && jvalue.Value.ToString().StartsWith("PR"))
                        {
                            isPR = true;
                        }
                        if (jvalue.Value is null)
                        {
                            continue;
                        }

                        if (isPR)
                        {
                            switch (kvp.Key)
                            {
                                case "id":
                                    ID id = new(jvalue.Value?.ToString());
                                    res.PullRequest.Id = id;
                                    break;
                                case "number":
                                    res.PullRequest.Number = Convert.ToInt32(jvalue.Value?.ToString());
                                    break;
                                case "isDraft":
                                    bool.TryParse(jvalue.Value?.ToString(), out var isDraft);
                                    res.PullRequest.IsDraft = isDraft;
                                    break;
                                case "state":
                                    var original = jvalue.Value?.ToString();
                                    var src = original.ToLower();
                                    TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                                    src = info.ToTitleCase(src).Replace(" ", string.Empty);
                                    Enum.TryParse(src, out PullRequestState state);
                                    res.PullRequest.State = state;
                                    break;
                            }
                        }
                        else
                        {
                            switch (kvp.Key)
                            {
                                case "id":
                                    ID id = new(jvalue.Value?.ToString());
                                    res.Issue.Id = id;
                                    break;
                                case "number":
                                    res.Issue.Number = Convert.ToInt32(jvalue.Value?.ToString());
                                    break;
                                case "state":
                                    {
                                        var original = jvalue.Value?.ToString();
                                        var src = original.ToLower();
                                        TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                                        src = info.ToTitleCase(src).Replace(" ", string.Empty);
                                        Enum.TryParse(src, out IssueState state);
                                        res.Issue.State = state;
                                    }
                                    break;
                                case "stateReason":
                                    {
                                        var original = jvalue.Value?.ToString();
                                        var src = original.ToLower();
                                        TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                                        src = info.ToTitleCase(src).Replace(" ", string.Empty);
                                        Enum.TryParse(src, out IssueStateReason stateReason);
                                        res.Issue.StateReason = stateReason;
                                    }
                                    break;
                            }
                        }

                        ParsedJsonString.AppendLine(str.PadLeft(str.Length + padding));
                    }
                    else if (kvp.Value is JObject)
                    {
                        string str = $"name = {kvp.Key}";
                        if (kvp.Key == "PullRequest")
                        {
                            res.PullRequest = new();
                        }
                        else if (kvp.Key == "Issue")
                        {
                            res.Issue = new();
                        }
                        ParsedJsonString.AppendLine(str.PadLeft(str.Length + padding));
                        Parse(padding + 2, kvp.Value, res);
                    }
                    else if (kvp.Value is JArray)
                    {
                        string str = $"name = {kvp.Key}";
                        ParsedJsonString.AppendLine(str.PadLeft(str.Length + padding));
                        JArray jarray = (JArray)kvp.Value;
                        int index = 1;

                        foreach (JToken token in jarray)
                        {
                            string idx = $"array index {index}";
                            ParsedJsonString.AppendLine(idx.PadLeft(idx.Length + padding + 1));
                            Parse(padding + 2, token, res);
                            index++;
                        }
                    }
                }
            }
            else if (jtoken is JArray)
            {
                JArray jarray = (JArray)jtoken;
                int index = 1;
                foreach (JToken token in jarray)
                {
                    string str = $"array index {index}";
                    ParsedJsonString.AppendLine(str.PadLeft(str.Length + padding + 1));
                    Parse(padding + 2, token, res);
                    index++;
                }
            }
            else if (jtoken is null)
            {
            }
            else
            {
                ParsedJsonString.Append(jtoken.ToString());
            }
        }

        public async Task<int> GetUnreadCount()
        {
            OctokitV3.NotificationsRequest request = new()
            {
                All = true,
            };

            OctokitV3.ApiOptions options = new()
            {
                PageCount = 1,
                PageSize = 50,
                StartPage = 1
            };

            // Even if there are more than 50 unread items, this method will only count up to a maximum of 50.
            var response = await App.Client.Activity.Notifications.GetAllForCurrent(request, options);

            int unreadCount = 0;
            foreach (var indivisual in response)
            {
                if (indivisual.Unread) unreadCount++;
            }

            return unreadCount;
        }
    }
}
