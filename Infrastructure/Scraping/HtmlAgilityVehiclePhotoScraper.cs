using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using WheelPixAPI.Options;

namespace WheelPixAPI.Infrastructure.Scraping;

public sealed class HtmlAgilityVehiclePhotoScraper : IVehiclePhotoScraper
{
    private readonly HttpClient _httpClient;
    private readonly ScrapingOptions _options;

    public HtmlAgilityVehiclePhotoScraper(HttpClient httpClient, IOptions<ScrapingOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        if (string.IsNullOrWhiteSpace(_options.SourceUrlTemplate))
        {
            throw new InvalidOperationException("Scraping:SourceUrlTemplate must be configured.");
        }

        if (string.IsNullOrWhiteSpace(_options.ImageXPath))
        {
            throw new InvalidOperationException("Scraping:ImageXPath must be configured.");
        }
    }

    public async Task<IReadOnlyList<string>> ScrapeAsync(string make, string model, int? year, CancellationToken cancellationToken = default)
    {
        var url = BuildUrl(make, model, year);
        var html = await _httpClient.GetStringAsync(url, cancellationToken);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var imageNodes = doc.DocumentNode.SelectNodes(_options.ImageXPath!);
        if (imageNodes is null)
        {
            return Array.Empty<string>();
        }

        var maxAllowedResults = _options.MaxAllowedResults < 1
            ? ScrapingOptions.DefaultMaxAllowedResults
            : _options.MaxAllowedResults;
        var maxResults = Math.Clamp(_options.MaxResults, 1, maxAllowedResults);

        return imageNodes
            .Select(node => node.GetAttributeValue("src", string.Empty).Trim())
            .Where(src => Uri.IsWellFormedUriString(src, UriKind.Absolute))
            .Where(src => !src.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Take(maxResults)
            .ToArray();
    }

    private string BuildUrl(string make, string model, int? year)
    {
        return _options.SourceUrlTemplate!
            .Replace("{make}", Uri.EscapeDataString(make), StringComparison.OrdinalIgnoreCase)
            .Replace("{model}", Uri.EscapeDataString(model), StringComparison.OrdinalIgnoreCase)
            .Replace("{year}", year?.ToString() ?? string.Empty, StringComparison.OrdinalIgnoreCase);
    }
}
