namespace WheelPixAPI.Options;

public sealed class ScrapingOptions
{
    public const string SectionName = "Scraping";

    public string SourceUrlTemplate { get; set; } =
        "https://www.bing.com/images/search?q={make}+{model}+{year}";

    public string ImageXPath { get; set; } = "//img[@src]";

    public int MaxResults { get; set; } = 10;
}
