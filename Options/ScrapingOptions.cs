namespace WheelPixAPI.Options;

public sealed class ScrapingOptions
{
    public const string SectionName = "Scraping";
    public const int DefaultMaxAllowedResults = 50;

    public string? SourceUrlTemplate { get; set; }

    public string? ImageXPath { get; set; }

    public int MaxResults { get; set; } = 10;

    public int MaxAllowedResults { get; set; } = DefaultMaxAllowedResults;
}
