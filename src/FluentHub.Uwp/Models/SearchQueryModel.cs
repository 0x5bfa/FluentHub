namespace FluentHub.Uwp.Models
{
    public class SearchQueryModel
    {
        public SearchQueryModel(string query, string label)
        {
            QueryString = query;
            Label = label;
        }

        public string QueryString { get; private set; }

        public string Label { get; private set; }
    }
}
