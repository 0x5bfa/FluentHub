using GraphQL;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class TreeQueries
    {
        public async Task<List<TreeEntry>> GetAllAsync(string name, string owner, string refs, string path)
        {
            var query = new Query()
                .Repository(name, owner)
                .Object(expression: refs + ":" + path)
                .Cast<OctokitGraphQLModel.Tree>()
                .Entries
                .Select(x => new TreeEntry
                {
                    Name = x.Name,
                    Path = x.Path,
                    Type = x.Type,
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }

        public async Task<(List<TreeEntry> Files, List<Commit> Commits)> GetWithObjectNameAsync(string name, string owner, string refs, string path)
        {
            #region objectQuery
            var queryToGetFileInfo = new Query()
                .Repository(name, owner)
                .Object(expression: refs + ":" + path)
                .Cast<OctokitGraphQLModel.Tree>()
                .Entries
                .Select(x => new TreeEntry
                {
                    Name = x.Name,
                    Path = x.Path,
                    Type = x.Type,
                })
                .Compile();
            #endregion

            var response1 = await App.Connection.Run(queryToGetFileInfo);

            List<Commit> items = new();

            var fragments = GetCommitFragmentList(response1.ToList());

            var request3 = new GraphQLRequest
            {
                Query = @$"
query {{
    repository(name: ""{name}"", owner: ""{owner}"") {{
        ref (qualifiedName: ""{refs}"") {{
            target {{
                ... on Commit {{
                    {fragments}
                }}
            }}
        }}
    }}
}}",
            };

            var response3 = await App.GraphQLHttpClient.SendQueryAsync<DynamicClass>(request3);
            List<Commit> zippedData = new();

            Parse(0, response3.Data["repository"] as JToken, zippedData);
            var str = ParsedJsonString.ToString();

            (List<TreeEntry> Files, List<Commit> Commits) results = (response1.ToList(), zippedData);

            return results;
        }

        private string GetCommitFragmentList(List<TreeEntry> files)
        {
            string fragments = "";

            for (int index = 0; index < files.Count; index++)
            {
                fragments += @$"
history{index}: history(first: 1, path: ""{files.ElementAt(index).Path}"") {{
    nodes {{
        message
        committedDate
    }}
}}
";
            }

            return fragments;
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

        private void Parse(int padding, JToken jtoken, List<Commit> commits)
        {
            if (jtoken is JValue)
            {
                JValue jvalue = (JValue)jtoken;
                string str = $"value = {jvalue.Value}";
                ParsedJsonString.AppendLine(str.PadLeft(str.Length + padding));
            }
            else if (jtoken is JObject)
            {
                bool elementAlreadyAdded = false;

                foreach (KeyValuePair<string, JToken> kvp in (JObject)jtoken)
                {
                    if (kvp.Value is JValue)
                    {
                        JValue jvalue = (JValue)kvp.Value;
                        string str = $"name = {kvp.Key}, value = {jvalue.Value}";

                        if (elementAlreadyAdded is false)
                        {
                            commits.Add(new Commit() { });
                            elementAlreadyAdded = true;
                        }

                        switch (kvp.Key)
                        {
                            case "message":
                                commits.Last().Message = jvalue.Value.ToString();
                                break;
                            case "committedDate":
                                DateTimeOffset.TryParse(jvalue.Value.ToString(), out var dto);
                                commits.Last().CommittedDate = dto;
                                commits.Last().CommittedDateHumanized = dto.Humanize();
                                break;
                        }

                        ParsedJsonString.AppendLine(str.PadLeft(str.Length + padding));
                    }
                    else if (kvp.Value is JObject)
                    {
                        string str = $"name = {kvp.Key}";
                        ParsedJsonString.AppendLine(str.PadLeft(str.Length + padding));
                        Parse(padding + 2, kvp.Value, commits);
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
                            Parse(padding + 2, token, commits);
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
                    Parse(padding + 2, token, commits);
                    index++;
                }
            }
            else
            {
                ParsedJsonString.AppendLine(jtoken.ToString());
            }
        }
    }
}
