namespace FluentHub.Uwp.Models
{
    public class SquareNavigationViewItemModel
    {
        public SquareNavigationViewItemModel() { }

        public SquareNavigationViewItemModel(string name, string glyphPrimary, string glyphSecondary, bool isSelected = false)
        {
            Name = name;
            GlyphPrimary = glyphPrimary;
            GlyphSecondary = glyphSecondary;
            IsSelected = isSelected;
        }

        public string Name { get; set; }

        public string GlyphPrimary { get; set; }

        public string GlyphSecondary { get; set; }

        public bool IsSelected { get; set; }
    }
}
